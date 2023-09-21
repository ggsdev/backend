using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.Interfaces
{
    public interface IWellEventRepository
    {
        Task Add(WellEvent wellEvent);
        Task AddRangeReasons(List<EventReason> eventReasons);
        Task<WellEvent?> GetEventById(Guid id);
        Task<WellEvent?> GetNextEvent(DateTime startDate, DateTime endDate);
        Task<EventReason?> GetNextReason(DateTime startDate, Guid wellEventId, Guid eventReasonId);
        Task<EventReason?> GetBeforeReason(DateTime startDate, Guid wellEventId, Guid eventReasonId);
        Task<EventReason?> GetEventReasonById(Guid id);
        void UpdateEventReasonById(EventReason eventReason);
        Task<List<WellEvent>> GetAllWellEvent(Guid wellId);
        void Update(WellEvent wellEvent);
        Task Save();



        #region Reason
        void UpdateReason(EventReason reason);
        Task AddReasonClosedEvent(EventReason data);

        void DeleteReason(EventReason reason);
        #endregion

    }
}
