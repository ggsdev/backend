using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellAppropriations.Infra.ViewModels;
using PRIO.src.Modules.Measuring.WellAppropriations.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Infra.Http.Services
{
    public class WellAppropriationService
    {
        private readonly IWellAppropriationRepository _repository;
        private readonly IProductionRepository _productionRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;

        public WellAppropriationService(IWellAppropriationRepository repository, IMapper mapper, IProductionRepository productionRepository, IInstallationRepository installationRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productionRepository = productionRepository;
            _installationRepository = installationRepository;
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
                        BTPData validBtp;

                        if (well.BTPDatas is not null)
                            foreach (var btp in well.BTPDatas.OrderByDescending(x => x.ApplicationDate))
                                if (btp.IsActive)
                                {
                                    wellContainBtpValid = true;
                                    validBtp = btp;
                                    break;
                                }


                        //if (wellContainBtpValid is false)
                        //    throw new ConflictException($"Todos os poços devem ter um teste de poço válido, poço: {well.Name}");
                    }

                    var totalPotencialOilAllWells = 0m;
                    var totalPotencialGasAllWells = 0m;
                    var totalPotencialWaterAllWells = 0m;

                    foreach (var well in field.Wells)
                    {
                        foreach (var btp in well.BTPDatas)
                        {
                            totalPotencialOilAllWells += btp.PotencialOil;
                            totalPotencialGasAllWells += btp.PotencialGas;
                            totalPotencialWaterAllWells += btp.PotencialWater;
                        }
                    }

                    Console.WriteLine(totalPotencialOilAllWells);
                }
            }

            if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            {
                var totalProduction = 0m;

                foreach (var fieldFr in production.FieldsFR)
                {
                    totalProduction += fieldFr.ProductionInField;
                }
            }
        }
    }
}
