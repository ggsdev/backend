using PRIO.src.Shared.SystemHistories.Infra.EF.Models;

namespace PRIO.src.Shared.SystemHistories.Interfaces
{
    public interface ISystemHistoryRepository
    {
        Task AddSystemHistory(SystemHistory history);
    }
}
