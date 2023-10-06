using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Balance.Injection.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Repositories
{
    public class InjectionRepository : IInjectionRepository
    {
        private readonly DataContext _context;
        public InjectionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<InjectionWaterWell?> GetWaterInjectionById(Guid? id)
        {
            return await _context.InjectionWaterWell
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<InjectionWaterGasField>> GetWaterInjectionsByInstallationId(Guid installationId)
        {
            return await _context.InjectionWaterField
                    .Include(x => x.BalanceField)
                        .ThenInclude(x => x.installationBalance)
                            .ThenInclude(x => x.Installation)
                                .Where(x => x.BalanceField.installationBalance.Installation.Id == installationId)
                .ToListAsync();
        }
        public void UpdateWaterInjection(InjectionWaterWell injection)
        {
            _context.InjectionWaterWell
                .Update(injection);
        }

        public async Task Save()
        {
            await _context
                .SaveChangesAsync();
        }

    }
}
