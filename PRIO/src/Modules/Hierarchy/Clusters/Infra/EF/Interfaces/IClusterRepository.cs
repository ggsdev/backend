using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces
{
    public interface IClusterRepository
    {
        Task AddClusterAsync(Cluster cluster);
        Task<Cluster?> GetClusterByIdAsync(Guid id);
        Task<List<Cluster>> GetAllClustersAsync();
        Task UpdateClusterAsync(Cluster cluster);
        Task DeleteClusterAsync(Cluster cluster);
        Task RestoreClusterAsync(Cluster cluster);
        Task SaveChangesAsync();

    }
}
