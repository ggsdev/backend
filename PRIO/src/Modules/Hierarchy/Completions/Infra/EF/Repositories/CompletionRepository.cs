using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Repositories
{
    public class CompletionRepository : ICompletionRepository
    {
        private readonly DataContext _context;
        public CompletionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Completion?> GetByCode(string? code)
        {
            return await _context.Completions
                .FirstOrDefaultAsync(x => x.CodCompletion == code);
        }
        public async Task<Completion?> GetExistingCompletionAsync(Guid? wellId, Guid? reservoirId)
        {
            return await _context.Completions
                .FirstOrDefaultAsync(x => x.Well.Id == wellId && x.Reservoir.Id == reservoirId);
        }
        public async Task<Completion?> GetByIdAsync(Guid? id)
        {
            return await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Completion?> GetWithWellReservoirZoneAsync(Guid? id)
        {
            return await _context.Completions
               .Include(x => x.Well)
               .Include(x => x.Reservoir)
               .ThenInclude(x => x.Zone)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Completion?> GetOnlyCompletion(Guid? id)
        {
            return await _context.Completions
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Completion?> GetWithUser(Guid? id)
        {
            return await _context.Completions
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Completion completion)
        {
            await _context.Completions.AddAsync(completion);
        }

        public async Task<List<Completion>> GetAsync()
        {
            return await _context.Completions
               .Include(x => x.Well)
               .Include(x => x.Reservoir)
               .Include(x => x.User)
               .ToListAsync();
        }

        public void Update(Completion completion)
        {
            _context.Completions.Update(completion);
        }

        public void Delete(Completion completion)
        {
            _context.Completions.Update(completion);
        }

        public void Restore(Completion completion)
        {
            _context.Completions.Update(completion);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
