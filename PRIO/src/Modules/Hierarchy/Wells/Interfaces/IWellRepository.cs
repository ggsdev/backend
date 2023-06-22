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
        Task<List<Well>> GetAsync();
        Task<Well?> GetWithFieldAsync(Guid? id);
        Task<Well?> GetOnlyWellAsync(Guid? id);
        Task<Well?> GetWithUserAsync(Guid? id);
        Task SaveChangesAsync();
    }
}
