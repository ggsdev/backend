using PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellAppropriations.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Repositories
{
    public class WellAppropriationRepository : IWellAppropriationRepository
    {
        private readonly DataContext _context;
        public WellAppropriationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(WellAppropriation wellAppropriation)
        {
            await _context.WellAppropriations.AddAsync(wellAppropriation);
        }
    }
}
