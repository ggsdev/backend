using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Modules.Measuring.WellProductions.Dtos;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Utils;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services
{
    public class WellProductionService
    {
        private readonly IWellProductionRepository _repository;
        private readonly IWellRepository _wellRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IBTPRepository _btpRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;

        public WellProductionService(IWellProductionRepository repository, IMapper mapper, IProductionRepository productionRepository, IInstallationRepository installationRepository, IBTPRepository bTPRepository, IWellRepository wellRepository, IFieldRepository fieldRepository)
        {
            _repository = repository;
            _wellRepository = wellRepository;
            _mapper = mapper;
            _productionRepository = productionRepository;
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
            _btpRepository = bTPRepository;
        }

        public async Task<AppropriationDto> CreateAppropriation(Guid productionId)
        {
            var production = await _productionRepository
                .GetById(productionId);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.IsActive is false)
                throw new NotFoundException(ErrorMessages.Inactive<Production>());

            if (production.WellProductions is not null && production.WellProductions.Count > 0)
                throw new ConflictException("Apropriação já foi feita.");

            if (production.Oil is null)
                throw new ConflictException("Importação do óleo não foi feita.");

            if (production.Gas is null)
                throw new ConflictException("Importação do gás não foi feita.");

            var installations = await _installationRepository
                .GetInstallationChildrenOfUEP(production.Installation.UepCod);

            var wellsInvalids = new List<string>();
            //validando se todos poços tem um teste válido
            foreach (var installation in installations)
            {
                foreach (var field in installation.Fields)
                {
                    foreach (var well in field.Wells)
                    {
                        var wellContainBtpValid = false;

                        var allBtpsValid = well.BTPDatas.Where(x => (x.FinalApplicationDate == null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        if (well.BTPDatas is not null)
                            foreach (var btp in allBtpsValid)
                                if (btp.IsValid)
                                {
                                    wellContainBtpValid = true;
                                    break;
                                }

                        if (wellContainBtpValid is false)
                            wellsInvalids.Add(well.Name);
                    }
                }
            }

            //if (wellsInvalids.Count > 0)
            //    throw new BadRequestException($"Todos os poços devem ter um teste de poço válido. Poços sem teste ou com teste inválido:", errors: wellsInvalids);

            var appropriationDto = new AppropriationDto
            {
                ProductionId = productionId,
                FieldProductions = new(),
            };
            var totalWaterInUep = 0m;

            if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
                    var totalWater = 0m;
                    var totalOil = 0m;
                    var totalGas = 0m;

                    var btps = await _btpRepository
                        .GetBtpDatasByFieldId(fieldFR.Field.Id);
                    var filtredByApplyDateAndFinal = btps
                        .Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                    var totalGasPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialGas);
                    var totalOilPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialOil);

                    var totalWaterPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialWater);

                    FieldProduction? fieldProduction = filtredByApplyDateAndFinal.Count() > 0 ? new()
                    {
                        Id = Guid.NewGuid(),
                        FieldId = fieldFR.Field.Id,
                        ProductionId = production.Id,
                    } : null;

                    var wellAppropiationsDto = new List<WellProductionDto>();

                    foreach (var btp in filtredByApplyDateAndFinal)
                    {
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialGas, totalGasPotencial);
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);
                        var wellPotencialWaterAsPercentageOfField = 0;

                        var productionGas = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellPotencialGasAsPercentageOfField) : 0;
                        var productionOil = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialOilAsPercentageOfField) : 0;
                        var productionWater = (productionOil * btp.BSW / 100) / (1 - btp.BSW / 100);

                        var wellAppropriation = new WellProduction
                        {
                            Id = Guid.NewGuid(),
                            Production = production,
                            BtpData = btp,
                            WellId = btp.Well.Id,
                            ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField,
                            ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField,
                            ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField,

                            ProductionGasInWell = productionGas,
                            ProductionOilInWell = productionOil,
                            ProductionWaterInWell = productionWater,

                            FieldProduction = fieldProduction,

                            ProductionGasAsPercentageOfInstallation = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialGasAsPercentageOfField, fieldFR.FRGas.Value) : 0,
                            ProductionOilAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialOilAsPercentageOfField, fieldFR.FROil.Value) : 0,
                            ProductionWaterAsPercentageOfInstallation = 0,
                        };

                        totalWater += wellAppropriation.ProductionWaterInWell;
                        totalOil += wellAppropriation.ProductionOilInWell;
                        totalGas += wellAppropriation.ProductionGasInWell;

                        var wellAppropiationDto = new WellProductionDto
                        {
                            WellProductionId = wellAppropriation.Id,
                            WellName = btp.WellName,
                            ProductionGasInWellM3 = Math.Round(wellAppropriation.ProductionGasInWell, 5),
                            ProductionOilInWellM3 = Math.Round(wellAppropriation.ProductionOilInWell, 5),
                            ProductionWaterInWellM3 = Math.Round(wellAppropriation.ProductionWaterInWell, 5),
                            ProductionGasInWellSCF = Math.Round(wellAppropriation.ProductionGasInWell * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            ProductionOilInWellBBL = Math.Round(wellAppropriation.ProductionOilInWell * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            ProductionWaterInWellBBL = Math.Round(wellAppropriation.ProductionWaterInWell * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            Downtime = "00:00:00"
                        };

                        await _repository.AddAsync(wellAppropriation);
                        wellAppropiationsDto.Add(wellAppropiationDto);
                    }

                    if (fieldProduction is not null)
                    {
                        fieldProduction.WaterProductionInField = totalWater;
                        fieldProduction.GasProductionInField = totalGas;
                        fieldProduction.OilProductionInField = totalOil;

                        totalWaterInUep += fieldProduction.WaterProductionInField;

                        await _productionRepository.AddFieldProduction(fieldProduction);

                        var fieldProductionDto = new FieldProductionDto
                        {
                            FieldProductionId = fieldProduction.Id,
                            FieldName = fieldFR.Field.Name,
                            GasProductionInFieldM3 = Math.Round(fieldProduction.GasProductionInField, 5),
                            OilProductionInFieldM3 = Math.Round(fieldProduction.OilProductionInField, 5),
                            WaterProductionInFieldM3 = Math.Round(fieldProduction.WaterProductionInField, 5),

                            GasProductionInFieldSCF = Math.Round(fieldProduction.GasProductionInField * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            OilProductionInFieldBBL = Math.Round(fieldProduction.OilProductionInField * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            WaterProductionInFieldBBL = Math.Round(fieldProduction.WaterProductionInField * ProductionUtils.m3ToBBLConversionMultiplier, 5),

                            WellAppropriations = wellAppropiationsDto,


                        };

                        appropriationDto.FieldProductions.Add(fieldProductionDto);

                    }
                }

                await _repository.Save();

                await DistributeAccrossEntites(productionId);
            }

            else
            {
                var uepFields = await _fieldRepository.GetFieldsByUepCode(production.Installation.UepCod);
                var btpsUEP = await _btpRepository
                    .GetBtpDatasByUEP(production.Installation.UepCod);

                var filtredByApplyDateAndFinal = btpsUEP
                        .Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                var totalGasPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialGas);

                var totalOilPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialOil);

                var totalWaterPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialWater);

                foreach (var fieldInDatabase in uepFields)
                {
                    var wellAppropiationsDto = new List<WellProductionDto>();

                    FieldProduction? fieldProduction = filtredByApplyDateAndFinal.Count() > 0 ? new()
                    {
                        Id = Guid.NewGuid(),
                        ProductionId = production.Id,
                        FieldId = fieldInDatabase.Id,

                    } : null;

                    var totalGas = 0m;
                    var totalOil = 0m;
                    var totalWater = 0m;

                    var btpsField = await _btpRepository
                            .GetBtpDatasByFieldId(fieldInDatabase.Id);

                    //BTPS ATIVOS NA DATA
                    var filtredsBTPsField = btpsField
                        .Where(x => (x.FinalApplicationDate == null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                    //TOTAIS DE POTENCIAIS
                    var totalGasPotencialField = filtredsBTPsField
                          .Sum(x => x.PotencialGas);
                    var totalOilPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialOil);
                    var totalWaterPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialWater);

                    foreach (var btp in filtredsBTPsField)
                    {
                        //UEP
                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialGas, totalGasPotencial);
                        var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);
                        var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialWater, totalWaterPotencial);

                        //FIELD
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialGas, totalGasPotencialField);
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencialField);
                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialWater, totalWaterPotencialField);

                        //BSW CALCS
                        var calcBSWOil = (100 - btp.BSW) / 100;
                        var calcBSWWater = btp.BSW / 100;

                        //PRODUCTIONS
                        var productionGas = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0));
                        var productionOIl = production.Oil.TotalOil * wellPotencialOilAsPercentageOfUEP;
                        var productionWater = (productionOIl * calcBSWWater) / calcBSWOil;

                        var wellAppropriation = new WellProduction
                        {
                            Id = Guid.NewGuid(),
                            BtpData = btp,
                            Production = production,
                            ProductionGasAsPercentageOfInstallation = wellPotencialGasAsPercentageOfUEP,
                            ProductionOilAsPercentageOfInstallation = wellPotencialOilAsPercentageOfUEP,
                            ProductionWaterAsPercentageOfInstallation = wellPotencialWaterAsPercentageOfUEP,
                            WellId = btp.Well.Id,
                            FieldProduction = fieldProduction,

                            ProductionGasInWell = productionGas,
                            ProductionOilInWell = productionOIl,
                            ProductionWaterInWell = productionWater,

                            ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField,
                            ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField,
                            ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField,
                        };

                        var wellAppropiationDto = new WellProductionDto
                        {
                            WellProductionId = wellAppropriation.Id,
                            WellName = btp.WellName,
                            ProductionGasInWellM3 = Math.Round(wellAppropriation.ProductionGasInWell, 5),
                            ProductionOilInWellM3 = Math.Round(wellAppropriation.ProductionOilInWell, 5),
                            ProductionWaterInWellM3 = Math.Round(wellAppropriation.ProductionWaterInWell, 5),
                            ProductionGasInWellSCF = Math.Round(wellAppropriation.ProductionGasInWell * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            ProductionOilInWellBBL = Math.Round(wellAppropriation.ProductionOilInWell * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            ProductionWaterInWellBBL = Math.Round(wellAppropriation.ProductionWaterInWell * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            Downtime = "00:00:00"
                        };

                        totalWater += wellAppropriation.ProductionWaterInWell;
                        totalOil += wellAppropriation.ProductionOilInWell;
                        totalGas += wellAppropriation.ProductionGasInWell;

                        if (fieldProduction is not null)
                            fieldProduction.FieldId = btp.Well.Field.Id;

                        wellAppropiationsDto.Add(wellAppropiationDto);

                        await _repository.AddAsync(wellAppropriation);
                    }
                    if (fieldProduction is not null)
                    {
                        fieldProduction.WaterProductionInField = totalWater;
                        fieldProduction.GasProductionInField = totalGas;
                        fieldProduction.OilProductionInField = totalOil;

                        totalWaterInUep += fieldProduction.WaterProductionInField;

                        var fieldProductionDto = new FieldProductionDto
                        {
                            FieldProductionId = fieldProduction.Id,
                            FieldName = fieldInDatabase.Name,
                            GasProductionInFieldM3 = Math.Round(fieldProduction.GasProductionInField, 5),
                            OilProductionInFieldM3 = Math.Round(fieldProduction.OilProductionInField, 5),
                            WaterProductionInFieldM3 = Math.Round(fieldProduction.WaterProductionInField, 5),

                            GasProductionInFieldSCF = Math.Round(fieldProduction.GasProductionInField * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            OilProductionInFieldBBL = Math.Round(fieldProduction.OilProductionInField * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            WaterProductionInFieldBBL = Math.Round(fieldProduction.WaterProductionInField * ProductionUtils.m3ToBBLConversionMultiplier, 5),

                            WellAppropriations = wellAppropiationsDto,

                        };

                        appropriationDto.FieldProductions.Add(fieldProductionDto);

                        await _productionRepository.AddFieldProduction(fieldProduction);
                    }

                }

                await _repository.Save();

                await DistributeAccrossEntites(production.Id);
            }

            var waterInUep = new Water
            {
                Id = Guid.NewGuid(),
                Production = production,
                StatusWater = true,
                TotalWater = totalWaterInUep,
            };

            var waterDto = new WaterDto
            {
                Id = waterInUep.Id,
                TotalWaterM3 = Math.Round(waterInUep.TotalWater, 5),
                TotalWaterBBL = Math.Round(waterInUep.TotalWater * ProductionUtils.m3ToBBLConversionMultiplier, 5)
            };

            await _productionRepository.AddWaterProduction(waterInUep);

            production.Water = waterInUep;

            appropriationDto.WaterProduction = waterDto;

            _productionRepository.Update(production);

            await _repository.Save();

            return appropriationDto;
        }
        private async Task DistributeAccrossEntites(Guid productionId)
        {
            var listProductions = await _repository.getAllFieldsProductionsByProductionId(productionId);

            foreach (var field in listProductions)
            {
                foreach (var wellProduction in field.WellProductions)
                {
                    var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);


                    foreach (var completion in wellInDatabase.Completions)
                    {
                        var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);
                        var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                        var allocationReservoir = completion.AllocationReservoir.Value;

                        var completionProduction = new CompletionProduction
                        {
                            Id = Guid.NewGuid(),
                            GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWell,
                            OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWell,
                            WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWell,
                            ProductionId = productionId,
                            CompletionId = completion.Id,
                            WellProduction = wellProduction,
                        };

                        if (reservoirProduction is null)
                        {
                            var createReservoirProduction = new ReservoirProduction
                            {
                                Id = Guid.NewGuid(),
                                ReservoirId = completion.Reservoir.Id,
                                ProductionId = productionId,
                                GasProductionInReservoir = completionProduction.GasProductionInCompletion,
                                OilProductionInReservoir = completionProduction.OilProductionInCompletion,
                                WaterProductionInReservoir = completionProduction.WaterProductionInCompletion,
                            };
                            await _repository.AddReservoirProductionAsync(createReservoirProduction);

                            completionProduction.ReservoirProduction = createReservoirProduction;

                            if (zoneProduction is null)
                            {
                                var createZoneProduction = new ZoneProduction
                                {
                                    Id = Guid.NewGuid(),
                                    ZoneId = completion.Reservoir.Zone.Id,
                                    ProductionId = productionId,
                                    GasProductionInZone = completionProduction.GasProductionInCompletion,
                                    OilProductionInZone = completionProduction.OilProductionInCompletion,
                                    WaterProductionInZone = completionProduction.WaterProductionInCompletion,
                                };
                                await _repository.AddZoneProductionAsync(createZoneProduction);

                                createReservoirProduction.ZoneProduction = createZoneProduction;
                            }
                            else
                            {
                                zoneProduction.GasProductionInZone += completionProduction.GasProductionInCompletion;
                                zoneProduction.OilProductionInZone += completionProduction.OilProductionInCompletion;
                                zoneProduction.WaterProductionInZone += completionProduction.WaterProductionInCompletion;
                            }
                        }

                        else
                        {
                            reservoirProduction.GasProductionInReservoir += completionProduction.GasProductionInCompletion;
                            reservoirProduction.OilProductionInReservoir += completionProduction.OilProductionInCompletion;
                            reservoirProduction.WaterProductionInReservoir += completionProduction.WaterProductionInCompletion;
                            completionProduction.ReservoirProduction = reservoirProduction;

                            zoneProduction.GasProductionInZone += completionProduction.GasProductionInCompletion;
                            zoneProduction.OilProductionInZone += completionProduction.OilProductionInCompletion;
                            zoneProduction.WaterProductionInZone += completionProduction.WaterProductionInCompletion;
                        }

                        await _repository.AddCompletionProductionAsync(completionProduction);
                        await _repository.Save();
                    }
                }
            }
        }
        public async Task ReAppropriateWithNfsm(Guid productionId)
        {
            var production = await _productionRepository
               .GetById(productionId);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.WellProductions is not null && production.WellProductions.Count == 0)
                throw new ConflictException("Apropriação ainda não foi feita");

            var installations = await _installationRepository
                .GetInstallationChildrenOfUEP(production.Installation.UepCod);

            var wellsInvalids = new List<string>();

            foreach (var installation in installations)
            {
                foreach (var field in installation.Fields)
                {
                    foreach (var well in field.Wells)
                    {
                        var wellContainBtpValid = false;

                        var allBtpsValid = well.BTPDatas.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        if (well.BTPDatas is not null)
                            foreach (var btp in allBtpsValid)
                                if (btp.IsValid)
                                {
                                    wellContainBtpValid = true;
                                    break;
                                }

                        if (wellContainBtpValid is false)
                            wellsInvalids.Add(well.Name);
                    }
                }
            }

            //if (wellsInvalids.Count > 0)
            //    throw new BadRequestException($"Todos os poços devem ter um teste de poço válido. Poços sem teste ou com teste inválido:", errors: wellsInvalids);

            var totalWaterInUep = 0m;

            if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
                    var fieldProductionInDatabase = await _productionRepository
                            .GetFieldProductionByFieldAndProductionId(fieldFR.Field.Id, productionId);

                    if (fieldProductionInDatabase is null)
                        throw new NotFoundException("Produção de campo não distribuida");

                    var totalGasPotencial = fieldProductionInDatabase.WellProductions
                        .Sum(x => x.BtpData.PotencialGas);

                    var totalOilPotencial = fieldProductionInDatabase.WellProductions
                        .Sum(x => x.BtpData.PotencialOil);

                    var totalWaterPotencial = fieldProductionInDatabase.WellProductions
                        .Sum(x => x.BtpData.PotencialWater);

                    var totalWater = 0m;
                    var totalOil = 0m;
                    var totalGas = 0m;

                    foreach (var wellProduction in fieldProductionInDatabase.WellProductions)
                    {
                        //PERCENT FIELD
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.BtpData.PotencialGas, totalGasPotencial);
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.BtpData.PotencialOil, totalOilPotencial);
                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.BtpData.PotencialWater, totalWaterPotencial);

                        //PRODUCTIONS
                        var productionGas = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellPotencialGasAsPercentageOfField) : 0;
                        var productionOil = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialOilAsPercentageOfField) : 0;
                        var productionWater = (productionOil * wellProduction.BtpData.BSW) / (100 - wellProduction.BtpData.BSW);

                        wellProduction.ProductionGasInWell = productionGas;
                        wellProduction.ProductionOilInWell = productionOil;
                        wellProduction.ProductionWaterInWell = productionWater;

                        totalWater += wellProduction.ProductionWaterInWell;
                        totalOil += wellProduction.ProductionOilInWell;
                        totalGas += wellProduction.ProductionGasInWell;

                        _repository.Update(wellProduction);
                    }

                    if (fieldProductionInDatabase is not null)
                    {
                        fieldProductionInDatabase.WaterProductionInField = totalWater;
                        fieldProductionInDatabase.GasProductionInField = totalGas;
                        fieldProductionInDatabase.OilProductionInField = totalOil;

                        _productionRepository.UpdateFieldProduction(fieldProductionInDatabase);

                        totalWaterInUep += fieldProductionInDatabase.WaterProductionInField;
                    }

                    var listProductions = await _repository.getAllFieldsProductionsByProductionId(productionId);

                    foreach (var field in listProductions)
                    {
                        foreach (var wellProduction in field.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);

                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                if (completionProductionInDatabase is null)
                                    throw new NotFoundException($"Distribuição da produção da completação no dia: {production.MeasuredAt} não encontrada.");

                                if (reservoirProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                if (zoneProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                completionProductionInDatabase.OilProductionInCompletion = 0m;
                                completionProductionInDatabase.GasProductionInCompletion = 0m;
                                completionProductionInDatabase.WaterProductionInCompletion = 0m;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.OilProductionInReservoir = 0m;
                                reservoirProduction.GasProductionInReservoir = 0m;
                                reservoirProduction.WaterProductionInReservoir = 0m;

                                _repository.UpdateReservoirProduction(reservoirProduction);

                                zoneProduction.OilProductionInZone = 0m;
                                zoneProduction.GasProductionInZone = 0m;
                                zoneProduction.WaterProductionInZone = 0m;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }

                    }

                    foreach (var field in listProductions)
                    {
                        foreach (var wellProduction in field.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);
                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var allocationReservoir = completion.AllocationReservoir.Value;

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWell;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWell;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWell;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.GasProductionInReservoir += completionProductionInDatabase.GasProductionInCompletion;
                                reservoirProduction.OilProductionInReservoir += completionProductionInDatabase.OilProductionInCompletion;
                                reservoirProduction.WaterProductionInReservoir += completionProductionInDatabase.WaterProductionInCompletion;

                                zoneProduction.GasProductionInZone += completionProductionInDatabase.GasProductionInCompletion;
                                zoneProduction.OilProductionInZone += completionProductionInDatabase.OilProductionInCompletion;
                                zoneProduction.WaterProductionInZone += completionProductionInDatabase.WaterProductionInCompletion;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }
                    }

                }
            }
            else
            {
                var uepFields = await _fieldRepository
                    .GetFieldsByUepCode(production.Installation.UepCod);

                var btpsUEP = await _btpRepository
                   .GetBtpDatasByUEP(production.Installation.UepCod);

                var filtredByApplyDateAndFinal = btpsUEP
                        .Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                var totalGasPotencial = filtredByApplyDateAndFinal
                   .Sum(x => x.PotencialGas);

                var totalOilPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialOil);

                var totalWaterPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialWater);

                var totalLiquidPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialLiquid);

                foreach (var field in uepFields)
                {
                    var fieldProductionInDatabase = await _productionRepository
                            .GetFieldProductionByFieldAndProductionId(field.Id, productionId);

                    if (fieldProductionInDatabase is null)
                        throw new NotFoundException("Produção de campo não distribuida");

                    var totalGas = 0m;
                    var totalOil = 0m;
                    var totalWater = 0m;

                    //BTP FIELD
                    var btpsField = await _btpRepository
                        .GetBtpDatasByFieldId(field.Id);

                    //BTPS ATIVOS NA DATA
                    var filtredsBTPsField = btpsField
                        .Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                    //TOTAIS DE POTENCIAIS
                    var totalGasPotencialField = filtredsBTPsField
                          .Sum(x => x.PotencialGas);
                    var totalOilPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialOil);
                    var totalWaterPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialWater);

                    foreach (var wellProd in fieldProductionInDatabase.WellProductions)
                    {
                        //UEP
                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProd.BtpData.PotencialGas, totalGasPotencial);
                        var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProd.BtpData.PotencialOil, totalOilPotencial);

                        //BSW CALCS
                        var calcBSWOil = (100 - wellProd.BtpData.BSW) / 100;
                        var calcBSWWater = wellProd.BtpData.BSW / 100;

                        //PRODUCTIONS
                        var productionGas = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0));
                        var productionOIl = production.Oil.TotalOil * wellPotencialOilAsPercentageOfUEP;
                        var productionWater = (productionOIl * calcBSWWater) / calcBSWOil;

                        wellProd.ProductionOilInWell = productionOIl;
                        wellProd.ProductionGasInWell = productionGas;
                        wellProd.ProductionWaterInWell = productionWater;

                        totalWater += wellProd.ProductionWaterInWell;
                        totalGas += wellProd.ProductionGasInWell;
                        totalOil += wellProd.ProductionOilInWell;
                    }

                    fieldProductionInDatabase.WaterProductionInField = totalWater;
                    fieldProductionInDatabase.GasProductionInField = totalGas;
                    fieldProductionInDatabase.OilProductionInField = totalOil;

                    totalWaterInUep += fieldProductionInDatabase.WaterProductionInField;

                    _productionRepository.UpdateFieldProduction(fieldProductionInDatabase);

                    var listProductions = await _repository.getAllFieldsProductionsByProductionId(productionId);

                    foreach (var fieldProduction in listProductions)
                    {
                        foreach (var wellProduction in fieldProduction.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);

                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                if (completionProductionInDatabase is null)
                                    throw new NotFoundException($"Distribuição da produção da completação no dia: {production.MeasuredAt} não encontrada.");

                                if (reservoirProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                if (zoneProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                completionProductionInDatabase.OilProductionInCompletion = 0m;
                                completionProductionInDatabase.GasProductionInCompletion = 0m;
                                completionProductionInDatabase.WaterProductionInCompletion = 0m;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.OilProductionInReservoir = 0m;
                                reservoirProduction.GasProductionInReservoir = 0m;
                                reservoirProduction.WaterProductionInReservoir = 0m;

                                _repository.UpdateReservoirProduction(reservoirProduction);

                                zoneProduction.OilProductionInZone = 0m;
                                zoneProduction.GasProductionInZone = 0m;
                                zoneProduction.WaterProductionInZone = 0m;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }

                    }

                    foreach (var fieldProduction in listProductions)
                    {
                        foreach (var wellProduction in fieldProduction.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);
                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var allocationReservoir = completion.AllocationReservoir.Value;

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWell;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWell;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWell;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.GasProductionInReservoir += completionProductionInDatabase.GasProductionInCompletion;
                                reservoirProduction.OilProductionInReservoir += completionProductionInDatabase.OilProductionInCompletion;
                                reservoirProduction.WaterProductionInReservoir += completionProductionInDatabase.WaterProductionInCompletion;

                                zoneProduction.GasProductionInZone += completionProductionInDatabase.GasProductionInCompletion;
                                zoneProduction.OilProductionInZone += completionProductionInDatabase.OilProductionInCompletion;
                                zoneProduction.WaterProductionInZone += completionProductionInDatabase.WaterProductionInCompletion;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }
                    }
                }

            }

            if (production.Water is not null)
                production.Water.TotalWater = totalWaterInUep;

            _productionRepository.Update(production);

            await _repository.Save();
        }
        public async Task ReAppropriateWithWellTest(Guid productionId)
        {
            var production = await _productionRepository
              .GetById(productionId);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.WellProductions is not null && production.WellProductions.Count == 0)
                throw new ConflictException("Apropriação ainda não foi feita");

            var installations = await _installationRepository
                .GetInstallationChildrenOfUEP(production.Installation.UepCod);

            var wellsInvalids = new List<string>();


            foreach (var installation in installations)
            {
                foreach (var field in installation.Fields)
                {
                    foreach (var well in field.Wells)
                    {
                        var wellContainBtpValid = false;

                        var allBtpsValid = well.BTPDatas.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        if (well.BTPDatas is not null)
                            foreach (var btp in allBtpsValid)
                                if (btp.IsValid)
                                {
                                    wellContainBtpValid = true;
                                    break;
                                }

                        if (wellContainBtpValid is false)
                            wellsInvalids.Add(well.Name);
                    }
                }
            }
            var totalWaterInUep = 0m;

            if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
                    var fieldProductionInDatabase = await _productionRepository
                        .GetFieldProductionByFieldAndProductionId(fieldFR.Field.Id, productionId);

                    var btps = await _btpRepository
                        .GetBtpDatasByFieldId(fieldFR.Field.Id);

                    var filtredByApplyDateAndFinal = btps
                        .Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                    var totalGasPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialGas);

                    var totalOilPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialOil);

                    var totalWaterPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialWater);

                    var totalWater = 0m;
                    var totalOil = 0m;
                    var totalGas = 0m;

                    foreach (var wellProduction in fieldProductionInDatabase.WellProductions)
                    {
                        var btpValid = await _btpRepository.GetBTPsDataByWellIdAndActiveAsync(wellProduction.WellId);
                        if (btpValid is null)
                            throw new NotFoundException("");

                        wellProduction.BtpData = btpValid;

                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btpValid.PotencialGas, totalGasPotencial);
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btpValid.PotencialOil, totalOilPotencial);
                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btpValid.PotencialWater, totalWaterPotencial);


                        wellProduction.ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField;
                        wellProduction.ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField;
                        wellProduction.ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField;

                        wellProduction.ProductionGasAsPercentageOfInstallation = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialGasAsPercentageOfField, fieldFR.FRGas.Value) : 0;

                        wellProduction.ProductionOilAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialOilAsPercentageOfField, fieldFR.FROil.Value) : 0;

                        wellProduction.ProductionWaterAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialWaterAsPercentageOfField, fieldFR.FROil.Value) : 0;


                        wellProduction.ProductionGasInWell = WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellPotencialGasAsPercentageOfField);

                        wellProduction.ProductionOilInWell = WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialOilAsPercentageOfField);

                        wellProduction.ProductionWaterInWell = WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialWaterAsPercentageOfField);

                        totalWater += wellProduction.ProductionWaterInWell;
                        totalOil += wellProduction.ProductionOilInWell;
                        totalGas += wellProduction.ProductionGasInWell;

                        _repository.Update(wellProduction);
                    }

                    if (fieldProductionInDatabase is not null)
                    {
                        fieldProductionInDatabase.WaterProductionInField = totalWater;
                        fieldProductionInDatabase.GasProductionInField = totalGas;
                        fieldProductionInDatabase.OilProductionInField = totalOil;

                        _productionRepository.UpdateFieldProduction(fieldProductionInDatabase);
                    }

                    var listProductions = await _repository.getAllFieldsProductionsByProductionId(productionId);

                    foreach (var field in listProductions)
                    {
                        foreach (var wellProduction in field.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);

                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                if (completionProductionInDatabase is null)
                                    throw new NotFoundException($"Distribuição da produção da completação no dia: {production.MeasuredAt} não encontrada.");

                                if (reservoirProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                if (zoneProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                completionProductionInDatabase.OilProductionInCompletion = 0m;
                                completionProductionInDatabase.GasProductionInCompletion = 0m;
                                completionProductionInDatabase.WaterProductionInCompletion = 0m;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.OilProductionInReservoir = 0m;
                                reservoirProduction.GasProductionInReservoir = 0m;
                                reservoirProduction.WaterProductionInReservoir = 0m;

                                _repository.UpdateReservoirProduction(reservoirProduction);

                                zoneProduction.OilProductionInZone = 0m;
                                zoneProduction.GasProductionInZone = 0m;
                                zoneProduction.WaterProductionInZone = 0m;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }

                    }

                    foreach (var field in listProductions)
                    {
                        foreach (var wellProduction in field.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);
                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var allocationReservoir = completion.AllocationReservoir.Value;

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWell;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWell;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWell;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.GasProductionInReservoir += completionProductionInDatabase.GasProductionInCompletion;
                                reservoirProduction.OilProductionInReservoir += completionProductionInDatabase.OilProductionInCompletion;
                                reservoirProduction.WaterProductionInReservoir += completionProductionInDatabase.WaterProductionInCompletion;

                                zoneProduction.GasProductionInZone += completionProductionInDatabase.GasProductionInCompletion;
                                zoneProduction.OilProductionInZone += completionProductionInDatabase.OilProductionInCompletion;
                                zoneProduction.WaterProductionInZone += completionProductionInDatabase.WaterProductionInCompletion;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }
                    }
                }
            }
            else
            {
                var uepFields = await _fieldRepository
                 .GetFieldsByUepCode(production.Installation.UepCod);

                var btpsUEP = await _btpRepository
                   .GetBtpDatasByUEP(production.Installation.UepCod);

                var filtredByApplyDateAndFinal = btpsUEP
                        .Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                var totalGasPotencial = filtredByApplyDateAndFinal
                   .Sum(x => x.PotencialGas);

                var totalOilPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialOil);

                var totalWaterPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialWater);

                var totalLiquidPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialLiquid);

                foreach (var field in uepFields)
                {
                    var fieldProductionInDatabase = await _productionRepository
                            .GetFieldProductionByFieldAndProductionId(field.Id, productionId);

                    if (fieldProductionInDatabase is null)
                        throw new NotFoundException("Produção de campo não distribuida");

                    var totalGas = 0m;
                    var totalOil = 0m;
                    var totalWater = 0m;

                    foreach (var wellProd in fieldProductionInDatabase.WellProductions)
                    {
                        var btpValid = await _btpRepository.GetBTPsDataByWellIdAndActiveAsync(wellProd.WellId);
                        if (btpValid is null)
                            throw new NotFoundException("");

                        wellProd.BtpData = btpValid;

                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btpValid.PotencialGas, totalGasPotencial);
                        var wellPotencialLiquidAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btpValid.PotencialLiquid, totalLiquidPotencial);

                        var calcBSWOil = (100 - btpValid.BSW) / 100;
                        var calcBSWWater = btpValid.BSW / 100;

                        wellProd.ProductionOilInWell = production.Oil is not null ? production.Oil.TotalOil * wellPotencialLiquidAsPercentageOfUEP * calcBSWOil : 0;

                        wellProd.ProductionGasInWell = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0));

                        wellProd.ProductionWaterInWell = production.Oil is not null ? production.Oil.TotalOil * wellPotencialLiquidAsPercentageOfUEP * calcBSWWater : 0;

                        totalWater += wellProd.ProductionWaterInWell;
                        totalGas += wellProd.ProductionGasInWell;
                        totalOil += wellProd.ProductionOilInWell;
                    }

                    fieldProductionInDatabase.WaterProductionInField = totalWater;
                    fieldProductionInDatabase.GasProductionInField = totalGas;
                    fieldProductionInDatabase.OilProductionInField = totalOil;

                    totalWaterInUep += fieldProductionInDatabase.WaterProductionInField;

                    _productionRepository.UpdateFieldProduction(fieldProductionInDatabase);


                    var listProductions = await _repository.getAllFieldsProductionsByProductionId(productionId);

                    foreach (var fieldProduction in listProductions)
                    {
                        foreach (var wellProduction in fieldProduction.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);

                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                if (completionProductionInDatabase is null)
                                    throw new NotFoundException($"Distribuição da produção da completação no dia: {production.MeasuredAt} não encontrada.");

                                if (reservoirProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                if (zoneProduction is null)
                                    throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                completionProductionInDatabase.OilProductionInCompletion = 0m;
                                completionProductionInDatabase.GasProductionInCompletion = 0m;
                                completionProductionInDatabase.WaterProductionInCompletion = 0m;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.OilProductionInReservoir = 0m;
                                reservoirProduction.GasProductionInReservoir = 0m;
                                reservoirProduction.WaterProductionInReservoir = 0m;

                                _repository.UpdateReservoirProduction(reservoirProduction);

                                zoneProduction.OilProductionInZone = 0m;
                                zoneProduction.GasProductionInZone = 0m;
                                zoneProduction.WaterProductionInZone = 0m;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }

                    }

                    foreach (var fieldProduction in listProductions)
                    {
                        foreach (var wellProduction in fieldProduction.WellProductions)
                        {
                            var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                            foreach (var completion in wellInDatabase.Completions)
                            {
                                var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(productionId, completion.Reservoir.Id);
                                var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(productionId, completion.Reservoir.Zone.Id);

                                var allocationReservoir = completion.AllocationReservoir.Value;

                                var completionProductionInDatabase = await _repository
                                    .GetCompletionProduction(completion.Id, productionId);

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWell;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWell;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWell;

                                _repository.UpdateCompletionProduction(completionProductionInDatabase);

                                reservoirProduction.GasProductionInReservoir += completionProductionInDatabase.GasProductionInCompletion;
                                reservoirProduction.OilProductionInReservoir += completionProductionInDatabase.OilProductionInCompletion;
                                reservoirProduction.WaterProductionInReservoir += completionProductionInDatabase.WaterProductionInCompletion;

                                zoneProduction.GasProductionInZone += completionProductionInDatabase.GasProductionInCompletion;
                                zoneProduction.OilProductionInZone += completionProductionInDatabase.OilProductionInCompletion;
                                zoneProduction.WaterProductionInZone += completionProductionInDatabase.WaterProductionInCompletion;

                                _repository.UpdateZoneProduction(zoneProduction);

                            }
                        }
                    }
                }


            }
            await _repository.Save();
        }
    }
}