using AutoMapper;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Modules.Balance.Injection.Dtos;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Balance.Injection.Interfaces;
using PRIO.src.Modules.Balance.Injection.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;

namespace PRIO.src.Modules.Balance.Injection.Infra.Http.Services
{
    public class InjectionService
    {
        private readonly IInjectionRepository _repository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMapper _mapper;
        private readonly IPIRepository _PIrepository;
        private readonly SystemHistoryService _systemHistoryService;
        public InjectionService(IInjectionRepository repository, IPIRepository pIRepository, SystemHistoryService systemHistoryService, IFieldRepository fieldRepository, IBalanceRepository balanceRepository, IMapper mapper, IInstallationRepository installationRepository)
        {
            _repository = repository;
            _PIrepository = pIRepository;
            _systemHistoryService = systemHistoryService;
            _fieldRepository = fieldRepository;
            _balanceRepository = balanceRepository;
            _mapper = mapper;
            _installationRepository = installationRepository;
        }

        public async Task<WaterInjectionUpdateDto> UpdateWaterInjection(UpdateWaterInjectionViewModel body, User loggedUser)
        {
            if (body.FIRS < 0 || body.FIRS > 1)
                throw new BadRequestException("FIRS deve ser um valor entre 0 e 1");

            _ = await _fieldRepository.GetByIdAsync(body.FieldId)
                   ?? throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var fieldBalance = await _balanceRepository
                .GetBalanceField(body.FieldId, body.DateInjection.Date)
                ?? throw new BadRequestException("Balanço de campo não criado ainda, é necessário fechar a produção do dia.");

            var fieldInjection = new InjectionWaterGasField
            {
                Id = Guid.NewGuid(),
                MeasurementAt = body.DateInjection,
                BalanceField = fieldBalance,
            };

            var resultDto = new WaterInjectionUpdateDto();
            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var waterInjection in body.AssignedValues)
            {
                var waterInjectionInDatabase = await _repository.GetWaterInjectionById(waterInjection.InjectionId)
                    ?? throw new NotFoundException("Dados do PI não encontrados");

                if (waterInjection.InjectionId is not null && waterInjection.AssignedValue is not null)
                {
                    waterInjectionInDatabase.AssignedValue = waterInjection.AssignedValue.Value;
                    waterInjectionInDatabase.UpdatedBy = loggedUser;

                    resultDto.AssignedValues.Add(new WaterAssignatedValuesDto
                    {
                        AssignedValue = waterInjectionInDatabase.AssignedValue,
                        InjectionId = waterInjection.InjectionId.Value,
                        UpdatedBy = updatedBy
                    });
                }

                fieldInjection.Amount += waterInjectionInDatabase.AssignedValue;
                waterInjectionInDatabase.InjectionWaterGasField = fieldInjection;

                _repository.UpdateWaterInjection(waterInjectionInDatabase);
            }

            fieldBalance.IsParameterized = true;
            fieldBalance.TOtalWaterInjectedRS = (decimal)(body.FIRS * fieldInjection.Amount);
            fieldBalance.TotalWaterDisposal = (decimal)((1 - body.FIRS) * fieldInjection.Amount);

            _balanceRepository.UpdateFieldBalance(fieldBalance);

            resultDto.Total = fieldInjection.Amount;

            await _repository.Save();

            return resultDto;
        }


        public async Task<List<InjectionDto>> GetInjectionByInstallationId(Guid installationId)
        {
            _ = await _installationRepository
                .GetByIdAsync(installationId)
                ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var waterWellInjections = await _repository
                .GetWaterInjectionsByInstallationId(installationId);

            var result = new List<InjectionDto>();

            foreach (var waterWell in waterWellInjections)
            {
                result.Add(new InjectionDto
                {
                    InjectionId = waterWell.Id,

                });

            }

            return result;

        }
    }
}
