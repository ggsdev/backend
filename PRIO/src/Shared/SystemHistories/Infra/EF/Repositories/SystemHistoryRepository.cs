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

        public async Task AddSystemHistory(SystemHistory history)
        {
            await _context.SystemHistories.AddAsync(history);
        }
    }
}
