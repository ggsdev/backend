using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Dtos;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.ViewModels;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Zones.Infra.Http.Services
{
    public class ZoneService
    {
        private readonly IMapper _mapper;
        private readonly string _tableName = HistoryColumns.TableZones;
        private readonly IZoneRepository _zoneRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly ICompletionRepository _completionRepository;
        private readonly IWellEventRepository _eventWellRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IProductionRepository _productionRepository;

        public ZoneService(IMapper mapper, SystemHistoryService systemHistoryService, IFieldRepository fieldRepository, IZoneRepository zoneRepository, IReservoirRepository reservoirRepository, ICompletionRepository completionRepository, IWellRepository wellRepository, IWellEventRepository wellEventRepository, IProductionRepository productionRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _zoneRepository = zoneRepository;
            _systemHistoryService = systemHistoryService;
            _reservoirRepository = reservoirRepository;
            _completionRepository = completionRepository;
            _wellRepository = wellRepository;
            _eventWellRepository = wellEventRepository;
            _productionRepository = productionRepository;
        }

        public async Task<CreateUpdateZoneDTO> CreateZone(CreateZoneViewModel body, User user)
        {
            var zoneInDatabase = await _zoneRepository.GetByCode(body.CodZone);

            if (zoneInDatabase is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Zone>());

            var field = await _fieldRepository.GetByIdAsync(body.FieldId);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Field>());

            var zoneId = Guid.NewGuid();

            var zone = new Zone
            {
                Id = zoneId,
                CodZone = body.CodZone,
                Field = field,
                Description = body.Description,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };
            await _zoneRepository.AddAsync(zone);

            await _systemHistoryService
                .Create<Zone, ZoneHistoryDTO>(_tableName, user, zoneId, zone);

            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);

            return zoneDTO;
        }

        public async Task<List<ZoneDTO>> GetZones()
        {
            var zones = await _zoneRepository.GetAsync();

            var zonesDTO = _mapper.Map<List<Zone>, List<ZoneDTO>>(zones);
            return zonesDTO;
        }

        public async Task<ZoneDTO> GetZoneById(Guid id)
        {
            var zone = await _zoneRepository.GetByIdAsync(id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<CreateUpdateZoneDTO> UpdateZone(UpdateZoneViewModel body, Guid id, User user)
        {
            var zone = await _zoneRepository.GetByIdWithReservoirsAsync(id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zone.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Zone>());

            if (zone.Reservoirs is not null)
                if (zone.Reservoirs.Count > 0)
                    if (body.CodZone is not null)
                        if (body.CodZone != zone.CodZone)
                            throw new ConflictException(ErrorMessages.CodCantBeUpdated<Zone>());

            if (zone.Reservoirs is not null)
                if (zone.Reservoirs.Count > 0)
                    if (body.FieldId is not null)
                        if (body.FieldId != zone.Field.Id)
                            throw new ConflictException("Relacionamento não pode ser alterado.");

            if (body.CodZone is not null)
            {
                var zoneInDatabase = await _zoneRepository.GetByCode(body.CodZone);
                if (zoneInDatabase is not null && zoneInDatabase.Id != zone.Id)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Zone>());
            }

            var beforeChangesZone = _mapper.Map<ZoneHistoryDTO>(zone);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(zone, body);

            if (updatedProperties.Any() is false && (zone.Field?.Id == body.FieldId || body.FieldId is null))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Zone>());

            if (body.FieldId is not null && zone.Field?.Id != body.FieldId)
            {
                var field = await _fieldRepository.GetOnlyField(body.FieldId);

                if (field is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Field>());

                zone.Field = field;
                updatedProperties[nameof(ZoneHistoryDTO.fieldId)] = field.Id;
            }


            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, zone.Id, zone, beforeChangesZone);

            _zoneRepository.Update(zone);
            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task DeleteZone(Guid id, User user, string StatusDate)
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

            var zone = await _zoneRepository
                .GetZoneAndChildren(id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zone.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Zone>());

            var propertiesUpdated = new
            {
                IsActive = false,
                InactivatedAt = date,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(zone, propertiesUpdated);

            _zoneRepository.Update(zone);

            await _systemHistoryService
                .Delete<Zone, ZoneHistoryDTO>(_tableName, user, updatedProperties, zone.Id, zone);

            if (zone.Reservoirs is not null)
                foreach (var reservoir in zone.Reservoirs)
                {
                    if (reservoir.IsActive is true)
                    {
                        var reservoirPropertiesToUpdate = new
                        {
                            IsActive = false,
                            InactivatedAt = date,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
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
                                    InactivatedAt = date,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var completionUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionUpdatedProperties, completion.Id, completion);


                                var well = await _wellRepository.GetByIdWithFieldAndCompletions(completion.Well.Id);
                                var completionsActive = well.Completions.Where(c => c.IsActive == true && c.Id != completion.Id);

                                if (completionsActive.Count() == 0)
                                {
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
                                            SystemRelated = "Completações Inativas",
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
                                                        eventReason.EndDate = date;
                                                        var Interval = FormatTimeInterval(date, eventReason);
                                                        eventReason.Interval = Interval;

                                                        newEventReason.StartDate = date;
                                                        newEventReason.SystemRelated = "Completações Inativas";
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
                                                    newEventReason.SystemRelated = "Completações Inativas";

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
                                }

                                _completionRepository.Delete(completion);

                            }
                        }
                }

            await _zoneRepository.SaveChangesAsync();
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

        public async Task<CreateUpdateZoneDTO> RestoreZone(Guid id, User user)
        {
            var zone = await _zoneRepository.GetWithField(id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zone.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Zone>());

            if (zone.Field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (zone.Field.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Field>());


            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };
            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(zone, propertiesUpdated);

            await _systemHistoryService
                .Restore<Zone, ZoneHistoryDTO>(_tableName, user, updatedProperties, zone.Id, zone);

            _zoneRepository.Update(zone);

            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<List<SystemHistory>> GetZoneHistory(Guid id)
        {
            var zoneHistories = await _systemHistoryService.GetAll(id);

            if (zoneHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            foreach (var history in zoneHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return zoneHistories;
        }
    }
}
