﻿using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Utils;
using PRIO.src.Modules.Measuring.WellProductions.Infra.ViewModels;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services
{
    public class WellProductionService
    {
        private readonly IWellProductionRepository _repository;
        private readonly IProductionRepository _productionRepository;
        private readonly IBTPRepository _btpRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;

        public WellProductionService(IWellProductionRepository repository, IMapper mapper, IProductionRepository productionRepository, IInstallationRepository installationRepository, IBTPRepository bTPRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productionRepository = productionRepository;
            _installationRepository = installationRepository;
            _btpRepository = bTPRepository;
        }

        public async Task CreateAppropriation(WellProductionViewModel body)
        {
            var production = await _productionRepository
                .GetById(body.ProductionId);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.WellAppropriations is not null && production.WellAppropriations.Count > 0)
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

            if (production.FieldsFR is not null)
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


                            ProductionGasInWell = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, fieldFR.FRGas.Value, btp.BSW, wellPotencialGasAsPercentageOfField, WellProductionUtils.fluidGas) : 0,

                            ProductionOilInWell = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, fieldFR.FROil.Value, btp.BSW, wellPotencialOilAsPercentageOfField, WellProductionUtils.fluidOil) : 0,

                            ProductionWaterInWell = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, fieldFR.FROil.Value, btp.BSW, wellPotencialWaterAsPercentageOfField, WellProductionUtils.fluidWater) : 0,

                        };

                        foreach (var completion in btp.Well.Completions)
                        {
                            var allocationReservoir = completion.AllocationReservoir.Value;

                            var completionProduction = new CompletionProduction
                            {
                                Id = Guid.NewGuid(),
                                GasProductionInCompletion = allocationReservoir * wellAppropriation.ProductionGasInWell,
                                OilProductionInCompletion = allocationReservoir * wellAppropriation.ProductionOilInWell,
                                WaterProductionInCompletion = allocationReservoir * wellAppropriation.ProductionWaterInWell,
                                ProductionId = production.Id,
                                CompletionId = completion.Id,
                                WellProduction = wellAppropriation
                            };

                            //var reservoirProduction = wellAppropriation.ReservoirProductions.FirstOrDefault(rp => rp.ReservoirId == completion.Reservoir.Id);

                            //if (reservoirProduction == null)
                            //{
                            //    reservoirProduction = new ReservoirProduction
                            //    {
                            //        Id = Guid.NewGuid(),
                            //        ReservoirId = completion.Reservoir.Id,
                            //        ProductionId = production.Id,
                            //    };
                            //    wellAppropriation.ReservoirProductions.Add(reservoirProduction);
                            //}

                            //reservoirProduction.GasProductionInReservoir += allocationReservoir * wellAppropriation.ProductionGasInWell;
                            //reservoirProduction.OilProductionInReservoir += allocationReservoir * wellAppropriation.ProductionOilInWell;
                            //reservoirProduction.WaterProductionInReservoir += allocationReservoir * wellAppropriation.ProductionWaterInWell;
                        }

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
                }
            }

            else
            {


            }

            await _repository.Save();
        }
    }
}
