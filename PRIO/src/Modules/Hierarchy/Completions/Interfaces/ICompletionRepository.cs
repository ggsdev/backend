using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Completions.Interfaces
{
    public interface ICompletionRepository
    {
        Task AddAsync(Completion completion);
        void Update(Completion completion);
        void Delete(Completion completion);
        void Restore(Completion completion);
        Task<Completion?> GetByIdAsync(Guid? id);
        Task<List<Completion>> GetAsync(User user);
        Task<Completion?> GetOnlyCompletion(Guid? id);
        Task<Completion?> GetWithWellReservoirZoneAsync(Guid? id);
        Task<Completion?> GetWithUser(Guid? id);
        Task<Completion?> GetExistingCompletionAsync(Guid? wellId, Guid? reservoirId);
        Task SaveChangesAsync();
    }
}
