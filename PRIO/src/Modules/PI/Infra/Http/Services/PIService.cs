using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.PI.Interfaces;

namespace PRIO.src.Modules.PI.Infra.Http.Services
{
    public class PIService
    {
        IPIRepository _repository;
        IInstallationRepository _installationRepository;

        public PIService(IPIRepository repository, IInstallationRepository installationRepository)
        {
            _repository = repository;
            _installationRepository = installationRepository;
        }

        public async Task GetDataByUep(Guid id)
        {
            var uep = await _installationRepository.GetInstallationsByUepIdWithTagsPi(id);

        }

        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }
    }
}
