using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Interfaces
{
    public interface IWellRepository
    {
        Task AddAsync(Well well);
        void Update(Well well);
        void Delete(Well well);
        void Restore(Well well);
        Task<Well?> GetByIdAsync(Guid? id);
        Task<Well?> GetByNameAsync(string? name);
        Task<List<Well>> GetAsync();
        Task<Well?> GetByCode(string? cod);
        Task<Well?> GetWithFieldAsync(Guid? id);
        Task<Well?> GetOnlyWellAsync(Guid? id);
        Task<Well?> GetByIdWithFieldAndCompletions(Guid? id);
        Task<Well?> GetWithUserAsync(Guid? id);
        Task<Well?> GetWellAndChildren(Guid? id);
        Task SaveChangesAsync();
    }
}
