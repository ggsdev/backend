using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels;
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

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Services
{

    public class ReservoirService
    {
        private readonly IMapper _mapper;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly IWellEventRepository _eventWellRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly IProductionRepository _productionRepository;

        public ReservoirService(IMapper mapper, IReservoirRepository reservoirRepository, IZoneRepository zoneRepository, SystemHistoryService systemHistoryService, IWellRepository wellRepository, IWellEventRepository wellEventRepository, IProductionRepository productionRepository)
        {
            _mapper = mapper;
            _reservoirRepository = reservoirRepository;
            _zoneRepository = zoneRepository;
            _systemHistoryService = systemHistoryService;
            _wellRepository = wellRepository;
            _eventWellRepository = wellEventRepository;
            _productionRepository = productionRepository;
        }

        public async Task<CreateUpdateReservoirDTO> CreateReservoir(CreateReservoirViewModel body, User user)
        {

            var zoneInDatabase = await _zoneRepository.GetOnlyZone(body.ZoneId);

            if (zoneInDatabase is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zoneInDatabase.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Zone>());

            var reservoirSameName = await _reservoirRepository.GetByNameAsync(body.Name);
            if (reservoirSameName is not null)
                throw new ConflictException($"Já existe um reservatório com o nome: {body.Name}.");

            var reservoirId = Guid.NewGuid();

            var reservoir = new Reservoir
            {
                Id = reservoirId,
                Name = body.Name,
                Description = body.Description,
                Zone = zoneInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _reservoirRepository.AddAsync(reservoir);

            await _systemHistoryService
                .Create<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, reservoirId, reservoir);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);

            return reservoirDTO;
        }

        public async Task<List<ReservoirDTO>> GetReservoirs(User user)
        {
            var reservoirs = await _reservoirRepository.GetAsync(user);

            var reservoirsDTO = _mapper.Map<List<Reservoir>, List<ReservoirDTO>>(reservoirs);
            return reservoirsDTO;
        }

        public async Task<ReservoirDTO> GetReservoirById(Guid id)
        {
            var reservoir = await _reservoirRepository.GetByIdAsync(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return reservoirDTO;
        }

        public async Task<CreateUpdateReservoirDTO> UpdateReservoir(UpdateReservoirViewModel body, Guid id, User user)
        {
            var reservoir = await _reservoirRepository
                .GetByIdWithCompletionsAsync(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Reservoir>());


            var beforeChangesReservoir = _mapper.Map<ReservoirHistoryDTO>(reservoir);

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(reservoir, body);

            if (updatedProperties.Any() is false && (body.ZoneId is null || body.ZoneId == reservoir.Zone?.Id))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Zone>());

            if (body.ZoneId is not null && reservoir.Zone?.Id != body.ZoneId)
            {
                var zoneInDatabase = await _zoneRepository.GetOnlyZone(body.ZoneId);

                if (zoneInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Zone>());

                reservoir.Zone = zoneInDatabase;
                updatedProperties[nameof(ReservoirHistoryDTO.zoneId)] = zoneInDatabase.Id;
            }

            _reservoirRepository.Update(reservoir);

            await _systemHistoryService
                .Update(HistoryColumns.TableReservoirs, user, updatedProperties, reservoir.Id, reservoir, beforeChangesReservoir);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);
            return reservoirDTO;
        }

        public async Task DeleteReservoir(Guid id, User user, string StatusDate)
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

            var reservoir = await _reservoirRepository
                .GetReservoirAndChildren(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Reservoir>());

            var propertiesUpdated = new
            {
                IsActive = false,
                InactivatedAt = date,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
            };

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
                            .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableReservoirs, user, completionUpdatedProperties, completion.Id, completion);


                        var well = await _wellRepository.GetByIdWithFieldAndCompletions(completion.Well.Id);
                        var completionsActive = well.Completions.Where(c => c.IsActive == true && c.Id != completion.Id);

                        if (!completionsActive.Any())
                        {
                            var lastEventOfAll = well.WellEvents
                                .Where(we => we.EndDate == null)
                                .LastOrDefault();

                            if (lastEventOfAll is not null && lastEventOfAll.EventStatus.ToUpper() == "A")
                            {
                                var codeSequencial = string.Empty;
                                if (int.TryParse(lastEventOfAll.IdAutoGenerated.Split(" ")[0][3..], out int lastCode))
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


                        _reservoirRepository.Delete(reservoir);
                    }
                }
            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(reservoir, propertiesUpdated);

            await _systemHistoryService
                .Delete<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, updatedProperties, reservoir.Id, reservoir);

            _reservoirRepository.Update(reservoir);

            await _reservoirRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateReservoirDTO> RestoreReservoir(Guid id, User user)
        {
            var reservoir = await _reservoirRepository.GetWithZoneAsync(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is true)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Reservoir>());

            if (reservoir.Zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (reservoir.Zone.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Zone>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(reservoir, propertiesUpdated);

            await _systemHistoryService
                .Restore<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, updatedProperties, reservoir.Id, reservoir);

            _reservoirRepository.Update(reservoir);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);
            return reservoirDTO;
        }

        public async Task<List<SystemHistory>> GetReservoirHistory(Guid id)
        {
            var reservoirHistories = await _systemHistoryService.GetAll(id);

            if (reservoirHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            foreach (var history in reservoirHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return reservoirHistories;
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
