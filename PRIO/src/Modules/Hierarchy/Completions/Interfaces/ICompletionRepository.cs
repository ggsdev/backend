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
        Task<Completion?> GetByCode(string? cod);
        Task<List<Completion>> GetAsync();
        Task<Completion?> GetOnlyCompletion(Guid? id);
        Task<Completion?> GetWithWellReservoirZoneAsync(Guid? id);
        Task<Completion?> GetWithUser(Guid? id);
        Task<Completion?> GetExistingCompletionAsync(Guid? wellId, Guid? reservoirId);
        Task SaveChangesAsync();
    }
}
