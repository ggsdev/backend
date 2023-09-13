using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.Interfaces
{
    public interface IWellEventRepository
    {
        Task Add(WellEvent wellEvent);
        Task<WellEvent?> GetEventById(Guid id);
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
