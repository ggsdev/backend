using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories
{
    public class InstallationRepository : IInstallationRepository
    {
        private readonly DataContext _context;
        public InstallationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Installation?> GetInstallationByIdAsync(Guid id)
        {
            return await _context.Installations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddInstallationAsync(Installation installation)
        {
            await _context.Installations.AddAsync(installation);
        }

        public async Task AddSystemHistoryAsync(SystemHistory history)
        {
            await _context.SystemHistories.AddAsync(history);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
