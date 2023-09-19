using AutoMapper;
using dotenv.net;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Modules.Measuring.WellEvents.Dtos;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.ViewModels;
using PRIO.src.Modules.Measuring.WellProductions.Dtos;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Dtos;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Utils;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared.Errors;
using System.Data;
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

            var lastEventWrongList = new List<string>();

            foreach (var well in body.Wells)
            {
                var wellInDatabase = await _wellRepository
                    .GetWithFieldAsync(well.WellId);

                if (wellInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Well>());

                if (wellInDatabase.IsActive is false)
                    throw new BadRequestException($"Poço: {wellInDatabase.Name} está inativo.");

                var lastEvent = wellInDatabase.WellEvents
                    .OrderBy(e => e.CreatedAt)
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
                    .OrderBy(e => e.CreatedAt)
                    .LastOrDefault();

                var lastEventOfTypeClosing = well.WellEvents
                    .OrderBy(e => e.CreatedAt)
                    .LastOrDefault(x => x.EventStatus == "F");

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
                    IdAutoGenerated = $"{well?.Field?.Name?[..3]}{codeSequencial} {well?.Name}",
                    StateANP = body.StateAnp,
                    StatusANP = body.StatusAnp,
                    Well = well!,
                    EventRelatedCode = body.EventRelatedCode,
                    CreatedBy = loggedUser
                };

                await _wellEventRepository.Add(closingEvent);

                var eventReason = new EventReason
                {
                    Id = Guid.NewGuid(),
                    SystemRelated = body.SystemRelated,
                    StartDate = parsedStartDate,
                    WellEvent = closingEvent,
                    CreatedBy = loggedUser
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
                                    AutoGeneratedId = lastEvent.IdAutoGenerated,
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

            if (parsedStartDate < convertAppDate)
                throw new ConflictException("Data do evento está menor que data do inicio da aplicação");

            var lastEvent = well.WellEvents
                .OrderBy(e => e.StartDate)
                .Where(x => x.StartDate <= parsedStartDate) //checar logica pensando em criação de um evento no passado
                .LastOrDefault();

            if (lastEvent is not null && lastEvent.EventStatus == "A")
                throw new ConflictException("O último evento do poço deve ser de fechamento para que seja possível cadastrar um evento de abertura.");


            if (parsedStartDate < lastEvent.StartDate)
                throw new BadRequestException("Data de início do evento deve ser maior que a data de início do último evento associado.");

            var lastEventOfTypeOpening = well.WellEvents
                .OrderBy(e => e.StartDate)
                .LastOrDefault(x => x.EventStatus == "A");

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
                    IdAutoGenerated = $"{well.Field?.Name?[..3]}{codeSequencial} {well.Name}",
                    StateANP = body.StateAnp,
                    StatusANP = body.StatusAnp,
                    Well = well,
                    CreatedBy = loggedUser,
                };
                await _wellEventRepository.Add(openingEvent);

                lastEvent.EndDate = parsedStartDate;
                lastEvent.Interval = (parsedStartDate - lastEvent.StartDate).TotalHours;
                _wellEventRepository.Update(lastEvent);

                var production = await _productionRepository
                    .GetExistingByDate(parsedStartDate);

                if (production is not null)
                {
                    var totalWaterInUep = 0m;
                    decimal? totalWaterWithFieldFR = 0m;

                    if (production.FieldsFR is not null && production.FieldsFR.Any())
                    {
                        var uepFields = await _fieldRepository
                           .GetFieldsByUepCode(production.Installation.UepCod);
                        var wellTestsUEP = await _btpRepository
                           .GetBtpDatasByUEP(production.Installation.UepCod);
                        var filtredUEPsByApplyDateAndFinal = wellTestsUEP.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date) && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR"
                                || x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                                && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        decimal totalPotencialGasUEP = 0;
                        decimal totalPotencialOilUEP = 0;
                        decimal totalPotencialWaterUEP = 0;

                        foreach (var btp in filtredUEPsByApplyDateAndFinal)
                        {
                            double totalInterval = 0;
                            var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                            || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);
                            foreach (var a in filtredEvents)
                            {
                                if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                                {
                                    totalInterval += ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                }
                                else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                                {
                                    totalInterval += 24;
                                }
                                else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                                {
                                    totalInterval += ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                }
                                else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                                {
                                    totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                }
                                else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                                {
                                    totalInterval += 24;
                                }
                                else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                                {
                                    totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                }
                            }
                            totalPotencialGasUEP += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                            totalPotencialOilUEP += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                            totalPotencialWaterUEP += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                        }

                        foreach (var fieldFR in production.FieldsFR)
                        {
                            var btps = await _btpRepository
                                .GetBtpDatasByFieldId(fieldFR.Field.Id);
                            var filtredByApplyDateAndFinal = btps
                                .Where(x => (x.FinalApplicationDate == null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                                || (x.FinalApplicationDate != null && x.ApplicationDate != null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                                && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                            var totalOilPotencial = filtredByApplyDateAndFinal
                                .Sum(x => x.PotencialOil);
                            if (fieldFR.FROil is not null)
                            {
                                foreach (var btp in filtredByApplyDateAndFinal)
                                {
                                    var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField(btp.PotencialOil, totalOilPotencial);
                                    totalWaterWithFieldFR += (production.Oil.TotalOil * fieldFR.FROil * wellPotencialOilAsPercentageOfField * btp.BSW) / (100 - btp.BSW);
                                }
                            }
                        }

                        foreach (var fieldFR in production.FieldsFR)
                        {
                            var totalWater = 0m;
                            var totalOil = 0m;
                            var totalGas = 0m;
                            var wellTestsField = await _btpRepository
                                .GetBtpDatasByFieldId(fieldFR.Field.Id);
                            var filtredByApplyDateAndFinal = wellTestsField
                                .Where(x => (x.FinalApplicationDate == null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                                || (x.FinalApplicationDate != null && x.ApplicationDate != null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                                && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));
                            var totalGasPotencial = filtredByApplyDateAndFinal
                                .Sum(x => x.PotencialGas);
                            var totalOilPotencial = filtredByApplyDateAndFinal
                                .Sum(x => x.PotencialOil);
                            var totalWaterPotencial = filtredByApplyDateAndFinal
                                .Sum(x => x.PotencialWater);

                            decimal totalPotencialGasField = 0;
                            decimal totalPotencialOilField = 0;
                            decimal totalPotencialWaterField = 0;
                            foreach (var btp in filtredByApplyDateAndFinal)
                            {
                                double totalInterval = 0;
                                var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                                foreach (var a in filtredEvents)
                                {
                                    if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                                    {
                                        totalInterval += ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                                    {
                                        totalInterval += 24;
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                                    {
                                        totalInterval += ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                                    {
                                        totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                                    {
                                        totalInterval += 24;
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                                    {
                                        totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                    }
                                }

                                totalPotencialGasField += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                                totalPotencialOilField += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                                totalPotencialWaterField += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                            }

                            var fieldProduction = await _productionRepository
                                .GetFieldProductionByFieldAndProductionId(fieldFR.Field.Id, production.Id);

                            foreach (var btp in filtredByApplyDateAndFinal)
                            {
                                double totalInterval = 0;
                                var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                        || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                                var listEvents = new List<CreateWellLossDTO>();

                                foreach (var a in filtredEvents)
                                {
                                    if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                                    {
                                        double interval = ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)interval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                                    {
                                        double interval = 24;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)interval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                                    {
                                        double interval = ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                                    {
                                        double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                                    {
                                        double interval = 24;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                                    {
                                        double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                }

                                var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasUEP);
                                var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilUEP);
                                var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterUEP);
                                var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasField);
                                var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilField);
                                var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterField);
                                var productionGas = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.GasProductionInField, wellPotencialGasAsPercentageOfField) : 0;
                                var productionOil = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProduction(fieldFR.OilProductionInField, wellPotencialOilAsPercentageOfField) : 0;
                                var productionWater = (productionOil * btp.BSW) / (100 - btp.BSW);

                                int hours = (int)totalInterval;
                                double minutesDecimal = (totalInterval - hours) * 60;
                                int minutes = (int)minutesDecimal;
                                double secondsDecimal = (minutesDecimal - minutes) * 60;
                                int seconds = (int)secondsDecimal;
                                DateTime dateTime = DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
                                string formattedTime = dateTime.ToString("HH:mm:ss");

                                var wellProd = await _productionRepository
                                    .GetWellProductionByWellAndProductionId(btp.Well.Id, production.Id);

                                if (wellProd is not null)
                                {
                                    wellProd.ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField;
                                    wellProd.ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField;
                                    wellProd.ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField;

                                    wellProd.ProductionGasInWellM3 = productionGas;
                                    wellProd.ProductionOilInWellM3 = productionOil;
                                    wellProd.ProductionWaterInWellM3 = productionWater;

                                    wellProd.ProductionGasAsPercentageOfInstallation = fieldFR.FRGas is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialGasAsPercentageOfField, fieldFR.FRGas.Value) : 0;

                                    wellProd.ProductionOilAsPercentageOfInstallation = fieldFR.FROil is not null ? WellProductionUtils.CalculateWellProductionAsPercentageOfInstallation(wellPotencialOilAsPercentageOfField, fieldFR.FROil.Value) : 0;

                                    wellProd.ProductionWaterAsPercentageOfInstallation = totalWaterWithFieldFR != 0 ? productionWater / totalWaterWithFieldFR.Value : 0;
                                    wellProd.Downtime = formattedTime;

                                    totalWater += wellProd.ProductionWaterInWellM3;
                                    totalOil += wellProd.ProductionOilInWellM3;
                                    totalGas += wellProd.ProductionGasInWellM3;

                                    foreach (var ev in listEvents)
                                    {
                                        int year = production.MeasuredAt.Year;
                                        int month = production.MeasuredAt.Month;
                                        int daysInMonth = DateTime.DaysInMonth(year, month);

                                        var wellLoss = await _productionRepository
                                            .GetWellLossByEventAndWellProductionId(ev.Id, wellProd.Id);

                                        if (wellLoss is not null)
                                        {
                                            wellLoss.Downtime = ev.Downtime;
                                            wellLoss.EfficienceLoss = (((btp.PotencialOil * ev.Downtime) / 24) / totalPotencialOilField) / daysInMonth;
                                            wellLoss.ProductionLostOil = (((btp.PotencialOil * ev.Downtime) / totalPotencialOilField) / 24) * fieldFR.OilProductionInField;
                                            wellLoss.ProportionalDay = ((btp.PotencialOil * ev.Downtime) / 24) / totalPotencialOilField;

                                            wellProd.EfficienceLoss += wellLoss.EfficienceLoss;
                                            wellProd.ProductionLostOil += wellLoss.ProductionLostOil;
                                            wellProd.ProportionalDay += wellLoss.ProportionalDay;

                                            wellLoss.ProductionLostGas = (((btp.PotencialGas * ev.Downtime) / totalPotencialGasField) / 24) * fieldFR.GasProductionInField;

                                            wellProd.ProductionLostGas += wellLoss.ProductionLostGas;

                                            //wellLoss.ProductionLostWater = (((btp.PotencialWater * ev.Downtime) / totalPotencialWaterField) / 24) * fieldFR.OilProductionInField;

                                            wellProd.ProductionLostWater += wellLoss.ProductionLostWater;

                                            _wellProductionRepository.UpdateWellLost(wellLoss);
                                        }
                                    }

                                    _wellProductionRepository.Update(wellProd);
                                }

                            }

                            if (fieldProduction is not null)
                            {
                                fieldProduction.WaterProductionInField = totalWater;
                                fieldProduction.GasProductionInField = totalGas;
                                fieldProduction.OilProductionInField = totalOil;

                                _productionRepository.UpdateFieldProduction(fieldProduction);
                            }
                        }

                        totalWaterInUep = totalWaterWithFieldFR.Value;
                        await _wellEventRepository.Save();

                        var listProductions = await _wellProductionRepository.getAllFieldsProductionsByProductionId(production.Id);

                        foreach (var field in listProductions)
                        {
                            foreach (var wellProduction in field.WellProductions)
                            {
                                var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                                foreach (var completion in wellInDatabase.Completions)
                                {
                                    var reservoirProduction = await _wellProductionRepository.GetReservoirProductionForWellAndReservoir(production.Id, completion.Reservoir.Id);

                                    var zoneProduction = await _wellProductionRepository.GetZoneProductionForWellAndReservoir(production.Id, completion.Reservoir.Zone.Id);
                                    var completionProductionInDatabase = await _wellProductionRepository
                                        .GetCompletionProduction(completion.Id, production.Id);

                                    if (completionProductionInDatabase is null)
                                        throw new NotFoundException($"Distribuição da produção da completação no dia: {production.MeasuredAt} não encontrada.");

                                    if (reservoirProduction is null)
                                        throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                    if (zoneProduction is null)
                                        throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                    completionProductionInDatabase.OilProductionInCompletion = 0m;
                                    completionProductionInDatabase.GasProductionInCompletion = 0m;
                                    completionProductionInDatabase.WaterProductionInCompletion = 0m;

                                    _wellProductionRepository.UpdateCompletionProduction(completionProductionInDatabase);

                                    reservoirProduction.OilProductionInReservoir = 0m;
                                    reservoirProduction.GasProductionInReservoir = 0m;
                                    reservoirProduction.WaterProductionInReservoir = 0m;

                                    _wellProductionRepository.UpdateReservoirProduction(reservoirProduction);

                                    zoneProduction.OilProductionInZone = 0m;
                                    zoneProduction.GasProductionInZone = 0m;
                                    zoneProduction.WaterProductionInZone = 0m;

                                    _wellProductionRepository.UpdateZoneProduction(zoneProduction);

                                }
                            }
                        }

                        foreach (var field in listProductions)
                        {
                            foreach (var wellProduction in field.WellProductions)
                            {
                                var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                                foreach (var completion in wellInDatabase.Completions)
                                {
                                    var reservoirProduction = await _wellProductionRepository.GetReservoirProductionForWellAndReservoir(production.Id, completion.Reservoir.Id);
                                    var zoneProduction = await _wellProductionRepository.GetZoneProductionForWellAndReservoir(production.Id, completion.Reservoir.Zone.Id);

                                    var allocationReservoir = completion.AllocationReservoir.Value;
                                    var completionProductionInDatabase = await _wellProductionRepository
                                        .GetCompletionProduction(completion.Id, production.Id);

                                    completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWellM3;
                                    completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWellM3;
                                    completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWellM3;

                                    _wellProductionRepository.UpdateCompletionProduction(completionProductionInDatabase);

                                    reservoirProduction.GasProductionInReservoir += completionProductionInDatabase.GasProductionInCompletion;
                                    reservoirProduction.OilProductionInReservoir += completionProductionInDatabase.OilProductionInCompletion;
                                    reservoirProduction.WaterProductionInReservoir += completionProductionInDatabase.WaterProductionInCompletion;

                                    zoneProduction.GasProductionInZone += completionProductionInDatabase.GasProductionInCompletion;
                                    zoneProduction.OilProductionInZone += completionProductionInDatabase.OilProductionInCompletion;
                                    zoneProduction.WaterProductionInZone += completionProductionInDatabase.WaterProductionInCompletion;

                                    _wellProductionRepository.UpdateZoneProduction(zoneProduction);

                                }
                            }
                        }
                    }
                    else
                    {
                        var uepFields = await _fieldRepository.GetFieldsByUepCode(production.Installation.UepCod);
                        var btpsUEP = await _btpRepository
                            .GetBtpDatasByUEP(production.Installation.UepCod);

                        var filtredByApplyDateAndFinal = btpsUEP
                                .Where(x => (x.FinalApplicationDate == null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                                || (x.FinalApplicationDate != null && x.ApplicationDate != null && x.Well.CategoryOperator is not null && x.Well.CategoryOperator.ToUpper() == "PRODUTOR" && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                                && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                        decimal totalPotencialGasUEP = 0;
                        decimal totalPotencialOilUEP = 0;
                        decimal totalPotencialWaterUEP = 0;

                        //LOOP INTERVAL - CALC SUM UEP
                        foreach (var btp in filtredByApplyDateAndFinal)
                        {
                            double totalInterval = 0;

                            var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                            || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                            foreach (var a in filtredEvents)
                            {
                                if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                                {
                                    totalInterval += ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                }
                                else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                                {
                                    totalInterval += 24;
                                }
                                else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                                {
                                    totalInterval += ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                }
                                else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                                {
                                    totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                }
                                else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                                {
                                    totalInterval += 24;
                                }
                                else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                                {
                                    totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                }
                            }
                            totalPotencialGasUEP += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                            totalPotencialOilUEP += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                            totalPotencialWaterUEP += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                        }

                        foreach (var fieldInDatabase in uepFields)
                        {
                            var wellAppropiationsDto = new List<WellProductionDto>();

                            var fieldProduction = await _productionRepository
                                .GetFieldProductionByFieldAndProductionId(fieldInDatabase.Id, production.Id);

                            var totalGas = 0m;
                            var totalOil = 0m;
                            var totalWater = 0m;

                            var btpsField = await _btpRepository
                                    .GetBtpDatasByFieldId(fieldInDatabase.Id);
                            var filtredsBTPsField = btpsField
                                .Where(x => (x.FinalApplicationDate == null && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date)
                                || (x.FinalApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= production.MeasuredAt.Date
                                && DateTime.Parse(x.ApplicationDate) <= production.MeasuredAt.Date));

                            var totalGasPotencialField = filtredsBTPsField
                                .Sum(x => x.PotencialGas);
                            var totalOilPotencialField = filtredsBTPsField
                                .Sum(x => x.PotencialOil);
                            var totalWaterPotencialField = filtredsBTPsField
                                .Sum(x => x.PotencialWater);

                            decimal totalPotencialGasField = 0;
                            decimal totalPotencialOilField = 0;
                            decimal totalPotencialWaterField = 0;

                            foreach (var btp in filtredsBTPsField)
                            {
                                double totalInterval = 0;
                                var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                                foreach (var a in filtredEvents)
                                {
                                    if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                                    {
                                        totalInterval += ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                                    {
                                        totalInterval += 24;
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                                    {
                                        totalInterval += ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                                    {
                                        totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                                    {
                                        totalInterval += 24;
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                                    {
                                        totalInterval += ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                    }
                                }

                                totalPotencialGasField += btp.PotencialGas * (24 - (decimal)totalInterval) / 24;
                                totalPotencialOilField += btp.PotencialOil * (24 - (decimal)totalInterval) / 24;
                                totalPotencialWaterField += btp.PotencialWater * (24 - (decimal)totalInterval) / 24;
                            }

                            foreach (var btp in filtredsBTPsField)
                            {
                                double totalInterval = 0;
                                var filtredEvents = btp.Well.WellEvents.Where(x => x.StartDate.Date <= production.MeasuredAt && x.EndDate == null && x.EventStatus == "F"
                                        || x.StartDate.Date <= production.MeasuredAt && x.EndDate != null && x.EndDate >= production.MeasuredAt && x.EventStatus == "F").OrderBy(x => x.StartDate);

                                var listEvents = new List<CreateWellLossDTO>();
                                foreach (var a in filtredEvents)
                                {
                                    if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt)
                                    {
                                        double interval = ((a.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)interval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt)
                                    {
                                        double interval = 24;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)interval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date == production.MeasuredAt.Date)
                                    {
                                        double interval = ((a.EndDate.Value - a.StartDate).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt.Date && a.EndDate is not null && a.EndDate.Value.Date > production.MeasuredAt.Date)
                                    {
                                        double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate < production.MeasuredAt && a.EndDate is null)
                                    {
                                        double interval = 24;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                    else if (a.StartDate.Date == production.MeasuredAt && a.EndDate is null)
                                    {
                                        double interval = ((production.MeasuredAt.AddDays(1) - a.StartDate).TotalMinutes) / 60;
                                        totalInterval += interval;
                                        listEvents.Add(new CreateWellLossDTO
                                        {
                                            Downtime = (decimal)totalInterval,
                                            Event = a,
                                            Id = Guid.NewGuid(),
                                            MeasuredAt = production.MeasuredAt
                                        });
                                    }
                                }

                                var wellPotencialGasAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasUEP);
                                var wellPotencialOilAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilUEP);
                                var wellPotencialWaterAsPercentageOfUEP = WellProductionUtils.CalculateWellProductionAsPercentageOfUEP((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterUEP);
                                var wellPotencialGasAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialGas * ((24 - (decimal)totalInterval) / 24)), totalPotencialGasField);
                                var wellPotencialOilAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialOil * ((24 - (decimal)totalInterval) / 24)), totalPotencialOilField);
                                var wellPotencialWaterAsPercentageOfField = WellProductionUtils.CalculateWellProductionAsPercentageOfField((btp.PotencialWater * ((24 - (decimal)totalInterval) / 24)), totalPotencialWaterField);
                                var productionGas = wellPotencialGasAsPercentageOfUEP * ((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0));
                                var productionOIl = production.Oil.TotalOil * wellPotencialOilAsPercentageOfUEP;
                                var calcBSWOil = (100 - btp.BSW) / 100;
                                var calcBSWWater = btp.BSW / 100;
                                var productionWater = (productionOIl * calcBSWWater) / calcBSWOil;

                                int hours = (int)totalInterval;
                                double minutesDecimal = (totalInterval - hours) * 60;
                                int minutes = (int)minutesDecimal;
                                double secondsDecimal = (minutesDecimal - minutes) * 60;
                                int seconds = (int)secondsDecimal;
                                DateTime dateTime = DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
                                string formattedTime = dateTime.ToString("HH:mm:ss");


                                var wellProd = await _productionRepository
                                    .GetWellProductionByWellAndProductionId(btp.Well.Id, production.Id);

                                if (wellProd is not null)
                                {
                                    wellProd.ProductionGasAsPercentageOfInstallation = wellPotencialGasAsPercentageOfUEP;
                                    wellProd.ProductionOilAsPercentageOfInstallation = wellPotencialOilAsPercentageOfUEP;
                                    wellProd.ProductionWaterAsPercentageOfInstallation = wellPotencialWaterAsPercentageOfUEP;

                                    wellProd.ProductionGasInWellM3 = productionGas;
                                    wellProd.ProductionGasInWellSCF = ProductionUtils.m3ToSCFConversionMultipler * productionGas;

                                    wellProd.ProductionOilInWellM3 = productionOIl;
                                    wellProd.ProductionOilInWellBBL = ProductionUtils.m3ToBBLConversionMultiplier * productionOIl;

                                    wellProd.ProductionWaterInWellM3 = productionWater;
                                    wellProd.ProductionWaterInWellBBL = ProductionUtils.m3ToBBLConversionMultiplier * productionWater;
                                    wellProd.ProductionGasAsPercentageOfField = wellPotencialGasAsPercentageOfField;
                                    wellProd.ProductionOilAsPercentageOfField = wellPotencialOilAsPercentageOfField;
                                    wellProd.ProductionWaterAsPercentageOfField = wellPotencialWaterAsPercentageOfField;

                                    wellProd.Downtime = formattedTime;

                                    totalWater += wellProd.ProductionWaterInWellM3;
                                    totalOil += wellProd.ProductionOilInWellM3;
                                    totalGas += wellProd.ProductionGasInWellM3;

                                    if (fieldProduction is not null)
                                        fieldProduction.FieldId = btp.Well.Field.Id;

                                    foreach (var ev in listEvents)
                                    {
                                        int year = production.MeasuredAt.Year;
                                        int month = production.MeasuredAt.Month;

                                        int daysInMonth = DateTime.DaysInMonth(year, month);

                                        var wellLoss = await _productionRepository
                                                .GetWellLossByEventAndWellProductionId(ev.Id, wellProd.Id);

                                        if (wellLoss is not null)
                                        {
                                            wellLoss.Downtime = ev.Downtime;
                                            wellLoss.MeasuredAt = ev.MeasuredAt;

                                            wellLoss.EfficienceLoss = (((btp.PotencialOil * ev.Downtime) / 24) / totalOilPotencialField) / daysInMonth;
                                            wellLoss.ProductionLostOil = (btp.PotencialOil * ev.Downtime) / 24;
                                            wellLoss.ProportionalDay = ((btp.PotencialOil * ev.Downtime) / 24) / totalOilPotencialField;

                                            wellProd.EfficienceLoss += wellLoss.EfficienceLoss;
                                            wellProd.ProductionLostOil += wellLoss.ProductionLostOil;
                                            wellProd.ProportionalDay += wellLoss.ProportionalDay;

                                            wellLoss.ProductionLostGas = (btp.PotencialGas * ev.Downtime) / 24;

                                            wellProd.ProductionLostGas += wellLoss.ProductionLostGas;

                                            wellLoss.ProductionLostWater = (btp.PotencialWater * ev.Downtime) / 24;

                                            wellProd.ProductionLostWater += wellLoss.ProductionLostWater;

                                            _wellProductionRepository.UpdateWellLost(wellLoss);
                                        }
                                    }

                                    _wellProductionRepository.Update(wellProd);
                                }

                            }

                            if (fieldProduction is not null)
                            {
                                fieldProduction.WaterProductionInField = totalWater;
                                fieldProduction.GasProductionInField = totalGas;
                                fieldProduction.OilProductionInField = totalOil;

                                totalWaterInUep += fieldProduction.WaterProductionInField;

                                _productionRepository.UpdateFieldProduction(fieldProduction);

                                var listProductions = await _wellProductionRepository.getAllFieldsProductionsByProductionId(production.Id);

                                foreach (var fieldP in listProductions)
                                {
                                    foreach (var wellProduction in fieldP.WellProductions)
                                    {
                                        var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                                        foreach (var completion in wellInDatabase.Completions)
                                        {
                                            var reservoirProduction = await _wellProductionRepository.GetReservoirProductionForWellAndReservoir(production.Id, completion.Reservoir.Id);

                                            var zoneProduction = await _wellProductionRepository.GetZoneProductionForWellAndReservoir(production.Id, completion.Reservoir.Zone.Id);
                                            var completionProductionInDatabase = await _wellProductionRepository
                                                .GetCompletionProduction(completion.Id, production.Id);

                                            if (completionProductionInDatabase is null)
                                                throw new NotFoundException($"Distribuição da produção da completação no dia: {production.MeasuredAt} não encontrada.");

                                            if (reservoirProduction is null)
                                                throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                            if (zoneProduction is null)
                                                throw new NotFoundException($"Distribuição da produção do reservatório no dia: {production.MeasuredAt} não encontrada.");

                                            completionProductionInDatabase.OilProductionInCompletion = 0m;
                                            completionProductionInDatabase.GasProductionInCompletion = 0m;
                                            completionProductionInDatabase.WaterProductionInCompletion = 0m;

                                            _wellProductionRepository.UpdateCompletionProduction(completionProductionInDatabase);

                                            reservoirProduction.OilProductionInReservoir = 0m;
                                            reservoirProduction.GasProductionInReservoir = 0m;
                                            reservoirProduction.WaterProductionInReservoir = 0m;

                                            _wellProductionRepository.UpdateReservoirProduction(reservoirProduction);

                                            zoneProduction.OilProductionInZone = 0m;
                                            zoneProduction.GasProductionInZone = 0m;
                                            zoneProduction.WaterProductionInZone = 0m;

                                            _wellProductionRepository.UpdateZoneProduction(zoneProduction);

                                        }
                                    }

                                }

                                foreach (var fieldP in listProductions)
                                {
                                    foreach (var wellProduction in fieldP.WellProductions)
                                    {
                                        var wellInDatabase = await _wellRepository.GetByIdAsync(wellProduction.WellId);

                                        foreach (var completion in wellInDatabase.Completions)
                                        {
                                            var reservoirProduction = await _wellProductionRepository.GetReservoirProductionForWellAndReservoir(production.Id, completion.Reservoir.Id);
                                            var zoneProduction = await _wellProductionRepository.GetZoneProductionForWellAndReservoir(production.Id, completion.Reservoir.Zone.Id);

                                            var allocationReservoir = completion.AllocationReservoir.Value;
                                            var completionProductionInDatabase = await _wellProductionRepository
                                                .GetCompletionProduction(completion.Id, production.Id);

                                            completionProductionInDatabase.GasProductionInCompletion = allocationReservoir * wellProduction.ProductionGasInWellM3;
                                            completionProductionInDatabase.OilProductionInCompletion = allocationReservoir * wellProduction.ProductionOilInWellM3;
                                            completionProductionInDatabase.WaterProductionInCompletion = allocationReservoir * wellProduction.ProductionWaterInWellM3;

                                            _wellProductionRepository.UpdateCompletionProduction(completionProductionInDatabase);

                                            reservoirProduction.GasProductionInReservoir += completionProductionInDatabase.GasProductionInCompletion;
                                            reservoirProduction.OilProductionInReservoir += completionProductionInDatabase.OilProductionInCompletion;
                                            reservoirProduction.WaterProductionInReservoir += completionProductionInDatabase.WaterProductionInCompletion;

                                            zoneProduction.GasProductionInZone += completionProductionInDatabase.GasProductionInCompletion;
                                            zoneProduction.OilProductionInZone += completionProductionInDatabase.OilProductionInCompletion;
                                            zoneProduction.WaterProductionInZone += completionProductionInDatabase.WaterProductionInCompletion;

                                            _wellProductionRepository.UpdateZoneProduction(zoneProduction);

                                        }
                                    }
                                }
                            }
                        }

                        await _wellProductionRepository.Save();


                    }

                    if (production.Water is not null)
                        production.Water.TotalWater = totalWaterInUep;

                    _productionRepository.Update(production);

                    await _wellProductionRepository.Save();
                }

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
                    IdAutoGenerated = $"{well.Field?.Name?[..3]}{codeSequencial} {well.Name}",
                    StateANP = body.StateAnp,
                    StatusANP = body.StatusAnp,
                    Well = well,
                    CreatedBy = loggedUser,

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
                        if (lastEventReason.StartDate < parsedStartDate && lastEventReason.EndDate is null)
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

                            for (int j = 0; j < dif; j++)
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
                    .OrderBy(e => e.StartDate)
                    .LastOrDefault();

                if (lastEvent is not null && lastEvent.EventStatus == eventType)
                {
                    var lastEventReason = lastEvent.EventReasons
                        .OrderBy(x => x.StartDate)
                        .LastOrDefault();

                    var wellDto = new WellWithEventDto
                    {
                        EventId = lastEvent.Id,
                        AutoGeneratedId = lastEvent.IdAutoGenerated,
                        WellId = well.Id,
                        Status = lastEvent.EventStatus,
                        DateLastStatus = lastEventReason is not null ? lastEventReason.StartDate.ToString("dd/MM/yyyy HH:mm") : lastEvent.StartDate.ToString("dd/MM/yyyy HH:mm"),
                        CategoryOperator = well.CategoryOperator,
                        Name = well.Name
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
                        Name = ev.Well.Name
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
                    (x.StartDate.Date <= day && (x.EndDate == null || x.EndDate.Value.Date >= day))
                    || (x.StartDate.Date < day && x.EndDate != null && x.EndDate.Value.Date >= day)
                    )
                    .Select(eventItem =>
                    {
                        eventItem.EventReasons = eventItem.EventReasons
                            .Where(reason => (reason.StartDate.Date <= day && reason.EndDate != null && reason.EndDate.Value.Date >= day)
                            || (reason.StartDate.Date <= day && reason.EndDate == null))
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
                        Name = ev.Well.Name
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

            //var lastEventReason = _wellEventRepository.EventReasons
            //  .OrderBy(x => x.CreatedAt)
            //  .LastOrDefault();

            //if (lastEventReason is not null && lastEventReason.SystemRelated.ToLower() == body.SystemRelated.ToLower())
            //    throw new BadRequestException("Sistema relacionado deve ser diferente do anterior");

            var systemsRelated = new List<string>
            {
                "submarino","topside","estratégia"
            };

            if (systemsRelated.Contains(body.SystemRelated.ToLower()) is false)
                throw new BadRequestException($"Sistemas relacionados permitidos são: {string.Join(", ", systemsRelated)}");


            wellReason.SystemRelated = body.SystemRelated;
            wellReason.UpdatedBy = loggedUser;

            _wellEventRepository.UpdateReason(wellReason);

            var updatedByUser = _mapper.Map<UserDTO>(loggedUser);
            var createdByUser = _mapper.Map<UserDTO>(wellReason.CreatedBy);

            var dto = new ReasonDetailedDto
            {
                Id = wellReason.Id,
                StartDate = wellReason.StartDate.ToString("dd/MM/yyyy HH:mm"),
                SystemRelated = wellReason.SystemRelated,
                Downtime = wellReason.Interval,
                EndDate = wellReason.EndDate?.ToString("dd/MM/yyyy HH:mm"),
                UpdatedBy = updatedByUser,
                CreatedBy = createdByUser
            };

            await _wellEventRepository.Save();
            return dto;
        }
        public async Task UpdateClosedEvent(Guid eventId, UpdateEventAndSystemRelated body, User loggedUser)
        {
            if (body.SystemRelated is null && body.DateSystemRelated is not null)
                throw new BadRequestException("Sistema relacionado é obrigatório.");

            if (body.SystemRelated is not null && body.DateSystemRelated is null)
                throw new BadRequestException("Data do sistema relacionado é obrigatória.");

            var closingEvent = await _wellEventRepository
                .GetEventById(eventId);

            if (closingEvent is null)
                throw new NotFoundException("Evento não encontrado.");

            if (closingEvent.EventStatus != "F")
                throw new NotFoundException("Evento deve ser de fechamento para ser atualizado.");

            if ((closingEvent.WellLosses is null || closingEvent.WellLosses.Any()) && (body.EventDateAndHour is not null || body.DateSystemRelated is not null))
                throw new ConflictException("Não é possível editar um evento após a produção ter sido apropriada.");

            if (closingEvent.EventReasons.Any() is false)
                throw new BadRequestException("É preciso ter um motivo anterior.");

            var lastEventReason = closingEvent.EventReasons
              .OrderBy(x => x.StartDate)
              .LastOrDefault();

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
                    if (closingEvent.EndDate is not null && parsedStartDate > closingEvent.EndDate)
                        throw new BadRequestException("Data de início do evento não pode ser maior que data de fim.");

                    if (parsedStartDate < closingEvent.EventRelated.StartDate)
                        throw new ConflictException($"Não é possível atualizar o evento para uma data anterior a data de início do evento relacionado: {closingEvent.EventRelated.IdAutoGenerated}.");

                    var currentDate = closingEvent.StartDate;

                    if (closingEvent.EndDate.HasValue)
                    {
                        while (currentDate <= closingEvent.EndDate)
                        {
                            var productionInDate = await _productionRepository.GetCleanByDate(currentDate);

                            if (productionInDate is not null && productionInDate.IsCalculated)
                                throw new BadRequestException($"Não é possível editar evento, existe uma produção já apropriada no dia: {currentDate:dd/MM/yyyy}");

                            currentDate = currentDate.AddDays(1);
                        }
                    }

                    else
                    {
                        currentDate = closingEvent.StartDate;

                        while (currentDate <= dateNow)
                        {
                            var productionInDate = await _productionRepository.GetCleanByDate(currentDate);

                            if (productionInDate is not null && productionInDate.IsCalculated)
                                throw new BadRequestException($"Não é possível editar evento, existe uma produção já apropriada no dia: {currentDate:dd/MM/yyyy}");

                            currentDate = currentDate.AddDays(1);
                        }
                    }

                    var firstEventReason = closingEvent.EventReasons
                        .OrderBy(x => x.StartDate)
                        .FirstOrDefault();

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

                    closingEvent.StartDate = parsedStartDate;
                    closingEvent.EventRelated.EndDate = parsedStartDate;
                    closingEvent.EventRelated.Interval = (parsedStartDate - closingEvent.EventRelated.StartDate).TotalHours;

                    if (closingEvent.EndDate is not null)
                    {
                        closingEvent.Interval = (closingEvent.EndDate.Value - parsedStartDate).TotalHours;
                    }

                    _wellEventRepository.Update(closingEvent);
                }
                else
                    throw new BadRequestException("Formato de data de início inválido deve ser 'dd/MM/yy HH:mm'.");
            }

            if (lastEventReason is not null)
            {
                if (DateTime.TryParseExact(body.DateSystemRelated, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateSystem) is true)
                {
                    if (parsedDateSystem < closingEvent.EventRelated.StartDate)
                        throw new ConflictException($"Não é possível adicionar um sistema para uma data anterior a data de início do evento relacionado: {closingEvent.EventRelated.IdAutoGenerated}.");

                    var currentDate = parsedDateSystem;

                    while (currentDate <= parsedDateSystem)
                    {
                        var productionInDate = await _productionRepository.GetCleanByDate(currentDate);

                        if (productionInDate is not null && productionInDate.IsCalculated)
                            throw new BadRequestException($"Não é possível adicionar sistema relacionado, existe uma produção já apropriada no dia: {currentDate:dd/MM/yyyy}");

                        currentDate = currentDate.AddDays(1);
                    }


                    if (lastEventReason.StartDate < dateNow && lastEventReason.EndDate is null && body.SystemRelated is not null)
                    {
                        var dif = (dateNow - lastEventReason.StartDate).TotalHours / 24;
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

                        for (int j = 0; j < dif; j++)
                        {
                            var newEventReason = new EventReason
                            {
                                Id = Guid.NewGuid(),
                                SystemRelated = lastEventReason.SystemRelated,
                                Comment = lastEventReason.Comment,
                                WellEvent = closingEvent,
                                StartDate = refStartDate,
                                IsActive = true,
                                IsJobGenerated = false,
                                CreatedBy = loggedUser
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
                                    WellEvent = closingEvent,
                                    StartDate = refStartDate,
                                    EndDate = parsedDateSystem,
                                    IsActive = true,
                                    IsJobGenerated = false,
                                    CreatedBy = loggedUser

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
