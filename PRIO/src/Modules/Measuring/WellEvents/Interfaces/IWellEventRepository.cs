using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.Interfaces
{
    public interface IWellEventRepository
    {
        Task Add(WellEvent wellEvent);
        void Update(WellEvent wellEvent);
        Task<WellEvent?> GetRelatedEvent(Guid eventRelatedId);
        Task Save();
        Task<List<WellEvent>> GetWellsWithEvents(Guid fieldId, string eventType);
    }
}
