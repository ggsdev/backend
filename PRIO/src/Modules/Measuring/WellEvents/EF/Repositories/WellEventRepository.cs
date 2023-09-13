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

        public async Task<WellEvent?> GetEventById(Guid id)
        {
            return await _context.WellEvents
                .Include(x => x.EventReasons)
                .Include(x => x.Well)
                    .ThenInclude(x => x.Field)
                        .ThenInclude(x => x.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EventReason?> GetEventReasonById(Guid id)
        {
            return await _context.EventReasons
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateEventReasonById(EventReason eventReason)
        {
            _context.EventReasons
                .Update(eventReason);
        }
        public async Task<List<WellEvent>> GetAllWellEvent(Guid wellId)
        {
            return await _context.WellEvents
                .Include(x => x.EventReasons)
                .Include(x => x.Well)
                .Where(x => x.Well.Id == wellId)
                .ToListAsync();
        }

        public async Task Add(WellEvent wellEvent)
        {
            await _context.WellEvents.AddAsync(wellEvent);
        }

        public void Update(WellEvent wellEvent)
        {
            _context.WellEvents.Update(wellEvent);
        }

        public void DeleteReason(EventReason reason)
        {
            _context.EventReasons.Remove(reason);
        }

        public void UpdateReason(EventReason reason)
        {
            _context.EventReasons.Update(reason);
        }

        public async Task AddReasonClosedEvent(EventReason data)
        {
            await _context.EventReasons.AddAsync(data);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
