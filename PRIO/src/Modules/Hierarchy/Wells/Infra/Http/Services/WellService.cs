using AutoMapper;
using dotenv.net;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Dtos;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.ViewModels;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;
using System.Data;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.Http.Services
{
    public class WellService
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IWellEventRepository _eventWellRepository;
        private readonly ICompletionRepository _completionRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IManualConfigRepository _manualConfigRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableWells;
        private readonly IDictionary<string, string> variablesEnv = DotEnv.Read();

        public WellService(IMapper mapper, IFieldRepository fieldRepository, SystemHistoryService systemHistoryService, IWellRepository wellRepository, ICompletionRepository completionRepositor, IWellEventRepository wellEventRepository, IProductionRepository productionRepository, IManualConfigRepository manualConfigRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _wellRepository = wellRepository;
            _completionRepository = completionRepositor;
            _systemHistoryService = systemHistoryService;
            _eventWellRepository = wellEventRepository;
            _productionRepository = productionRepository;
            _manualConfigRepository = manualConfigRepository;
            //_context = cpn;
        }

        public async Task<CreateUpdateWellDTO> CreateWell(CreateWellViewModel body, User user)
        {
            var appDate = variablesEnv["APPLICATIONSTARTDATE"];

            var convertAppDate = DateTime.Parse(appDate);

            var wellExistingCode = await _wellRepository
                .GetByCode(body.CodWellAnp);

            if (wellExistingCode is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Well>());

            var field = await _fieldRepository.GetOnlyField(body.FieldId);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new NotFoundException(ErrorMessages.Inactive<Field>());

            var wellSameName = await _wellRepository
                .GetByNameAsync(body.Name);

            if (wellSameName is not null)
                throw new ConflictException($"Já existe um poço com o nome: {body.Name}");

            var wellId = Guid.NewGuid();

            var well = new Well
            {
                Id = wellId,
                CodWell = body.CodWell is not null ? body.CodWell : GenerateCode.Generate(body.Name),
                Name = body.Name,
                WellOperatorName = body.WellOperatorName,
                CodWellAnp = body.CodWellAnp,
                CategoryAnp = body.CategoryAnp,
                CategoryReclassificationAnp = body.CategoryReclassificationAnp,
                CategoryOperator = body.CategoryOperator,
                StatusOperator = body.StatusOperator,
                Type = body.Type,
                WaterDepth = body.WaterDepth,
                ArtificialLift = body.ArtificialLift,
                Latitude4C = body.Latitude4C,
                Longitude4C = body.Longitude4C,
                LongitudeDD = body.LongitudeDD,
                LatitudeDD = body.LatitudeDD,
                DatumHorizontal = body.DatumHorizontal,
                TypeBaseCoordinate = body.TypeBaseCoordinate,
                CoordX = body.CoordX,
                CoordY = body.CoordY,
                Description = body.Description,
                Field = field,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            var wellEvent = new WellEvent
            {
                Id = Guid.NewGuid(),
                Well = well,
                StartDate = convertAppDate,
                IdAutoGenerated = $"{field?.Name?[..3]}0001",
                StatusANP = "Produzindo",
                StateANP = "1",
                EventStatus = "A",
                CreatedBy = user,
            };

            var closingWellEvent = new WellEvent
            {
                Id = Guid.NewGuid(),
                Well = (Well)well,
                StartDate = DateTime.UtcNow.AddHours(-3),
                IdAutoGenerated = $"{field?.Name?[..3]}0001",
                StatusANP = "Produzindo",
                StateANP = "1",
                CreatedBy = user,
                EventStatus = "F",
                EventRelated = wellEvent,
            };

            var eventReason = new EventReason
            {
                Id = Guid.NewGuid(),
                WellEvent = closingWellEvent,
                SystemRelated = "Inativo",
                StartDate = DateTime.UtcNow.AddHours(-3),
                WellEventId = closingWellEvent.Id,
                CreatedBy = user,
            };

            var manualConfig = new ManualWellConfiguration
            {
                Id = Guid.NewGuid(),
                Well = well,
            };
            var buildUp = new BuildUp
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = false,
                IsOperating = false,
                Value = 0,
                ManualWellConfiguration = manualConfig
            };
            if (well.CategoryOperator.ToUpper().Contains("PRODUTOR"))
            {
                var productivityIndex = new ProductivityIndex
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = false,
                    IsOperating = false,
                    Value = 0,
                    ManualWellConfiguration = manualConfig
                };
                await _manualConfigRepository.AddProductivityAsync(productivityIndex);
            }
            else if (well.CategoryOperator.ToUpper().Contains("INJETOR"))
            {
                var injectivityIndex = new InjectivityIndex
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = false,
                    IsOperating = false,
                    Value = 0,
                    ManualWellConfiguration = manualConfig
                };
                await _manualConfigRepository.AddInjectivityAsync(injectivityIndex);
            }
            await _eventWellRepository.Add(wellEvent);
            await _eventWellRepository.Add(closingWellEvent);
            await _eventWellRepository.AddReasonClosedEvent(eventReason);

            await _manualConfigRepository.AddConfigAsync(manualConfig);
            await _manualConfigRepository.AddBuildUpAsync(buildUp);

            await _wellRepository.AddAsync(well);

            await _systemHistoryService
                .Create<Well, WellHistoryDTO>(_tableName, user, wellId, well);

            await _wellRepository.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return wellDTO;
        }
        public async Task<WellWithManualConfigDTO> CreateConfig(CreateConfigViewModels body, Guid wellId, User user)
        {
            var well = await _wellRepository.GetByIdAsync(wellId) ?? throw new NotFoundException("Poço não encontrado");
            if (well.ManualWellConfiguration is null)
                throw new NotFoundException("Configuração Manual do poço não encontrada");

            var manualConfig = await _manualConfigRepository.GetManualConfig(well.ManualWellConfiguration.Id);
            if (manualConfig is null)
                throw new NotFoundException("Configuração Manual do poço não encontrada");

            if (body.Index is null || body.BuildUpValue is null)
                throw new ConflictException("Novos valores não informados.");

            var instance = variablesEnv["INSTANCE"];

            foreach (var BuildUp in manualConfig.BuildUp)
            {
                BuildUp.IsActive = false;
                BuildUp.IsOperating = false;
                _manualConfigRepository.UpdateBuildUp(BuildUp);
            }
            var buildUp = new BuildUp
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                IsOperating = instance == "FRADE",
                Value = body.BuildUpValue.Value,
                ManualWellConfiguration = manualConfig
            };
            var buildUpDTO = new BuildUpDTO
            {
                IsActive = buildUp.IsActive,
                IsOperating = buildUp.IsOperating,
                Value = buildUp.Value
            };
            var manualConfigDTO = new ManualConfigDTO
            {
                Id = manualConfig.Id,
                BuildUp = buildUpDTO,
            };
            await _manualConfigRepository.AddBuildUpAsync(buildUp);
            if (well.CategoryOperator.ToUpper().Contains("PRODUTOR"))
            {
                foreach (var IP in manualConfig.ProductivityIndex)
                {
                    IP.IsActive = false;
                    IP.IsOperating = false;
                    _manualConfigRepository.UpdateProductivity(IP);
                }

                var productivityIndex = new ProductivityIndex
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true,
                    IsOperating = instance == "FRADE",
                    Value = body.Index.Value,
                    ManualWellConfiguration = manualConfig
                };
                var productivityDTO = new ProductivityIndexDTO
                {
                    IsActive = productivityIndex.IsActive,
                    IsOperating = productivityIndex.IsOperating,
                    Value = productivityIndex.Value
                };
                manualConfigDTO.ProductivityIndex = productivityDTO;
                await _manualConfigRepository.AddProductivityAsync(productivityIndex);
            }
            else if (well.CategoryOperator.ToUpper().Contains("INJETOR"))
            {
                foreach (var II in manualConfig.InjectivityIndex)
                {
                    II.IsActive = false;
                    II.IsOperating = false;
                    _manualConfigRepository.UpdateInjectivity(II);
                }
                var injectivityIndex = new InjectivityIndex
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true,
                    IsOperating = instance == "FRADE",
                    Value = body.Index.Value,
                    ManualWellConfiguration = manualConfig
                };
                var injectivityDTO = new InjectivityIndexDTO
                {
                    IsActive = injectivityIndex.IsActive,
                    IsOperating = injectivityIndex.IsOperating,
                    Value = injectivityIndex.Value
                };
                manualConfigDTO.InjectivityIndex = injectivityDTO;
                await _manualConfigRepository.AddInjectivityAsync(injectivityIndex);
            }
            var wellDTO = new WellWithManualConfigDTO
            {
                Id = well.Id,
                CodWell = well.CodWell,
                CodWellAnp = well.CodWellAnp,
                WellOperatorName = well.WellOperatorName,
                CategoryOperator = well.CategoryOperator,
                IsActive = well.IsActive,
                Name = well.Name,
                ManualConfig = manualConfigDTO
            };
            await _manualConfigRepository.SaveAsync();

            return wellDTO;

        }
        public async Task<List<WellDTO>> GetWells(User user)
        {
            var wells = await _wellRepository.GetAsync(user);

            var wellsDTO = _mapper.Map<List<Well>, List<WellDTO>>(wells);
            return wellsDTO;
        }
        public async Task<WellWithManualConfigDTO> GetWellsConfig(User user, Guid wellId)
        {
            var well = await _wellRepository.GetByIdAsync(wellId) ?? throw new NotFoundException("Poço não encontrado");
            if (well.ManualWellConfiguration is null)
                throw new NotFoundException("Configuração Manual do poço não encontrada");

            var manualConfig = await _manualConfigRepository.GetManualConfig(well.ManualWellConfiguration.Id);
            if (manualConfig is null)
                throw new NotFoundException("Configuração Manual do poço não encontrada");

            var buildUpActive = manualConfig.BuildUp.Where(x => x.IsActive == true).FirstOrDefault();
            var buildUpDTO = new BuildUpDTO
            {
                IsActive = buildUpActive is not null ? buildUpActive.IsActive : false,
                IsOperating = buildUpActive is not null ? buildUpActive.IsOperating : false,
                Value = buildUpActive is not null ? buildUpActive.Value : 0,
            };
            var manualConfigDTO = new ManualConfigDTO
            {
                Id = manualConfig.Id,
                BuildUp = buildUpDTO,
            };
            var wellDTO = new WellWithManualConfigDTO
            {
                Id = well.Id,
                CodWell = well.CodWell,
                CodWellAnp = well.CodWellAnp,
                WellOperatorName = well.WellOperatorName,
                CategoryOperator = well.CategoryOperator,
                IsActive = well.IsActive,
                Name = well.Name,
                ManualConfig = manualConfigDTO
            };
            if (well.CategoryOperator.ToUpper().Contains("PRODUTOR"))
            {
                var productivityIndexActive = manualConfig.ProductivityIndex.Where(x => x.IsActive == true).FirstOrDefault();
                var productivityDTO = new ProductivityIndexDTO
                {
                    IsActive = productivityIndexActive is not null ? productivityIndexActive.IsActive : false,
                    IsOperating = productivityIndexActive is not null ? productivityIndexActive.IsOperating : false,
                    Value = productivityIndexActive is not null ? productivityIndexActive.Value : 0,
                };
                manualConfigDTO.ProductivityIndex = productivityDTO;
            }
            else if (well.CategoryOperator.ToUpper().Contains("INJETOR"))
            {
                var injectivityIndexActive = manualConfig.ProductivityIndex.Where(x => x.IsActive == true).FirstOrDefault();
                var injectivityDTO = new InjectivityIndexDTO
                {
                    IsActive = injectivityIndexActive is not null ? injectivityIndexActive.IsActive : false,
                    IsOperating = injectivityIndexActive is not null ? injectivityIndexActive.IsOperating : false,
                    Value = injectivityIndexActive is not null ? injectivityIndexActive.Value : 0,
                };
                manualConfigDTO.InjectivityIndex = injectivityDTO;
            }

            return wellDTO;
        }
        public async Task<WellDTO> GetWellById(Guid id)
        {
            var well = await _wellRepository.GetByIdAsync(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            var wellDTO = _mapper.Map<Well, WellDTO>(well);
            return wellDTO;
        }
        public async Task<CreateUpdateWellDTO> UpdateWell(UpdateWellViewModel body, Guid id, User user)
        {
            var well = await _wellRepository.GetByIdWithFieldAndCompletions(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is false && well.StatusOperator is false)
                throw new ConflictException(ErrorMessages.Inactive<Well>());

            if (well.Completions is not null && well.Completions.Count > 0)
                if (body.CodWellAnp is not null)
                    if (body.CodWellAnp != well.CodWellAnp)
                        throw new ConflictException(ErrorMessages.CodCantBeUpdated<Well>());

            if (well.Field is not null)
                if (well.Completions.Count > 0)
                    if (body.FieldId is not null)
                        if (body.FieldId != well.Field.Id)
                            throw new ConflictException("Campo associado não pode ser alterado.");

            if (body.CodWellAnp is not null)
            {
                var wellInDatabase = await _wellRepository.GetByCode(body.CodWellAnp);
                if (wellInDatabase is not null && wellInDatabase.Id != well.Id)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Well>());

            }
            var beforeChangesWell = _mapper.Map<WellHistoryDTO>(well);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(well, body);

            if (updatedProperties.Any() is false && (well.Field?.Id == body.FieldId || body.FieldId is null))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Well>());

            if (body.FieldId is not null && well.Field?.Id != body.FieldId)
            {
                var fieldInDatabase = await _fieldRepository.GetOnlyField(body.FieldId);

                if (fieldInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Field>());

                well.Field = fieldInDatabase;
                updatedProperties[nameof(WellHistoryDTO.fieldId)] = fieldInDatabase.Id;
            }

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, well.Id, well, beforeChangesWell);

            _wellRepository.Update(well);

            await _wellRepository.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return wellDTO;
        }
        public async Task DeleteWell(Guid id, User user, string StatusDate)
        {
            DateTime date;
            if (StatusDate is null)
            {
                throw new ConflictException("Data da inativação não informada");
            }
            else
            {
                var checkDate = DateTime.TryParse(StatusDate, out DateTime day);
                if (checkDate is false)
                    throw new ConflictException("Data não é válida.");

                var dateToday = DateTime.UtcNow.AddHours(-3);
                if (dateToday < day)
                    throw new NotFoundException("Data fornecida é maior que a data atual.");

                date = day;
            }
            var production = await _productionRepository.GetCleanByDate(date);
            if (production is not null)
                throw new ConflictException("Existe uma produção para essa data.");

            var well = await _wellRepository.GetByIdWithFieldAndCompletions(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is false && well.StatusOperator is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Well>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
                StatusOperator = false,
                InactivatedAt = date
            };

            if (well.Completions is not null)
                foreach (var completion in well.Completions)
                {
                    if (completion.IsActive is true)
                    {
                        var completionPropertiesToUpdate = new
                        {
                            IsActive = false,
                            InactivatedAt = date,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                        };

                        var completionUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableReservoirs, user, completionUpdatedProperties, completion.Id, completion);

                        _completionRepository.Delete(completion);
                    }
                }

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(well, propertiesUpdated);

            await _systemHistoryService
                .Delete<Well, WellHistoryDTO>(_tableName, user, updatedProperties, well.Id, well);

            //criação evento de fechamento
            var lastEventOfAll = well.WellEvents
                 .Where(we => we.EndDate == null)
                 .LastOrDefault();


            if (lastEventOfAll is not null && lastEventOfAll.EventStatus.ToUpper() == "A")
            {
                var lastEventOfTypeClosing = well.WellEvents
                .OrderBy(e => e.StartDate)
                .LastOrDefault(x => x.EventStatus == "F");

                int lastCode;
                var codeSequencial = string.Empty;
                if (lastEventOfTypeClosing is not null && int.TryParse(lastEventOfTypeClosing.IdAutoGenerated.Split(" ")[0].Substring(3), out lastCode))
                {
                    lastCode++;
                    codeSequencial = lastCode.ToString("0000");
                }

                if (lastEventOfTypeClosing is null)
                    codeSequencial = "0001";

                var wellEvent = new WellEvent
                {
                    Id = Guid.NewGuid(),
                    StartDate = date,
                    IdAutoGenerated = $"{well.Field?.Name?.Substring(0, 3)}{codeSequencial} {well.Name}",
                    Well = well,
                    EventStatus = "F",
                    StateANP = "4",
                    StatusANP = "Fechado",
                    CreatedBy = user

                };
                await _eventWellRepository.Add(wellEvent);

                var newEventReason = new EventReason
                {
                    Id = Guid.NewGuid(),
                    SystemRelated = "Inativo",
                    StartDate = date,
                    WellEvent = wellEvent,
                    CreatedBy = user
                };

                await _eventWellRepository.AddReasonClosedEvent(newEventReason);

                lastEventOfAll.Interval = (date - lastEventOfAll.StartDate).TotalHours;
                lastEventOfAll.EndDate = date;

                _eventWellRepository.Update(lastEventOfAll);
            }
            else if (lastEventOfAll is not null && lastEventOfAll.EventStatus.ToUpper() == "F" && lastEventOfAll.EndDate is null)
            {
                var eventReason = lastEventOfAll.EventReasons.OrderBy(x => x.StartDate).LastOrDefault();
                if (eventReason.StartDate >= date)
                    throw new ConflictException("Data da inativação não pode ser menor que data do último evento.");

                if (eventReason.StartDate < date && eventReason.EndDate is null)
                {
                    var dif = (date - lastEventOfAll.StartDate).TotalHours / 24;
                    eventReason.EndDate = eventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10);

                    var FirstresultIntervalTimeSpan = (eventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10) - eventReason.StartDate).TotalHours;
                    int FirstintervalHours = (int)FirstresultIntervalTimeSpan;
                    var FirstintervalMinutesDecimal = (FirstresultIntervalTimeSpan - FirstintervalHours) * 60;
                    int FirstintervalMinutes = (int)FirstintervalMinutesDecimal;
                    var FirstintervalSecondsDecimal = (FirstintervalMinutesDecimal - FirstintervalMinutes) * 60;
                    int FirstintervalSeconds = (int)FirstintervalSecondsDecimal;
                    string FirstReasonFormattedHours;
                    string firstFormattedMinutes = FirstintervalMinutes < 10 ? $"0{FirstintervalMinutes}" : FirstintervalMinutes.ToString();
                    string firstFormattedSecond = FirstintervalSeconds < 10 ? $"0{FirstintervalSeconds}" : FirstintervalSeconds.ToString();
                    if (FirstintervalHours >= 1000)
                    {
                        int digitCount = (int)Math.Floor(Math.Log10(FirstintervalHours)) + 1;
                        FirstReasonFormattedHours = FirstintervalHours.ToString(new string('0', digitCount));
                    }
                    else
                    {
                        FirstReasonFormattedHours = FirstintervalHours.ToString("00");
                    }
                    var FirstReasonFormattedTime = $"{FirstReasonFormattedHours}:{firstFormattedMinutes}:{firstFormattedSecond}";
                    eventReason.Interval = FirstReasonFormattedTime;

                    DateTime refStartDate = eventReason.StartDate.Date.AddDays(1);
                    DateTime refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);

                    var resultIntervalTimeSpan = (refStartEnd - refStartDate).TotalHours;
                    int intervalHours = (int)resultIntervalTimeSpan;
                    var intervalMinutesDecimal = (resultIntervalTimeSpan - intervalHours) * 60;
                    int intervalMinutes = (int)intervalMinutesDecimal;
                    var intervalSecondsDecimal = (intervalMinutesDecimal - intervalMinutes) * 60;
                    int intervalSeconds = (int)intervalSecondsDecimal;

                    for (int j = 0; j < dif; j++)
                    {
                        var newEventReason = new EventReason
                        {
                            Id = Guid.NewGuid(),
                            StartDate = refStartDate,
                            WellEvent = lastEventOfAll,
                            SystemRelated = eventReason.SystemRelated,
                            CreatedBy = user
                        };
                        if (j == 0)
                        {
                            if (date.Date == eventReason.StartDate.Date)
                            {
                                Console.WriteLine("oi");
                                eventReason.EndDate = date;
                                var Interval = FormatTimeInterval(date, eventReason);
                                eventReason.Interval = Interval;

                                newEventReason.StartDate = date;
                                newEventReason.SystemRelated = "Inativo";
                                await _eventWellRepository.AddReasonClosedEvent(newEventReason);
                                break;
                            }
                        }
                        if (date.Date == refStartDate)
                        {
                            var newEventReason2 = new EventReason
                            {
                                Id = Guid.NewGuid(),
                                SystemRelated = eventReason.SystemRelated,
                                Comment = eventReason.Comment,
                                WellEvent = lastEventOfAll,
                                StartDate = refStartDate,
                                EndDate = date,
                                IsActive = true,
                                IsJobGenerated = false,
                                CreatedBy = user
                            };
                            var Interval = FormatTimeInterval(date, newEventReason2);
                            newEventReason2.Interval = Interval;

                            newEventReason.EndDate = null;
                            newEventReason.StartDate = date;
                            newEventReason.SystemRelated = "Inativo";

                            await _eventWellRepository.AddReasonClosedEvent(newEventReason2);
                            await _eventWellRepository.AddReasonClosedEvent(newEventReason);
                            break;
                        }
                        else
                        {
                            newEventReason.EndDate = refStartEnd;
                            string ReasonFormattedMinutes = intervalMinutes < 10 ? $"0{intervalMinutes}" : intervalMinutes.ToString();
                            string ReasonFormattedSecond = intervalSeconds < 10 ? $"0{intervalSeconds}" : intervalSeconds.ToString();
                            string ReasonFormattedHours;
                            if (intervalHours >= 1000)
                            {
                                int digitCount = (int)Math.Floor(Math.Log10(intervalHours)) + 1;
                                ReasonFormattedHours = intervalHours.ToString(new string('0', digitCount));
                            }
                            else
                            {
                                ReasonFormattedHours = intervalHours.ToString("00");
                            }
                            var reasonFormattedTime = $"{ReasonFormattedHours}:{ReasonFormattedMinutes}:{ReasonFormattedSecond}";
                            newEventReason.Interval = reasonFormattedTime;
                            refStartDate = newEventReason.StartDate.AddDays(1);
                            refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);
                        }

                        await _eventWellRepository.AddReasonClosedEvent(newEventReason);
                    }
                }
            }

            _wellRepository.Update(well);

            await _wellRepository.SaveChangesAsync();
        }
        public async Task<CreateUpdateWellDTO> RestoreWell(Guid id, User user)
        {
            var well = await _wellRepository.GetByIdWithFieldAndCompletions(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is true && well.StatusOperator is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Well>());

            if (well.Field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (well.Field.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Field>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
                StatusOperator = true
            };
            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(well, propertiesUpdated);

            await _systemHistoryService
                .Restore<Well, WellHistoryDTO>(_tableName, user, updatedProperties, well.Id, well);

            _wellRepository.Update(well);

            await _wellRepository.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return wellDTO;
        }
        public async Task<List<SystemHistory>> GetWellHistory(Guid id)
        {
            var wellHistories = await _systemHistoryService.GetAll(id);

            if (wellHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            foreach (var history in wellHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return wellHistories;
        }
        private static string FormatTimeInterval(DateTime dateNow, EventReason lastEventReason)
        {
            var resultTimeSpan = (dateNow - lastEventReason.StartDate).TotalHours;

            int hours = (int)resultTimeSpan;
            var minutesDecimal = (resultTimeSpan - hours) * 60;
            int minutes = (int)minutesDecimal;
            var secondsDecimal = (minutesDecimal - minutes) * 60;
            int seconds = (int)secondsDecimal;
            string formattedMinutes = minutes < 10 ? $"0{minutes}" : minutes.ToString();
            string formattedSecond = seconds < 10 ? $"0{seconds}" : seconds.ToString();
            string formattedHours;
            if (hours >= 1000)
            {
                int digitCount = (int)Math.Floor(Math.Log10(hours)) + 1;
                formattedHours = hours.ToString(new string('0', digitCount));
            }
            else
            {
                formattedHours = hours.ToString("00");
            }

            return $"{formattedHours}:{formattedMinutes}:{formattedSecond}";
        }
        //public async Task<PaginatedDataDTO<WellDTO>> GetWellsPaginated(int pageNumber, int pageSize, string requestUrl)
        //{
        //    var totalCount = await _context.Wells.CountAsync();

        //    var wells = await _context.Wells
        //            .Include(x => x.User)
        //            .Include(x => x.Completions)
        //            .Include(x => x.Field)
        //            .ThenInclude(f => f.Installation)
        //            .ThenInclude(i => i.Cluster)
        //            .OrderBy(x => x.Id)
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize)
        //            .ToListAsync();

        //    var wellsDTO = _mapper.Map<List<Well>, List<WellDTO>>(wells);

        //    var paginatedData = new PaginatedDataDTO<WellDTO>
        //    {
        //        Count = wellsDTO.Count,
        //        Data = wellsDTO
        //    };

        //    var previousPageNumber = pageNumber > 1 ? pageNumber - 1 : 0;
        //    var nextPageNumber = pageNumber * pageSize < totalCount ? pageNumber + 1 : 0;

        //    var uriBuilder = new UriBuilder(requestUrl);
        //    var queryParams = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        //    if (previousPageNumber > 0)
        //    {
        //        queryParams.Set("pageNumber", previousPageNumber.ToString());
        //        uriBuilder.Query = queryParams.ToString();
        //        paginatedData.PreviousPage = uriBuilder.ToString();
        //    }

        //    if (nextPageNumber > 0)
        //    {
        //        queryParams.Set("pageNumber", nextPageNumber.ToString());
        //        uriBuilder.Query = queryParams.ToString();
        //        paginatedData.NextPage = uriBuilder.ToString();
        //    }

        //    return paginatedData;
        //}
    }
}
