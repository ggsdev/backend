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


        public async Task<List<InjectionWaterGasField>> GetInjectionsByInstallationId(Guid installationId)
        {
            return await _context.InjectionWaterGasField
                    .Include(x => x.Field)
                        .ThenInclude(x => x.Installation)
                    .Include(x => x.BalanceField)
                        .ThenInclude(x => x.InstallationBalance)
                            .ThenInclude(x => x.Installation)
                                .Where(x => x.BalanceField.InstallationBalance.Installation.Id == installationId)
                .ToListAsync();
        }
        public async Task AddWellInjectionAsync(InjectionWaterWell injection)
        {
            await _context.InjectionWaterWell.AddAsync(injection);
        }
        public async Task AddGasWellInjectionAsync(InjectionGasWell injection)
        {
            await _context.InjectionGasWell.AddAsync(injection);
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

        public Task<InjectionGasWell?> GetGasInjectionById(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}
