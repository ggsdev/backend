using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Dtos;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.ViewModels;
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
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableClusters;

        public ClusterService(IMapper mapper, IClusterRepository clusterRepository, SystemHistoryService systemHistoryService)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
            _systemHistoryService = systemHistoryService;
        }

        public async Task<ClusterDTO> CreateCluster(CreateClusterViewModel body, User user)
        {
            var cluster = await _clusterRepository.GetByCod(body.CodCluster);
            if (cluster is not null)
                throw new ConflictException("Cluster com esse código já existe, tente outro");

            var clusterId = Guid.NewGuid();
            cluster = new Cluster
            {
                Id = clusterId,
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                CodCluster = body.CodCluster is not null ? body.CodCluster : GenerateCode.Generate(body.Name)
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
            var cluster = await _clusterRepository.GetClusterByIdAsync(id);

            if (cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

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
            var cluster = await _clusterRepository.GetClusterByIdAsync(id);
            if (cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            if (cluster.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Cluster>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(cluster, propertiesUpdated);

            await _systemHistoryService
                .Delete<Cluster, ClusterHistoryDTO>(_tableName, user, updatedProperties, cluster.Id, cluster);

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
