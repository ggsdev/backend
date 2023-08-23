using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Repositories
{
    public class WellProductionRepository : IWellProductionRepository
    {
        private readonly DataContext _context;
        public WellProductionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(WellProduction wellAppropriation)
        {
            await _context.WellProductions.AddAsync(wellAppropriation);
        }
    }
}
