using AutoMapper;
using dotenv.net;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Dtos;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.ViewModels;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared.Errors;
using System.Data;
using System.Globalization;

namespace PRIO.src.Modules.Measuring.WellEvents.Infra.Http.Services
{
    public class WellEventService
    {
        private readonly IWellEventRepository _wellEventRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IBTPRepository _btpRepository;
        private readonly IWellProductionRepository _wellProductionRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;
        private readonly IDictionary<string, string> variablesEnv = DotEnv.Read();
        public WellEventService(IWellEventRepository wellEventRepository, IInstallationRepository installationRepository, IFieldRepository fieldRepository, IWellRepository wellRepository, IProductionRepository productionRepository, IWellProductionRepository wellProductionRepository, IBTPRepository bTPRepository, IMapper mapper)
        {
            _wellEventRepository = wellEventRepository;
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
            _wellRepository = wellRepository;
            _productionRepository = productionRepository;
            _wellProductionRepository = wellProductionRepository;
            _btpRepository = bTPRepository;
            _mapper = mapper;
        }
        public async Task CloseWellEvent(CreateClosingEventViewModel body, User loggedUser)
        {
            var appDate = variablesEnv["APPLICATIONSTARTDATE"];
            var convertAppDate = DateTime.Parse(appDate);

            if (DateTime.TryParseExact(body.EventDateAndHour, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is false)
                throw new BadRequestException("Formato de data inválido deve ser 'dd/MM/yy HH:mm'.");

            var productionInDate = await _productionRepository.GetExistingByDate(parsedStartDate);

            if (productionInDate is not null && (productionInDate.WellProductions is null || productionInDate.WellProductions.Any()))
                throw new ConflictException("Não é possível cadastrar nessa data, a produção já foi apropriada.");

            var dateNow = DateTime.UtcNow.AddHours(-3);

            if (parsedStartDate > dateNow)
                throw new ConflictException("Não é possível cadastrar um evento no futuro.");

            if (parsedStartDate < convertAppDate)
                throw new ConflictException("Data do evento está menor que data do inicio da aplicação ");

            var wellsList = new List<Well>();
            var wellsInactive = new List<Well>();
            var completionsInactive = new List<Completion>();
            var lastEventWrongList = new List<string>();

            foreach (var well in body.Wells)
            {
                var foundWell = await _wellRepository.GetByIdWithFieldAndCompletions(well.WellId);
                if (!foundWell.IsActive)
                    wellsInactive.Add(foundWell);
                else
                {
                    var inactiveCompletions = foundWell.Completions.Where(completion => completion.IsActive).ToList();
                    if (inactiveCompletions.Count == 0)
                    {
                        completionsInactive.AddRange(inactiveCompletions);
                    }

                }
            }

            if (wellsInactive.Count() != 0)
                throw new ConflictException("Erro: Poços precisam estar ativos para criação de evento de fechamento.");


            if (wellsInactive.Count() != 0)
                throw new ConflictException("Erro: Poços precisam estar ativos para criação de evento de fechamento.");


            foreach (var well in body.Wells)
            {
                var wellInDatabase = await _wellRepository
                    .GetWithFieldAsync(well.WellId);

                if (wellInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Well>());

                if (wellInDatabase.IsActive is false)
                    throw new BadRequestException($"Poço: {wellInDatabase.Name} está inativo.");

                var lastEvent = wellInDatabase.WellEvents
                    .Where(x => x.EndDate == null)
                    .LastOrDefault();

                if (lastEvent is null)
                    throw new ConflictException($"O poço: {wellInDatabase.Name} não possui um evento de abertura anterior.");

                if (lastEvent.EventStatus != "A")
                {
                    lastEventWrongList.Add($"Poço: {wellInDatabase.Name}");
                    continue;
                }

                if (lastEvent is not null && parsedStartDate < lastEvent.StartDate)
                    throw new BadRequestException("Data de início do evento deve ser maior que a data de início do último evento associado.");

                wellsList.Add(wellInDatabase);
            }

            if (lastEventWrongList.Count > 0)
                throw new BadRequestException(message: "O último evento do poço deve ser de abertura para que seja possível cadastrar um evento de fechamento.", errors: lastEventWrongList);

            foreach (var well in wellsList)
            {
                var lastEvent = well.WellEvents
                    .Where(e => e.EndDate == null)
                    .LastOrDefault();

                var lastEventOfTypeClosing = await _wellEventRepository
                .GetLastWellEvent("F");

                var codeSequencial = string.Empty;

                if (lastEventOfTypeClosing is not null && int.TryParse(lastEventOfTypeClosing.IdAutoGenerated.Split(" ")[0][3..], out int lastCode))
                {
                    lastCode++;
                    codeSequencial = lastCode.ToString("0000");
                }
                else
                {
                    codeSequencial = "0001";
                }

                var closingEvent = new WellEvent
                {
                    Id = Guid.NewGuid(),
                    EventRelated = lastEvent,
                    EventStatus = "F",
                    StartDate = parsedStartDate,
                    Reason = body.Reason,
                    IdAutoGenerated = $"{well?.Field?.Name?[..3]}{codeSequencial}",
                    StateANP = body.StateAnp,
                    StatusANP = body.StatusAnp,
                    Well = well!,
                    EventRelatedCode = body.EventRelatedCode,
                    CreatedBy = loggedUser,
                    UpdatedBy = loggedUser

                };

                await _wellEventRepository.Add(closingEvent);

                var eventReason = new EventReason
                {
                    Id = Guid.NewGuid(),
                    SystemRelated = body.SystemRelated,
                    StartDate = parsedStartDate,
                    WellEvent = closingEvent,
                    CreatedBy = loggedUser,
                    UpdatedBy = loggedUser
                };

                await _wellEventRepository.AddReasonClosedEvent(eventReason);

                if (lastEvent is not null)
                {
                    lastEvent.EndDate = parsedStartDate;
                    lastEvent.Interval = (parsedStartDate - lastEvent.StartDate).TotalHours;

                    _wellEventRepository.Update(lastEvent);
                }

            }

            await _wellEventRepository.Save();
        }

        public async Task<ClosingEventDto> GetUepsForWellEvent(User user)
        {
            var ueps = await _installationRepository
                .GetUEPsAsync();

            var installations = await _installationRepository
                .GetAsync(user);

            var uepsList = new List<UepDto>();

            foreach (var uep in ueps)
            {
                var uepDto = new UepDto
                {
                    UepName = uep.Name,
                    UepCod = uep.UepCod,
                    UepId = uep.Id,
                    Installations = new()
                };

                uepsList.Add(uepDto);
            }

            foreach (var uep in uepsList)
            {
                foreach (var installation in installations)
                {
                    var installationDto = new InstallationWithFieldsOnlyDto
                    {
                        CodInstallationAnp = installation.CodInstallationAnp,
                        InstallationId = installation.Id,
                        GasSafetyBurnVolume = installation.GasSafetyBurnVolume,
                        Name = installation.Name,
                        Fields = new()
                    };

                    foreach (var field in installation.Fields)
                    {
                        var wellDtoList = new List<WellWithEventDto>();

                        foreach (var well in field.Wells)
                        {
                            var lastEvent = well.WellEvents
                                .OrderBy(e => e.CreatedAt)
                                .LastOrDefault();

                            if (lastEvent is not null && lastEvent.EventStatus == "A")
                            {
                                var wellDto = new WellWithEventDto
                                {
                                    EventId = lastEvent.Id,
                                    AutoGeneratedId = lastEvent.IdAutoGenerated,
                                    WellId = well.Id,
                                    Status = lastEvent.EventStatus,
                                    DateLastStatus = lastEvent.StartDate.ToString("dd/MM/yyyy HH:mm"),
                                    Name = well.Name,
                                    WellStatus = well.IsActive,
                                    CategoryOperator = well.CategoryOperator,
                                };

                                wellDtoList.Add(wellDto);
                            }
                        }

                        var fieldDto = new FieldWithWellAndWellEventsDto
                        {
                            Name = field.Name,
                            FieldId = field.Id,
                            Wells = wellDtoList,
                        };

                        installationDto.Fields.Add(fieldDto);
                    }

                    uep.Installations.Add(installationDto);
                }
            }

            var closingEventDto = new ClosingEventDto { Ueps = uepsList };

            return closingEventDto;
        }

        public async Task OpenWellEvent(CreateOpeningEventViewModel body, User loggedUser)
        {
            var appDate = variablesEnv["APPLICATIONSTARTDATE"];
            var convertAppDate = DateTime.Parse(appDate);
            if (DateTime.TryParseExact(body.EventDateAndHour, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is false)
                throw new BadRequestException("Formato de data inválido deve ser 'dd/MM/yy HH:mm'.");

            var productionInDate = await _productionRepository.GetExistingByDate(parsedStartDate);

            if (productionInDate is not null && (productionInDate.WellProductions is null || productionInDate.WellProductions.Any()))
                throw new ConflictException("Não é possível cadastrar nessa data, a produção já foi apropriada.");

            var dateNow = DateTime.UtcNow.AddHours(-3);

            if (parsedStartDate > dateNow)
                throw new ConflictException("Não é possível cadastrar um evento no futuro");

            var wellsList = new List<Well>();

            var well = await _wellRepository
                .GetWithFieldAsync(body.WellId)
                ?? throw new NotFoundException(ErrorMessages.NotFound<Well>());

            var countCompletions = well.Completions.Where(c => c.IsActive).ToList().Count();

            if (well.IsActive is false)
                throw new ConflictException("Erro: Poço precisa estar ativo para criação de evento de abertura.");

            if (countCompletions == 0)
                throw new ConflictException("Erro: Poço precisa ter completações ativas para criação de evento de abertura.");


            if (parsedStartDate < convertAppDate)
                throw new ConflictException("Data do evento está menor que data do inicio da aplicação");

            var lastEvent = well.WellEvents
                .OrderBy(e => e.StartDate)
                .Where(x => x.EndDate == null)
                .LastOrDefault();

            if (lastEvent is not null && lastEvent.EventStatus == "A")
                throw new ConflictException("O último evento do poço deve ser de fechamento para que seja possível cadastrar um evento de abertura.");


            if (parsedStartDate < lastEvent.StartDate)
                throw new BadRequestException("Data de início do evento deve ser maior que a data de início do último evento associado.");

            var lastEventOfTypeOpening = await _wellEventRepository
                .GetLastWellEvent("A");

            var codeSequencial = string.Empty;

            if (lastEventOfTypeOpening is not null && int.TryParse(lastEventOfTypeOpening.IdAutoGenerated.Split(" ")[0][3..], out int lastCode))
            {
                lastCode++;
                codeSequencial = lastCode.ToString("0000");
            }
            else
            {
                codeSequencial = "0001";
            }

            var lastEventReasonGeneratedByJob = lastEvent.EventReasons
                .OrderBy(x => x.StartDate)
                .LastOrDefault();

            if (lastEventReasonGeneratedByJob is not null && lastEventReasonGeneratedByJob.StartDate > parsedStartDate.Date && lastEventReasonGeneratedByJob.IsJobGenerated)
            {
                var list = lastEvent.EventReasons
                    .OrderBy(x => x.StartDate)
                    .TakeLast(2)
                    .ToList();

                if (lastEvent.StartDate > parsedStartDate)
                    throw new ConflictException("A data do evento de abertura deve ser maior que a ultima data de fechamento.");

                if (lastEventReasonGeneratedByJob.EndDate is not null)
                    throw new ConflictException("Última justificativa para fechamento do poço deve estar em aberto.");

                var beforeLastEventReasonGeneratedByJob = list[0];
                if (parsedStartDate < beforeLastEventReasonGeneratedByJob.StartDate)
                    throw new ConflictException("Existe um evento de fechamento atualizado pelo sistema, o evento de abertura deve estar entre esse e seu antecessor.");
                _wellEventRepository.DeleteReason(lastEventReasonGeneratedByJob);

                beforeLastEventReasonGeneratedByJob.EndDate = parsedStartDate;
                beforeLastEventReasonGeneratedByJob.Interval = FormatTimeInterval(parsedStartDate, beforeLastEventReasonGeneratedByJob);

                _wellEventRepository.UpdateReason(beforeLastEventReasonGeneratedByJob);
                var openingEvent = new WellEvent
                {
                    Id = Guid.NewGuid(),
                    EventRelated = lastEvent,
                    EventStatus = "A",
                    StartDate = parsedStartDate,
                    Reason = body.Reason,
                    IdAutoGenerated = $"{well.Field?.Name?[..3]}{codeSequencial}",
                    StateANP = body.StateAnp,
                    StatusANP = body.StatusAnp,
                    Well = well,
                    CreatedBy = loggedUser,
                    UpdatedBy = loggedUser,
                };
                await _wellEventRepository.Add(openingEvent);

                lastEvent.EndDate = parsedStartDate;
                lastEvent.Interval = (parsedStartDate - lastEvent.StartDate).TotalHours;
                _wellEventRepository.Update(lastEvent);

                var production = await _productionRepository
                    .GetExistingByDate(parsedStartDate);

            }
            else
            {
                var openingEvent = new WellEvent
                {
                    Id = Guid.NewGuid(),
                    EventRelated = lastEvent,
                    EventStatus = "A",
                    StartDate = parsedStartDate,
                    Reason = body.Reason,
                    IdAutoGenerated = $"{well.Field?.Name?[..3]}{codeSequencial}",
                    StateANP = body.StateAnp,
                    StatusANP = body.StatusAnp,
                    Well = well,
                    CreatedBy = loggedUser,
                    UpdatedBy = loggedUser,

                };

                await _wellEventRepository.Add(openingEvent);
                if (lastEvent is not null)
                {
                    lastEvent.EndDate = parsedStartDate;
                    lastEvent.Interval = (parsedStartDate - lastEvent.StartDate)
                        .TotalHours;

                    var lastEventReason = lastEvent.EventReasons
                        .OrderBy(x => x.StartDate)
                        .LastOrDefault();


                    if (lastEventReason is not null)
                    {

                        if (lastEventReason.StartDate <= parsedStartDate && lastEventReason.EndDate is null)
                        {
                            var dif = (parsedStartDate - lastEventReason.StartDate).TotalHours / 24;
                            lastEventReason.EndDate = lastEventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10);

                            var firstresultIntervalTimeSpan = (lastEventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10) - lastEventReason.StartDate).TotalHours;
                            int firstintervalHours = (int)firstresultIntervalTimeSpan;
                            var firstintervalMinutesDecimal = (firstresultIntervalTimeSpan - firstintervalHours) * 60;
                            int firstintervalMinutes = (int)firstintervalMinutesDecimal;
                            var firstintervalSecondsDecimal = (firstintervalMinutesDecimal - firstintervalMinutes) * 60;
                            int firstintervalSeconds = (int)firstintervalSecondsDecimal;
                            string firstReasonFormattedHours;
                            string firstFormattedMinutes = firstintervalMinutes < 10 ? $"0{firstintervalMinutes}" : firstintervalMinutes.ToString();
                            string firstFormattedSecond = firstintervalSeconds < 10 ? $"0{firstintervalSeconds}" : firstintervalSeconds.ToString();
                            if (firstintervalHours >= 1000)
                            {
                                int digitCount = (int)Math.Floor(Math.Log10(firstintervalHours)) + 1;
                                firstReasonFormattedHours = firstintervalHours.ToString(new string('0', digitCount));
                            }
                            else
                            {
                                firstReasonFormattedHours = firstintervalHours.ToString("00");
                            }
                            var firstReasonFormattedTime = $"{firstReasonFormattedHours}:{firstFormattedMinutes}:{firstFormattedSecond}";
                            lastEventReason.Interval = firstReasonFormattedTime;

                            DateTime refStartDate = lastEventReason.StartDate.Date.AddDays(1);
                            DateTime refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);

                            var resultIntervalTimeSpan = (refStartEnd - refStartDate).TotalHours;
                            int intervalHours = (int)resultIntervalTimeSpan;
                            var intervalMinutesDecimal = (resultIntervalTimeSpan - intervalHours) * 60;
                            int intervalMinutes = (int)intervalMinutesDecimal;
                            var intervalSecondsDecimal = (intervalMinutesDecimal - intervalMinutes) * 60;
                            int intervalSeconds = (int)intervalSecondsDecimal;

                            for (int j = 0; j <= dif; j++)
                            {
                                var newEventReason = new EventReason
                                {
                                    Id = Guid.NewGuid(),
                                    SystemRelated = lastEventReason.SystemRelated,
                                    Comment = lastEventReason.Comment,
                                    WellEvent = lastEvent,
                                    StartDate = refStartDate,
                                    IsActive = true,
                                    IsJobGenerated = false,
                                    CreatedBy = loggedUser,
                                    UpdatedBy = loggedUser
                                };
                                if (j == 0)
                                {
                                    if (parsedStartDate.Date == lastEventReason.StartDate.Date)
                                    {
                                        lastEventReason.EndDate = parsedStartDate;
                                        var Interval = FormatTimeInterval(parsedStartDate, lastEventReason);
                                        lastEventReason.Interval = Interval;

                                        break;
                                    }
                                }


                                if (parsedStartDate.Date == refStartDate)
                                {
                                    var newEventReason2 = new EventReason
                                    {
                                        Id = Guid.NewGuid(),
                                        SystemRelated = lastEventReason.SystemRelated,
                                        Comment = lastEventReason.Comment,
                                        WellEvent = lastEvent,
                                        StartDate = refStartDate,
                                        EndDate = parsedStartDate,
                                        IsActive = true,
                                        IsJobGenerated = false,
                                        CreatedBy = loggedUser,
                                        UpdatedBy = loggedUser
                                    };
                                    var Interval = FormatTimeInterval(parsedStartDate, newEventReason2);
                                    newEventReason2.Interval = Interval;

                                    await _wellEventRepository.AddReasonClosedEvent(newEventReason2);
                                    break;
                                }

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


                                await _wellEventRepository.AddReasonClosedEvent(newEventReason);
                            }

                            _wellEventRepository.UpdateReason(lastEventReason);
                        }
                    }

                    _wellEventRepository
                        .Update(lastEvent);
                }
            }

