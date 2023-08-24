using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
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
        public void Update(WellProduction wellApp)
        {
            _context.WellProductions.Update(wellApp);
        }

        public async Task<List<WellProduction>> GetByProductionId(Guid productionId)
        {
            return await _context.WellProductions
                .Include(x => x.Production)
                .Include(x => x.BtpData)
                .Where(x => x.Production.Id == productionId)
                .ToListAsync();
        }
        public async Task AddCompletionProductionAsync(CompletionProduction completionApp)
        {
            await _context.CompletionProductions.AddAsync(completionApp);
        }
        public void UpdateCompletionProduction(CompletionProduction completionApp)
        {
            _context.CompletionProductions.Update(completionApp);
        }
        public async Task AddReservoirProductionAsync(ReservoirProduction reservoirApp)
        {
            await _context.ReservoirProductions.AddAsync(reservoirApp);
        }
        public void UpdateReservoirProduction(ReservoirProduction reservoirApp)
        {
            _context.ReservoirProductions.Update(reservoirApp);
        }
        public async Task AddZoneProductionAsync(ZoneProduction zoneApp)
        {
            await _context.ZoneProductions.AddAsync(zoneApp);
        }
        public void UpdateZoneProduction(ZoneProduction zoneApp)
        {
            _context.ZoneProductions.Update(zoneApp);
        }
        public async Task<ReservoirProduction?> GetReservoirProductionForWellAndReservoir(Guid productionId, Guid ReservoirId)
        {
            return await _context.ReservoirProductions.Where(x => x.ProductionId == productionId && x.ReservoirId == ReservoirId).FirstOrDefaultAsync();
        }
        public async Task<ZoneProduction?> GetZoneProductionForWellAndReservoir(Guid productionId, Guid ReservoirId)
        {
            return await _context.ZoneProductions.Where(x => x.ProductionId == productionId && x.ZoneId == ReservoirId).FirstOrDefaultAsync();
        }
    }
}
