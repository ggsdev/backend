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

        public async Task<Well?> GetByIdAsync(Guid? id)
        {
            return await _context.Wells
               .Include(x => x.User)
                .Include(x => x.Completions)
                .Include(x => x.Field)
                .ThenInclude(f => f.Installation)
                .ThenInclude(i => i.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Well?> GetWithFieldAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.Field)
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
