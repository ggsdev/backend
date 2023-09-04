using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.Interfaces
{
    public interface IWellEventRepository
    {
        Task Add(WellEvent wellEvent);
        Task<WellEvent?> GetClosedEventById(Guid id);
        void Update(WellEvent wellEvent);
        Task Save();



        #region Reason
        void UpdateReason(EventReason reason);
        Task AddReasonClosedEvent(EventReason data);
        #endregion

    }
}
