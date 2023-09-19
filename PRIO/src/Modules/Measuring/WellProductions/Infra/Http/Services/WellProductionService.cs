using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Dtos;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Dtos;
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

            #region Validate Errors
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
            var closedProducingWells = new List<Well>();
            var producingWells = new List<Well>();
            foreach (var installation in installations)
            {
                foreach (var field in installation.Fields)
                {
                    foreach (var well in field.Wells)
                    {
                        var wellContainBtpValid = false;

                        var allBtpsOfProducingWellsValid = well.WellTests.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date) && well.CategoryOperator is not null && well.CategoryOperator.ToUpper() == "PRODUTOR"
                        || well.CategoryOperator is not null && well.CategoryOperator.ToUpper() == "PRODUTOR" && (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        if (allBtpsOfProducingWellsValid is not null)
                        {
                            foreach (var btp in allBtpsOfProducingWellsValid)
                            {
                                if (btp.IsValid)
                                {
                                    wellContainBtpValid = true;
                                    break;
                                }
                            }
                            if (!allBtpsOfProducingWellsValid.Any() && well.CategoryOperator is not null && well.CategoryOperator.ToUpper().Contains("INJETOR"))
                            {
                                wellContainBtpValid = true;
                                break;
                            }
                        }

                        if (wellContainBtpValid is false)
                        {
                            wellsInvalids.Add(well.Name);
                            continue;
                        }
                    }
                }
            }
            if (wellsInvalids.Count > 0)
                throw new BadRequestException($"Todos os poços devem ter um teste de poço válido. Poços sem teste ou com teste inválido:", errors: wellsInvalids);
            #endregion

            var appropriationDto = new AppropriationDto
            {
                ProductionId = productionId,
                FieldProductions = new(),
            };

            var totalWaterInUep = 0m;
            decimal? totalWaterWithFieldFR = 0m;

            if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            {
                var wellTestsUEP = await _btpRepository.GetBtpDatasByUEP(production.Installation.UepCod);
                var filtredUEPsByApplyDateAndFinal = FilterBtp(wellTestsUEP, production);

                decimal totalPotencialGasUEP = 0;
                decimal totalPotencialOilUEP = 0;
                decimal totalPotencialWaterUEP = 0;

                foreach (var btp in filtredUEPsByApplyDateAndFinal)
                {
                    double totalInterval = 0;
                    decimal? totalAllocationByReservoir = 0;

                    var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                    || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                    foreach (var a in filtredEvents)
                    {
                        totalInterval += CalcInterval(a, production);

                        var completions = a.Well.Completions.Where(c => c.IsActive == true);
                        if (completions.Count() != 0)
                        {
                            totalAllocationByReservoir += completions.Sum(c => c.AllocationReservoir);
                            if (totalAllocationByReservoir is null)
                                throw new ConflictException($"Alocações das completações do poço {a.Well.Name} estão com valores nulos.");
                        }

                    }

                    totalPotencialGasUEP += btp.PotencialGas * totalAllocationByReservoir.Value * (24 - (decimal)totalInterval) / 24;
                    totalPotencialOilUEP += btp.PotencialOil * totalAllocationByReservoir.Value * (24 - (decimal)totalInterval) / 24;
                    totalPotencialWaterUEP += btp.PotencialWater * totalAllocationByReservoir.Value * (24 - (decimal)totalInterval) / 24;
                }
                foreach (var fieldFR in production.FieldsFR)
                {
                    var wellTestsByField = await _btpRepository
                        .GetBtpDatasByFieldId(fieldFR.Field.Id);
                    var filtredByApplyDateAndFinal = FilterBtp(wellTestsByField, production);
                    var totalOilPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialOil);

                    if (fieldFR.FROil is not null)
                    {
                        foreach (var btp in filtredByApplyDateAndFinal)
                        {
                            var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);
                            totalWaterWithFieldFR += (production.Oil.TotalOil * fieldFR.FROil * wellPotencialOilAsPercentageOfField * btp.BSW) / (100 - btp.BSW);
                        }
                    }
                }

                foreach (var fieldFR in production.FieldsFR)
                {
                    var totalWater = 0m;
                    var totalOil = 0m;
                    var totalGas = 0m;

                    var wellTestsField = await _btpRepository.GetBtpDatasByFieldId(fieldFR.Field.Id);
                    var filtredByApplyDateAndFinal = FilterBtp(wellTestsField, production);

                    var totalGasPotencial = filtredByApplyDateAndFinal.Sum(x => x.PotencialGas);
                    var totalOilPotencial = filtredByApplyDateAndFinal.Sum(x => x.PotencialOil);
                    var totalWaterPotencial = filtredByApplyDateAndFinal.Sum(x => x.PotencialWater);

                    decimal totalPotencialGasField = 0;
                    decimal totalPotencialOilField = 0;
                    decimal totalPotencialWaterField = 0;
                    foreach (var btp in filtredByApplyDateAndFinal)
                    {
                        double totalInterval = 0;
                        decimal? totalAllocationByReservoir = 0;
                        var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                        || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        foreach (var a in filtredEvents)
                        {
                            var completions = a.Well.Completions.Where(c => c.IsActive == true);
                            if (completions.Count() != 0)
                            {
                                totalAllocationByReservoir += completions.Sum(c => c.AllocationReservoir);
                                if (totalAllocationByReservoir is null)
                                    throw new ConflictException($"Alocações das completações do poço {a.Well.Name} estão com valores nulos.");
                            }
                            totalInterval += CalcInterval(a, production);
                        }

                        totalPotencialGasField += btp.PotencialGas * totalAllocationByReservoir.Value * (24 - (decimal)totalInterval) / 24;
                        totalPotencialOilField += btp.PotencialOil * totalAllocationByReservoir.Value * (24 - (decimal)totalInterval) / 24;
                        totalPotencialWaterField += btp.PotencialWater * totalAllocationByReservoir.Value * (24 - (decimal)totalInterval) / 24;
                    }

                    FieldProduction? fieldProduction = filtredByApplyDateAndFinal.Any() ? new()
                    {
                        Id = Guid.NewGuid(),
                        FieldId = fieldFR.Field.Id,
                        ProductionId = production.Id,
                    } : null;

                    var wellAppropiationsDto = new List<WellProductionDto>();
                    foreach (var btp in filtredByApplyDateAndFinal)
                    {
                        double totalInterval = 0;
                        var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        var listEvents = new List<CreateWellLossDTO>();
                        foreach (var a in filtredEvents)
                        {
                            if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                            {
                                double interval = ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)interval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                            {
                                double interval = 24;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)interval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                            {
                                double interval = ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                            {
                                double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                            {
                                double interval = 24;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                            {
                                double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }

                        }
                        if (totalPotencialOilUEP == 0)
                            throw new ConflictException("Erro: Somatório dos potenciais do óleo está zerado.");

                        if (totalPotencialGasUEP == 0)
                            throw new ConflictException("Erro: Somatório dos potenciais do gás está zerado.");

                        if (totalPotencialWaterUEP == 0)
                            throw new ConflictException("Erro: Somatório dos potenciais do água está zerado.");

                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasUEP);
                        var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilUEP);
                        var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterUEP);

                        var wellPotencialGasAsPercentageOfField = totalPotencialGasField != 0 ? WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasField) : 0;
                        var wellPotencialOilAsPercentageOfField = totalPotencialOilField != 0 ? WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilField) : 0;
                        var wellPotencialWaterAsPercentageOfField = totalPotencialWaterField != 0 ? WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterField) : 0;

                        var productionGas = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellPotencialGasAsPercentageOfField) : 0;
                        var productionOil = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialOilAsPercentageOfField) : 0;
                        var productionWater = (productionOil * btp.BSW) / (100 - btp.BSW);

                        int hours = (int)totalInterval;
                        double minutesDecimal = (totalInterval - hours) * 60;
                        int minutes = (int)minutesDecimal;
                        double secondsDecimal = (minutesDecimal - minutes) * 60;
                        int seconds = (int)secondsDecimal;
                        DateTime dateTime = DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
                        string formattedTime = dateTime.ToString("HH:mm:ss");

                        var wellAppropriation = new WellProduction
                        {
                            Id = Guid.NewGuid(),
                            Production = production,
                            WellTest = btp,
                            WellId = btp.Well.Id,
                            FieldProduction = fieldProduction,
                            ProductionGasInWellM3 = productionGas,
                            ProductionGasInWellSCF = ProductionUtils.m3ToSCFConversionMultipler * productionGas,
                            ProductionOilInWellM3 = productionOil,
                            ProductionOilInWellBBL = ProductionUtils.m3ToBBLConversionMultiplier * productionOil,
                            ProductionWaterInWellM3 = productionWater,
                            ProductionWaterInWellBBL = ProductionUtils.m3ToBBLConversionMultiplier * productionWater,
                            ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField,
                            ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField,
                            ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField,
                            ProductionGasAsPercentageOfInstallation = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialGasAsPercentageOfField, fieldFR.FRGas.Value) : 0,
                            ProductionOilAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialOilAsPercentageOfField, fieldFR.FROil.Value) : 0,
                            ProductionWaterAsPercentageOfInstallation = totalWaterWithFieldFR != 0 ? productionWater / totalWaterWithFieldFR.Value : 0,
                            Downtime = formattedTime,
                        };

                        totalWater += wellAppropriation.ProductionWaterInWellM3;
                        totalOil += wellAppropriation.ProductionOilInWellM3;
                        totalGas += wellAppropriation.ProductionGasInWellM3;

                        var wellAppropiationDto = new WellProductionDto
                        {
                            WellProductionId = wellAppropriation.Id,
                            WellName = btp.WellName,
                            ProductionGasInWellM3 = Math.Round(wellAppropriation.ProductionGasInWellM3, 5),
                            ProductionOilInWellM3 = Math.Round(wellAppropriation.ProductionOilInWellM3, 5),
                            ProductionWaterInWellM3 = Math.Round(wellAppropriation.ProductionWaterInWellM3, 5),
                            ProductionGasInWellSCF = Math.Round(wellAppropriation.ProductionGasInWellM3 * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            ProductionOilInWellBBL = Math.Round(wellAppropriation.ProductionOilInWellM3 * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            ProductionWaterInWellBBL = Math.Round(wellAppropriation.ProductionWaterInWellM3 * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            Downtime = formattedTime,

                        };

                        foreach (var ev in listEvents)
                        {
                            int year = production.MeasuredAt.Year;
                            int month = production.MeasuredAt.Month;
                            int daysInMonth = DateTime.DaysInMonth(year, month);

                            var wellLoss = new WellLosses
                            {
                                Id = ev.Id,
                                MeasuredAt = ev.MeasuredAt,
                                WellAllocation = wellAppropriation,
                                Downtime = ev.Downtime,
                                Event = ev.Event,

                                EfficienceLoss = (((btp.PotencialOil * ev.Downtime) / 24) / totalPotencialOilField) / daysInMonth,
                                ProductionLostOil = (((btp.PotencialOil * ev.Downtime) / totalPotencialOilField) / 24) * fieldFR.OilProductionInField,
                                ProportionalDay = ((btp.PotencialOil * ev.Downtime) / 24) / totalPotencialOilField,

                                ProductionLostGas = (((btp.PotencialGas * ev.Downtime) / totalPotencialGasField) / 24) * fieldFR.GasProductionInField,

                                ProductionLostWater = (((btp.PotencialWater * ev.Downtime) / totalPotencialWaterField) / 24) * fieldFR.OilProductionInField,
                            };

                            wellAppropriation.EfficienceLoss += wellLoss.EfficienceLoss;
                            wellAppropriation.ProductionLostOil += wellLoss.ProductionLostOil;
                            wellAppropriation.ProportionalDay += wellLoss.ProportionalDay;

                            wellAppropriation.ProductionLostGas += wellLoss.ProductionLostGas;

                            wellAppropriation.ProductionLostWater += wellLoss.ProductionLostWater;

                            await _repository.AddWellLossAsync(wellLoss);
                        }
                        wellAppropiationDto.EfficienceLoss = wellAppropriation.EfficienceLoss;
                        wellAppropiationDto.ProductionLostOilM3 = wellAppropriation.ProductionLostOil;
                        wellAppropiationDto.ProductionLostOilBBL = wellAppropriation.ProductionLostOil * ProductionUtils.m3ToBBLConversionMultiplier;
                        wellAppropiationDto.ProportionalDay = wellAppropriation.ProportionalDay;

                        wellAppropiationDto.ProductionLostGasM3 = wellAppropriation.ProductionLostGas;
                        wellAppropiationDto.ProductionLostGasSCF = wellAppropriation.ProductionLostGas * ProductionUtils.m3ToSCFConversionMultipler;

                        wellAppropiationDto.ProductionLostWaterBBL = wellAppropriation.ProductionLostWater * ProductionUtils.m3ToBBLConversionMultiplier;
                        wellAppropiationDto.ProductionLostWaterM3 = wellAppropriation.ProductionLostWater;

                        await _repository.AddAsync(wellAppropriation);
                        wellAppropiationsDto.Add(wellAppropiationDto);
                    }

                    if (fieldProduction is not null)
                    {
                        fieldProduction.WaterProductionInField = totalWater;
                        fieldProduction.GasProductionInField = totalGas;
                        fieldProduction.OilProductionInField = totalOil;

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

                totalWaterInUep = totalWaterWithFieldFR.Value;
                await _repository.Save();

                await DistributeAccrossEntites(productionId);
            }

            else
            {
                var uepFields = await _fieldRepository.GetFieldsByUepCode(production.Installation.UepCod);
                var btpsUEP = await _btpRepository
                    .GetBtpDatasByUEP(production.Installation.UepCod);
                var filtredByApplyDateAndFinal = FilterBtp(btpsUEP, production);

                decimal totalPotencialGasUEP = 0;
                decimal totalPotencialOilUEP = 0;
                decimal totalPotencialWaterUEP = 0;

                foreach (var btp in filtredByApplyDateAndFinal)
                {
                    double totalInterval = 0;
                    var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                    || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                    foreach (var a in filtredEvents)
                        totalInterval += CalcInterval(a, production);

                    totalPotencialGasUEP += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                    totalPotencialOilUEP += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                    totalPotencialWaterUEP += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                }

                foreach (var fieldInDatabase in uepFields)
                {
                    var wellAppropiationsDto = new List<WellProductionDto>();

                    FieldProduction? fieldProduction = filtredByApplyDateAndFinal.Any() ? new()
                    {
                        Id = Guid.NewGuid(),
                        ProductionId = production.Id,
                        FieldId = fieldInDatabase.Id,

                    } : null;

                    var totalGas = 0m;
                    var totalOil = 0m;
                    var totalWater = 0m;

                    var totalGasDto = 0m;
                    var totalOilDto = 0m;
                    var totalWaterDto = 0m;

                    var btpsField = await _btpRepository
                            .GetBtpDatasByFieldId(fieldInDatabase.Id);
                    var filtredsBTPsField = FilterBtp(btpsField, production);

                    var totalGasPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialGas);
                    var totalOilPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialOil);
                    var totalWaterPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialWater);

                    decimal totalPotencialGasField = 0;
                    decimal totalPotencialOilField = 0;
                    decimal totalPotencialWaterField = 0;

                    foreach (var btp in filtredsBTPsField)
                    {
                        double totalInterval = 0;
                        var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                        || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        foreach (var a in filtredEvents)
                            totalInterval += CalcInterval(a, production);

                        totalPotencialGasField += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                        totalPotencialOilField += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                        totalPotencialWaterField += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                    }

                    foreach (var btp in filtredsBTPsField)
                    {
                        double totalInterval = 0;
                        var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        var listEvents = new List<CreateWellLossDTO>();
                        foreach (var a in filtredEvents)
                        {
                            if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                            {
                                double interval = ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)interval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                            {
                                double interval = 24;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)interval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                            {
                                double interval = ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                            {
                                double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                            {
                                double interval = 24;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                            else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                            {
                                double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                totalInterval += interval;
                                listEvents.Add(new CreateWellLossDTO
                                {
                                    Downtime = (decimal)totalInterval,
                                    Event = a,
                                    Id = Guid.NewGuid(),
                                    MeasuredAt = production.MeasuredAt
                                });
                            }
                        }

                        if (totalPotencialOilUEP == 0)
                            throw new ConflictException("Erro: Somatório dos potenciais do óleo está zerado.");

                        if (totalPotencialGasUEP == 0)
                            throw new ConflictException("Erro: Somatório dos potenciais do gás está zerado.");

                        if (totalPotencialWaterUEP == 0)
                            throw new ConflictException("Erro: Somatório dos potenciais do água está zerado.");

                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasUEP);
                        var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilUEP);
                        var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterUEP);
                        var wellPotencialGasAsPercentageOfField = totalPotencialGasField != 0 ? WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasField) : 0;
                        var wellPotencialOilAsPercentageOfField = totalPotencialOilField != 0 ? WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilField) : 0;
                        var wellPotencialWaterAsPercentageOfField = totalPotencialWaterField != 0 ? WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterField) : 0;
                        var productionGas = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0));
                        var productionOIl = production.Oil.TotalOil * wellPotencialOilAsPercentageOfUEP;
                        var calcBSWOil = (100 - btp.BSW) / 100;
                        var calcBSWWater = btp.BSW / 100;
                        var productionWater = (productionOIl * calcBSWWater) / calcBSWOil;

                        int hours = (int)totalInterval;
                        double minutesDecimal = (totalInterval - hours) * 60;
                        int minutes = (int)minutesDecimal;
                        double secondsDecimal = (minutesDecimal - minutes) * 60;
                        int seconds = (int)secondsDecimal;
                        DateTime dateTime = DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
                        string formattedTime = dateTime.ToString("HH:mm:ss");

                        var wellAppropriation = new WellProduction
                        {
                            Id = Guid.NewGuid(),
                            WellTest = btp,
                            Production = production,
                            ProductionGasAsPercentageOfInstallation = wellPotencialGasAsPercentageOfUEP,
                            ProductionOilAsPercentageOfInstallation = wellPotencialOilAsPercentageOfUEP,
                            ProductionWaterAsPercentageOfInstallation = wellPotencialWaterAsPercentageOfUEP,
                            WellId = btp.Well.Id,
                            FieldProduction = fieldProduction,
                            ProductionGasInWellM3 = productionGas,
                            ProductionGasInWellSCF = ProductionUtils.m3ToSCFConversionMultipler * productionGas,
                            ProductionOilInWellM3 = productionOIl,
                            ProductionOilInWellBBL = ProductionUtils.m3ToBBLConversionMultiplier * productionOIl,
                            ProductionWaterInWellM3 = productionWater,
                            ProductionWaterInWellBBL = ProductionUtils.m3ToBBLConversionMultiplier * productionWater,
                            ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField,
                            ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField,
                            ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField,
                            Downtime = formattedTime,

                        };

                        var wellAppropiationDto = new WellProductionDto
                        {
                            WellProductionId = wellAppropriation.Id,
                            WellName = btp.WellName,
                            ProductionGasInWellM3 = Math.Round(wellAppropriation.ProductionGasInWellM3, 5),
                            ProductionOilInWellM3 = Math.Round(wellAppropriation.ProductionOilInWellM3, 5),
                            ProductionWaterInWellM3 = Math.Round(wellAppropriation.ProductionWaterInWellM3, 5),
                            ProductionGasInWellSCF = Math.Round(wellAppropriation.ProductionGasInWellSCF, 5),
                            ProductionOilInWellBBL = Math.Round(wellAppropriation.ProductionOilInWellBBL, 5),
                            ProductionWaterInWellBBL = Math.Round(wellAppropriation.ProductionWaterInWellBBL, 5),
                            Downtime = wellAppropriation.Downtime
                        };

                        totalWater += wellAppropriation.ProductionWaterInWellM3;
                        totalOil += wellAppropriation.ProductionOilInWellM3;
                        totalGas += wellAppropriation.ProductionGasInWellM3;

                        totalWaterDto += wellAppropiationDto.ProductionWaterInWellM3;
                        totalOilDto += wellAppropiationDto.ProductionOilInWellM3;
                        totalGasDto += wellAppropiationDto.ProductionGasInWellM3;

                        if (fieldProduction is not null)
                            fieldProduction.FieldId = btp.Well.Field.Id;

                        foreach (var ev in listEvents)
                        {
                            int year = production.MeasuredAt.Year;
                            int month = production.MeasuredAt.Month;

                            int daysInMonth = DateTime.DaysInMonth(year, month);

                            var wellLoss = new WellLosses
                            {
                                Id = ev.Id,
                                MeasuredAt = ev.MeasuredAt,
                                WellAllocation = wellAppropriation,
                                Downtime = ev.Downtime,
                                Event = ev.Event,
                                EfficienceLoss = (((btp.PotencialOil * ev.Downtime) / 24) / totalOilPotencialField) / daysInMonth,
                                ProductionLostOil = ((btp.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * ev.Downtime / 24,
                                ProportionalDay = ((btp.PotencialOil * ev.Downtime) / 24) / totalOilPotencialField,

                                ProductionLostGas = ((btp.PotencialGas / totalPotencialGasUEP) * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0))) * ev.Downtime / 24,


                                ProductionLostWater = (((btp.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * ev.Downtime / 24) * calcBSWWater / calcBSWOil,

                            };
                            wellAppropriation.EfficienceLoss += wellLoss.EfficienceLoss;
                            wellAppropriation.ProductionLostOil += wellLoss.ProductionLostOil;
                            wellAppropriation.ProportionalDay += wellLoss.ProportionalDay;

                            wellAppropriation.ProductionLostGas += wellLoss.ProductionLostGas;

                            wellAppropriation.ProductionLostWater += wellLoss.ProductionLostWater;

                            await _repository.AddWellLossAsync(wellLoss);
                        }

                        wellAppropiationDto.EfficienceLoss = Math.Round(wellAppropriation.EfficienceLoss, 5);
                        wellAppropiationDto.ProductionLostOilM3 = Math.Round(wellAppropriation.ProductionLostOil, 5);
                        wellAppropiationDto.ProportionalDay = Math.Round(wellAppropriation.ProportionalDay, 5);

                        wellAppropiationDto.ProductionLostGasM3 = Math.Round(wellAppropriation.ProductionLostGas, 5);

                        wellAppropiationDto.ProductionLostWaterM3 = Math.Round(wellAppropriation.ProductionLostWater, 5);

                        wellAppropiationDto.ProductionLostGasSCF = Math.Round(wellAppropriation.ProductionLostGas * ProductionUtils.m3ToSCFConversionMultipler, 5);
                        wellAppropiationDto.ProductionLostWaterBBL = Math.Round(wellAppropriation.ProductionLostWater * ProductionUtils.m3ToBBLConversionMultiplier, 5);
                        wellAppropiationDto.ProductionLostOilBBL = Math.Round(wellAppropriation.ProductionLostOil * ProductionUtils.m3ToBBLConversionMultiplier, 5);


                        wellAppropiationsDto.Add(wellAppropiationDto);
                        await _repository.AddAsync(wellAppropriation);

                    }

                    if (fieldProduction is not null)
                    {
                        fieldProduction.WaterProductionInField = totalWater;
                        fieldProduction.GasProductionInField = totalGas;
                        fieldProduction.OilProductionInField = totalOil;

                        var fieldLossOil = wellAppropiationsDto.Sum(x => x.ProductionLostOilM3);
                        var fieldLossGas = wellAppropiationsDto.Sum(x => x.ProductionLostGasM3);
                        var fieldLossWater = wellAppropiationsDto.Sum(x => x.ProductionLostWaterM3);

                        totalWaterInUep += fieldProduction.WaterProductionInField;

                        var orderedWellAppropriationsDto = wellAppropiationsDto
                            .OrderBy(x => x.WellName)
                            .ToList();

                        var fieldProductionDto = new FieldProductionDto
                        {
                            FieldProductionId = fieldProduction.Id,
                            FieldName = fieldInDatabase.Name,
                            GasProductionInFieldM3 = Math.Round(totalGasDto, 5),
                            OilProductionInFieldM3 = Math.Round(totalOilDto, 5),
                            WaterProductionInFieldM3 = Math.Round(totalWaterDto, 5),
                            GasProductionInFieldSCF = Math.Round(totalGasDto * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            OilProductionInFieldBBL = Math.Round(totalOilDto * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            WaterProductionInFieldBBL = Math.Round(totalWaterDto * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            WellAppropriations = orderedWellAppropriationsDto,

                            GasLossInFieldM3 = Math.Round(fieldLossGas, 5),
                            OilLossInFieldM3 = Math.Round(fieldLossOil, 5),
                            WaterLossInFieldM3 = Math.Round(fieldLossWater, 5),

                            GasLossInFieldSCF = Math.Round(fieldLossGas * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            OilLossInFieldBBL = Math.Round(fieldLossOil * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            WaterLossInFieldBBL = Math.Round(fieldLossWater * ProductionUtils.m3ToBBLConversionMultiplier, 5),
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
            production.IsCalculated = true;

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
                            GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWellM3,
                            OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWellM3,
                            WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWellM3,
                            ProductionId = productionId,
                            CompletionId = completion.Id,
                            WellAllocation = wellProduction,
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

                        var allBtpsValid = well.WellTests.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        if (well.WellTests is not null)
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

            if (wellsInvalids.Count > 0)
                throw new BadRequestException($"Todos os poços devem ter um teste de poço válido. Poços sem teste ou com teste inválido:", errors: wellsInvalids);

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
                       .Sum(x => ((24 - WellProductionUtils.CalculateDowntimeInHours(x.Downtime)) / 24) * x.WellTest.PotencialGas);
                    var totalOilPotencial = fieldProductionInDatabase.WellProductions
                       .Sum(x => ((24 - WellProductionUtils.CalculateDowntimeInHours(x.Downtime)) / 24) * x.WellTest.PotencialOil);
                    var totalWaterPotencial = fieldProductionInDatabase.WellProductions
                       .Sum(x => ((24 - WellProductionUtils.CalculateDowntimeInHours(x.Downtime)) / 24) * x.WellTest.PotencialWater);

                    var totalGasPotencialWithoutDowntime = fieldProductionInDatabase.WellProductions
                .Sum(x => x.WellTest.PotencialGas);
                    var totalOilPotencialWithoutDowntime = fieldProductionInDatabase.WellProductions
                       .Sum(x => x.WellTest.PotencialOil);
                    var totalWaterPotencialWithoutDowntime = fieldProductionInDatabase.WellProductions
                       .Sum(x => x.WellTest.PotencialWater);

                    var totalWater = 0m;
                    var totalOil = 0m;
                    var totalGas = 0m;

                    foreach (var wellProduction in fieldProductionInDatabase.WellProductions)
                    {
                        //PERCENT FIELD
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.WellTest.PotencialGas, totalGasPotencial);
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.WellTest.PotencialOil, totalOilPotencial);
                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.WellTest.PotencialWater, totalWaterPotencial);
                        var calcBSWOil = (100 - wellProduction.WellTest.BSW) / 100;
                        var calcBSWWater = wellProduction.WellTest.BSW / 100;
                        //PRODUCTIONS
                        var productionGas = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellPotencialGasAsPercentageOfField) : 0;
                        var productionOil = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialOilAsPercentageOfField) : 0;
                        var productionWater = (productionOil * wellProduction.WellTest.BSW) / (100 - wellProduction.WellTest.BSW);

                        wellProduction.ProductionGasInWellM3 = productionGas * (24 - WellProductionUtils.CalculateDowntimeInHours(wellProduction.Downtime)) / 24;
                        wellProduction.ProductionOilInWellM3 = productionOil * (24 - WellProductionUtils.CalculateDowntimeInHours(wellProduction.Downtime)) / 24;
                        wellProduction.ProductionWaterInWellM3 = productionWater * (24 - WellProductionUtils.CalculateDowntimeInHours(wellProduction.Downtime)) / 24;

                        wellProduction.ProductionOilInWellBBL = wellProduction.ProductionOilInWellM3 * ProductionUtils.m3ToBBLConversionMultiplier;
                        wellProduction.ProductionGasInWellSCF = wellProduction.ProductionGasInWellM3 * ProductionUtils.m3ToSCFConversionMultipler;
                        wellProduction.ProductionWaterInWellBBL = wellProduction.ProductionWaterInWellM3 * ProductionUtils.m3ToBBLConversionMultiplier;

                        foreach (var wellLoss in wellProduction.WellLosses)
                        {

                            wellLoss.ProductionLostOil = (((wellProduction.WellTest.PotencialOil / totalOilPotencialWithoutDowntime) * (production.Oil is not null ? production.Oil.TotalOil : 0) * (fieldFR.FROil is not null ? fieldFR.FROil.Value : 0))) * wellLoss.Downtime / 24;

                            wellLoss.ProductionLostGas = ((wellProduction.WellTest.PotencialGas / totalGasPotencialWithoutDowntime * (fieldFR.FRGas is not null ? fieldFR.FRGas.Value : 0)) * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0))) * wellLoss.Downtime / 24;
                            wellLoss.ProductionLostWater = (((wellProduction.WellTest.PotencialOil / totalOilPotencialWithoutDowntime) * production.Oil.TotalOil * fieldFR.FROil.Value) * wellLoss.Downtime / 24) * calcBSWWater / calcBSWOil;

                            _repository.UpdateWellLost(wellLoss);
                        }

                        wellProduction.ProductionLostGas = wellProduction.WellLosses.Sum(x => x.ProductionLostGas);
                        wellProduction.ProductionLostOil = wellProduction.WellLosses.Sum(x => x.ProductionLostOil);
                        wellProduction.ProductionLostWater = wellProduction.WellLosses.Sum(x => x.ProductionLostWater);

                        totalWater += wellProduction.ProductionWaterInWellM3;
                        totalOil += wellProduction.ProductionOilInWellM3;
                        totalGas += wellProduction.ProductionGasInWellM3;

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

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWellM3;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWellM3;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWellM3;

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
                var filtredByApplyDateAndFinal = FilterBtp(btpsUEP, production);

                decimal totalPotencialGasUEP = 0;
                decimal totalPotencialOilUEP = 0;
                decimal totalPotencialWaterUEP = 0;

                //LOOP INTERVAL - CALC SUM UEP
                foreach (var btp in filtredByApplyDateAndFinal)
                {
                    double totalInterval = 0;

                    var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                    || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                    foreach (var a in filtredEvents)
                        totalInterval += CalcInterval(a, production);

                    totalPotencialGasUEP += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                    totalPotencialOilUEP += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                    totalPotencialWaterUEP += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                }

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
                        double totalInterval = 0;
                        var filtredEvents = wellProd.WellTest.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        foreach (var a in filtredEvents)
                            totalInterval += CalcInterval(a, production);

                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProd.WellTest.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasUEP);
                        var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProd.WellTest.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilUEP);
                        var calcBSWOil = (100 - wellProd.WellTest.BSW) / 100;
                        var calcBSWWater = wellProd.WellTest.BSW / 100;
                        var productionGas = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0));
                        var productionOIl = production.Oil.TotalOil * wellPotencialOilAsPercentageOfUEP;
                        var productionWater = (productionOIl * calcBSWWater) / calcBSWOil;

                        wellProd.ProductionOilInWellM3 = productionOIl;
                        wellProd.ProductionGasInWellM3 = productionGas;
                        wellProd.ProductionWaterInWellM3 = productionWater;

                        wellProd.ProductionOilInWellBBL = productionOIl * ProductionUtils.m3ToBBLConversionMultiplier;
                        wellProd.ProductionGasInWellSCF = productionGas * ProductionUtils.m3ToSCFConversionMultipler;
                        wellProd.ProductionWaterInWellBBL = productionWater * ProductionUtils.m3ToBBLConversionMultiplier;

                        foreach (var wellLoss in wellProd.WellLosses)
                        {

                            wellLoss.ProductionLostOil = ((wellProd.WellTest.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * wellLoss.Downtime / 24;
                            wellLoss.ProductionLostGas = ((wellProd.WellTest.PotencialGas / totalPotencialGasUEP) * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0))) * wellLoss.Downtime / 24;
                            wellLoss.ProductionLostWater = (((wellProd.WellTest.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * wellLoss.Downtime / 24) * calcBSWWater / calcBSWOil;

                            _repository.UpdateWellLost(wellLoss);
                        }

                        wellProd.ProductionLostGas = wellProd.WellLosses.Sum(x => x.ProductionLostGas);
                        wellProd.ProductionLostOil = wellProd.WellLosses.Sum(x => x.ProductionLostOil);
                        wellProd.ProductionLostWater = wellProd.WellLosses.Sum(x => x.ProductionLostWater);

                        totalWater += wellProd.ProductionWaterInWellM3;
                        totalGas += wellProd.ProductionGasInWellM3;
                        totalOil += wellProd.ProductionOilInWellM3;

                        _repository.Update(wellProd);
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

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWellM3;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWellM3;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWellM3;

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

                        var allBtpsValid = well.WellTests.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        if (well.WellTests is not null)
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
            decimal? totalWaterWithFieldFR = 0;
            int year = production.MeasuredAt.Year;
            int month = production.MeasuredAt.Month;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            {
                var UEPWellTests = await _btpRepository
                   .GetBtpDatasByUEP(production.Installation.UepCod);
                var filtredUEPsByApplyDateAndFinal = FilterBtp(UEPWellTests, production);

                decimal totalPotencialGasUEP = 0;
                decimal totalPotencialOilUEP = 0;
                decimal totalPotencialWaterUEP = 0;

                foreach (var wellTest in filtredUEPsByApplyDateAndFinal)
                {
                    double totalInterval = 0;
                    var filtredEvents = wellTest.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                    || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);
                    foreach (var a in filtredEvents)
                    {
                        if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                        {
                            totalInterval += ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                        }
                        else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                        {
                            totalInterval += 24;
                        }
                        else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                        {
                            totalInterval += ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                        }
                        else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                        {
                            totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                        }
                        else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                        {
                            totalInterval += 24;
                        }
                        else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                        {
                            totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                        }
                    }
                    totalPotencialGasUEP += wellTest.PotencialGas * (24 - (decimal)totalInterval) / 24;
                    totalPotencialOilUEP += wellTest.PotencialOil * (24 - (decimal)totalInterval) / 24;
                    totalPotencialWaterUEP += wellTest.PotencialWater * (24 - (decimal)totalInterval) / 24;
                }

                foreach (var fieldFR in production.FieldsFR)
                {
                    var FieldWellTests = await _btpRepository
                        .GetBtpDatasByFieldId(fieldFR.Field.Id);
                    var filtredByApplyDateAndFinal = FilterBtp(FieldWellTests, production);

                    var totalOilPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialOil);

                    foreach (var btp in filtredByApplyDateAndFinal)
                    {
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);
                        totalWaterWithFieldFR += (production.Oil.TotalOil * fieldFR.FROil * wellPotencialOilAsPercentageOfField * btp.BSW) / (100 - btp.BSW);
                    }
                }

                foreach (var fieldFR in production.FieldsFR)
                {
                    var totalWater = 0m;
                    var totalOil = 0m;
                    var totalGas = 0m;

                    var fieldProductionInDatabase = await _productionRepository
                        .GetFieldProductionByFieldAndProductionId(fieldFR.Field.Id, productionId);

                    var btps = await _btpRepository
                        .GetBtpDatasByFieldId(fieldFR.Field.Id);
                    var filtredByApplyDateAndFinal = FilterBtp(btps, production);

                    var totalGasPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialGas);
                    var totalOilPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialOil);
                    var totalWaterPotencial = filtredByApplyDateAndFinal
                        .Sum(x => x.PotencialWater);

                    decimal totalPotencialGasField = 0;
                    decimal totalPotencialOilField = 0;
                    decimal totalPotencialWaterField = 0;

                    foreach (var wellTest in filtredByApplyDateAndFinal)
                    {
                        double totalInterval = 0;

                        var filtredEvents = wellTest.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                        || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        foreach (var a in filtredEvents)
                            totalInterval += CalcInterval(a, production);

                        totalPotencialGasField += wellTest.PotencialGas * (24 - (decimal)totalInterval) / 24;
                        totalPotencialOilField += wellTest.PotencialOil * (24 - (decimal)totalInterval) / 24;
                        totalPotencialWaterField += wellTest.PotencialWater * (24 - (decimal)totalInterval) / 24;
                    }

                    foreach (var wellProduction in fieldProductionInDatabase.WellProductions)
                    {
                        var btpValid = await _btpRepository.GetBTPsDataByWellIdAndActiveAsync(wellProduction.WellId);
                        if (btpValid is null)
                            throw new NotFoundException("");

                        wellProduction.WellTest = btpValid;

                        double totalInterval = 0;
                        var filtredEvents = wellProduction.WellTest.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        foreach (var a in filtredEvents)
                            totalInterval += CalcInterval(a, production);

                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProduction.WellTest.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasUEP);
                        var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProduction.WellTest.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilUEP);
                        var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProduction.WellTest.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterUEP);
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((wellProduction.WellTest.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasField);
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((wellProduction.WellTest.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilField);
                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((wellProduction.WellTest.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterField);
                        var productionGas = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellPotencialGasAsPercentageOfField) : 0;
                        var productionOil = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialOilAsPercentageOfField) : 0;
                        var productionWater = (productionOil * wellProduction.WellTest.BSW) / (100 - wellProduction.WellTest.BSW);

                        wellProduction.ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField;
                        wellProduction.ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField;
                        wellProduction.ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField;
                        wellProduction.ProductionGasAsPercentageOfInstallation = wellPotencialGasAsPercentageOfField * fieldFR.FRGas.Value;
                        wellProduction.ProductionOilAsPercentageOfInstallation = wellPotencialOilAsPercentageOfField * fieldFR.FROil.Value;
                        wellProduction.ProductionWaterAsPercentageOfInstallation = productionWater / totalWaterWithFieldFR.Value;
                        wellProduction.ProductionOilInWellM3 = productionOil;
                        wellProduction.ProductionOilInWellBBL = productionOil * ProductionUtils.m3ToBBLConversionMultiplier;
                        wellProduction.ProductionGasInWellM3 = productionGas;
                        wellProduction.ProductionGasInWellSCF = productionGas * ProductionUtils.m3ToSCFConversionMultipler;
                        wellProduction.ProductionWaterInWellM3 = productionWater;
                        wellProduction.ProductionWaterInWellBBL = productionWater * ProductionUtils.m3ToBBLConversionMultiplier;

                        foreach (var wellLoss in wellProduction.WellLosses)
                        {
                            wellLoss.ProductionLostOil = ((wellProduction.WellTest.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * wellLoss.Downtime / 24;
                            wellLoss.ProductionLostGas = ((wellProduction.WellTest.PotencialGas / totalPotencialGasUEP) * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0))) * wellLoss.Downtime / 24;
                            wellLoss.ProductionLostWater = (((wellProduction.WellTest.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * wellLoss.Downtime / 24) * btpValid.BSW / (100 - btpValid.BSW);
                            wellLoss.ProportionalDay = wellLoss.ProductionLostOil / totalPotencialOilField;
                            wellLoss.EfficienceLoss = wellLoss.ProportionalDay / daysInMonth;

                            _repository.UpdateWellLost(wellLoss);
                        }

                        totalWater += wellProduction.ProductionWaterInWellM3;
                        totalOil += wellProduction.ProductionOilInWellM3;
                        totalGas += wellProduction.ProductionGasInWellM3;

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

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWellM3;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWellM3;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWellM3;

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

                var filtredByApplyDateAndFinal = FilterBtp(btpsUEP, production);

                decimal totalPotencialGasUEP = 0;
                decimal totalPotencialOilUEP = 0;
                decimal totalPotencialWaterUEP = 0;
                foreach (var btp in filtredByApplyDateAndFinal)
                {
                    double totalInterval = 0;

                    var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                    || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                    foreach (var a in filtredEvents)
                        totalInterval += CalcInterval(a, production);

                    totalPotencialGasUEP += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                    totalPotencialOilUEP += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                    totalPotencialWaterUEP += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                }

                foreach (var field in uepFields)
                {
                    var fieldProductionInDatabase = await _productionRepository
                            .GetFieldProductionByFieldAndProductionId(field.Id, productionId);

                    if (fieldProductionInDatabase is null)
                        throw new NotFoundException("Produção de campo não distribuida");

                    var totalGas = 0m;
                    var totalOil = 0m;
                    var totalWater = 0m;

                    var btpsField = await _btpRepository
                          .GetBtpDatasByFieldId(field.Id);
                    var filtredsBTPsField = FilterBtp(btpsField, production);

                    var totalGasPotencialField = filtredsBTPsField
                          .Sum(x => x.PotencialGas);
                    var totalOilPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialOil);
                    var totalWaterPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialWater);

                    decimal totalPotencialGasField = 0;
                    decimal totalPotencialOilField = 0;
                    decimal totalPotencialWaterField = 0;

                    foreach (var btp in filtredsBTPsField)
                    {
                        double totalInterval = 0;
                        var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                        || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        foreach (var a in filtredEvents)
                            totalInterval += CalcInterval(a, production);

                        totalPotencialGasField += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                        totalPotencialOilField += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                        totalPotencialWaterField += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                    }

                    foreach (var wellProd in fieldProductionInDatabase.WellProductions)
                    {
                        var btpValid = await _btpRepository.GetBTPsDataByWellIdAndActiveAsync(wellProd.WellId);
                        if (btpValid is null)
                            throw new NotFoundException("Não foi encontrado teste de poço ativo.");

                        wellProd.WellTest = btpValid;

                        double totalInterval = 0;
                        var filtredEvents = wellProd.WellTest.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                        var listEvents = new List<CreateWellLossDTO>();
                        foreach (var a in filtredEvents)
                            totalInterval += CalcInterval(a, production);

                        var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProd.WellTest.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasUEP);
                        var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProd.WellTest.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilUEP);
                        var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((wellProd.WellTest.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterUEP);
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((wellProd.WellTest.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasField);
                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((wellProd.WellTest.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilField);
                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((wellProd.WellTest.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterField);



                        var calcBSWOil = (100 - btpValid.BSW) / 100;
                        var calcBSWWater = btpValid.BSW / 100;
                        var productionGas = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0));
                        var productionOIl = production.Oil.TotalOil * wellPotencialOilAsPercentageOfUEP;
                        var productionWater = (productionOIl * calcBSWWater) / calcBSWOil;

                        //int hours = (int)totalInterval;
                        //double minutesDecimal = (totalInterval - hours) * 60;
                        //int minutes = (int)minutesDecimal;
                        //double secondsDecimal = (minutesDecimal - minutes) * 60;
                        //int seconds = (int)secondsDecimal;
                        //DateTime dateTime = DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
                        //string formattedTime = dateTime.ToString("HH:mm:ss");

                        wellProd.ProductionGasAsPercentageOfInstallation = wellPotencialGasAsPercentageOfUEP;
                        wellProd.ProductionOilAsPercentageOfInstallation = wellPotencialOilAsPercentageOfUEP;
                        wellProd.ProductionWaterAsPercentageOfInstallation = wellPotencialWaterAsPercentageOfUEP;
                        wellProd.ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField;
                        wellProd.ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField;
                        wellProd.ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField;
                        wellProd.ProductionOilInWellM3 = productionOIl;
                        wellProd.ProductionOilInWellBBL = productionOIl * ProductionUtils.m3ToBBLConversionMultiplier;

                        wellProd.ProductionGasInWellM3 = productionGas;
                        wellProd.ProductionGasInWellSCF = productionGas * ProductionUtils.m3ToSCFConversionMultipler;

                        wellProd.ProductionWaterInWellM3 = productionWater;
                        wellProd.ProductionWaterInWellBBL = productionWater * ProductionUtils.m3ToBBLConversionMultiplier;
                        //wellProd.Downtime = formattedTime;

                        foreach (var wellLoss in wellProd.WellLosses)
                        {

                            wellLoss.ProductionLostOil = ((wellProd.WellTest.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * wellLoss.Downtime / 24;
                            wellLoss.ProductionLostGas = ((wellProd.WellTest.PotencialGas / totalPotencialGasUEP) * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0))) * wellLoss.Downtime / 24;
                            wellLoss.ProductionLostWater = (((wellProd.WellTest.PotencialOil / totalPotencialOilUEP) * production.Oil.TotalOil) * wellLoss.Downtime / 24) * calcBSWWater / calcBSWOil;

                            wellLoss.ProportionalDay = wellLoss.ProductionLostOil / totalOilPotencialField;
                            wellLoss.EfficienceLoss = wellLoss.ProportionalDay / daysInMonth;

                            _repository.UpdateWellLost(wellLoss);
                        }


                        wellProd.ProductionLostGas = wellProd.WellLosses.Sum(x => x.ProductionLostGas);
                        wellProd.ProductionLostOil = wellProd.WellLosses.Sum(x => x.ProductionLostOil);
                        wellProd.ProductionLostWater = wellProd.WellLosses.Sum(x => x.ProductionLostWater);

                        wellProd.EfficienceLoss = wellProd.WellLosses.Sum(x => x.EfficienceLoss);
                        wellProd.ProportionalDay = wellProd.WellLosses.Sum(x => x.ProportionalDay);

                        _repository.Update(wellProd);

                        totalWater += wellProd.ProductionWaterInWellM3;
                        totalGas += wellProd.ProductionGasInWellM3;
                        totalOil += wellProd.ProductionOilInWellM3;
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

                                completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWellM3;
                                completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWellM3;
                                completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWellM3;

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
        private double CalcInterval(WellEvent a, Production production)
        {
            if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
            {
                return ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
            }
            else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
            {
                return 24;
            }
            else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
            {
                return ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
            }
            else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
            {
                return ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
            }
            else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
            {
                return 24;
            }
            else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
            {
                return ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
            }
            return 0;
        }
        private IEnumerable<WellTests> FilterBtp(List<WellTests> wellTests, Production production)
        {
            return wellTests.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date) && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR"
                        || x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));
        }
    }
}