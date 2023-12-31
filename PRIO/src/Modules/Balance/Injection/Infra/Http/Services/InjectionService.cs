﻿using AutoMapper;
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
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;

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

        public async Task<WaterInjectionUpdateDto> CreateWaterDailyInjection(CreateDailyWaterInjectionViewModel body, DateTime dateInjection, User loggedUser)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

            if (body.FIRS < 0 || body.FIRS > 1)
                throw new BadRequestException("FIRS deve ser um valor entre 0 e 1");

            if (instance == PIConfig._valenteInstance && body.AssignedWaterValues.Any())
            {
                var allValuesAreSame = body.AssignedWaterValues
                    .All(item => item.AssignedValue == body.AssignedWaterValues
                    .First().AssignedValue && item.AssignedWFLValue == body.AssignedWaterValues
                    .First().AssignedWFLValue);

                if (allValuesAreSame is false)
                    throw new BadRequestException("Todos valores de WFL devem ser iguais.");
            }

            var field = await _fieldRepository.GetByIdAsync(body.FieldId)
                   ?? throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var fieldBalance = await _balanceRepository
                .GetBalanceField(body.FieldId!.Value, dateInjection.Date)
                ?? throw new BadRequestException("Balanço de campo não criado ainda, é necessário fechar a produção do dia.");

            if (fieldBalance.IsParameterized is false && instance == PIConfig._valenteInstance)
                throw new ConflictException("Dados operacionais precisam ser confirmados.");

            var fieldInjection = await _repository
                .GetWaterGasFieldInjectionByDate(dateInjection);

            if (fieldInjection is not null && fieldInjection.WellsWaterInjections.Any())
                throw new ConflictException($"Injeção de água do dia: {dateInjection:dd/MMM/yyyy} já atribuída.");

            fieldInjection ??= new InjectionWaterGasField
            {
                Id = Guid.NewGuid(),
                MeasurementAt = dateInjection,
                BalanceField = fieldBalance,
                Field = field,
                FIRS = body.FIRS!.Value,
                AmountGasLift = 0,
                AmountWater = 0
            };

            var resultDto = new WaterInjectionUpdateDto
            {
                FieldInjectionId = fieldInjection.Id
            };

            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var injection in body.AssignedWaterValues)
            {
                if (injection.WellInjectionId is not null)
                {
                    var flowWaterInjectionInDatabase = await _repository.GetWaterInjectionById(injection.WellInjectionId)
                    ?? throw new NotFoundException("Dados de vazão de água não encontrados.");

                    if (injection.AssignedValue is not null)
                    {
                        flowWaterInjectionInDatabase.AssignedValue = injection.AssignedValue.Value;
                        flowWaterInjectionInDatabase.UpdatedBy = loggedUser;

                        resultDto.AssignedValues.Add(new InjectionValuesDto
                        {
                            InjectionValue = flowWaterInjectionInDatabase.AssignedValue,
                            InjectionId = injection.WellInjectionId.Value,
                            UpdatedBy = updatedBy
                        });
                        _repository.UpdateWaterInjection(flowWaterInjectionInDatabase);
                    }

                }

                if (injection.WFLInjectionId is not null)
                {
                    var WFLWaterInjectionInDatabase = await _repository.GetWaterInjectionById(injection.WFLInjectionId)
                    ?? throw new NotFoundException("Dados de vazão de WFL não encontrados.");

                    if (injection.AssignedWFLValue is not null)
                    {
                        WFLWaterInjectionInDatabase.WellValues.Value.GroupAmountAssigned = injection.AssignedWFLValue.Value;
                        WFLWaterInjectionInDatabase.AssignedValue = WFLWaterInjectionInDatabase.WellValues.Value.GroupAmountAssigned.Value * WFLWaterInjectionInDatabase.WellValues.Value.Potencial;
                        WFLWaterInjectionInDatabase.UpdatedBy = loggedUser;

                        resultDto.AssignedValues.Add(new InjectionValuesDto
                        {
                            InjectionValue = WFLWaterInjectionInDatabase.AssignedValue,
                            InjectionId = injection.WFLInjectionId.Value,
                            UpdatedBy = updatedBy
                        });

                        _repository.UpdateWaterInjection(WFLWaterInjectionInDatabase);
                    }
                }
            }

            var waterInjectionWells = await _repository
               .GetWaterWellInjectionsByDate(dateInjection, field.Id);

            if (instance == PIConfig._valenteInstance)
            {
                foreach (var waterInjection in waterInjectionWells)
                {
                    waterInjection.InjectionWaterGasField = fieldInjection;

                    if (waterInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._wfl1)
                    {
                        fieldInjection.AmountWater += waterInjection.WellValues.Value.GroupAmountAssigned is not null ? waterInjection.WellValues.Value.GroupAmountAssigned.Value * waterInjection.WellValues.Value.Potencial : 0;
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
            }

            fieldBalance.TotalWaterInjectedRS = (decimal)(body.FIRS!.Value * fieldInjection.AmountWater);
            fieldBalance.TotalWaterDisposal = (decimal)((1 - body.FIRS.Value) * fieldInjection.AmountWater);
            fieldBalance.TotalWaterInjected = (decimal)fieldInjection.AmountWater;
            fieldBalance.FIRS = body.FIRS;

            await _repository.AddOrUpdateInjection(fieldInjection);

            DistributeToParentBalances(fieldBalance);

            _balanceRepository.UpdateFieldBalance(fieldBalance);

            resultDto.TotalWaterInjected = fieldInjection.AmountWater;

            await _repository.Save();
            return resultDto;
        }

        public async Task<GasInjectionUpdateDto> CreateGasDailyInjection(CreateDailyGasInjectionViewModel body, DateTime dateInjection, User loggedUser)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

            if (instance == PIConfig._valenteInstance && body.AssignedGasValues.Any())
            {
                var allValuesAreSame = body.AssignedGasValues
                    .All(item => item.AssignedValue == body.AssignedGasValues
                    .First().AssignedValue && item.AssignedGFLValue == body.AssignedGasValues
                    .First().AssignedGFLValue);

                if (allValuesAreSame is false)
                    throw new BadRequestException("Todos valores de GFL devem ser iguais.");
            }

            var field = await _fieldRepository.GetByIdAsync(body.FieldId)
                   ?? throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var fieldBalance = await _balanceRepository
                .GetBalanceField(body.FieldId!.Value, dateInjection.Date)
                ?? throw new BadRequestException("Balanço de campo não criado ainda, é necessário fechar a produção do dia.");

            //if (fieldBalance.IsParameterized is false && instance == PIConfig._valenteInstance)
            //    throw new ConflictException("Dados operacionais precisam ser confirmados.");

            var fieldInjection = await _repository
                .GetWaterGasFieldInjectionByDate(dateInjection);

            if (fieldInjection is not null && fieldInjection.WellsGasInjections.Any())
                throw new ConflictException($"Injeção de gás do dia: {dateInjection:dd/MMM/yyyy} já atribuída.");

            fieldInjection ??= new InjectionWaterGasField
            {
                Id = Guid.NewGuid(),
                MeasurementAt = dateInjection,
                BalanceField = fieldBalance,
                Field = field,
                AmountGasLift = 0,
                AmountWater = 0
            };

            var resultDto = new GasInjectionUpdateDto
            {
                FieldInjectionId = fieldInjection.Id
            };

            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var injection in body.AssignedGasValues)
            {
                if (injection.WellInjectionId is not null)
                {
                    var flowGasInjectionInDatabase = await _repository.GetGasInjectionById(injection.WellInjectionId)
                    ?? throw new NotFoundException("Dados de vazão de gás não encontrados.");

                    if (injection.AssignedValue is not null)
                    {
                        flowGasInjectionInDatabase.AssignedValue = injection.AssignedValue.Value;
                        flowGasInjectionInDatabase.UpdatedBy = loggedUser;

                        resultDto.AssignedValues.Add(new InjectionValuesDto
                        {
                            InjectionValue = flowGasInjectionInDatabase.AssignedValue,
                            InjectionId = injection.WellInjectionId.Value,
                            UpdatedBy = updatedBy
                        });
                    }

                    _repository.UpdateGasInjection(flowGasInjectionInDatabase);
                }

                if (injection.GFLInjectionId is not null && instance == PIConfig._valenteInstance)
                {
                    var GFLGasInjectionInDatabase = await _repository.GetGasInjectionById(injection.GFLInjectionId)
                    ?? throw new NotFoundException("Dados de vazão de GFL não encontrados.");

                    if (injection.AssignedGFLValue is not null)
                    {
                        GFLGasInjectionInDatabase.WellValues.Value.GroupAmountAssigned = injection.AssignedGFLValue.Value;
                        GFLGasInjectionInDatabase.AssignedValue = GFLGasInjectionInDatabase.WellValues.Value.GroupAmountAssigned.Value * GFLGasInjectionInDatabase.WellValues.Value.Potencial;
                        GFLGasInjectionInDatabase.UpdatedBy = loggedUser;

                        resultDto.AssignedValues.Add(new InjectionValuesDto
                        {
                            InjectionValue = GFLGasInjectionInDatabase.AssignedValue,
                            InjectionId = injection.GFLInjectionId.Value,
                            UpdatedBy = updatedBy
                        });
                    }

                    _repository.UpdateGasInjection(GFLGasInjectionInDatabase);
                }

            }

            var gasInjectionWells = await _repository
                .GetGasWellInjectionsByDate(dateInjection, field.Id);

            if (instance == PIConfig._valenteInstance)
            {
                foreach (var gasInjection in gasInjectionWells)
                {
                    gasInjection.InjectionWaterGasField = fieldInjection;

                    if (gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl1 || gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl6 || gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl4)
                    {
                        fieldInjection.AmountGasLift += gasInjection.WellValues.Value.GroupAmountAssigned is not null ? gasInjection.WellValues.Value.GroupAmountAssigned.Value * gasInjection.WellValues.Value.Potencial : 0;
                    }
                }
            }

            if (instance == PIConfig._forteInstance)
            {
                foreach (var gasInjection in gasInjectionWells)
                {
                    gasInjection.InjectionWaterGasField = fieldInjection;
                    fieldInjection.AmountGasLift += gasInjection.AssignedValue;
                }
            }

            await _repository.AddOrUpdateInjection(fieldInjection);

            resultDto.TotalGasLift = fieldInjection.AmountGasLift;

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

        public async Task<FieldInjectionDto> GetInjectionByFieldInjectionId(Guid fieldInjectionId)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

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

            if (instance == PIConfig._valenteInstance)
            {
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

                        if (parameterDto.Parameter == PIConfig._wfl1)
                            waterInjectedDto.Parameters.Add(parameterDto);
                    }

                    foreach (var waterInjectionFlow in fieldInjection.WellsWaterInjections)
                    {
                        if (waterInjectionFlow.WellValues.Well.Name == waterInjection.WellValues.Well.Name && waterInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._wfl1)
                            parameterDto.Values.Add(new WellValuesDto
                            {
                                WellInjectionId = waterInjectionFlow.Id,
                                WellName = waterInjectionFlow.WellValues.Well.Name!,
                                TagFlow = waterInjectionFlow.WellValues.Value.Attribute.Name,
                                FlowVolumeAssigned = Math.Round(waterInjectionFlow.AssignedValue, 5),
                                FlowVolumePI = waterInjectionFlow.WellValues.Value.Amount is not null ? Math.Round(waterInjectionFlow.WellValues.Value.Amount.Value, 5) : null,
                                DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                                WFLInjectionId = waterInjection.Id,
                                TagWFL = waterInjection.WellValues.Value.Attribute.Name,

                                VolumeWFLAssigned = waterInjection.WellValues.Value.GroupAmountAssigned is not null ? Math.Round(waterInjection.WellValues.Value.GroupAmountAssigned.Value, 5) : 0,

                                VolumeWFLPI = waterInjection.WellValues.Value.GroupAmountPI is not null ? Math.Round(waterInjection.WellValues.Value.GroupAmountPI.Value, 5) : null,

                                VolumeInjected = Math.Round(waterInjection.AssignedValue, 5)
                            });
                    }
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

                    foreach (var gasInjectionFlow in fieldInjection.WellsGasInjections)
                    {
                        if (gasInjectionFlow.WellValues.Well.Name == gasInjection.WellValues.Well.Name && gasInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._gfl1 && gasInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._gfl4 && gasInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._gfl6)
                            parameterDto.Values.Add(new GasValuesDto
                            {
                                WellInjectionId = gasInjectionFlow.Id,
                                WellName = gasInjectionFlow.WellValues.Well.Name!,
                                TagFlow = gasInjectionFlow.WellValues.Value.Attribute.Name,
                                FlowVolumeAssigned = Math.Round(gasInjectionFlow.AssignedValue, 5),
                                FlowVolumePI = gasInjectionFlow.WellValues.Value.Amount is not null ? Math.Round(gasInjectionFlow.WellValues.Value.Amount.Value, 5) : null,
                                DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),

                                GFLInjectionId = gasInjection.Id,
                                TagGFL = gasInjection.WellValues.Value.Attribute.Name,

                                VolumeGFLAssigned = gasInjection.WellValues.Value.GroupAmountAssigned is not null ? Math.Round(gasInjection.WellValues.Value.GroupAmountAssigned.Value, 5) : 0,

                                VolumeGFLPI = gasInjection.WellValues.Value.GroupAmountPI is not null ? Math.Round(gasInjection.WellValues.Value.GroupAmountPI.Value, 5) : null,

                                VolumeInjected = Math.Round(gasInjection.AssignedValue, 5)
                            });
                    }
                }

            }

            if (instance == PIConfig._forteInstance)
            {
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
                        WellName = waterInjection.WellValues.Well.Name!,
                        TagFlow = waterInjection.WellValues.Value.Attribute.Name,
                        FlowVolumeAssigned = Math.Round(waterInjection.AssignedValue, 5),
                        FlowVolumePI = waterInjection.WellValues.Value.Amount is not null ? Math.Round(waterInjection.WellValues.Value.Amount.Value, 5) : null,
                        DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                        VolumeInjected = Math.Round(waterInjection.AssignedValue, 5)
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

                    parameterDto.Values.Add(new GasValuesDto
                    {
                        WellInjectionId = gasInjection.Id,
                        WellName = gasInjection.WellValues.Well.Name!,
                        TagFlow = gasInjection.WellValues.Value.Attribute.Name,
                        FlowVolumeAssigned = Math.Round(gasInjection.AssignedValue, 5),
                        FlowVolumePI = gasInjection.WellValues.Value.Amount is not null ? Math.Round(gasInjection.WellValues.Value.Amount.Value, 5) : null,
                        DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                        VolumeInjected = Math.Round(gasInjection.AssignedValue, 5)
                    });
                }

            }

            if (waterInjectedDto.Parameters.Any())
            {
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

                waterInjectedDto.Parameters = orderedWater;
            }

            if (gasLiftDto.Parameters.Any())
            {
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

                gasLiftDto.Parameters = orderedGas;
            }

            result.WaterInjectedFields = waterInjectedDto;

            result.GasLiftFields = gasLiftDto;

            return result;
        }

        public async Task<DailyInjectionDto> GetDailyInjectionTags(DateTime dateInjection, Guid installationId)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

            var installation = await _installationRepository.GetByIdAsync(installationId)
            ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var fieldInjected = await _repository
                .GetWaterGasFieldInjectionByDate(dateInjection);

            if (fieldInjected is not null && fieldInjected.WellsGasInjections.Any() && fieldInjected.WellsWaterInjections.Any())
                throw new ConflictException($"Injeção diária no dia: {dateInjection:dd/MMM/yyyy} já criada.");

            foreach (var field in installation.Fields)
            {
                var fieldBalance = await _balanceRepository
                    .GetBalanceField(field.Id, dateInjection.Date)
                    ?? throw new BadRequestException("Balanço(s) de campo não criado(s) ainda, é necessário fechar a produção do dia.");

                if (fieldBalance.IsParameterized is false && instance == PIConfig._valenteInstance)
                    throw new ConflictException("Dados operacionais precisam ser confirmados.");
            }

            //var productionInDatabase = await _productionRepository
            //    .GetCleanByDate(dateInjection);

            //if (productionInDatabase is null || productionInDatabase.StatusProduction == ProductionUtils.openStatus)
            //    throw new ConflictException("Produção do dia precisa feita e fechada antes de criar uma injeção.");

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

                if (instance == PIConfig._valenteInstance)
                {
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

                            if (parameterDto.Parameter == PIConfig._wfl1)
                                waterInjectedDto.Parameters.Add(parameterDto);
                        }

                        foreach (var waterInjectionFlow in waterWellInjections)
                        {
                            if (waterInjectionFlow.WellValues.Well.Name == waterInjection.WellValues.Well.Name && waterInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._wfl1)
                                parameterDto.Values.Add(new WellValuesDto
                                {
                                    WellInjectionId = waterInjectionFlow.Id,
                                    WellName = waterInjectionFlow.WellValues.Well.Name!,
                                    TagFlow = waterInjectionFlow.WellValues.Value.Attribute.Name,
                                    FlowVolumeAssigned = Math.Round(waterInjectionFlow.AssignedValue, 5),
                                    FlowVolumePI = waterInjectionFlow.WellValues.Value.Amount is not null ? Math.Round(waterInjectionFlow.WellValues.Value.Amount.Value, 5) : null,
                                    DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                                    WFLInjectionId = waterInjection.Id,
                                    TagWFL = waterInjection.WellValues.Value.Attribute.Name,

                                    VolumeWFLAssigned = waterInjection.WellValues.Value.GroupAmountAssigned is not null ? Math.Round(waterInjection.WellValues.Value.GroupAmountAssigned.Value, 5) : 0,

                                    VolumeWFLPI = waterInjection.WellValues.Value.GroupAmountPI is not null ? Math.Round(waterInjection.WellValues.Value.GroupAmountPI.Value, 5) : null,

                                    VolumeInjected = waterInjection.AssignedValue,
                                });
                        }

                        if (waterInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._wfl1)
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

                        foreach (var gasInjectionFlow in gasWellInjections)
                        {
                            if (gasInjectionFlow.WellValues.Well.Name == gasInjection.WellValues.Well.Name && gasInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._gfl1 && gasInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._gfl4 && gasInjectionFlow.WellValues.Value.Attribute.Element.Parameter != PIConfig._gfl6)
                                parameterDto.Values.Add(new GasValuesDto
                                {
                                    WellInjectionId = gasInjectionFlow.Id,
                                    WellName = gasInjectionFlow.WellValues.Well.Name!,
                                    TagFlow = gasInjectionFlow.WellValues.Value.Attribute.Name,
                                    FlowVolumeAssigned = Math.Round(gasInjectionFlow.AssignedValue, 5),
                                    FlowVolumePI = gasInjectionFlow.WellValues.Value.Amount is not null ? Math.Round(gasInjectionFlow.WellValues.Value.Amount.Value, 5) : null,
                                    DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                                    GFLInjectionId = gasInjection.Id,
                                    TagGFL = gasInjection.WellValues.Value.Attribute.Name,

                                    VolumeGFLAssigned = gasInjection.WellValues.Value.GroupAmountAssigned is not null ? Math.Round(gasInjection.WellValues.Value.GroupAmountAssigned.Value, 5) : null,

                                    VolumeGFLPI = gasInjection.WellValues.Value.GroupAmountPI is not null ? Math.Round(gasInjection.WellValues.Value.GroupAmountPI.Value, 5) : null,

                                    VolumeInjected = gasInjection.AssignedValue
                                });
                        }

                        if (gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl1 || gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl4 || gasInjection.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl6)
                            result.TotalGasLift += gasInjection.AssignedValue;
                    }
                }

                if (instance == PIConfig._forteInstance)
                {
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
                            WellName = waterInjection.WellValues.Well.Name!,
                            TagFlow = waterInjection.WellValues.Value.Attribute.Name,
                            FlowVolumeAssigned = Math.Round(waterInjection.AssignedValue, 5),
                            FlowVolumePI = waterInjection.WellValues.Value.Amount is not null ? Math.Round(waterInjection.WellValues.Value.Amount.Value, 5) : null,
                            DateRead = waterInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                            VolumeInjected = Math.Round(waterInjection.AssignedValue, 5)
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

                            gasLiftDto.Parameters.Add(parameterDto);
                        }

                        parameterDto.Values.Add(new GasValuesDto
                        {
                            WellInjectionId = gasInjection.Id,
                            WellName = gasInjection.WellValues.Well.Name!,
                            TagFlow = gasInjection.WellValues.Value.Attribute.Name,
                            FlowVolumeAssigned = Math.Round(gasInjection.AssignedValue, 5),
                            FlowVolumePI = gasInjection.WellValues.Value.Amount is not null ? Math.Round(gasInjection.WellValues.Value.Amount.Value, 5) : null,
                            DateRead = gasInjection.MeasurementAt.ToString("dd/MMM/yyyy"),
                            VolumeInjected = Math.Round(gasInjection.AssignedValue, 5)
                        });

                        result.TotalGasLift += gasInjection.AssignedValue;
                    }
                }

                if (waterInjectedDto.Parameters.Any())
                {
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

                    waterInjectedDto.Parameters = orderedWater;
                }

                if (gasLiftDto.Parameters.Any())
                {
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

                    gasLiftDto.Parameters = orderedGas;
                }

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

            if (fieldInjectionInDatabase.WellsGasInjections.Any() is false)
                throw new ConflictException("Injeção de gás precisa ser feita.");

            if (fieldInjectionInDatabase.WellsWaterInjections.Any() is false)
                throw new ConflictException("Injeção de água precisa ser feita.");

            fieldInjectionInDatabase.Status = true;

            _repository.UpdateWaterGasInjection(fieldInjectionInDatabase);

            await _repository.Save();
        }
        public async Task<WaterInjectionUpdateDto> UpdateWaterInjection(UpdateWaterInjectionViewModel body, Guid fieldInjectionId, User loggedUser)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

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
                    var flowWaterInjectionInDatabase = fieldInjectionInDatabase.WellsWaterInjections
                   .FirstOrDefault(x => x.Id == bodyWellInjection.WellInjectionId)
                   ?? throw new NotFoundException("Vazão de água do poço não encontrada.");

                    flowWaterInjectionInDatabase.AssignedValue = bodyWellInjection.AssignedValue.Value;
                    flowWaterInjectionInDatabase.UpdatedBy = loggedUser;

                    resultDto.AssignedValues.Add(new InjectionValuesDto
                    {
                        InjectionValue = flowWaterInjectionInDatabase.AssignedValue,
                        InjectionId = bodyWellInjection.WellInjectionId.Value,
                        UpdatedBy = updatedBy
                    });

                    _repository.UpdateWaterInjection(flowWaterInjectionInDatabase);
                }

                if (bodyWellInjection.AssignedWFLValue is not null && bodyWellInjection.WFLInjectionId is not null)
                {
                    var WFLWaterInjectionInDatabase = fieldInjectionInDatabase.WellsWaterInjections
                   .FirstOrDefault(x => x.Id == bodyWellInjection.WFLInjectionId)
                   ?? throw new NotFoundException("Vazão de água do poço não encontrada.");

                    WFLWaterInjectionInDatabase.WellValues.Value.GroupAmountAssigned = bodyWellInjection.AssignedWFLValue.Value;
                    WFLWaterInjectionInDatabase.AssignedValue = WFLWaterInjectionInDatabase.WellValues.Value.GroupAmountAssigned.Value * WFLWaterInjectionInDatabase.WellValues.Value.Potencial;
                    WFLWaterInjectionInDatabase.UpdatedBy = loggedUser;

                    resultDto.AssignedValues.Add(new InjectionValuesDto
                    {
                        InjectionValue = WFLWaterInjectionInDatabase.AssignedValue,
                        InjectionId = bodyWellInjection.WFLInjectionId.Value,
                        UpdatedBy = updatedBy
                    });

                    _repository.UpdateWaterInjection(WFLWaterInjectionInDatabase);
                }
            }

            if (instance == PIConfig._valenteInstance)
            {
                fieldInjectionInDatabase.AmountWater = fieldInjectionInDatabase.WellsWaterInjections
                        .Where(x => x.WellValues.Value.Attribute.Element.Parameter == PIConfig._wfl1)
                        .Sum(x => x.AssignedValue);
            }
            if (instance == PIConfig._forteInstance)
            {
                fieldInjectionInDatabase.AmountWater = fieldInjectionInDatabase.WellsWaterInjections
                        .Sum(x => x.AssignedValue);
            }

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

        public async Task<GasInjectionUpdateDto> UpdateGasInjection(UpdateGasInjectionViewModel body, Guid fieldInjectionId, User loggedUser)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

            var fieldInjection = await _repository
                .GetWaterGasFieldInjectionsById(fieldInjectionId)
                ?? throw new NotFoundException("Injeção de campo não encontrada.");

            var resultDto = new GasInjectionUpdateDto
            {
                FieldInjectionId = fieldInjectionId,
            };

            var updatedBy = _mapper.Map<UserDTO>(loggedUser);

            foreach (var bodyWellInjection in body.AssignedValues)
            {
                if (bodyWellInjection.AssignedValue is not null && bodyWellInjection.WellInjectionId is not null)
                {
                    var gasInjectionInDatabase = fieldInjection.WellsGasInjections
                   .FirstOrDefault(x => x.Id == bodyWellInjection.WellInjectionId)
                   ?? throw new NotFoundException("Vazão de gas lift do poço não encontrada.");

                    gasInjectionInDatabase.AssignedValue = bodyWellInjection.AssignedValue.Value;
                    gasInjectionInDatabase.UpdatedBy = loggedUser;

                    _repository.UpdateGasInjection(gasInjectionInDatabase);

                    resultDto.AssignedValues.Add(new InjectionValuesDto
                    {
                        InjectionValue = gasInjectionInDatabase.AssignedValue,
                        InjectionId = bodyWellInjection.WellInjectionId.Value,
                        UpdatedBy = updatedBy
                    });
                }

                if (bodyWellInjection.AssignedGFLValue is not null && bodyWellInjection.GFLInjectionId is not null)
                {
                    var GFLGasInjectionInDatabase = fieldInjection.WellsGasInjections
                   .FirstOrDefault(x => x.Id == bodyWellInjection.GFLInjectionId)
                   ?? throw new NotFoundException("Vazão de GFL do poço não encontrada.");

                    GFLGasInjectionInDatabase.WellValues.Value.GroupAmountAssigned = bodyWellInjection.AssignedGFLValue.Value;
                    GFLGasInjectionInDatabase.AssignedValue = bodyWellInjection.AssignedGFLValue.Value * GFLGasInjectionInDatabase.WellValues.Value.Potencial;
                    GFLGasInjectionInDatabase.UpdatedBy = loggedUser;

                    _repository.UpdateGasInjection(GFLGasInjectionInDatabase);

                    resultDto.AssignedValues.Add(new InjectionValuesDto
                    {
                        InjectionValue = GFLGasInjectionInDatabase.AssignedValue,
                        InjectionId = bodyWellInjection.GFLInjectionId.Value,
                        UpdatedBy = updatedBy
                    });
                }
            }


            if (instance == PIConfig._valenteInstance)
            {
                fieldInjection.AmountGasLift = fieldInjection.WellsGasInjections
                    .Where(x => x.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl1 || x.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl6 || x.WellValues.Value.Attribute.Element.Parameter == PIConfig._gfl4)
                    .Sum(x => x.AssignedValue);
            }

            if (instance == PIConfig._forteInstance)
            {
                fieldInjection.AmountGasLift = fieldInjection.WellsGasInjections
                    .Sum(x => x.AssignedValue);
            }

            _repository.UpdateWaterGasInjection(fieldInjection);

            resultDto.TotalGasLift = fieldInjection.AmountGasLift;

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

    }
}
