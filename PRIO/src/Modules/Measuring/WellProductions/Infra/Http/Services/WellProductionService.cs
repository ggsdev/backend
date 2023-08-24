﻿using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
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
        private readonly IMapper _mapper;

        public WellProductionService(IWellProductionRepository repository, IMapper mapper, IProductionRepository productionRepository, IInstallationRepository installationRepository, IBTPRepository bTPRepository, IWellRepository wellRepository)
        {
            _repository = repository;
            _wellRepository = wellRepository;
            _mapper = mapper;
            _productionRepository = productionRepository;
            _installationRepository = installationRepository;
            _btpRepository = bTPRepository;
        }

        public async Task CreateAppropriation(Guid productionId)
        {
            var production = await _productionRepository
                .GetById(productionId);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.WellProductions is not null && production.WellProductions.Count > 0)
                throw new ConflictException("Apropriação já foi feita");

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
                        .Where(x => (x.FinalApplicationDate == null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
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
                    foreach (var btp in filtredByApplyDateAndFinal)
                    {
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialGas, totalGasPotencial);

                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);

                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialWater, totalWaterPotencial);

                        var wellAppropriation = new WellProduction
                        {
                            Id = Guid.NewGuid(),

                            BtpData = btp,

                            Production = production,

                            ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField,
                            ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField,
                            ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField,

                            WellId = btp.Well.Id,

                            FieldProduction = fieldProduction,

                            ProductionOilAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialOilAsPercentageOfField, fieldFR.FROil.Value, btp.BSW, WellProductionUtils.fluidOil) : 0,

                            ProductionGasAsPercentageOfInstallation = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialGasAsPercentageOfField, fieldFR.FRGas.Value, btp.BSW, WellProductionUtils.fluidGas) : 0,

                            ProductionWaterAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialWaterAsPercentageOfField, fieldFR.FROil.Value, btp.BSW, WellProductionUtils.fluidWater) : 0,


                            ProductionGasInWell = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, btp.BSW, wellPotencialGasAsPercentageOfField, WellProductionUtils.fluidGas) : 0,

                            ProductionOilInWell = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, btp.BSW, wellPotencialOilAsPercentageOfField, WellProductionUtils.fluidOil) : 0,

                            ProductionWaterInWell = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, btp.BSW, wellPotencialWaterAsPercentageOfField, WellProductionUtils.fluidWater) : 0,

                        };

                        totalWater += wellAppropriation.ProductionWaterInWell;
                        totalOil += wellAppropriation.ProductionOilInWell;
                        totalGas += wellAppropriation.ProductionGasInWell;

                        await _repository.AddAsync(wellAppropriation);
                    }
                    if (fieldProduction is not null)
                    {
                        fieldProduction.WaterProductionInField = totalWater;
                        fieldProduction.GasProductionInField = totalGas;
                        fieldProduction.OilProductionInField = totalOil;

                        await _productionRepository.AddFieldProduction(fieldProduction);

                    }
                    await _repository.Save();


                }
                var listProductions = await _repository.getAllFieldsProductionsByProductionId(production.Id);

                foreach (var field in listProductions)
                {
                    foreach (var wellProduction in field.WellProductions)
                    {
                        var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                        foreach (var completion in wellInDatabase.Completions)
                        {
                            var reservoirProduction = await _repository.GetReservoirProductionForWellAndReservoir(production.Id, completion.Reservoir.Id);
                            var zoneProduction = await _repository.GetZoneProductionForWellAndReservoir(production.Id, completion.Reservoir.Zone.Id);

                            var allocationReservoir = completion.AllocationReservoir.Value;

                            var completionProduction = new CompletionProduction
                            {
                                Id = Guid.NewGuid(),
                                GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWell,
                                OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWell,
                                WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWell,
                                ProductionId = production.Id,
                                CompletionId = completion.Id,
                                WellProduction = wellProduction,
                            };

                            if (reservoirProduction is null)
                            {
                                var createReservoirProduction = new ReservoirProduction
                                {
                                    Id = Guid.NewGuid(),
                                    ReservoirId = completion.Reservoir.Id,
                                    ProductionId = production.Id,
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
                                        ProductionId = production.Id,
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
            else
            {

                var uep = await _installationRepository.GetByIdAsync(production.Installation.Id);
                var btpsUEP = await _btpRepository
                    .GetBtpDatasByUEP(uep.UepCod);

                var filtredByApplyDateAndFinal = btpsUEP
                        .Where(x => (x.FinalApplicationDate == null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                var totalGasPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialGas);

                var totalOilPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialOil);

                var totalWaterPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialWater);

                var totalLiquidPotencial = filtredByApplyDateAndFinal
                    .Sum(x => x.PotencialLiquid);

                var totalGas = 0m;
                var totalOil = 0m;
                var totalWater = 0m;

                FieldProduction? fieldProduction = filtredByApplyDateAndFinal.Count() > 0 ? new()
                {
                    Id = Guid.NewGuid(),
                    ProductionId = production.Id,
                } : null;

                foreach (var btp in filtredByApplyDateAndFinal)
                {

                    var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialGas, totalGasPotencial);
                    var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);
                    var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialWater, totalWaterPotencial);
                    var wellPotencialLiquidAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialLiquid, totalLiquidPotencial);
                    var btpsField = await _btpRepository
                        .GetBtpDatasByFieldId(btp.Well.Field.Id);

                    var filtredsBTPsField = btpsField
                        .Where(x => (x.FinalApplicationDate == null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                        || (x.FinalApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                        && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                    var totalGasPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialGas);
                    var totalOilPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialOil);
                    var totalWaterPotencialField = filtredsBTPsField
                        .Sum(x => x.PotencialWater);

                    var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialGas, totalGasPotencialField);
                    var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencialField);
                    var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialWater, totalWaterPotencialField);

                    var calcBSWOil = (100 - btp.BSW) / 100;
                    var calcBSWWater = btp.BSW / 100;
                    var wellAppropriation = new WellProduction
                    {
                        //CONSIDERO CERTO
                        Id = Guid.NewGuid(),
                        BtpData = btp,
                        Production = production,
                        ProductionGasAsPercentageOfInstallation = wellPotencialGasAsPercentageOfUEP,
                        ProductionOilAsPercentageOfInstallation = wellPotencialOilAsPercentageOfUEP,
                        ProductionWaterAsPercentageOfInstallation = wellPotencialWaterAsPercentageOfUEP,
                        WellId = btp.Well.Id,
                        FieldProduction = fieldProduction,
                        ProductionGasInWell = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0)),

                        // NÃO AVALIADO
                        ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField,
                        ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField,
                        ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField,

                        ProductionOilInWell = production.Oil.TotalOil * wellPotencialLiquidAsPercentageOfUEP * calcBSWOil
                        ,
                        ProductionWaterInWell = production.Oil.TotalOil * wellPotencialLiquidAsPercentageOfUEP * calcBSWWater
                        ,
                    };


                    totalWater += wellAppropriation.ProductionWaterInWell;
                    totalOil += wellAppropriation.ProductionOilInWell;
                    totalGas += wellAppropriation.ProductionGasInWell;

                    await _repository.AddAsync(wellAppropriation);
                }

                if (fieldProduction is not null)
                {
                    fieldProduction.WaterProductionInField = totalWater;
                    fieldProduction.GasProductionInField = totalGas;
                    fieldProduction.OilProductionInField = totalOil;

                    await _productionRepository.AddFieldProduction(fieldProduction);
                }


                await _repository.Save();
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

            if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
                    var fieldProductionInDatabase = await _productionRepository
                            .GetFieldProductionByFieldAndProductionId(fieldFR.Field.Id, productionId);

                    if (fieldProductionInDatabase is null)
                        throw new NotFoundException("Produção de campo não distribuida");

                    var totalWater = fieldProductionInDatabase.WaterProductionInField;
                    var totalOil = fieldProductionInDatabase.OilProductionInField;
                    var totalGas = fieldProductionInDatabase.GasProductionInField;

                    var wellProductionsInDatabase = await _repository.GetByProductionId(productionId);

                    var totalGasPotencial = wellProductionsInDatabase
                        .Sum(x => x.BtpData.PotencialGas);

                    var totalOilPotencial = wellProductionsInDatabase
                        .Sum(x => x.BtpData.PotencialOil);

                    var totalWaterPotencial = wellProductionsInDatabase
                        .Sum(x => x.BtpData.PotencialWater);

                    foreach (var wellProduction in wellProductionsInDatabase)
                    {
                        var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.BtpData.PotencialGas, totalGasPotencial);

                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.BtpData.PotencialOil, totalOilPotencial);

                        var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(wellProduction.BtpData.PotencialWater, totalWaterPotencial);

                        wellProduction.ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField;
                        wellProduction.ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField;
                        wellProduction.ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField;

                        wellProduction.ProductionGasAsPercentageOfInstallation = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialGasAsPercentageOfField, fieldFR.FRGas.Value, wellProduction.BtpData.BSW, WellProductionUtils.fluidGas) : 0;

                        wellProduction.ProductionOilAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialOilAsPercentageOfField, fieldFR.FROil.Value, wellProduction.BtpData.BSW, WellProductionUtils.fluidOil) : 0;

                        wellProduction.ProductionWaterAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialWaterAsPercentageOfField, fieldFR.FROil.Value, wellProduction.BtpData.BSW, WellProductionUtils.fluidWater) : 0;


                        wellProduction.ProductionGasInWell = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellProduction.BtpData.BSW, wellPotencialGasAsPercentageOfField, WellProductionUtils.fluidGas) : 0;

                        wellProduction.ProductionOilInWell = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellProduction.BtpData.BSW, wellPotencialOilAsPercentageOfField, WellProductionUtils.fluidOil) : 0;

                        wellProduction.ProductionWaterInWell = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellProduction.BtpData.BSW, wellPotencialWaterAsPercentageOfField, WellProductionUtils.fluidWater) : 0;

                        totalWater += wellProduction.ProductionWaterInWell;
                        totalOil += wellProduction.ProductionOilInWell;
                        totalGas += wellProduction.ProductionGasInWell;

                        _repository.Update(wellProduction);
                    }

                    Console.WriteLine(fieldFR.Field.Name);
                    Console.WriteLine(totalWater);
                    Console.WriteLine(totalGas);
                    Console.WriteLine(totalOil);

                    if (fieldProductionInDatabase is not null)
                    {
                        fieldProductionInDatabase.WaterProductionInField = totalWater;
                        fieldProductionInDatabase.GasProductionInField = totalGas;
                        fieldProductionInDatabase.OilProductionInField = totalOil;

                        _productionRepository.UpdateFieldProduction(fieldProductionInDatabase);
                    }
                }
            }
        }
    }
}