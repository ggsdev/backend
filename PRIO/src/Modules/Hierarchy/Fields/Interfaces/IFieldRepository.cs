using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Interfaces
{
    public interface IFieldRepository
    {
        Task AddAsync(Field field);
        void Update(Field field);
        void Delete(Field field);
        void Restore(Field field);
        Task<Field?> GetByIdAsync(Guid? id);
        Task<Field?> GetByIdWithWellsAndZonesAsync(Guid? id);
        Task<List<Field>> GetAsync();
        Task<Field?> GetOnlyField(Guid? id);
        Task SaveChangesAsync();
    }
}
