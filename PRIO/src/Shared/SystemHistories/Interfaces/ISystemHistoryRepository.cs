using PRIO.src.Shared.SystemHistories.Infra.EF.Models;

namespace PRIO.src.Shared.SystemHistories.Interfaces
{
    public interface ISystemHistoryRepository
    {
        Task AddAsync(SystemHistory history);
        Task<SystemHistory?> GetFirst(Guid id);
        Task<SystemHistory?> GetLast(Guid id);
        Task<List<SystemHistory>> GetAll(Guid id);
    }
}
