using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces
{
    public interface IClusterRepository
    {
        Task AddClusterAsync(Cluster cluster);
        Task<Cluster?> GetClusterByIdAsync(Guid id);
        Task<List<Cluster>> GetAllClustersAsync();
        void UpdateCluster(Cluster cluster);
        void DeleteCluster(Cluster cluster);
        void RestoreCluster(Cluster cluster);
        Task SaveChangesAsync();

    }
}