            await _wellEventRepository.Save();
        }

        public async Task<List<WellWithEventDto>> GetWellsWithEvents(Guid fieldId, string eventType)
        {
            if (eventType.ToUpper().Trim() != "F" && eventType != "A".ToUpper().Trim())
                throw new BadRequestException("Tipos de evento permitidos são 'A' para abertura e 'F' para fechamento.");

            var fieldExists = await _fieldRepository
                .Any(fieldId);

            if (fieldExists is false)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var wellsInDatabase = await _wellRepository
                .GetWellsWithEvents(fieldId, eventType);

            if (wellsInDatabase.Count == 0)
            {
                var eventTypeDescription = eventType == "F" ? "fechados" : "abertos";
                throw new NotFoundException($"Não foram encontrados poços {eventTypeDescription} nesse campo.");
            }

            var wellDtoList = new List<WellWithEventDto>();

            foreach (var well in wellsInDatabase)
            {
                var lastEvent = well.WellEvents
                    .OrderBy(e => e.CreatedAt)
                    .LastOrDefault();

                if (lastEvent is not null && lastEvent.EventStatus == eventType)
                {
                    var lastEventReason = lastEvent.EventReasons
                        .OrderBy(x => x.CreatedAt)
                        .LastOrDefault();

                    var wellDto = new WellWithEventDto
                    {
                        EventId = lastEvent.Id,
                        AutoGeneratedId = lastEvent.IdAutoGenerated,
                        WellId = well.Id,
                        Status = lastEvent.EventStatus,
                        DateLastStatus = lastEventReason is not null ? lastEventReason.StartDate.ToString("dd/MM/yyyy HH:mm") : lastEvent.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        CategoryOperator = well.CategoryOperator,
                        Name = well.Name,
                        WellStatus = well.IsActive,
                    };

                    wellDtoList.Add(wellDto);
                }
            }

            return wellDtoList;
        }

