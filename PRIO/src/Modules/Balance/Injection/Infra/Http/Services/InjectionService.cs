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
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Balance.Injection.Infra.Http.Services
{
    public class InjectionService
    {
        private readonly IInjectionRepository _repository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMapper _mapper;

        public InjectionService(IInjectionRepository repository, IFieldRepository fieldRepository, IBalanceRepository balanceRepository, IMapper mapper, IInstallationRepository installationRepository)
        {
            _repository = repository;
            _fieldRepository = fieldRepository;
            _balanceRepository = balanceRepository;
            _mapper = mapper;
            _installationRepository = installationRepository;
        }

        public async Task<WaterInjectionUpdateDto> UpdateWaterInjection(UpdateWaterInjectionViewModel body, User loggedUser)
        {
            if (body.FIRS < 0 || body.FIRS > 1)
                throw new BadRequestException("FIRS deve ser um valor entre 0 e 1");

            var field = await _fieldRepository.GetByIdAsync(body.FieldId)
                   ?? throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var fieldBalance = await _balanceRepository
                .GetBalanceField(body.FieldId, body.DateInjection.Date)
                ?? throw new BadRequestException("Balanço de campo não criado ainda, é necessário fechar a produção do dia.");

            var fieldInjection = new InjectionWaterGasField
            {
                Id = Guid.NewGuid(),
                MeasurementAt = body.DateInjection,
                BalanceField = fieldBalance,
                Field = field,
            };

            var resultDto = new WaterInjectionUpdateDto();
            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var waterInjection in body.AssignedValues)
            {
                var waterInjectionInDatabase = await _repository.GetWaterInjectionById(waterInjection.InjectionId)
                    ?? throw new NotFoundException("Dados de injeção de água não encontrados.");

                var gasInjectionInDatabase = await _repository.GetGasInjectionById(waterInjection.InjectionId)
                    ?? throw new NotFoundException("Dados de gas lift não encontrados.");

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

                fieldInjection.AmountWater += waterInjectionInDatabase.AssignedValue;
                waterInjectionInDatabase.InjectionWaterGasField = fieldInjection;

                _repository.UpdateWaterInjection(waterInjectionInDatabase);
            }

            fieldBalance.IsParameterized = true;
            fieldBalance.TotalWaterInjectedRS = (decimal)(body.FIRS * fieldInjection.AmountWater);
            fieldBalance.TotalWaterDisposal = (decimal)((1 - body.FIRS) * fieldInjection.AmountWater);

            _balanceRepository.UpdateFieldBalance(fieldBalance);

            resultDto.Total = fieldInjection.AmountWater;

            await _repository.Save();

            return resultDto;
        }


        public async Task<List<InjectionDto>> GetInjectionByInstallationId(Guid installationId)
        {
            var installation = await _installationRepository
                .GetByIdAsync(installationId)
                ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var fieldInjections = await _repository
                .GetInjectionsByInstallationId(installationId);

            var uep = await _installationRepository.GetByUEPCod(installation.UepCod);

            var result = new List<InjectionDto>();

            foreach (var fieldInjection in fieldInjections)
            {
                result.Add(new InjectionDto
                {
                    InjectionId = fieldInjection.Id,
                    Field = fieldInjection.Field.Name,
                    Installation = fieldInjection.Field.Installation.Name,
                    GasLift = fieldInjection.AmountGasLift,
                    InjectedWater = fieldInjection.AmountWater,
                    InjectionDate = fieldInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Status = fieldInjection.BalanceField.IsParameterized,
                    Uep = uep.Name,
                });

            }

            return result;

        }
    }
}
