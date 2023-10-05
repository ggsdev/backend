using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly DataContext _context;
        public BalanceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddUEPBalance(UEPsBalance UEPBalance)
        {
            await _context.UEPsBalance.AddAsync(UEPBalance);
        }
        public async Task AddInstallationBalance(InstallationsBalance installationBalance)
        {
            await _context.InstallationsBalance.AddAsync(installationBalance);
        }
        public async Task AddFieldBalance(FieldsBalance fieldBalance)
        {
            await _context.FieldsBalance.AddAsync(fieldBalance);
        }
    }
}
