using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Interfaces
{
    public interface IInstallationRepository
    {
        Task AddInstallationAsync(Installation installation);
        Task<Installation?> GetInstallationByIdAsync(Guid id);
        Task AddSystemHistoryAsync(SystemHistory history);
        Task SaveChangesAsync();
    }
}
