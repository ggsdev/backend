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

        public async Task<WaterInjectionUpdateDto> CreateDailyInjection(UpdateWaterInjectionViewModel body, DateTime dateInjection, User loggedUser)
        {
            //if is parametrized true

            if (body.FIRS < 0 || body.FIRS > 1)
                throw new BadRequestException("FIRS deve ser um valor entre 0 e 1");

            var field = await _fieldRepository.GetByIdAsync(body.FieldId)
                   ?? throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var fieldBalance = await _balanceRepository
                .GetBalanceField(body.FieldId!.Value, dateInjection.Date)
                ?? throw new BadRequestException("Balanço de campo não criado ainda, é necessário fechar a produção do dia.");

            var injectionInDatabase = await _repository
                .AnyByDate(dateInjection);

            if (injectionInDatabase)
                throw new ConflictException($"Injeção do dia: {dateInjection:dd/MMM/yyyy} já atribuída");

            var fieldInjection = new InjectionWaterGasField
            {
                Id = Guid.NewGuid(),
                MeasurementAt = dateInjection,
                BalanceField = fieldBalance,
                Field = field,
                Status = true,
                FIRS = body.FIRS!.Value
            };

            var resultDto = new WaterInjectionUpdateDto();
            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var injection in body.AssignedValues)
            {
                var waterInjectionInDatabase = await _repository.GetWaterInjectionById(injection.InjectionId)
                    ?? throw new NotFoundException("Dados de injeção de água não encontrados.");

                if (injection.InjectionId is not null && injection.AssignedValue is not null)
                {
                    waterInjectionInDatabase.AssignedValue = injection.AssignedValue.Value;
                    waterInjectionInDatabase.UpdatedBy = loggedUser;

                    resultDto.AssignedValues.Add(new WaterAssignatedValuesDto
                    {
                        AssignedValue = waterInjectionInDatabase.AssignedValue,
                        InjectionId = injection.InjectionId.Value,
                        UpdatedBy = updatedBy
                    });
                }

                _repository.UpdateWaterInjection(waterInjectionInDatabase);
            }

            var waterInjectionWells = await _repository
               .GetWaterWellInjectionsByDate(dateInjection);

            var gasInjectionWells = await _repository
                .GetGasWellInjectionsByDate(dateInjection);

            foreach (var waterInjection in waterInjectionWells)
            {
                waterInjection.InjectionWaterGasField = fieldInjection;
                fieldInjection.AmountWater += waterInjection.AssignedValue;
            }

            foreach (var gasInjection in gasInjectionWells)
            {
                gasInjection.InjectionWaterGasField = fieldInjection;
                fieldInjection.AmountGasLift += gasInjection.AssignedValue;
            }

            fieldBalance.TotalWaterInjectedRS = (decimal)(body.FIRS.Value * fieldInjection.AmountWater);
            fieldBalance.TotalWaterDisposal = (decimal)((1 - body.FIRS.Value) * fieldInjection.AmountWater);
            fieldBalance.TotalWaterInjected = (decimal)fieldInjection.AmountWater;

            await _repository.AddWaterGasInjection(fieldInjection);

            _balanceRepository.UpdateFieldBalance(fieldBalance);

            resultDto.TotalWaterInjected = fieldInjection.AmountWater;

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
                    FieldInjectionId = fieldInjection.Id,
                    Field = fieldInjection.Field.Name,
                    Installation = fieldInjection.Field.Installation.Name,
                    GasLift = Math.Round(fieldInjection.AmountGasLift, 5),
                    InjectedWater = Math.Round(fieldInjection.AmountWater, 5),
                    InjectionDate = fieldInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Status = fieldInjection.Status,
                    Uep = uep.Name,
                });

            }

            return result;

        }

        public async Task<DailyInjectionDto> GetInjectionByFieldInjectionId(Guid fieldInjectionId)
        {
            var fieldInjection = await _repository
                .GetWaterGasFieldInjectionsById(fieldInjectionId)
                ?? throw new NotFoundException("Injeção de campo não encontrada.");

            var result = new DailyInjectionDto
            {
                TotalGasLift = Math.Round(fieldInjection.AmountGasLift, 5),
                DateInjection = fieldInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                Installation = fieldInjection.Field.Installation.Name,
                Uep = fieldInjection.Field.Installation.UepName,
                Status = fieldInjection.Status,
                TotalWaterInjected = Math.Round(fieldInjection.AmountWater, 5),
            };

            var gasLiftDto = new GasLiftInjectedDto
            {
                FieldInjectionId = fieldInjection.Id,
                Field = fieldInjection.Field.Name,
            };

            var waterInjectedDto = new WaterInjectedDto
            {
                Field = fieldInjection.Field.Name,
                FieldInjectionId = fieldInjection.Id,
                FIRS = fieldInjection.FIRS,
            };

            foreach (var waterInjection in fieldInjection.WellsWaterInjections)
            {
                waterInjectedDto.Values.Add(new WellWaterInjectedDto
                {
                    WellInjectionId = waterInjection.Id,
                    DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Tag = waterInjection.WellValues.Value.Attribute.Name,
                    VolumeAssigned = Math.Round(waterInjection.AssignedValue, 5),
                    VolumePI = waterInjection.WellValues.Value.Amount is not null ? Math.Round(waterInjection.WellValues.Value.Amount.Value, 5) : null,
                    WellName = waterInjection.WellValues.Value.Attribute.WellName
                });
            }

            foreach (var gasInjection in fieldInjection.WellsGasInjections)
            {
                var parameterDto = gasLiftDto.Parameters
                    .FirstOrDefault(x => x.Parameter == gasInjection.WellValues.Value.Attribute.Element.Parameter);

                if (parameterDto is null)
                {
                    parameterDto = new ElementGasDto
                    {
                        Parameter = gasInjection.WellValues.Value.Attribute.Element.Parameter,
                    };

                    gasLiftDto.Parameters.Add(parameterDto);
                }

                var gasValuesDto = new GasValuesDto
                {
                    WellInjectionId = gasInjection.Id,
                    DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Tag = gasInjection.WellValues.Value.Attribute.Name,
                    Volume = Math.Round(gasInjection.AssignedValue, 5),
                    WellName = gasInjection.WellValues.Value.Attribute.WellName,
                };

                parameterDto.Values.Add(gasValuesDto);
            }

            result.WaterInjected = waterInjectedDto;
            result.GasLift = gasLiftDto;


            return result;
        }

        public async Task<DailyInjectionDto> GetDailyInjection(DateTime dateInjection)
        {
            var fieldInjection = await _repository
               .GetWaterGasFieldInjectionByDate(dateInjection)
               ?? throw new NotFoundException("Injeção de campo não encontrada.");

            var result = new DailyInjectionDto
            {
                TotalGasLift = Math.Round(fieldInjection.AmountGasLift, 5),
                DateInjection = fieldInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                Installation = fieldInjection.Field.Installation.Name,
                Uep = fieldInjection.Field.Installation.UepName,
                Status = fieldInjection.Status,
                TotalWaterInjected = Math.Round(fieldInjection.AmountWater, 5),
            };

            var gasLiftDto = new GasLiftInjectedDto
            {
                FieldInjectionId = fieldInjection.Id,
                Field = fieldInjection.Field.Name,
            };

            var waterInjectedDto = new WaterInjectedDto
            {
                Field = fieldInjection.Field.Name,
                FieldInjectionId = fieldInjection.Id,
                FIRS = fieldInjection.FIRS,
            };

            foreach (var waterInjection in fieldInjection.WellsWaterInjections)
            {
                waterInjectedDto.Values.Add(new WellWaterInjectedDto
                {
                    WellInjectionId = waterInjection.Id,
                    DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Tag = waterInjection.WellValues.Value.Attribute.Name,
                    VolumeAssigned = Math.Round(waterInjection.AssignedValue, 5),
                    VolumePI = waterInjection.WellValues.Value.Amount is not null ? Math.Round(waterInjection.WellValues.Value.Amount.Value, 5) : null,
                    WellName = waterInjection.WellValues.Value.Attribute.WellName
                });
            }

            foreach (var gasInjection in fieldInjection.WellsGasInjections)
            {
                var parameterDto = gasLiftDto.Parameters
                    .FirstOrDefault(x => x.Parameter == gasInjection.WellValues.Value.Attribute.Element.Parameter);

                if (parameterDto is null)
                {
                    parameterDto = new ElementGasDto
                    {
                        Parameter = gasInjection.WellValues.Value.Attribute.Element.Parameter,
                    };
                    gasLiftDto.Parameters.Add(parameterDto);
                }

                var gasValuesDto = new GasValuesDto
                {
                    WellInjectionId = gasInjection.Id,
                    DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Tag = gasInjection.WellValues.Value.Attribute.Name,
                    Volume = Math.Round(gasInjection.AssignedValue, 5),
                    WellName = gasInjection.WellValues.Value.Attribute.WellName,
                };

                parameterDto.Values.Add(gasValuesDto);
            }

            result.WaterInjected = waterInjectedDto;
            result.GasLift = gasLiftDto;


            return result;
        }
    }
}
