using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
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

            //validando se todos poços tem um teste válido
            foreach (var installation in installations)
            {
                foreach (var field in installation.Fields)
                {
                    foreach (var well in field.Wells)
                    {
                        var wellContainBtpValid = false;

                        if (well.BTPDatas is not null)
                            foreach (var btp in well.BTPDatas.OrderByDescending(x => x.ApplicationDate))
                                if (btp.IsActive)
                                {
                                    wellContainBtpValid = true;
                                    break;
                                }


                        //if (wellContainBtpValid is false)
                        //    throw new ConflictException($"Todos os poços devem ter um teste de poço válido, poço: {well.Name}");
                    }
                }
            }

            if (production.FieldsFR is not null)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
                    var totalGasPotencial = await _btpRepository.SumFluidTotalPotencialByFieldId(fieldFR.Field.Id, AppropriationUtils.fluidGas);

                    var totalOilPotencial = await _btpRepository.SumFluidTotalPotencialByFieldId(fieldFR.Field.Id, AppropriationUtils.fluidOil);

                    var totalWaterPotencial = await _btpRepository.SumFluidTotalPotencialByFieldId(fieldFR.Field.Id, AppropriationUtils.fluidWater);

                    var btps = await _btpRepository.GetBtpDatasByFieldId(fieldFR.Field.Id);

                    foreach (var btp in btps)
                    {
                        Console.WriteLine(btp.PotencialLiquid);

                    }

                }
            }
            else
            {


            }


            //    var totalPotencialOilAllWells = 0m;
            //    var totalPotencialGasAllWells = 0m;
            //    var totalPotencialWaterAllWells = 0m;
            //}

        }

    }
}
