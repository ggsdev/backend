using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Interfaces
{
    public interface IInstallationRepository
    {
        Task AddAsync(Installation installation);
        Task AddFRAsync(FieldFR fr);
        void Update(Installation installation);
        void Delete(Installation installation);
        void Restore(Installation installation);
        Task<Installation?> GetByIdAsync(Guid? id);
        Task<List<FieldFR?>> GetFRsByUEPAsync(string? uep);
        Task<List<FieldFR?>> GetFRsByIdAsync(Guid? id);
        Task<List<FieldFR>> GetFRsByIdAsync(Guid id);
        Task<Installation?> GetByNameAsync(string? name);
        Task<Installation?> GetByIdWithCalculationsAsync(Guid? id);
        Task<Installation?> GetByCod(string? cod);
        Task<Installation?> GetByUEPCod(string? cod);
        Task<List<Installation?>> GetByUEPWithFieldsCod(string? cod);
        Task<Installation?> GetInstallationMeasurementByUepAndAnpCodAsync(string codInstallation, string acronym);
        Task<Installation?> GetByIdWithFieldsMeasuringPointsAsync(Guid? id);
        Task<List<Installation>> GetAsync();
        Task<List<Installation>> GetUEPsAsync();
        Task<List<Installation>> GetByIdWithFieldsCod(Guid id);
        Task<Installation?> GetInstallationAndChildren(Guid? id);
        Task SaveChangesAsync();
    }
}
