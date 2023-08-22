using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellAppropriations.Infra.Utils;
using PRIO.src.Modules.Measuring.WellAppropriations.Infra.ViewModels;
using PRIO.src.Modules.Measuring.WellAppropriations.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Infra.Http.Services
{
    public class WellAppropriationService
    {
        private readonly IWellAppropriationRepository _repository;
        private readonly IProductionRepository _productionRepository;
        private readonly IBTPRepository _btpRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;

        public WellAppropriationService(IWellAppropriationRepository repository, IMapper mapper, IProductionRepository productionRepository, IInstallationRepository installationRepository, IBTPRepository bTPRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productionRepository = productionRepository;
            _installationRepository = installationRepository;
            _btpRepository = bTPRepository;
        }

        public async Task CreateAppropriation(WellAppropriationViewModel body)
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

            if (wellsInvalids.Count > 0)
                throw new BadRequestException($"Todos os poços devem ter um teste de poço válido. Poços sem teste ou com teste inválido:", errors: wellsInvalids);

            if (production.FieldsFR is not null)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
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

                    foreach (var btp in filtredByApplyDateAndFinal)
                    {
                        var wellPotencialGasAsPercentageOfField = AppropriationUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialGas, totalGasPotencial);

                        var wellPotencialOilAsPercentageOfField = AppropriationUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);

                        var wellPotencialWaterAsPercentageOfField = AppropriationUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialWater, totalWaterPotencial);

                        var wellAppropriation = new WellAppropriation
                        {
                            Id = Guid.NewGuid(),

                            BtpData = btp,

                            Production = production,

                            ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField,
                            ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField,
                            ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField,

                            ProductionOilAsPercentageOfInstallation = fieldFR.FROil is not null ? AppropriationUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialOilAsPercentageOfField, fieldFR.FROil.Value, btp.BSW, AppropriationUtils.fluidOil) : 0,

                            ProductionGasAsPercentageOfInstallation = fieldFR.FRGas is not null ? AppropriationUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialGasAsPercentageOfField, fieldFR.FRGas.Value, btp.BSW, AppropriationUtils.fluidGas) : 0,

                            ProductionWaterAsPercentageOfInstallation = fieldFR.FROil is not null ? AppropriationUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialWaterAsPercentageOfField, fieldFR.FROil.Value, btp.BSW, AppropriationUtils.fluidWater) : 0,


                            ProductionGasInWell = fieldFR.FRGas is not null ? AppropriationUtils.CalculateWellProduction(fieldFR.ProductionInField, fieldFR.FRGas.Value, btp.BSW, wellPotencialGasAsPercentageOfField, AppropriationUtils.fluidGas) : 0,

                            ProductionOilInWell = fieldFR.FROil is not null ? AppropriationUtils.CalculateWellProduction(fieldFR.ProductionInField, fieldFR.FROil.Value, btp.BSW, wellPotencialOilAsPercentageOfField, AppropriationUtils.fluidOil) : 0,

                            ProductionWaterInWell = fieldFR.FROil is not null ? AppropriationUtils.CalculateWellProduction(fieldFR.ProductionInField, fieldFR.FROil.Value, btp.BSW, wellPotencialWaterAsPercentageOfField, AppropriationUtils.fluidWater) : 0,

                        };

                        await _repository.AddAsync(wellAppropriation);
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
