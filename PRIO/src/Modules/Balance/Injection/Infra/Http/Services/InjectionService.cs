using PRIO.src.Modules.Balance.Injection.Interfaces;
using PRIO.src.Modules.Balance.Injection.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Balance.Injection.Infra.Http.Services
{
    public class InjectionService
    {
        private readonly IInjectionRepository _repository;
        private readonly IPIRepository _PIrepository;
        public InjectionService(IInjectionRepository repository, IPIRepository pIRepository)
        {
            _repository = repository;
            _PIrepository = pIRepository;
        }

        public async Task UpdateWaterInjection(UpdateWaterInjectionViewModel body, User loggedUser)
        {

            foreach (var injection in body.AssignedValues)
            {
                var injectionInDatabase = await _repository.GetWaterInjectionById(injection.InjectionId)
                    ?? throw new NotFoundException("Dados do PI não encontrados");

            }

        }
    }
}
