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
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Services
{
    public class ClusterService
    {
        private readonly IMapper _mapper;
        private readonly IClusterRepository _clusterRepository;
        private readonly ISystemHistoryRepository _systemHistoryRepository;

        public ClusterService(IMapper mapper, IClusterRepository clusterRepository, ISystemHistoryRepository systemHistoryRepository)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
            _systemHistoryRepository = systemHistoryRepository;
        }

        public async Task<ClusterDTO> CreateCluster(CreateClusterViewModel body, User user)
        {
            var clusterId = Guid.NewGuid();

            var cluster = new Cluster
            {
                Id = clusterId,
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                CodCluster = body.CodCluster is not null ? body.CodCluster : GenerateCode.Generate(body.Name)
            };

            await _clusterRepository.AddClusterAsync(cluster);

            var currentData = _mapper.Map<Cluster, ClusterHistoryDTO>(cluster);
            var currentDate = DateTime.UtcNow;
            currentData.createdAt = currentDate;
            currentData.updatedAt = currentDate;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableClusters,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = clusterId,

                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);

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
                throw new NotFoundException("Cluster not found");

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return clusterDTO;
        }

        public async Task<ClusterDTO> UpdateCluster(Guid id, UpdateClusterViewModel body, User user)
        {
            var cluster = await _clusterRepository.GetClusterByIdAsync(id);

            if (cluster is null)
                throw new NotFoundException("Cluster not found");

            var beforeChangesCluster = _mapper.Map<ClusterHistoryDTO>(cluster);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(cluster, body);

            if (updatedProperties.Any() is false)
                throw new BadRequestException("This cluster already has these values, try to update to other values.");

            _clusterRepository.UpdateCluster(cluster);

            var firstHistory = await _systemHistoryRepository.GetFirst(id);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Cluster, ClusterHistoryDTO>(cluster);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableClusters,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = cluster.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesCluster,
            };

            await _systemHistoryRepository.AddAsync(history);
            await _clusterRepository.SaveChangesAsync();

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return clusterDTO;
        }

        public async Task DeleteCluster(Guid id, User user)
        {
            var cluster = await _clusterRepository.GetClusterByIdAsync(id);

            if (cluster is null || cluster.IsActive is false)
                throw new NotFoundException("Cluster not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            cluster.IsActive = false;
            cluster.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Cluster, ClusterHistoryDTO>(cluster);
            currentData.updatedAt = DateTime.UtcNow;
            currentData.deletedAt = cluster.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableClusters,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = cluster.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = cluster.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    cluster.IsActive,
                    cluster.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _clusterRepository.DeleteCluster(cluster);

            await _clusterRepository.SaveChangesAsync();
        }

        public async Task<ClusterDTO> RestoreCluster(Guid id, User user)
        {
            var cluster = await _clusterRepository.GetClusterByIdAsync(id);

            if (cluster is null || cluster.IsActive is true)
                throw new NotFoundException("Cluster not found or active already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            cluster.IsActive = true;
            cluster.DeletedAt = null;

            var currentData = _mapper.Map<Cluster, ClusterHistoryDTO>(cluster);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableClusters,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = cluster.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = cluster.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    cluster.IsActive,
                    cluster.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _clusterRepository.RestoreCluster(cluster);
            await _clusterRepository.SaveChangesAsync();

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return clusterDTO;
        }

        public async Task<List<SystemHistory>> GetClusterHistory(Guid id)
        {
            var clusterHistories = await _systemHistoryRepository.GetAll(id);

            if (clusterHistories is null)
                throw new NotFoundException("Cluster not found");

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
