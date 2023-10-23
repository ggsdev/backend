using AutoMapper;
using dotenv.net;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Modules.Balance.Injection.Dtos;
using PRIO.src.Modules.Balance.Injection.Dtos.PRIO.src.Modules.Balance.Injection.Dtos;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Balance.Injection.Interfaces;
using PRIO.src.Modules.Balance.Injection.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Balance.Injection.Infra.Http.Services
{
    public class InjectionService
    {
        private readonly IInjectionRepository _repository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMapper _mapper;

        public InjectionService(IInjectionRepository repository, IFieldRepository fieldRepository, IBalanceRepository balanceRepository, IMapper mapper, IInstallationRepository installationRepository, IWellRepository wellRepository, IProductionRepository productionRepository)
        {
            _repository = repository;
            _fieldRepository = fieldRepository;
            _balanceRepository = balanceRepository;
            _mapper = mapper;
            _installationRepository = installationRepository;
            _wellRepository = wellRepository;
            _productionRepository = productionRepository;
        }

        public async Task<WaterInjectionUpdateDto> CreateDailyInjection(CreateDailyInjectionViewModel body, DateTime dateInjection, User loggedUser)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

            if (body.FIRS < 0 || body.FIRS > 1)
                throw new BadRequestException("FIRS deve ser um valor entre 0 e 1");

            var field = await _fieldRepository.GetByIdAsync(body.FieldId)
                   ?? throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var fieldBalance = await _balanceRepository
                .GetBalanceField(body.FieldId!.Value, dateInjection.Date)
                ?? throw new BadRequestException("Balanço de campo não criado ainda, é necessário fechar a produção do dia.");

            if (fieldBalance.IsParameterized is false)
                throw new ConflictException("Dados operacionais precisam ser confirmados.");

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
                FIRS = body.FIRS!.Value,
            };

            var resultDto = new WaterInjectionUpdateDto
            {
                FieldInjectionId = fieldInjection.Id
            };

            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var injection in body.AssignedValues)
            {
                var waterInjectionInDatabase = await _repository.GetWaterInjectionById(injection.WellInjectionId)
                    ?? throw new NotFoundException("Dados de injeção de água não encontrados.");

                if (injection.WellInjectionId is not null && injection.AssignedValue is not null)
                {
                    waterInjectionInDatabase.AssignedValue = injection.AssignedValue.Value;
                    waterInjectionInDatabase.UpdatedBy = loggedUser;

                    resultDto.AssignedValues.Add(new WaterAssignatedValuesDto
                    {
                        AssignedValue = waterInjectionInDatabase.AssignedValue,
                        InjectionId = injection.WellInjectionId.Value,
                        UpdatedBy = updatedBy
                    });
                }

                _repository.UpdateWaterInjection(waterInjectionInDatabase);
            }

            var waterInjectionWells = await _repository
               .GetWaterWellInjectionsByDate(dateInjection, field.Id);

            var gasInjectionWells = await _repository
                .GetGasWellInjectionsByDate(dateInjection, field.Id);

            if (instance == PIConfig._valenteInstance)
            {
                foreach (var waterInjection in waterInjectionWells)
                {
                    if (waterInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._wfl1)
                    {
                        waterInjection.InjectionWaterGasField = fieldInjection;
                        fieldInjection.AmountWater += waterInjection.AssignedValue;
                    }
                }

                foreach (var gasInjection in gasInjectionWells)
                {
                    if (gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl1 || gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl6 || gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl4)
                    {
                        gasInjection.InjectionWaterGasField = fieldInjection;
                        fieldInjection.AmountGasLift += gasInjection.AssignedValue;
                    }
                }

            }

            if (instance == PIConfig._forteInstance)
            {
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
            }

            fieldBalance.TotalWaterInjectedRS = (decimal)(body.FIRS.Value * fieldInjection.AmountWater);
            fieldBalance.TotalWaterDisposal = (decimal)((1 - body.FIRS.Value) * fieldInjection.AmountWater);
            fieldBalance.TotalWaterInjected = (decimal)fieldInjection.AmountWater;
            fieldBalance.FIRS = body.FIRS;

            await _repository.AddWaterGasInjection(fieldInjection);

            DistributeToParentBalances(fieldBalance);

            _balanceRepository.UpdateFieldBalance(fieldBalance);

            resultDto.TotalWaterInjected = fieldInjection.AmountWater;

            await _repository.Save();
            return resultDto;
        }

        private static void DistributeToParentBalances(FieldsBalance fieldBalance)
        {
            fieldBalance.InstallationBalance.DischargedSurface += fieldBalance.DischargedSurface;
            fieldBalance.InstallationBalance.TotalWaterCaptured += fieldBalance.TotalWaterCaptured;
            fieldBalance.InstallationBalance.TotalWaterDisposal += fieldBalance.TotalWaterDisposal;
            fieldBalance.InstallationBalance.TotalWaterTransferred += fieldBalance.TotalWaterTransferred;
            fieldBalance.InstallationBalance.TotalWaterInjected += fieldBalance.TotalWaterInjected;
            fieldBalance.InstallationBalance.TotalWaterInjectedRS += fieldBalance.TotalWaterInjectedRS;
            fieldBalance.InstallationBalance.TotalWaterReceived += fieldBalance.TotalWaterReceived;

            fieldBalance.InstallationBalance.UEPBalance.DischargedSurface += fieldBalance.DischargedSurface;
            fieldBalance.InstallationBalance.UEPBalance.TotalWaterCaptured += fieldBalance.TotalWaterCaptured;
            fieldBalance.InstallationBalance.UEPBalance.TotalWaterDisposal += fieldBalance.TotalWaterDisposal;
            fieldBalance.InstallationBalance.UEPBalance.TotalWaterTransferred += fieldBalance.TotalWaterTransferred;
            fieldBalance.InstallationBalance.UEPBalance.TotalWaterInjected += fieldBalance.TotalWaterInjected;
            fieldBalance.InstallationBalance.UEPBalance.TotalWaterInjectedRS += fieldBalance.TotalWaterInjectedRS;
            fieldBalance.InstallationBalance.UEPBalance.TotalWaterReceived += fieldBalance.TotalWaterReceived;
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

        public async Task<FieldInjectionDto> GetInjectionByFieldInjectionId(Guid fieldInjectionId)
        {
            var fieldInjection = await _repository
                .GetWaterGasFieldInjectionsById(fieldInjectionId)
                ?? throw new NotFoundException("Injeção de campo não encontrada.");

            var result = new FieldInjectionDto
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
                Field = fieldInjection.Field.Name!,
            };

            var waterInjectedDto = new WaterInjectedDto
            {
                Field = fieldInjection.Field.Name!,
                FIRS = fieldInjection.FIRS
            };

            foreach (var waterInjection in fieldInjection.WellsWaterInjections)
            {
                var parameterDto = waterInjectedDto.Parameters
                       .FirstOrDefault(x => x.Parameter == waterInjection.WellValues.Value.Attribute.Element.Parameter);

                if (parameterDto is null)
                {
                    parameterDto = new ElementWaterDto
                    {
                        Parameter = waterInjection.WellValues.Value.Attribute.Element.Parameter,
                    };

                    waterInjectedDto.Parameters.Add(parameterDto);
                }

                parameterDto.Values.Add(new WellValuesDto
                {
                    WellInjectionId = waterInjection.Id,
                    DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Tag = waterInjection.WellValues.Value.Attribute.Name,
                    VolumeAssigned = Math.Round(waterInjection.AssignedValue, 5),
                    VolumePI = waterInjection.WellValues.Value.Amount is not null ? Math.Round(waterInjection.WellValues.Value.Amount.Value, 5) : null,
                    WellName = waterInjection.WellValues.Well.Name!,
                    GroupAmount = waterInjection.WellValues.Value.GroupAmount
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

                    if (parameterDto.Parameter == PIConfig._gfl1 || parameterDto.Parameter == PIConfig._gfl4 || parameterDto.Parameter == PIConfig._gfl6)
                        gasLiftDto.Parameters.Add(parameterDto);

                }

                var gasValuesDto = new GasValuesDto
                {
                    WellInjectionId = gasInjection.Id,
                    DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                    Tag = gasInjection.WellValues.Value.Attribute.Name,
                    VolumePI = gasInjection.WellValues.Value.Amount is not null ? Math.Round(gasInjection.WellValues.Value.Amount.Value, 5) : null,
                    WellName = gasInjection.WellValues.Well.Name!/*wellInDatabase.Name!*/,
                    VolumeAssigned = gasInjection.AssignedValue,
                    GroupAmount = gasInjection.WellValues.Value.GroupAmount
                };

                parameterDto.Values.Add(gasValuesDto);
            }

            var orderedWater = waterInjectedDto.Parameters
                    .OrderBy(x => x.Parameter, new NaturalStringComparer())
                    .ToList();

            foreach (var parameter in orderedWater)
            {
                var orderedValues = parameter.Values
                   .OrderBy(x => x.WellName, new NaturalStringComparer())
                   .ToList();

                parameter.Values = orderedValues;
            }

            var orderedGas = gasLiftDto.Parameters
                .OrderBy(x => x.Parameter)
                .ToList();

            foreach (var parameter in orderedGas)
            {
                var orderedValues = parameter.Values
                    .OrderBy(x => x.WellName, new NaturalStringComparer())
                    .ToList();

                parameter.Values = orderedValues;
            }

            waterInjectedDto.Parameters = orderedWater;
            gasLiftDto.Parameters = orderedGas;

            result.WaterInjectedFields = waterInjectedDto;

            result.GasLiftFields = gasLiftDto;

            return result;
        }

        public async Task<DailyInjectionDto> GetDailyInjectionTags(DateTime dateInjection, Guid installationId)
        {
            var installation = await _installationRepository.GetByIdAsync(installationId)
            ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var fieldInjected = await _repository
                .AnyByDate(dateInjection);

            if (fieldInjected)
                throw new ConflictException($"Injeção diária no dia: {dateInjection:dd/MMM/yyyy} já criada.");

            var productionInDatabase = await _productionRepository
                .GetExistingByDate(dateInjection);

            if (productionInDatabase is null || productionInDatabase.StatusProduction == ProductionUtils.openStatus)
                throw new ConflictException("Produção do dia precisa feita e fechada antes de criar uma injeção.");

            var result = new DailyInjectionDto
            {
                DateInjection = dateInjection.ToString("dd/MMM/yyyy"),
                Status = false,
            };

            foreach (var field in installation.Fields)
            {
                var waterWellInjections = await _repository
              .GetWaterWellInjectionsByDate(dateInjection, field.Id);

                var gasWellInjections = await _repository
                   .GetGasWellInjectionsByDate(dateInjection, field.Id);

                var gasLiftDto = new GasLiftInjectedDto
                {
                    Field = field.Name!,
                };

                var waterInjectedDto = new WaterInjectedDto
                {
                    Field = field.Name!
                };

                foreach (var waterInjection in waterWellInjections)
                {
                    var parameterDto = waterInjectedDto.Parameters
                        .FirstOrDefault(x => x.Parameter == waterInjection.WellValues.Value.Attribute.Element.Parameter);

                    if (parameterDto is null)
                    {
                        parameterDto = new ElementWaterDto
                        {
                            Parameter = waterInjection.WellValues.Value.Attribute.Element.Parameter,
                        };

                        waterInjectedDto.Parameters.Add(parameterDto);
                    }

                    parameterDto.Values.Add(new WellValuesDto
                    {
                        WellInjectionId = waterInjection.Id,
                        DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                        Tag = waterInjection.WellValues.Value.Attribute.Name,
                        VolumeAssigned = Math.Round(waterInjection.AssignedValue, 5),
                        VolumePI = waterInjection.WellValues.Value.Amount is not null ? Math.Round(waterInjection.WellValues.Value.Amount.Value, 5) : null,
                        WellName = waterInjection.WellValues.Well.Name!,
                        GroupAmount = waterInjection.WellValues.Value.GroupAmount
                    });

                    result.TotalWaterInjected += waterInjection.AssignedValue;
                }


                foreach (var gasInjection in gasWellInjections)
                {
                    var parameterDto = gasLiftDto.Parameters
                        .FirstOrDefault(x => x.Parameter == gasInjection.WellValues.Value.Attribute.Element.Parameter);

                    if (parameterDto is null)
                    {
                        parameterDto = new ElementGasDto
                        {
                            Parameter = gasInjection.WellValues.Value.Attribute.Element.Parameter,
                        };

                        if (parameterDto.Parameter == PIConfig._gfl1 || parameterDto.Parameter == PIConfig._gfl4 || parameterDto.Parameter == PIConfig._gfl6)
                            gasLiftDto.Parameters.Add(parameterDto);

                    }

                    parameterDto.Values.Add(new GasValuesDto
                    {
                        WellInjectionId = gasInjection.Id,
                        DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                        Tag = gasInjection.WellValues.Value.Attribute.Name,
                        VolumePI = gasInjection.WellValues.Value.Amount is not null ? Math.Round(gasInjection.WellValues.Value.Amount.Value, 5) : null,
                        WellName = gasInjection.WellValues.Well.Name,
                        VolumeAssigned = gasInjection.AssignedValue,
                        GroupAmount = gasInjection.WellValues.Value.GroupAmount
                    });

                    result.TotalGasLift += gasInjection.AssignedValue;
                }

                var orderedWater = waterInjectedDto.Parameters
                   .OrderBy(x => x.Parameter, new NaturalStringComparer())
                   .ToList();

                foreach (var parameter in orderedWater)
                {
                    var orderedValues = parameter.Values
                        .OrderBy(x => x.WellName, new NaturalStringComparer())
                        .ToList();

                    parameter.Values = orderedValues;
                }

                var orderedGas = gasLiftDto.Parameters
                    .OrderBy(x => x.Parameter)
                    .ToList();

                foreach (var parameter in orderedGas)
                {
                    var orderedValues = parameter.Values
                        .OrderBy(x => x.WellName, new NaturalStringComparer())
                        .ToList();

                    parameter.Values = orderedValues;
                }

                waterInjectedDto.Parameters = orderedWater;
                gasLiftDto.Parameters = orderedGas;


                result.WaterInjectedFields.Add(waterInjectedDto);
                result.GasLiftFields.Add(gasLiftDto);
            }

            result.TotalGasLift = Math.Round(result.TotalGasLift, 5);
            result.TotalWaterInjected = Math.Round(result.TotalWaterInjected, 5);

            return result;
        }

        public async Task UpdateInjectionStatus(Guid fieldInjectionId)
        {
            var fieldInjectionInDatabase = await _repository
                .GetWaterGasFieldInjectionsById(fieldInjectionId)
                ?? throw new NotFoundException("Injeção de campo não encontrada.");

            fieldInjectionInDatabase.Status = true;

            _repository.UpdateWaterGasInjection(fieldInjectionInDatabase);

            await _repository.Save();
        }
        public async Task<WaterInjectionUpdateDto> UpdateInjection(UpdateInjectionViewModel body, Guid fieldInjectionId, User loggedUser)
        {
            var fieldInjectionInDatabase = await _repository
                .GetWaterGasFieldInjectionsById(fieldInjectionId)
                ?? throw new NotFoundException("Injeção de campo não encontrada.");

            var resultDto = new WaterInjectionUpdateDto
            {
                FieldInjectionId = fieldInjectionId,
            };

            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var bodyWellInjection in body.AssignedValues)
            {
                if (bodyWellInjection.AssignedValue is not null && bodyWellInjection.WellInjectionId is not null)
                {
                    var waterInjectionInDatabase = fieldInjectionInDatabase.WellsWaterInjections
                   .FirstOrDefault(x => x.Id == bodyWellInjection.WellInjectionId)
                   ?? throw new NotFoundException("Injeção de água do poço não encontrada.");

                    waterInjectionInDatabase.AssignedValue = bodyWellInjection.AssignedValue.Value;
                    waterInjectionInDatabase.UpdatedBy = loggedUser;

                    _repository.UpdateWaterInjection(waterInjectionInDatabase);

                    resultDto.AssignedValues.Add(new WaterAssignatedValuesDto
                    {
                        AssignedValue = waterInjectionInDatabase.AssignedValue,
                        InjectionId = bodyWellInjection.WellInjectionId.Value,
                        UpdatedBy = updatedBy
                    });
                }
            }

            fieldInjectionInDatabase.AmountWater = fieldInjectionInDatabase.WellsWaterInjections
                .Sum(x => x.AssignedValue);

            if (body.FIRS is not null)
            {
                fieldInjectionInDatabase.BalanceField.FIRS = body.FIRS;
                fieldInjectionInDatabase.FIRS = body.FIRS.Value;
            }

            fieldInjectionInDatabase.BalanceField.TotalWaterInjectedRS = (decimal)((body.FIRS is not null ? body.FIRS.Value : fieldInjectionInDatabase.FIRS) * fieldInjectionInDatabase.AmountWater);
            fieldInjectionInDatabase.BalanceField.TotalWaterDisposal = (decimal)((1 - (body.FIRS is not null ? body.FIRS.Value : fieldInjectionInDatabase.FIRS)) * fieldInjectionInDatabase.AmountWater);
            fieldInjectionInDatabase.BalanceField.TotalWaterInjected = (decimal)fieldInjectionInDatabase.AmountWater;

            fieldInjectionInDatabase.BalanceField.InstallationBalance.TotalWaterInjected = fieldInjectionInDatabase.BalanceField.InstallationBalance.BalanceFields
                .Sum(x => x.TotalWaterInjected);

            fieldInjectionInDatabase.BalanceField.InstallationBalance.TotalWaterDisposal = fieldInjectionInDatabase.BalanceField.InstallationBalance.BalanceFields
                .Sum(x => x.TotalWaterDisposal);

            fieldInjectionInDatabase.BalanceField.InstallationBalance.TotalWaterInjectedRS = fieldInjectionInDatabase.BalanceField.InstallationBalance.BalanceFields
                .Sum(x => x.TotalWaterInjectedRS);

            fieldInjectionInDatabase.BalanceField.InstallationBalance.UEPBalance.TotalWaterInjected = fieldInjectionInDatabase.BalanceField.InstallationBalance.UEPBalance.InstallationsBalance
                .Sum(x => x.TotalWaterInjected);

            fieldInjectionInDatabase.BalanceField.InstallationBalance.UEPBalance.TotalWaterInjectedRS = fieldInjectionInDatabase.BalanceField.InstallationBalance.UEPBalance.InstallationsBalance
                .Sum(x => x.TotalWaterInjectedRS);

            fieldInjectionInDatabase.BalanceField.InstallationBalance.UEPBalance.TotalWaterDisposal = fieldInjectionInDatabase.BalanceField.InstallationBalance.UEPBalance.InstallationsBalance
                .Sum(x => x.TotalWaterDisposal);

            _repository.UpdateWaterGasInjection(fieldInjectionInDatabase);

            resultDto.TotalWaterInjected = fieldInjectionInDatabase.AmountWater;

            await _repository.Save();

            return resultDto;
        }

    }
}