        public async Task<List<WellWithEventDto>> GetWellEvents(Guid wellId, string? date)
        {
            var wellExists = await _wellRepository.GetByIdWithEventsAsync(wellId) ?? throw new NotFoundException("Poço não encontrado");
            var events = await _wellEventRepository.GetAllWellEvent(wellId);
            if (events.Count == 0)
                throw new NotFoundException($"Não foram encontrados eventos para o poço {wellExists.Name}.");

            var wellDtoList = new List<WellWithEventDto>();

            if (date == null)
            {
                foreach (var ev in events)
                {
                    var lastEventReason = ev.EventReasons
                        .OrderBy(x => x.StartDate)
                        .LastOrDefault();

                    var eventDTO = new WellWithEventDto
                    {
                        EventId = ev.Id,
                        AutoGeneratedId = ev.IdAutoGenerated,
                        WellId = ev.Well.Id,
                        Status = ev.EventStatus,
                        DateLastStatus = lastEventReason is not null ? lastEventReason.StartDate.ToString("dd/MM/yyyy HH:mm") : ev.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        CategoryOperator = ev.Well.CategoryOperator,
                        Name = ev.Well.Name,
                        WellStatus = ev.Well.IsActive
                    };

                    wellDtoList.Add(eventDTO);
                }

                return wellDtoList;
            }
            else
            {
                var checkDate = DateTime.TryParse(date, out DateTime day);
                if (checkDate is false)
                    throw new ConflictException("Data não é válida.");

                var dateToday = DateTime.UtcNow.AddHours(-3).Date;
                if (dateToday <= day)
                    throw new NotFoundException("Downtime não foi fechado para esse dia.");

                var filtredEventsByDate = events.Where(x =>
                    x.StartDate.Date <= day && (x.EndDate == null || x.EndDate.Value.Date >= day)
                    || x.StartDate.Date < day && x.EndDate != null && x.EndDate.Value.Date >= day
                    )
                    .Select(eventItem =>
                    {
                        eventItem.EventReasons = eventItem.EventReasons
                            .Where(reason => reason.StartDate.Date <= day && reason.EndDate != null && reason.EndDate.Value.Date >= day
                            || reason.StartDate.Date <= day && reason.EndDate == null)
                            .ToList();
                        return eventItem;
                    }).ToList();


                foreach (var ev in filtredEventsByDate)
                {
                    var lastEventReason = ev.EventReasons
                        .OrderBy(x => x.StartDate)
                        .LastOrDefault();

                    var eventDTO = new WellWithEventDto
                    {
                        EventId = ev.Id,
                        AutoGeneratedId = ev.IdAutoGenerated,
                        WellId = ev.Well.Id,
                        Status = ev.EventStatus,
                        DateLastStatus = lastEventReason is not null ? lastEventReason.StartDate.ToString("dd/MM/yyyy HH:mm") : ev.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        CategoryOperator = ev.Well.CategoryOperator,
                        Name = ev.Well.Name,
                        WellStatus = ev.Well.IsActive
                    };

                    wellDtoList.Add(eventDTO);
                }

                return wellDtoList;

            }
        }

