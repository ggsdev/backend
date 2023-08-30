using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.Interfaces
{
    public interface IWellEventRepository
    {
        Task Add(WellEvent wellEvent);
        Task<WellEvent?> GetById(Guid id);
        void Update(WellEvent wellEvent);
        Task Save();
    }
}
