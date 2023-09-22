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
                .Include(x => x.WellLosses)
                .Include(x => x.EventRelated)
                .Include(x => x.EventReasons)
                .Include(x => x.Well)
                    .ThenInclude(x => x.Field)
                        .ThenInclude(x => x.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WellEvent?> GetNextEvent(DateTime startDate, DateTime endDate)
        {
            return await _context.WellEvents
                .OrderBy(x => x.StartDate)
                .Where(x => x.StartDate >= startDate)
                .Where(x => x.EndDate >= endDate || x.EndDate == null)
                    .Where(x => x.EventStatus == "A")
                .FirstOrDefaultAsync();
        }

        public async Task<EventReason?> GetNextReason(DateTime startDate, Guid wellEventId, Guid eventReasonId)
        {
            return await _context.EventReasons
                .Include(x => x.WellEvent)
                .Where(x => x.StartDate > startDate && x.WellEvent.Id == wellEventId && x.Id != eventReasonId)
                .OrderBy(x => x.StartDate)
                    .FirstOrDefaultAsync();
        }

        public async Task<EventReason?> GetBeforeReason(DateTime startDate, Guid wellEventId, Guid eventReasonId)
        {
            return await _context.EventReasons
                .Include(x => x.WellEvent)
                .Include(x => x.CreatedBy)
                .Where(x => x.StartDate < startDate && x.WellEvent.Id == wellEventId && x.Id != eventReasonId)
                .OrderByDescending(x => x.StartDate)
                    .FirstOrDefaultAsync();
        }

        public async Task<EventReason?> GetEventReasonById(Guid id)
        {

            return await _context.EventReasons
                .Include(x => x.WellEvent)
                    .ThenInclude(x => x.EventReasons)
                .Include(er => er.WellEvent)
                    .ThenInclude(we => we.Well)
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
        public async Task<WellEvent?> GetLastWellEvent(string typeEvent)
        {
            return await _context.WellEvents
                .Where(x => x.EndDate == null && x.EventStatus == typeEvent)
                .FirstOrDefaultAsync();
        }

        public async Task Add(WellEvent wellEvent)
        {
            await _context.WellEvents.AddAsync(wellEvent);
        }
        public async Task AddRangeReasons(List<EventReason> eventReasons)
        {
            await _context.EventReasons.AddRangeAsync(eventReasons);
        }

        public void Update(WellEvent wellEvent)
        {
            _context.WellEvents.Update(wellEvent);
        }

        public void DeleteReason(EventReason reason)
        {
            _context.EventReasons.Remove(reason);
        }
        public void DeleteRangeReason(List<EventReason> reasons)
        {
            _context.EventReasons.RemoveRange(reasons);
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