        public async Task<WellEventByIdDto> GetEventById(Guid eventId)
        {
            var wellEvent = await _wellEventRepository.GetEventById(eventId);

            if (wellEvent is null)
                throw new NotFoundException("Evento não encontrado.");

            var createdUserDtoEvent = _mapper.Map<UserDTO>(wellEvent.CreatedBy);
            var updatedUserDtoEvent = _mapper.Map<UserDTO>(wellEvent.UpdatedBy);

            var reasonsDetailed = new List<ReasonDetailedDto>();

            var lastEventReason = wellEvent.EventReasons
                .OrderBy(x => x.CreatedAt)
                .LastOrDefault();

            if (wellEvent.EventReasons.Any() && lastEventReason is not null)
            {
                foreach (var wellReason in wellEvent.EventReasons)
                {
                    var updatedUserDtoReason = _mapper.Map<UserDTO>(wellReason.UpdatedBy);
                    var createdUserDto = _mapper.Map<UserDTO>(wellReason.CreatedBy);

                    var reasonDetailedDto = new ReasonDetailedDto
                    {
                        Id = wellReason.Id,
                        StartDate = wellReason.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        SystemRelated = wellReason.SystemRelated,
                        Downtime = wellReason.Interval,
                        EndDate = wellReason.EndDate?.ToString("dd/MM/yyyy HH:mm"),
                        UpdatedBy = updatedUserDtoReason,
                        CreatedBy = createdUserDto,
                    };

                    reasonsDetailed.Add(reasonDetailedDto);
                }
            }

            var uep = await _installationRepository.GetByUEPCod(wellEvent.Well.Field.Installation.UepCod);

            if (wellEvent.EventStatus == "F")
            {
                reasonsDetailed = reasonsDetailed
                        .OrderBy(x => DateTime.ParseExact(x.StartDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture))
                        .ToList();

                var wellEventDto = new WellEventByIdDto
                {
                    Id = wellEvent.Id,
                    AutoGeneratedId = wellEvent.IdAutoGenerated,
                    EventDateAndHour = wellEvent.StartDate.ToString("dd/MM/yyyy HH:mm"),
                    EventRelated = wellEvent.EventRelatedCode,
                    Field = wellEvent.Well.Field.Name,
                    Installation = wellEvent.Well.Field.Installation.Name,
                    Uep = uep.Name,
                    Reason = wellEvent.Reason,
                    StateAnp = wellEvent.StateANP,
                    StatusAnp = wellEvent.StatusANP,
                    SystemRelated = lastEventReason.SystemRelated,
                    DetailedClosing = reasonsDetailed,
                    CreatedBy = createdUserDtoEvent,
                    UpdatedBy = updatedUserDtoEvent
                };

                return wellEventDto;
            }

            var wellEventDtoOpen = new WellEventByIdDto
            {
                Id = wellEvent.Id,
                AutoGeneratedId = wellEvent.IdAutoGenerated,
                EventDateAndHour = wellEvent.StartDate.ToString("dd/MM/yyyy HH:mm"),
                EventRelated = wellEvent.EventRelatedCode,
                Field = wellEvent.Well.Field.Name,
                Installation = wellEvent.Well.Field.Installation.Name,
                Uep = uep.Name,
                Reason = wellEvent.Reason,
                StateAnp = wellEvent.StateANP,
                StatusAnp = wellEvent.StatusANP,
                CreatedBy = createdUserDtoEvent,
                UpdatedBy = updatedUserDtoEvent
            };

            return wellEventDtoOpen;
        }

