using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Repositories
{
    public class WellRepository : IWellRepository
    {
        private readonly DataContext _context;
        public WellRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Well?> GetByCode(string? cod)
        {
            return await _context.Wells
                    .FirstOrDefaultAsync(x => x.CodWellAnp == cod);
        }

        public async Task<List<Well>> GetWellsWithEvents(Guid fieldId, string eventType)
        {
            return await _context.Wells
                .Include(x => x.WellEvents)
                .ThenInclude(d => d.EventReasons)
                .Include(x => x.Field)
                .Where(x => x.Field.Id == fieldId)
                .ToListAsync();
        }

        public async Task<Well?> GetByIdAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.User)
                .Include(x => x.Completions)
                .ThenInclude(x => x.Reservoir)
                .ThenInclude(x => x.Zone)
                .Include(x => x.Field)
                .ThenInclude(f => f.Installation)
                .ThenInclude(i => i.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Well?> GetByIdWithEventsAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.WellEvents)
                .ThenInclude(x => x.EventReasons)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Well?> GetByNameAsync(string? name)
        {
            return await _context.Wells
                .Include(x => x.User)
                .Include(x => x.Completions)
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Name.Trim() == name.Trim());
        }

        public async Task<Well?> GetWellAndChildren(Guid? id)
        {
            return await _context.Wells
                                .Include(r => r.Completions)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Well?> GetByIdWithFieldAndCompletions(Guid? id)
        {
            return await _context.Wells
               .Include(x => x.User)
                .Include(x => x.Field)
                .Include(x => x.WellEvents)
                    .ThenInclude(x => x.EventReasons)
                .Include(x => x.Completions)
                .ThenInclude(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Well?> GetWithFieldAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.WellEvents)
                .ThenInclude(x => x.EventReasons)
                .Include(x => x.Field)
                .Include(x => x.Completions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Well?> GetWithUserAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Well?> GetOnlyWellAsync(Guid? id)
        {
            return await _context.Wells
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Well well)
        {
            await _context.Wells.AddAsync(well);
        }

        public async Task<List<Well>> GetAsync()
        {
            return await _context.Wells
                    .Include(x => x.User)
                    .Include(x => x.Completions)
                    .ThenInclude(x => x.Reservoir)
                    .ThenInclude(x => x.Zone)
                    .Include(x => x.Field)
                    .ThenInclude(f => f.Installation)
                    .ThenInclude(i => i.Cluster)
                    .ToListAsync();
        }

        public void Update(Well well)
        {
            _context.Wells.Update(well);
        }

        public void Delete(Well well)
        {
            _context.Wells.Update(well);
        }

        public void Restore(Well well)
        {
            _context.Wells.Update(well);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
