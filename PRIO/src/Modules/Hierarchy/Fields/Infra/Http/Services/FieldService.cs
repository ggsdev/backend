using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Services
{
    public class FieldService
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableFields;
        private readonly IZoneRepository _zoneRepository;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly IWellRepository _wellRepository;
        private readonly ICompletionRepository _completionRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IWellEventRepository _eventWellRepository;
        private readonly List<string> _wellFilters = new() { "PRODUTOR", "INJETOR" };

        public FieldService(IMapper mapper, IFieldRepository fieldRepository, SystemHistoryService systemHistoryService, IInstallationRepository installationRepository, IZoneRepository zoneRepository, IWellRepository wellRepository, ICompletionRepository completionRepository, IReservoirRepository reservoirRepository, IWellEventRepository wellEventRepository, IProductionRepository productionRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _installationRepository = installationRepository;
            _systemHistoryService = systemHistoryService;
            _zoneRepository = zoneRepository;
            _wellRepository = wellRepository;
            _completionRepository = completionRepository;
            _reservoirRepository = reservoirRepository;
            _eventWellRepository = wellEventRepository;
            _productionRepository = productionRepository;
        }

        public async Task<CreateUpdateFieldDTO> CreateField(CreateFieldViewModel body, User user)
        {
            var fieldExistingCode = await _fieldRepository.GetByCod(body.CodField);
            if (fieldExistingCode is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Field>());

            var installationInDatabase = await _installationRepository
                .GetByIdAsync(body.InstallationId) ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installationInDatabase.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Installation>());

            var fieldSameName = await _fieldRepository.GetByNameAsync(body.Name);
            if (fieldSameName is not null)
                throw new ConflictException($"Já existe um campo com o nome: {body.Name}.");

            var fieldId = Guid.NewGuid();

            var field = new Field
            {
                Id = fieldId,
                Name = body.Name,
                User = user,
                Description = body.Description,
                Basin = body.Basin,
                Location = body.Location,
                State = body.State,
                CodField = body.CodField,
                Installation = installationInDatabase,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _fieldRepository.AddAsync(field);

            await _systemHistoryService
                .Create<Field, FieldHistoryDTO>(_tableName, user, fieldId, field);

            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);
            return fieldDTO;
        }

        public async Task<List<FieldDTO>> GetFields(User user)
        {
            var fields = await _fieldRepository.GetAsync(user);

            var fieldsDTO = _mapper.Map<List<Field>, List<FieldDTO>>(fields);
            return fieldsDTO;
        }

        public async Task<FieldDTO> GetFieldById(Guid id, string? wellFilter)
        {
            var field = await _fieldRepository
                .GetByIdAsync(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (wellFilter is not null)
            {
                var verify = _wellFilters.Contains(wellFilter.Trim().ToUpper());
                if (!verify)
                {
                    string possibleFilters = string.Join(", ", _wellFilters);
                    throw new ConflictException("O filtro fornecido não é válido. Os filtros possíveis são: " + possibleFilters);
                }
                field.Wells = field.Wells.Where(x => x.CategoryOperator.Trim().ToUpper().Contains(wellFilter.Trim().ToUpper())).ToList();
            }


            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);
            return fieldDTO;
        }
        public async Task<CreateUpdateFieldDTO> UpdateField(Guid id, UpdateFieldViewModel body, User user)
        {
            var field = await _fieldRepository.GetByIdWithWellsAndZonesAsync(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Field>());

            if (field.Wells.Count > 0 || field.Zones.Count > 0)
                if (body.CodField is not null)
                    if (body.CodField != field.CodField)
                        throw new ConflictException(ErrorMessages.CodCantBeUpdated<Field>());

            if (field.Installation is not null)
                if (field.Wells is not null || field.Zones is not null)
                    if (body.InstallationId is not null)
                        if (body.InstallationId != field.Installation.Id)
                            throw new ConflictException("Relacionamento não pode ser alterado.");

            if (body.CodField is not null)
            {
                var fieldInDatabase = await _fieldRepository.GetByCod(body.CodField);
                if (fieldInDatabase is not null && fieldInDatabase.Id != field.Id)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Field>());
            }

            if (body.Name is not null)
            {
                var fieldInDatabase = await _fieldRepository.GetByNameAsync(body.Name);
                if (fieldInDatabase is not null && fieldInDatabase.Id != field.Id)
                    throw new ConflictException($"Já existe um campo com esse nome: {body.Name}");
            }

            var beforeChangesField = _mapper.Map<FieldHistoryDTO>(field);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(field, body);

            if (updatedProperties.Any() is false && (body.InstallationId is null || field.Installation?.Id == body.InstallationId))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Installation>());

            if (body.InstallationId is not null && field.Installation?.Id != body.InstallationId)
            {
                var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId);

                if (installationInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Installation>());

                field.Installation = installationInDatabase;
                updatedProperties[nameof(FieldHistoryDTO.installationId)] = installationInDatabase.Id;
            }

            _fieldRepository.Update(field);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, field.Id, field, beforeChangesField);

            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);

            return fieldDTO;
        }
        public async Task DeleteField(Guid id, User user, string StatusDate)
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

            var field = await _fieldRepository.GetFieldAndChildren(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Field>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
                InactivatedAt = date
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(field, propertiesUpdated);

            _fieldRepository.Update(field);

            await _systemHistoryService
                .Delete<Field, FieldHistoryDTO>(_tableName, user, updatedProperties, field.Id, field);

            if (field.Zones is not null)
                foreach (var zone in field.Zones)
                {
                    if (zone.IsActive is true)
                    {
                        var zonePropertiesToUpdate = new
                        {
                            IsActive = false,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                            InactivatedAt = date
                        };

                        var zoneUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(zone, zonePropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Zone, ZoneHistoryDTO>(HistoryColumns.TableZones, user, zoneUpdatedProperties, zone.Id, zone);

                        _zoneRepository.Delete(zone);
                    }

                    if (zone.Reservoirs is not null)
                        foreach (var reservoir in zone.Reservoirs)
                        {
                            if (reservoir.IsActive is true)
                            {
                                var reservoirPropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                    InactivatedAt = date
                                };

                                var reservoirUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(reservoir, reservoirPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, reservoirUpdatedProperties, reservoir.Id, reservoir);

                                _reservoirRepository.Delete(reservoir);
                            }

                            if (reservoir.Completions is not null)
                                foreach (var completion in reservoir.Completions)
                                {
                                    if (completion.IsActive is true)
                                    {
                                        var completionPropertiesToUpdate = new
                                        {
                                            IsActive = false,
                                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                                            InactivatedAt = date
                                        };

                                        var completionUpdatedProperties = UpdateFields
                                        .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                                        await _systemHistoryService
                                            .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionUpdatedProperties, completion.Id, completion);

                                        _completionRepository.Delete(completion);
                                    }
                                }
                        }
                }

            if (field.Wells is not null)
                foreach (var well in field.Wells)
                {
                    if (well.IsActive is true)
                    {
                        var wellPropertiesToUpdate = new
                        {
                            IsActive = false,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                            InactivatedAt = date
                        };

                        var wellUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(well, wellPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, wellUpdatedProperties, well.Id, well);

                        var lastEventOfAll = well.WellEvents
                           .Where(we => we.EndDate == null)
                           .LastOrDefault();

                        if (lastEventOfAll is not null && lastEventOfAll.EventStatus.ToUpper() == "A")
                        {

                            var codeSequencial = string.Empty;
                            if (int.TryParse(lastEventOfAll.IdAutoGenerated.Split(" ")[0].AsSpan(3), out int lastCode))
                            {
                                lastCode++;
                                codeSequencial = lastCode.ToString("0000");
                            }

                            var wellEvent = new WellEvent
                            {
                                Id = Guid.NewGuid(),
                                StartDate = date,
                                IdAutoGenerated = $"{well.Field?.Name?[..3]}{codeSequencial}",
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

                        _wellRepository.Delete(well);
                    }

                    if (well.Completions is not null)
                        foreach (var completion in well.Completions)
                        {
                            if (completion.IsActive is true)
                            {
                                var completionPropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var completionUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionUpdatedProperties, completion.Id, completion);

                                _completionRepository.Delete(completion);

                            }
                        }
                }

            await _fieldRepository.SaveChangesAsync();
        }
        public async Task<CreateUpdateFieldDTO> RestoreField(Guid id, User user)
        {
            var field = await _fieldRepository.GetByIdAsync(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Field>());

            if (field.Installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (field.Installation.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Installation>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(field, propertiesUpdated);

            await _systemHistoryService
                .Restore<Field, FieldHistoryDTO>(_tableName, user, updatedProperties, field.Id, field);

            _fieldRepository.Update(field);
            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);
            return fieldDTO;
        }
        public async Task<List<SystemHistory>> GetFieldHistory(Guid id)
        {
            var fieldHistories = await _systemHistoryService
                .GetAll(id);

            if (fieldHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            foreach (var history in fieldHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()) : null;
            }

            return fieldHistories;
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
    }
}