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

    public async Task<Cluster?> GetClusterByIdAsync(Guid? id)
    {
        return await _context.Clusters.FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<Cluster?> GetClusterByNameAsync(string? name)
    {
        return await _context.Clusters.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<Cluster?> GetClusterAndChildren(Guid? id)
    {
        return await _context.Clusters
            .Include(x => x.Installations)
                .ThenInclude(i => i.Fields)
                    .ThenInclude(f => f.Zones)
                        .ThenInclude(z => z.Reservoirs)
                            .ThenInclude(r => r.Completions)

            .Include(x => x.Installations)
                .ThenInclude(i => i.Fields)
                    .ThenInclude(f => f.Wells)
                            .ThenInclude(r => r.Completions)

            .Include(x => x.Installations)
                .ThenInclude(i => i.Fields)
                    .ThenInclude(f => f.Wells)
                        .ThenInclude(w => w.WellEvents)
                            .ThenInclude(we => we.EventReasons)

            .Include(x => x.Installations)
                .ThenInclude(i => i.MeasuringPoints)
                    .ThenInclude(m => m.MeasuringEquipments)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cluster?> GetClusterWithInstallationsAsync(Guid? id)
    {
        return await _context.Clusters
            .Include(x => x.Installations)
            .FirstOrDefaultAsync(c => c.Id == id);
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