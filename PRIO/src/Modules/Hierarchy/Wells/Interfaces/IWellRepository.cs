using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
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
        Task<Well?> GetByNameOrOperatorName(string name);
        Task<Well?> GetByIdWithEventsAsync(Guid? id);
        Task<Well?> GetByNameAsync(string? name);
        Task<Well?> GetByNameOrOperator(string name, string operatorName);
        Task<List<Well>> GetAsync(User user);
        Task<Well?> GetCleanById(Guid? id);
        Task<Well?> GetByCode(string? cod);
        Task<Well?> GetWithFieldAsync(Guid? id);
        Task<Well?> GetOnlyWellAsync(Guid? id);
        Task<Well?> GetByIdWithFieldAndCompletions(Guid? id);
        Task<Well?> GetWithUserAsync(Guid? id);
        Task<Well?> GetWellAndChildren(Guid? id);
        Task SaveChangesAsync();
        Task<List<Well>> GetWellsWithEvents(Guid fieldId, string eventType);
        Task<List<PI.Infra.EF.Models.Attribute>> GetTagsFromWell(string name, string operatorName);
    }
}
