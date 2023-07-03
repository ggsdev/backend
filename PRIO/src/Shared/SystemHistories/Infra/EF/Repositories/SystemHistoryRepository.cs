using Microsoft.EntityFrameworkCore;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Interfaces;

namespace PRIO.src.Shared.SystemHistories.Infra.EF.Repositories
{
    public class SystemHistoryRepository : ISystemHistoryRepository
    {
        private readonly DataContext _context;

        public SystemHistoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SystemHistory history)
        {
            await _context.SystemHistories.AddAsync(history);
        }

        public async Task<SystemHistory?> GetFirst(Guid id)
        {
            return await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == id)
               .FirstOrDefaultAsync();
        }

        public async Task<SystemHistory?> GetLast(Guid id)
        {
            return await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == id)
               .LastOrDefaultAsync();
        }

        public async Task<List<SystemHistory>> GetAll(Guid id)
        {
            return await _context.SystemHistories
                     .Where(x => x.TableItemId == id)
                     .OrderByDescending(x => x.CreatedAt)
                     .ToListAsync();
        }

        public async Task<List<SystemHistory>> GetImports()
        {
            return await _context.SystemHistories
                .Where(x => x.TypeOperation == "IMPORT" || x.TypeOperation == "IMPORTUPDATE")
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
