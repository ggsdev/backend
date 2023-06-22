using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

public class ClusterRepository : IClusterRepository
{
    private readonly DataContext _context;

    public ClusterRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddClusterAsync(Cluster cluster)
    {
        await _context.Clusters.AddAsync(cluster);
    }

    public async Task<Cluster?> GetClusterByIdAsync(Guid id)
    {
        return await _context.Clusters.FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<Cluster>> GetAllClustersAsync()
    {
        return await _context.Clusters.Include(x => x.User).ToListAsync();
    }
    public void UpdateCluster(Cluster cluster)
    {
        _context.Clusters.Update(cluster);
    }
    public void DeleteCluster(Cluster cluster)
    {
        _context.Clusters.Update(cluster);
    }
    public void RestoreCluster(Cluster cluster)
    {
        _context.Clusters.Update(cluster);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}