        public async Task<ReasonDetailedDto> UpdateReason(Guid reasonId, UpdateReasonViewModel body, User loggedUser)
        {
            var wellReason = await _wellEventRepository.GetEventReasonById(reasonId);

            if (wellReason is null)
                throw new NotFoundException("Razão do evento não encontrada.");

            var firstEventReason = wellReason
                .WellEvent
                .EventReasons
                .OrderBy(x => x.StartDate)
                .FirstOrDefault();

            if (firstEventReason is not null)
            {
                if (firstEventReason.Id == reasonId && body.StartDate is not null)
                    throw new ConflictException("Não é possível editar a data de início do primeiro sistema relacionado, considere atualizar a data de início do evento.");

                if (firstEventReason.EndDate is null && body.EndDate is not null)
                    throw new ConflictException("Não é possível editar a data de fim do primeiro sistema relacionado, considere abrir esse poço.");
            }

            var lastEventReason = wellReason
              .WellEvent
              .EventReasons
              .Where(x => x.EndDate == null)
              .FirstOrDefault();

            if (lastEventReason is not null && body.EndDate is not null && lastEventReason.Id == reasonId)
                throw new ConflictException("Não é possível editar a data de fim do último sistema relacionado, considere abrir esse poço.");

            var systemsRelated = new List<string>
            {
                "submarino","topside","estratégia"
            };

            if (body.SystemRelated is not null && systemsRelated.Contains(body.SystemRelated.ToLower()) is false)
                throw new BadRequestException($"Sistemas relacionados permitidos são: {string.Join(", ", systemsRelated)}");

            if (wellReason.WellEvent.Well.IsActive is false)
                throw new ConflictException("Erro: Poço precisa estar ativo para mudança de evento relacionado.");

            var updatedByUser = _mapper.Map<UserDTO>(loggedUser);
            var createdByUser = _mapper.Map<UserDTO>(wellReason.CreatedBy);

            if (body.SystemRelated is not null)
            {
                wellReason.SystemRelated = body.SystemRelated;
            }

            if (body.StartDate is not null)
            {
                if (DateTime.TryParseExact(body.StartDate, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is true)
                {
                    if (parsedStartDate.Date != wellReason.StartDate.Date)
                        throw new BadRequestException("Data de edição deve ser no mesmo dia.");

                    if (body.EndDate is not null)
                    {
                        if (DateTime.TryParseExact(body.EndDate, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedEndDate) is true)
                        {
                            if (parsedEndDate < parsedStartDate)
                                throw new BadRequestException("Data fim não pode ser menor que data inicial.");
                        }

                        else
                            throw new BadRequestException("Formato de data fim inválido deve ser 'dd/MM/yy HH:mm'.");
                    }
                    else
                    {
                        if (parsedStartDate > wellReason.EndDate)
                            throw new BadRequestException("Data inicial não pode ser maior que data final.");

                    }

                    var beforeReason = await _wellEventRepository
                      .GetBeforeReason(parsedStartDate, wellReason.WellEventId, reasonId);

                    wellReason.StartDate = parsedStartDate;

                    if (wellReason.EndDate is not null)
                    {
                        wellReason.Interval = FormatTimeInterval(wellReason.EndDate.Value, wellReason);
                    }

                    if (beforeReason is not null)
                    {
                        if (parsedStartDate <= beforeReason.StartDate)
                            throw new ConflictException("Data de início não pode ser menor ou igual a data de início do sistema relacionado anterior.");

                        if (wellReason.EndDate is null)
                        {
                            if (parsedStartDate.Date != beforeReason.StartDate.Date)
                            {
                                beforeReason.EndDate = beforeReason.StartDate.Date.AddDays(1).AddMilliseconds(-1);
                            }
                            else
                            {
                                beforeReason.EndDate = parsedStartDate;
                            }

                            beforeReason.UpdatedBy = loggedUser;
                            beforeReason.Interval = FormatTimeInterval(parsedStartDate, beforeReason);

                            _wellEventRepository.UpdateReason(beforeReason);
                        }
                        else
                        {
                            var createdEventReason = new EventReason
                            {
                                Id = Guid.NewGuid(),
                                CreatedBy = loggedUser,
                                UpdatedBy = loggedUser,
                                WellEvent = beforeReason.WellEvent,
                                Comment = beforeReason.Comment,
                                SystemRelated = beforeReason.SystemRelated,
                                WellEventId = beforeReason.WellEventId,
                                StartDate = parsedStartDate.Date,
                                EndDate = parsedStartDate,
                            };

                            var interNewEventReason = FormatTimeInterval(parsedStartDate, createdEventReason);
                            createdEventReason.Interval = interNewEventReason;
                            await _wellEventRepository.AddReasonClosedEvent(createdEventReason);
                        }


                    }
                }

                else
                    throw new BadRequestException("Formato de data de início inválido deve ser 'dd/MM/yy HH:mm'.");
            }

            if (body.EndDate is not null)
            {
                if (DateTime.TryParseExact(body.EndDate, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedEndDate) is true)
                {
                    if (parsedEndDate.Date != wellReason.StartDate.Date)
                        throw new BadRequestException("Data de edição deve ser no mesmo dia.");

                    if (body.StartDate is not null)
                    {
                        if (DateTime.TryParseExact(body.StartDate, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is true)
                        {
                            if (parsedEndDate < parsedStartDate)
                                throw new BadRequestException("Data fim não pode ser menor que data inicial.");
                        }

                        else
                            throw new BadRequestException("Formato de data de início inválido deve ser 'dd/MM/yy HH:mm'.");
                    }
                    else
                    {
                        if (parsedEndDate < wellReason.StartDate)
                            throw new BadRequestException("Data fim não pode ser menor que data inicial.");
                    }

                    var nextReason =
                        await _wellEventRepository.GetNextReason(wellReason.StartDate, wellReason.WellEventId, reasonId);

                    if (nextReason is not null)
                    {
                        if (parsedEndDate < wellReason.EndDate)
                        {
                            var createdEventReason = new EventReason
                            {
                                Id = Guid.NewGuid(),
                                CreatedBy = loggedUser,
                                SystemRelated = nextReason.SystemRelated,
                                WellEvent = nextReason.WellEvent,
                                StartDate = parsedEndDate,
                                WellEventId = nextReason.WellEventId,
                                UpdatedBy = loggedUser
                            };
                            if (nextReason.StartDate.TimeOfDay == TimeSpan.Zero)
                            {
                                createdEventReason.EndDate = parsedEndDate.Date.AddDays(1).AddMilliseconds(-1);
                                nextReason.StartDate = wellReason.EndDate.Value.Date.AddDays(1);
                            }
                            else
                            {
                                createdEventReason.EndDate = wellReason.EndDate.Value;
                                nextReason.StartDate = createdEventReason.EndDate.Value;
                            }

                            var interval = FormatTimeInterval(createdEventReason.EndDate.Value, createdEventReason);
                            createdEventReason.Interval = interval;

                            await _wellEventRepository.AddReasonClosedEvent(createdEventReason);
                        }

                        if (parsedEndDate >= wellReason.EndDate)
                        {
                            wellReason.EndDate = parsedEndDate;
                            nextReason.StartDate = parsedEndDate;
                        }

                        if (nextReason.EndDate is not null)
                        {
                            if (parsedEndDate > nextReason.EndDate)
                                throw new ConflictException("Data de fim não pode ser maior que a data fim do sistema relacionado posterior.");

                            var intervalNext = FormatTimeInterval(nextReason.EndDate.Value, nextReason);
                            nextReason.Interval = intervalNext;
                        }

                        nextReason.UpdatedBy = loggedUser;
                        _wellEventRepository.UpdateReason(nextReason);
                    }

                    wellReason.EndDate = parsedEndDate;
                    wellReason.Interval = FormatTimeInterval(parsedEndDate, wellReason);
                    wellReason.UpdatedBy = loggedUser;
                }
                else
                    throw new BadRequestException("Formato de data de fim inválido deve ser 'dd/MM/yy HH:mm'.");
            }

            if (body.StartDate is not null || body.SystemRelated is not null || body.EndDate is not null)
            {

                wellReason.UpdatedBy = loggedUser;

                _wellEventRepository.UpdateReason(wellReason);
            }

            var reasonDto = _mapper.Map<ReasonDetailedDto>(wellReason);

            await _wellEventRepository.Save();

            return reasonDto;
        }
        public async Task UpdateClosedEvent(Guid eventId, UpdateEventAndSystemRelated body, User loggedUser)
        {
            if (body.SystemRelated is null && body.DateSystemRelated is not null)
                throw new BadRequestException("Sistema relacionado é obrigatório.");

            if (body.SystemRelated is not null && body.DateSystemRelated is null)
                throw new BadRequestException("Data do sistema relacionado é obrigatória.");

            var wellEvent = await _wellEventRepository
                .GetEventById(eventId);

            if (wellEvent is null)
                throw new NotFoundException("Evento não encontrado.");

            if (wellEvent.EventStatus != "F")
                throw new BadRequestException("Evento deve ser de fechamento para ser editado.");

            if (wellEvent.EventRelated is null)
                throw new ConflictException("Não é possível editar o evento inicial do sistema.");

            if ((wellEvent.WellLosses is null || wellEvent.WellLosses.Any()) && (body.EventDateAndHour is not null || body.DateSystemRelated is not null))
                throw new ConflictException("Não é possível editar um evento após a produção ter sido apropriada.");

            if (wellEvent.EventReasons.Any() is false)
                throw new BadRequestException("É preciso ter um motivo anterior.");

            var lastEventReason = wellEvent.EventReasons
              .OrderBy(x => x.StartDate)
              .LastOrDefault();

            if (body.EventDateAndHour is not null && body.DateSystemRelated is not null)
            {
                if (DateTime.TryParseExact(body.DateSystemRelated, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateSystem) is true && DateTime.TryParseExact(body.EventDateAndHour, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is true)
                {
                    if (lastEventReason is not null && lastEventReason.StartDate == wellEvent.StartDate && lastEventReason.EndDate is null && parsedDateSystem < parsedStartDate && parsedDateSystem != lastEventReason.StartDate)
                        throw new BadRequestException("Não é possível alterar o primeiro sistema relacionado para uma data anterior a data do evento.");
                }
                else
                    throw new BadRequestException("Formato de data de sistema relacionado e evento deve ser 'dd/MM/yy HH:mm'.");
            }

            if (body.SystemRelated is not null && lastEventReason is not null && lastEventReason.SystemRelated.ToLower() == body.SystemRelated.ToLower())
                throw new BadRequestException("Sistema relacionado deve ser diferente do anterior");

            var systemsRelated = new List<string>
            {
                "submarino","topside","estratégia"
            };

            if (body.SystemRelated is not null && systemsRelated.Contains(body.SystemRelated.ToLower()) is false)
                throw new BadRequestException($"Sistemas relacionados permitidos são: {string.Join(", ", systemsRelated)}");

            var dateNow = DateTime.UtcNow.AddHours(-3);

            if (body.EventDateAndHour is not null)
            {
                if (DateTime.TryParseExact(body.EventDateAndHour, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is true)
                {
                    if (wellEvent.EndDate is not null && parsedStartDate > wellEvent.EndDate)
                        throw new BadRequestException("Data de início do evento não pode ser maior que data de fim.");

                    if (parsedStartDate < wellEvent.EventRelated.StartDate)
                        throw new ConflictException($"Não é possível atualizar o evento para uma data anterior a data de início do evento relacionado: {wellEvent.EventRelated.IdAutoGenerated}.");

                    var currentDate = wellEvent.StartDate;

                    if (wellEvent.EndDate.HasValue)
                    {
                        while (currentDate <= wellEvent.EndDate)
                        {
                            var productionInDate = await _productionRepository.GetCleanByDate(currentDate);

                            if (productionInDate is not null && productionInDate.IsCalculated)
                                throw new BadRequestException($"Não é possível editar evento, existe uma produção já apropriada no dia: {currentDate:dd/MM/yyyy}");

                            currentDate = currentDate.AddDays(1);
                        }
                    }

                    else
                    {
                        currentDate = wellEvent.StartDate;

                        while (currentDate <= dateNow)
                        {
                            var productionInDate = await _productionRepository.GetCleanByDate(currentDate);

                            if (productionInDate is not null && productionInDate.IsCalculated)
                                throw new BadRequestException($"Não é possível editar evento, existe uma produção já apropriada no dia: {currentDate:dd/MM/yyyy}");

                            currentDate = currentDate.AddDays(1);
                        }
                    }

                    var firstEventReason = wellEvent.EventReasons
                        .OrderBy(x => x.StartDate)
                        .FirstOrDefault();

                    if (wellEvent.StartDate.Date == parsedStartDate.Date)
                    {
                        if (firstEventReason is not null)
                        {
                            firstEventReason.StartDate = parsedStartDate;

                            if (firstEventReason.EndDate is not null)
                            {
                                var formatedInterval = FormatTimeInterval(firstEventReason.EndDate.Value, firstEventReason);

                                firstEventReason.Interval = formatedInterval;
                            }

                            _wellEventRepository.UpdateReason(firstEventReason);
                        }

                    }

                    if (parsedStartDate.Date < wellEvent.StartDate.Date)
                    {
                        var eventReasonsCreated = new List<EventReason>();

                        for (DateTime date = wellEvent.StartDate; date >= parsedStartDate; date = date.AddDays(-1))
                        {

                            if (date.Date == parsedStartDate.Date)
                            {
                                var lastCreatedEventReason = new EventReason
                                {
                                    SystemRelated = firstEventReason.SystemRelated,
                                    Id = Guid.NewGuid(),
                                    WellEvent = wellEvent,
                                    CreatedBy = loggedUser,
                                    WellEventId = wellEvent.Id,
                                    StartDate = parsedStartDate,
                                    EndDate = date.Date.AddDays(1).AddMilliseconds(-10),
                                    UpdatedBy = loggedUser

                                };

                                var interval = FormatTimeInterval(lastCreatedEventReason.EndDate.Value, lastCreatedEventReason);

                                lastCreatedEventReason.Interval = interval;

                                eventReasonsCreated.Add(lastCreatedEventReason);
                            }
                            else if (date.Date > parsedStartDate.Date)
                            {

                                var eventReason = new EventReason
                                {
                                    SystemRelated = firstEventReason.SystemRelated,
                                    Id = Guid.NewGuid(),
                                    WellEvent = wellEvent,
                                    CreatedBy = loggedUser,
                                    WellEventId = wellEvent.Id,
                                    StartDate = date.Date,
                                    EndDate = date.Date.AddDays(1).AddMilliseconds(-10),
                                    UpdatedBy = loggedUser,
                                };

                                var interval = FormatTimeInterval(eventReason.EndDate.Value, eventReason);

                                eventReason.Interval = interval;

                                eventReasonsCreated.Add(eventReason);
                            }
                        }

                        await _wellEventRepository.AddRangeReasons(eventReasonsCreated);
                    }

                    if (parsedStartDate.Date > wellEvent.StartDate.Date)
                    {
                        for (DateTime date = wellEvent.StartDate; date < parsedStartDate.AddDays(1); date = date.AddDays(1))
                        {
                            var endOfDay = date.AddDays(1);

                            var reasonsToDelete = new List<EventReason>();

                            foreach (var reason in wellEvent.EventReasons)
                            {
                                if (reason.StartDate < wellEvent.StartDate)
                                {
                                    reasonsToDelete.Add(reason);
                                }

                                if (reason.StartDate == wellEvent.StartDate)
                                {
                                    reason.StartDate = parsedStartDate;

                                    _wellEventRepository.UpdateReason(reason);
                                }

                            }

                            _wellEventRepository.DeleteRangeReason(reasonsToDelete);
                        }
                    }

                    wellEvent.StartDate = parsedStartDate;
                    wellEvent.EventRelated.EndDate = parsedStartDate;
                    wellEvent.EventRelated.Interval = (parsedStartDate - wellEvent.EventRelated.StartDate).TotalHours;
                    wellEvent.UpdatedBy = loggedUser;

                    if (wellEvent.EndDate is not null)
                    {
                        wellEvent.Interval = (wellEvent.EndDate.Value - parsedStartDate).TotalHours;
                    }

                    _wellEventRepository.Update(wellEvent);
                }
                else
                    throw new BadRequestException("Formato de data de início do evento inválido deve ser 'dd/MM/yy HH:mm'.");
            }

            if (body.DateSystemRelated is not null)
            {
                if (DateTime.TryParseExact(body.DateSystemRelated, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateSystem) is true)
                {
                    //if (parsedDateSystem < wellEvent.EventRelated.StartDate)
                    //    throw new ConflictException($"Não é possível adicionar um sistema para uma data anterior a data de início do evento relacionado: {wellEvent.EventRelated.IdAutoGenerated}.");

                    if (parsedDateSystem < wellEvent.StartDate)
                        throw new BadRequestException("Não é possível editar o sistema relacionado para uma data anterior ao evento");

                    var currentDate = parsedDateSystem;

                    while (currentDate <= parsedDateSystem)
                    {
                        var productionInDate = await _productionRepository.GetCleanByDate(currentDate);

                        if (productionInDate is not null && productionInDate.IsCalculated)
                            throw new BadRequestException($"Não é possível adicionar sistema relacionado, existe uma produção já apropriada no dia: {currentDate:dd/MM/yyyy}");

                        currentDate = currentDate.AddDays(1);
                    }


                    if (lastEventReason.StartDate < parsedDateSystem && lastEventReason.EndDate is null)
                    {
                        var dif = (parsedDateSystem - lastEventReason.StartDate).TotalHours / 24;
                        lastEventReason.EndDate = lastEventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10);

                        var firstresultIntervalTimeSpan = (lastEventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10) - lastEventReason.StartDate).TotalHours;
                        int firstintervalHours = (int)firstresultIntervalTimeSpan;
                        var firstintervalMinutesDecimal = (firstresultIntervalTimeSpan - firstintervalHours) * 60;
                        int firstintervalMinutes = (int)firstintervalMinutesDecimal;
                        var firstintervalSecondsDecimal = (firstintervalMinutesDecimal - firstintervalMinutes) * 60;
                        int firstintervalSeconds = (int)firstintervalSecondsDecimal;
                        string firstReasonFormattedHours;
                        var firstFormattedMinutes = firstintervalMinutes < 10 ? $"0{firstintervalMinutes}" : firstintervalMinutes.ToString();
                        var firstFormattedSecond = firstintervalSeconds < 10 ? $"0{firstintervalSeconds}" : firstintervalSeconds.ToString();
                        if (firstintervalHours >= 1000)
                        {
                            int digitCount = (int)Math.Floor(Math.Log10(firstintervalHours)) + 1;
                            firstReasonFormattedHours = firstintervalHours.ToString(new string('0', digitCount));
                        }
                        else
                        {
                            firstReasonFormattedHours = firstintervalHours.ToString("00");
                        }
                        var firstReasonFormattedTime = $"{firstReasonFormattedHours}:{firstFormattedMinutes}:{firstFormattedSecond}";
                        lastEventReason.Interval = firstReasonFormattedTime;

                        DateTime refStartDate = lastEventReason.StartDate.Date.AddDays(1);
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
                                SystemRelated = lastEventReason.SystemRelated,
                                Comment = lastEventReason.Comment,
                                WellEvent = wellEvent,
                                StartDate = refStartDate,
                                IsActive = true,
                                IsJobGenerated = false,
                                CreatedBy = loggedUser,
                                UpdatedBy = loggedUser
                            };
                            if (j == 0)
                            {
                                if (parsedDateSystem.Date == lastEventReason.StartDate.Date)
                                {
                                    lastEventReason.EndDate = parsedDateSystem;
                                    var Interval = FormatTimeInterval(parsedDateSystem, lastEventReason);
                                    lastEventReason.Interval = Interval;

                                    newEventReason.StartDate = parsedDateSystem;
                                    newEventReason.SystemRelated = body.SystemRelated;
                                    await _wellEventRepository.AddReasonClosedEvent(newEventReason);
                                    break;
                                }
                            }
                            if (parsedDateSystem.Date == refStartDate)
                            {
                                var newEventReason2 = new EventReason
                                {
                                    Id = Guid.NewGuid(),
                                    SystemRelated = lastEventReason.SystemRelated,
                                    Comment = lastEventReason.Comment,
                                    WellEvent = wellEvent,
                                    StartDate = refStartDate,
                                    EndDate = parsedDateSystem,
                                    IsActive = true,
                                    IsJobGenerated = false,
                                    CreatedBy = loggedUser,
                                    UpdatedBy = loggedUser

                                };
                                var Interval = FormatTimeInterval(parsedDateSystem, newEventReason2);
                                newEventReason2.Interval = Interval;

                                newEventReason.EndDate = null;
                                newEventReason.StartDate = parsedDateSystem;
                                newEventReason.SystemRelated = body.SystemRelated;

                                await _wellEventRepository.AddReasonClosedEvent(newEventReason2);
                                await _wellEventRepository.AddReasonClosedEvent(newEventReason);
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

                            await _wellEventRepository.AddReasonClosedEvent(newEventReason);
                        }
                    }

                }
                else
                    throw new BadRequestException("Formato de data de sistema relacionado deve ser 'dd/MM/yy HH:mm'.");
            }

            await _wellEventRepository.Save();
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
