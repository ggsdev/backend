using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Repositories
{
    public class WellEventRepository : IWellEventRepository
    {
        private readonly DataContext _context;
        public WellEventRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(WellEvent wellEvent)
        {
            await _context.WellEvents.AddAsync(wellEvent);
        }

        public void Update(WellEvent wellEvent)
        {
            _context.WellEvents.Update(wellEvent);
        }
        public async Task<List<WellEvent>> GetWellsWithEvents(Guid fieldId, string eventType)
        {
            return await _context.WellEvents
                .Include(x => x.Well)
                    .ThenInclude(x => x.Field)
                .Where(x => x.EventStatus == eventType && x.Well.Field.Id == fieldId)
                .ToListAsync();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<WellEvent?> GetRelatedEvent(Guid eventRelatedId)
        {
            return await _context.WellEvents.FirstOrDefaultAsync(x => x.Id == eventRelatedId);
        }
    }
}
