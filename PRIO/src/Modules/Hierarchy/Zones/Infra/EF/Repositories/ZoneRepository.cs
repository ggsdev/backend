using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly DataContext _context;
        public ZoneRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Zone?> GetByCode(string? code)
        {
            return await _context.Zones
                .FirstOrDefaultAsync(x => x.CodZone == code);
        }

        public async Task<Zone?> GetByIdAsync(Guid? id)
        {
            return await _context.Zones
                .Include(x => x.User)
                .Include(x => x.Field)
                .ThenInclude(f => f!.Installation)
                .ThenInclude(i => i!.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Zone?> GetZoneAndChildren(Guid? id)
        {
            return await _context.Zones
                            .Include(z => z.Reservoirs)
                                .ThenInclude(r => r.Completions)
                                .ThenInclude(c => c.Well)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Zone?> GetWithField(Guid? id)
        {
            return await _context.Zones
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Zone?> GetByIdWithReservoirsAsync(Guid? id)
        {
            return await _context.Zones
                .Include(x => x.User)
                .Include(x => x.Reservoirs)
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Zone?> GetOnlyZone(Guid? id)
        {
            return await _context.Zones
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Zone?> GetWithUser(Guid? id)
        {
            return await _context.Zones
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Zone?> GetZoneAsync(Guid? id)
        {
            return await _context.Zones
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Zone zone)
        {
            await _context.Zones.AddAsync(zone);
        }

        public async Task<List<Zone>> GetAsync()
        {
            return await _context.Zones
               .Include(x => x.User)
               .Include(x => x.Field)
               .ThenInclude(f => f.Installation)
               .ThenInclude(i => i.Cluster)
               .ToListAsync();
        }

        public void Update(Zone zone)
        {
            _context.Zones.Update(zone);
        }

        public void Delete(Zone zone)
        {
            _context.Zones.Update(zone);
        }

        public void Restore(Zone zone)
        {
            _context.Zones.Update(zone);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
