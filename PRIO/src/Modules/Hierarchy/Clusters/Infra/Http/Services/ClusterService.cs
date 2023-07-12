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
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableClusters;

        public ClusterService(IMapper mapper, IClusterRepository clusterRepository, SystemHistoryService systemHistoryService, IInstallationRepository installationRepository, IFieldRepository fieldRepository, IZoneRepository zoneRepository, IWellRepository wellRepository, IReservoirRepository reservoirRepository, ICompletionRepository completionRepository)
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
        }

        public async Task<ClusterDTO> CreateCluster(CreateClusterViewModel body, User user)
        {
            var cluster = await _clusterRepository.GetByCod(body.CodCluster);
            if (cluster is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Cluster>());

            var clusterId = Guid.NewGuid();
            cluster = new Cluster
            {
                Id = clusterId,
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                CodCluster = body.CodCluster is not null ? body.CodCluster : "N/A"
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

            if (cluster.Installations.Count > 0)
                if (body.CodCluster is not null)
                    if (body.CodCluster != cluster.CodCluster)
                        throw new ConflictException("Código do Cluster não pode ser alterado.");

            if (body.CodCluster is not null)
            {
                var clusterInDatabase = await _clusterRepository.GetByCod(body.CodCluster);
                if (clusterInDatabase is not null)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Cluster>());
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

        public async Task DeleteCluster(Guid id, User user)
        {
            var cluster = await _clusterRepository
                .GetClusterAndChildren(id);

            if (cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            if (cluster.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Cluster>());

            var clusterPropertiesToUpdate = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            if (cluster.Installations is not null)
                foreach (var installation in cluster.Installations)
                {
                    var installationPropertiesToUpdate = new
                    {
                        IsActive = false,
                        DeletedAt = DateTime.UtcNow,
                    };

                    var installationUpdatedProperties = UpdateFields
                    .CompareUpdateReturnOnlyUpdated(installation, installationPropertiesToUpdate);

                    await _systemHistoryService
                        .Delete<Installation, InstallationHistoryDTO>(HistoryColumns.TableInstallations, user, installationUpdatedProperties, installation.Id, installation);

                    _installationRepository.Delete(installation);

                    if (installation.Fields is not null)
                        foreach (var field in installation.Fields)
                        {
                            var fieldPropertiesToUpdate = new
                            {
                                IsActive = false,
                                DeletedAt = DateTime.UtcNow,
                            };

                            var fieldUpdatedProperties = UpdateFields
                            .CompareUpdateReturnOnlyUpdated(field, fieldPropertiesToUpdate);

                            await _systemHistoryService
                                .Delete<Field, FieldHistoryDTO>(HistoryColumns.TableFields, user, fieldUpdatedProperties, field.Id, field);

                            _fieldRepository.Delete(field);

                            if (field.Zones is not null)
                                foreach (var zone in field.Zones)
                                {
                                    var zonePropertiesToUpdate = new
                                    {
                                        IsActive = false,
                                        DeletedAt = DateTime.UtcNow,
                                    };

                                    var zoneUpdatedProperties = UpdateFields
                                    .CompareUpdateReturnOnlyUpdated(zone, zonePropertiesToUpdate);

                                    await _systemHistoryService
                                        .Delete<Zone, ZoneHistoryDTO>(HistoryColumns.TableZones, user, zoneUpdatedProperties, zone.Id, zone);

                                    _zoneRepository.Delete(zone);

                                    if (zone.Reservoirs is not null)
                                        foreach (var reservoir in zone.Reservoirs)
                                        {
                                            var reservoirPropertiesToUpdate = new
                                            {
                                                IsActive = false,
                                                DeletedAt = DateTime.UtcNow,
                                            };

                                            var reservoirUpdatedProperties = UpdateFields
                                            .CompareUpdateReturnOnlyUpdated(reservoir, reservoirPropertiesToUpdate);

                                            await _systemHistoryService
                                                .Delete<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, reservoirUpdatedProperties, reservoir.Id, reservoir);

                                            _reservoirRepository.Delete(reservoir);
                                        }
                                }

                            if (field.Wells is not null)
                                foreach (var well in field.Wells)
                                {
                                    var wellPropertiesToUpdate = new
                                    {
                                        IsActive = false,
                                        DeletedAt = DateTime.UtcNow,
                                    };

                                    var wellUpdatedProperties = UpdateFields
                                    .CompareUpdateReturnOnlyUpdated(well, wellPropertiesToUpdate);

                                    await _systemHistoryService
                                        .Delete<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, wellUpdatedProperties, well.Id, well);

                                    _wellRepository.Delete(well);


                                    if (well.Completions is not null)
                                        foreach (var completion in well.Completions)
                                        {
                                            var completionPropertiesToUpdate = new
                                            {
                                                IsActive = false,
                                                DeletedAt = DateTime.UtcNow,
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


            var clusterUpdatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(cluster, clusterPropertiesToUpdate);

            await _systemHistoryService
                .Delete<Cluster, ClusterHistoryDTO>(_tableName, user, clusterUpdatedProperties, cluster.Id, cluster);

            _clusterRepository.DeleteCluster(cluster);

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
