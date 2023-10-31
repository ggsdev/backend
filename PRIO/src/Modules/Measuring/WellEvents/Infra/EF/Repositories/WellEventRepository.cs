using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Repositories
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
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.WellLosses)
                .Include(x => x.EventRelated)
                .Include(x => x.EventReasons)
                    .ThenInclude(x => x.UpdatedBy)

                .Include(x => x.EventReasons)
                    .ThenInclude(x => x.CreatedBy)

                .Include(x => x.Well)
                    .ThenInclude(x => x.Field)
                        .ThenInclude(x => x.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WellEvent?> GetEventWithWellTestById(Guid id)
        {
            return await _context.WellEvents
                   .Include(x => x.Well)
                       .ThenInclude(x => x.WellTests)
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
                .Where(x => x.StartDate >= startDate && x.WellEvent.Id == wellEventId && x.Id != eventReasonId)
                .OrderBy(x => x.StartDate)
                    .FirstOrDefaultAsync();
        }

        public async Task<EventReason?> GetBeforeReason(DateTime startDate, Guid wellEventId, Guid eventReasonId)
        {
            return await _context.EventReasons
                .Include(x => x.WellEvent)
                .Include(x => x.CreatedBy)
                .Where(x => x.StartDate <= startDate && x.WellEvent.Id == wellEventId && x.Id != eventReasonId)
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
                    .ThenInclude(w => w.Completions)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateEventReasonById(EventReason eventReason)
        {
            _context.EventReasons
                .Update(eventReason);
        }
        public async Task<List<WellEvent>> GetAllWellEvent(Guid wellId)
        {
            var list = await _context.WellEvents
             .Include(x => x.EventReasons)
             .Include(x => x.EventRelated)
             .Include(x => x.Well)
             .Where(x => x.Well.Id == wellId)
             .OrderBy(x => x.EventRelated.Id)
             .ToListAsync();

            var newList = new List<WellEvent>();
            for (var i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    newList.Add(list[i]);
                }
                else
                {
                    var previousEventRelatedId = newList[i - 1]?.Id;
                    if (previousEventRelatedId is not null)
                    {
                        var newItem = list.FirstOrDefault(x => x.EventRelated?.Id == previousEventRelatedId);
                        if (newItem is not null)
                            newList.Add(newItem);
                    }
                }
            }
            newList.Reverse();
            return newList;
        }
        public async Task<WellEvent?> GetLastWellEvent()
        {
            return await _context.WellEvents
                .Where(x => x.EndDate == null)
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

        public async Task<List<WellEvent>> GetByRangeDate(DateTime beginning, DateTime end, Guid fieldId)
        {
            return await _context.WellEvents
                    .Include(x => x.Well)
                        .ThenInclude(x => x.Field)
                    .Include(x => x.WellLosses)
                    .Where(x => x.Well.Field!.Id == fieldId)
                    .Where(x => x.StartDate >= beginning)
                    .Where(x => x.EndDate == null || x.EndDate <= end)
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}
