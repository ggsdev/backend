﻿using AutoMapper;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Dtos;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.ViewModels;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared.Errors;
using System.Globalization;

namespace PRIO.src.Modules.Measuring.WellEvents.Http.Services
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
        public async Task CloseWellEvent(CreateClosingEventViewModel body)
        {
            if (DateTime.TryParseExact(body.EventDateAndHour, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is false)
                throw new BadRequestException("Formato de data inválido deve ser 'dd/MM/yy HH:mm'.");

            var dateNow = DateTime.UtcNow.AddHours(-3);

            if (parsedStartDate > dateNow)
                throw new ConflictException("Não é possível cadastrar um evento no futuro.");

            var wellsList = new List<Well>();

            var lastEventWrongList = new List<string>();

            foreach (var well in body.Wells)
            {
                var wellInDatabase = await _wellRepository
                    .GetWithFieldAsync(well.WellId);

                if (wellInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Well>());

                var lastEvent = wellInDatabase.WellEvents
                    .OrderBy(e => e.CreatedAt)
                    .LastOrDefault();

                if (lastEvent is null)
                    throw new ConflictException("O poço não possui um evento de abertura anterior.");

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
                    .OrderBy(e => e.CreatedAt)
                    .LastOrDefault();

                var lastEventOfTypeClosing = well.WellEvents
                    .OrderBy(e => e.CreatedAt)
                    .LastOrDefault(x => x.EventStatus == "F");

                var codeSequencial = string.Empty;

                if (lastEventOfTypeClosing is not null && int.TryParse(lastEventOfTypeClosing.IdAutoGenerated, out int lastCode))
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
                    IdAutoGenerated = $"{well?.Field?.Name?[..3]}{codeSequencial} {well?.Name}",
                    StateANP = body.StateAnp,
                    StatusANP = body.StatusAnp,
                    Well = well!,
                    EventRelatedCode = body.EventRelatedCode,
                };

                await _wellEventRepository.Add(closingEvent);

                var eventReason = new EventReason
                {
                    Id = Guid.NewGuid(),
                    SystemRelated = body.SystemRelated,
                    StartDate = parsedStartDate,
                    WellEvent = closingEvent,

                };

                await _wellEventRepository.AddReasonClosedEvent(eventReason);

                if (lastEvent is not null)
                {

                    lastEvent.EndDate = parsedStartDate;
                    lastEvent.Interval = (parsedStartDate - lastEvent.StartDate).TotalHours;

                    _wellEventRepository.Update(lastEvent);
                }

                //caso evento seja no passado recalcular
                if (dateNow.Date > parsedStartDate.Date)
                {

                }

            }

            await _wellEventRepository.Save();
        }

        public async Task<ClosingEventDto> GetUepsForWellEvent()
        {
            var ueps = await _installationRepository
                .GetUEPsAsync();

            var installations = await _installationRepository
                .GetAsync();

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
                                    WellId = well.Id,
                                    Status = lastEvent.EventStatus,
                                    DateLastStatus = lastEvent.StartDate.ToString("dd/MM/yyyy HH:mm"),
                                    Name = well.Name
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

        public async Task OpenWellEvent(CreateOpeningEventViewModel body)
        {
            if (DateTime.TryParseExact(body.EventDateAndHour, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate) is false)
                throw new BadRequestException("Formato de data inválido deve ser 'dd/MM/yy HH:mm'.");

            var dateNow = DateTime.UtcNow.AddHours(-3);

            if (parsedStartDate > dateNow)
                throw new ConflictException("Não é possível cadastrar um evento no futuro");

            var wellsList = new List<Well>();

            var wellInDatabase = await _wellRepository
                .GetWithFieldAsync(body.WellId);

            if (wellInDatabase is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            var lastEvent = wellInDatabase.WellEvents
                .OrderBy(e => e.StartDate)
                .Where(x => x.StartDate <= parsedStartDate) //checar logica pensando em criação de um evento no passado
                .LastOrDefault();

            if (lastEvent is null)
                throw new ConflictException("O poço não possui um evento de fechamento anterior.");

            if (lastEvent is not null && lastEvent.EventStatus != "F")
                throw new BadRequestException("O último evento do poço deve ser de fechamento para que seja possível cadastrar um evento de abertura.");

            if (lastEvent is not null && parsedStartDate < lastEvent.StartDate)
                throw new BadRequestException("Data de início do evento deve ser maior que a data de início do último evento associado.");

            var lastEventOfTypeOpening = wellInDatabase.WellEvents
                .OrderBy(e => e.StartDate)
                .LastOrDefault(x => x.EventStatus == "A");

            var codeSequencial = string.Empty;

            if (lastEventOfTypeOpening is not null && int.TryParse(lastEventOfTypeOpening.IdAutoGenerated, out int lastCode))
            {
                lastCode++;
                codeSequencial = lastCode.ToString("0000");
            }
            else
            {
                codeSequencial = "0001";
            }

            var openingEvent = new WellEvent
            {
                Id = Guid.NewGuid(),
                EventRelated = lastEvent,
                EventStatus = "A",
                StartDate = parsedStartDate,
                Reason = body.Reason,
                IdAutoGenerated = $"{wellInDatabase?.Field?.Name?[..3]}{codeSequencial} {wellInDatabase?.Name}",
                StateANP = body.StateAnp,
                StatusANP = body.StatusAnp,
                Well = wellInDatabase!,
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
                    var resultTimeSpan = (parsedStartDate - lastEventReason.StartDate)
                        .TotalHours;

                    var hours = (int)resultTimeSpan;
                    var minutesDecimal = (resultTimeSpan - hours) * 60;
                    var minutes = (int)minutesDecimal;
                    var secondsDecimal = (minutesDecimal - minutes) * 60;
                    var seconds = (int)secondsDecimal;
                    var dateTime = DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
                    var timeOperating = DateTime.Today.AddDays(1) - dateTime;
                    var formattedTime = dateTime.ToString("HH:mm:ss");
                    var formattedTimeTimeOperating = timeOperating.ToString("HH:mm:ss");

                    lastEventReason.Interval = formattedTime;
                    lastEventReason.EndDate = parsedStartDate;

                    _wellEventRepository
                        .UpdateReason(lastEventReason);
                }

                _wellEventRepository
                    .Update(lastEvent);
            }
            //var dateRange = new List<DateTime>();
            ////recalcular caso seja no passado ao menos d-1, podendo pegar vários dias
            //if (dateNow.Date > parsedStartDate.Date)
            //{
            //    for (var date = parsedStartDate; date <= dateNow; date = date.AddDays(1))
            //    {
            //        dateRange.Add(date);

            //    }

            //    foreach (var date in dateRange)
            //    {

            //        var production = await _productionRepository.GetExistingByDate(date);

            //        if (production is null)
            //            continue;

            //        var totalWaterInUep = 0m;
            //        decimal? totalWaterWithFieldFR = 0m;

            //        if (production.FieldsFR is not null && production.FieldsFR.Count > 0)
            //        {

            //            var uepFields = await _fieldRepository
            //        .GetFieldsByUepCode(production.Installation.UepCod);
            //            var wellTestsUEP = await _btpRepository
            //               .GetBtpDatasByUEP(production.Installation.UepCod);
            //            var filtredUEPsByApplyDateAndFinal = wellTestsUEP.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date) && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR"
            //                    || x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
            //                    && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

            //            decimal totalPotencialGasUEP = 0;
            //            decimal totalPotencialOilUEP = 0;
            //            decimal totalPotencialWaterUEP = 0;

            //            foreach (var btp in filtredUEPsByApplyDateAndFinal)
            //            {
            //                double totalInterval = 0;

            //                var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
            //                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F")
            //                    .OrderBy(x => x.StartDate);

            //                foreach (var a in filtredEvents)
            //                {
            //                    if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
            //                    {
            //                        totalInterval += ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
            //                    }
            //                    else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
            //                    {
            //                        totalInterval += 24;
            //                    }
            //                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
            //                    {
            //                        totalInterval += ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
            //                    }
            //                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
            //                    {
            //                        totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
            //                    }
            //                    else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
            //                    {
            //                        totalInterval += 24;
            //                    }

            //                    else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
            //                    {
            //                        totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
            //                    }
            //                }
            //                totalPotencialGasUEP += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
            //                totalPotencialOilUEP += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
            //                totalPotencialWaterUEP += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;

            //            }

            //            foreach (var fieldFR in production.FieldsFR)
            //            {
            //                var btps = await _btpRepository
            //                    .GetBtpDatasByFieldId(fieldFR.Field.Id);
            //                var filtredByApplyDateAndFinal = btps
            //                    .Where(x => (x.FinalApplicationDate == null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
            //                    || (x.FinalApplicationDate != null && x.ApplicationDate != null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
            //                    && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

            //                var totalOilPotencial = filtredByApplyDateAndFinal
            //                    .Sum(x => x.PotencialOil);
            //                if (fieldFR.FROil is not null)
            //                {
            //                    foreach (var btp in filtredByApplyDateAndFinal)
            //                    {
            //                        var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);
            //                        totalWaterWithFieldFR += (production.Oil.TotalOil * fieldFR.FROil * wellPotencialOilAsPercentageOfField * btp.BSW) / (100 - btp.BSW);
            //                    }
            //                }
            //            }
            //        }

            //        else
            //        {
            //        }
            //    }
            //}

            //await _wellEventRepository.Save();
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
                        WellId = well.Id,
                        Status = lastEvent.EventStatus,
                        DateLastStatus = lastEventReason is not null ? lastEventReason.StartDate.ToString("dd/MM/yyyy HH:mm") : lastEvent.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        Name = well.Name
                    };

                    wellDtoList.Add(wellDto);
                }
            }

            return wellDtoList;
        }

        public async Task<List<EventWithReasonDTO>> GetWellEvents(Guid wellId, string date)
        {
            var wellExists = await _wellRepository.GetByIdWithEventsAsync(wellId) ?? throw new NotFoundException("Poço não encontrado");
            var events = await _wellEventRepository.GetAllWellEvent(wellId);
            if (events.Count == 0)
            {
                throw new NotFoundException($"Não foram encontrados eventos para o poço {wellExists.Name}.");
            }

            var checkDate = DateTime.TryParse(date, out DateTime day);
            if (checkDate is false)
                throw new ConflictException("Data não é válida.");

            var filtredEventsByDate = events.Where(x => x.StartDate.Date <= day && x.EndDate != null && x.EndDate.Value.Date >= day
                || x.StartDate.Date <= day && x.EndDate == null
                )
                .Select(eventItem =>
                {
                    eventItem.EventReasons = eventItem.EventReasons
                        .Where(reason => (reason.StartDate.Date <= day && reason.EndDate != null && reason.EndDate.Value.Date >= day)
                        || (reason.StartDate.Date <= day && reason.EndDate == null))
                        .ToList();
                    return eventItem;
                }).ToList();

            var eventsDTO = _mapper.Map<List<WellEvent>, List<EventWithReasonDTO>>(filtredEventsByDate);

            return eventsDTO;
        }

        public async Task<WellEventByIdDto> GetClosedEventById(Guid eventId)
        {
            var wellEvent = await _wellEventRepository.GetClosedEventById(eventId);

            if (wellEvent is null)
                throw new NotFoundException("Evento de fechamento não encontrado.");

            var reasonsDetailed = new List<ReasonDetailedDto>();

            var lastEventReason = wellEvent.EventReasons
                .OrderBy(x => x.CreatedAt)
                .LastOrDefault();

            if (wellEvent.EventReasons.Any() && lastEventReason is not null)
            {
                foreach (var wellReason in wellEvent.EventReasons)
                {
                    var reasonDetailedDto = new ReasonDetailedDto
                    {
                        StartDate = wellReason.StartDate.ToString("dd/MM/yyyy : HH:mm"),
                        SystemRelated = wellReason.SystemRelated,
                        Downtime = wellReason.Interval,
                        EndDate = wellReason.EndDate?.ToString("dd:MM:yyyy : HH:mm"),
                    };

                    reasonsDetailed.Add(reasonDetailedDto);
                }
            }

            var uep = await _installationRepository.GetByUEPCod(wellEvent.Well.Field.Installation.UepCod);

            reasonsDetailed = reasonsDetailed
                    .OrderBy(x => DateTime.ParseExact(x.StartDate, "dd/MM/yyyy : HH:mm", CultureInfo.InvariantCulture))
                    .ToList();

            var wellEventDto = new WellEventByIdDto
            {
                Id = wellEvent.Id,
                EventDateAndHour = wellEvent.StartDate.ToString("dd:MM/yyyy : HH:mm"),
                EventRelated = wellEvent.EventRelatedCode,
                Field = wellEvent.Well.Field.Name,
                Installation = wellEvent.Well.Field.Installation.Name,
                Uep = uep.Name,
                Reason = wellEvent.Reason,
                StateAnp = wellEvent.StateANP,
                StatusAnp = wellEvent.StatusANP,
                SystemRelated = lastEventReason.SystemRelated,
                DetailedClosing = reasonsDetailed,
            };

            return wellEventDto;
        }

        public async Task AddReasonClosedEvent(Guid eventId, CreateReasonViewModel body)
        {
            var closingEvent = await _wellEventRepository
                .GetClosedEventById(eventId);

            if (closingEvent is null)
                throw new NotFoundException("Evento de fechamento não encontrado.");

            if (closingEvent.EventReasons.Any() is false)
                throw new BadRequestException("É preciso ter um motivo anterior.");

            var lastEventReason = closingEvent.EventReasons
              .OrderBy(x => x.CreatedAt)
              .LastOrDefault();

            if (lastEventReason is not null && lastEventReason.SystemRelated.ToLower() == body.SystemRelated.ToLower())
                throw new BadRequestException("Sistema relacionado deve ser diferente do anterior");

            var systemsRelated = new List<string>
            {
                "submarino","topside","estratégia"
            };

            if (systemsRelated.Contains(body.SystemRelated.ToLower()) is false)
                throw new BadRequestException($"Sistemas relacionados permitidos são: {string.Join(", ", systemsRelated)}");

            var dateNow = DateTime.UtcNow.AddHours(-3);

            var eventReason = new EventReason
            {
                Id = Guid.NewGuid(),
                SystemRelated = body.SystemRelated,
                StartDate = dateNow,
                WellEvent = closingEvent,
            };

            if (lastEventReason is not null)
            {
                var resultTimeSpan = (dateNow - lastEventReason.StartDate).TotalHours;

                int hours = (int)resultTimeSpan;
                var minutesDecimal = (resultTimeSpan - hours) * 60;
                int minutes = (int)minutesDecimal;
                var secondsDecimal = (minutesDecimal - minutes) * 60;
                int seconds = (int)secondsDecimal;

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
                var formattedTime = $"{formattedHours}:{minutes}:{seconds}";

                lastEventReason.Interval = formattedTime;
                lastEventReason.EndDate = DateTime.UtcNow.AddHours(-3);

                _wellEventRepository.UpdateReason(lastEventReason);
            }

            await _wellEventRepository.AddReasonClosedEvent(eventReason);

            await _wellEventRepository.Save();
        }
    }
}
