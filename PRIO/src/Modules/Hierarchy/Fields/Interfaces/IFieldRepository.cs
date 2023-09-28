using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Fields.Interfaces
{
    public interface IFieldRepository
    {
        Task AddAsync(Field field);
        void Update(Field field);
        void Delete(Field field);
        void Restore(Field field);
        Task<Field?> GetByIdAsync(Guid? id);
        Task<bool> Any(Guid id);
        Task<Field?> GetByNameAsync(string? name);
        Task<Field?> GetFieldAndChildren(Guid? id);
        Task<Field?> GetByCod(string? cod);
        Task<Field?> GetByIdWithWellsAndZonesAsync(Guid? id);
        Task<List<Field>> GetAsync(User user);
        Task<Field?> GetOnlyField(Guid? id);
        Task SaveChangesAsync();
        Task<List<Field>> GetFieldsByInstallationId(Guid id);
        Task<List<Field>> GetFieldsByUepCode(string code);
    }
}
