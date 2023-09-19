using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Dtos;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.ViewModels;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Services
{
    public class ClusterService
    {
        private readonly IMapper _mapper;
        private readonly IClusterRepository _clusterRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly IWellRepository _wellRepository;
        private readonly ICompletionRepository _completionRepository;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly IMeasuringPointRepository _pointRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IWellEventRepository _eventWellRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableClusters;

        public ClusterService(IMapper mapper, IClusterRepository clusterRepository, SystemHistoryService systemHistoryService, IInstallationRepository installationRepository, IFieldRepository fieldRepository, IZoneRepository zoneRepository, IWellRepository wellRepository, IReservoirRepository reservoirRepository, ICompletionRepository completionRepository, IMeasuringPointRepository measuringPointRepository, IEquipmentRepository equipmentRepository, IWellEventRepository wellEventRepository)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
            _zoneRepository = zoneRepository;
            _reservoirRepository = reservoirRepository;
            _wellRepository = wellRepository;
            _completionRepository = completionRepository;
            _systemHistoryService = systemHistoryService;
            _pointRepository = measuringPointRepository;
            _equipmentRepository = equipmentRepository;
            _eventWellRepository = wellEventRepository;
        }

        public async Task<ClusterDTO> CreateCluster(CreateClusterViewModel body, User user)
        {
            var cluster = await _clusterRepository.GetClusterByNameAsync(body.Name);
            if (cluster is not null)
                throw new ConflictException($"Já existe um Cluster com esse nome: {body.Name}");

            var clusterId = Guid.NewGuid();
            cluster = new Cluster
            {
                Id = clusterId,
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _clusterRepository.AddClusterAsync(cluster);

            await _systemHistoryService
                .Create<Cluster, ClusterHistoryDTO>(_tableName, user, clusterId, cluster);

            await _clusterRepository.SaveChangesAsync();

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return clusterDTO;
        }
        public async Task<List<ClusterDTO>> GetClusters()
        {
            var clusters = await _clusterRepository.GetAllClustersAsync();
            var clustersDTO = _mapper.Map<List<Cluster>, List<ClusterDTO>>(clusters);
            return clustersDTO;
        }

        public async Task<ClusterDTO> GetClusterById(Guid id)
        {
            var cluster = await _clusterRepository.GetClusterByIdAsync(id);

            if (cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return clusterDTO;
        }

        public async Task<ClusterDTO> UpdateCluster(Guid id, UpdateClusterViewModel body, User user)
        {
            var cluster = await _clusterRepository.GetClusterWithInstallationsAsync(id);

            if (cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            if (cluster.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Cluster>());

            if (body.Name is not null)
            {
                var clusterInDatabase = await _clusterRepository.GetClusterByNameAsync(body.Name);
                if (clusterInDatabase is not null && clusterInDatabase.Id != cluster.Id)
                    throw new ConflictException($"Já existe um Cluster com esse nome: {body.Name}");
            }

            var beforeChangesCluster = _mapper.Map<ClusterHistoryDTO>(cluster);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(cluster, body);
            if (updatedProperties.Any() is false)
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Cluster>());

            _clusterRepository.UpdateCluster(cluster);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, cluster.Id, cluster, beforeChangesCluster);

            await _clusterRepository.SaveChangesAsync();

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return clusterDTO;
        }

        public async Task DeleteCluster(Guid id, User user, string StatusDate)
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

            var cluster = await _clusterRepository
                .GetClusterAndChildren(id);

            if (cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            if (cluster.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Cluster>());

            var clusterPropertiesToUpdate = new
            {
                IsActive = false,
                InactivatedAt = date,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
            };
            var clusterUpdatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(cluster, clusterPropertiesToUpdate);

            await _systemHistoryService
                .Delete<Cluster, ClusterHistoryDTO>(_tableName, user, clusterUpdatedProperties, cluster.Id, cluster);

            _clusterRepository.DeleteCluster(cluster);

            if (cluster.Installations is not null)
                foreach (var installation in cluster.Installations)
                {
                    if (installation.IsActive is true)
                    {
                        var installationPropertiesToUpdate = new
                        {
                            IsActive = false,
                            InactivatedAt = date,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                        };

                        var installationUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(installation, installationPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Installation, InstallationHistoryDTO>(HistoryColumns.TableInstallations, user, installationUpdatedProperties, installation.Id, installation);

                        _installationRepository.Delete(installation);
                    }

                    if (installation.MeasuringPoints is not null)
                        foreach (var point in installation.MeasuringPoints)
                        {
                            if (point.IsActive is true)
                            {
                                var pointPropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var pointUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(point, pointPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<MeasuringPoint, MeasuringPointHistoryDTO>(HistoryColumns.TableMeasuringPoints, user, pointUpdatedProperties, point.Id, point);

                                _pointRepository.Delete(point);
                            }
                            if (point.MeasuringEquipments is not null)
                                foreach (var equipment in point.MeasuringEquipments)
                                {
                                    if (equipment.IsActive is true)
                                    {
                                        var equipmentPropertiesToUpdate = new
                                        {
                                            IsActive = false,
                                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                                        };

                                        var equipmentUpdatedProperties = UpdateFields
                                        .CompareUpdateReturnOnlyUpdated(equipment, equipmentPropertiesToUpdate);

                                        await _systemHistoryService
                                            .Delete<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(HistoryColumns.TableEquipments, user, equipmentUpdatedProperties, equipment.Id, equipment);

                                        _equipmentRepository.Delete(equipment);
                                    }
                                }
                        }

                    if (installation.Fields is not null)
                        foreach (var field in installation.Fields)
                        {
                            if (field.IsActive is true)
                            {
                                var fieldPropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    InactivatedAt = date,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var fieldUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(field, fieldPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Field, FieldHistoryDTO>(HistoryColumns.TableFields, user, fieldUpdatedProperties, field.Id, field);

                                _fieldRepository.Delete(field);
                            }

                            if (field.Zones is not null)
                                foreach (var zone in field.Zones)
                                {
                                    if (zone.IsActive is true)
                                    {
                                        var zonePropertiesToUpdate = new
                                        {
                                            IsActive = false,
                                            InactivatedAt = date,
                                            DeletedAt = DateTime.UtcNow.AddHours(-3),
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
                                            InactivatedAt = date,
                                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                                        };

                                        var wellUpdatedProperties = UpdateFields
                                        .CompareUpdateReturnOnlyUpdated(well, wellPropertiesToUpdate);

                                        await _systemHistoryService
                                            .Delete<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, wellUpdatedProperties, well.Id, well);

                                        var lastEventOfAll = well.WellEvents
                                           .OrderBy(e => e.StartDate)
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
                                            //await _eventWellRepository.AddReasonClosedEvent(newEventReason);

                                            //eventReason.Interval = (date - lastEventOfAll.StartDate).TotalHours.ToString();
                                            //eventReason.EndDate = date;

                                            //_eventWellRepository.UpdateReason(eventReason);
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
                        }
                }

            await _clusterRepository.SaveChangesAsync();
        }

        public async Task<ClusterDTO> RestoreCluster(Guid id, User user)
        {
            var cluster = await _clusterRepository.GetClusterByIdAsync(id);

            if (cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            if (cluster.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Cluster>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(cluster, propertiesUpdated);

            await _systemHistoryService
                .Restore<Cluster, ClusterHistoryDTO>(_tableName, user, updatedProperties, cluster.Id, cluster);

            _clusterRepository.RestoreCluster(cluster);
            await _clusterRepository.SaveChangesAsync();

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return clusterDTO;
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
        public async Task<List<SystemHistory>> GetClusterHistory(Guid id)
        {
            var clusterHistories = await _systemHistoryService
                .GetAll(id);

            foreach (var history in clusterHistories)
            {
                history.PreviousData = history.PreviousData is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!)
                    : null;

                history.CurrentData = history.CurrentData is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!)
                    : null;

                history.FieldsChanged = history.FieldsChanged is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!)
                    : null;
            }

            return clusterHistories;
        }
    }
}
