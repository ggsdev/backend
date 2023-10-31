using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Interfaces
{
    public interface IInstallationRepository
    {
        Task AddAsync(Installation installation);
        Task AddFRAsync(FieldFR fr);
        Task<FieldFR?> GetFrByDateMeasuredAndFieldId(DateTime date, Guid id);
        void UpdateFr(FieldFR fieldFr);
        void Update(Installation installation);
        void Delete(Installation installation);
        void Restore(Installation installation);
        Task<Installation?> GetByIdAsync(Guid? id);
        Task<Installation?> GetInstallationByIdWithField(Guid installationId);
        Task<List<FieldFR?>> GetFRsByUEPAsync(string? uep);
        Task<List<FieldFR?>> GetFRsByIdAsync(Guid? id);
        Task<List<Installation>> GetInstallationsByUepWithTagsPi(string uepCode);
        Task<List<FieldFR>> GetFRsByIdAsync(Guid id);
        Task<Installation?> GetByNameAsync(string? name);
        Task<Installation?> GetByIdWithCalculationsAsync(Guid? id);
        Task<Installation?> GetByCod(string? cod);
        Task<Installation?> GetByUEPCod(string? cod);
        Task<List<Installation?>> GetByUEPWithFieldsCod(string? cod);
        Task<Installation?> GetInstallationMeasurementByUepAndAnpCodAsync(string codInstallation, string acronym);
        Task<Installation?> GetByIdWithFieldsMeasuringPointsAsync(Guid? id);
        Task<List<Installation>> GetAsync(User user);
        Task<List<Installation>> GetUEPsAsync();
        Task<List<Installation>> GetUEPsCreateAsync(string table);
        Task<List<Installation>> GetByIdWithFieldsCod(Guid id, User user);
        Task<Installation?> GetInstallationAndChildren(Guid? id);
        Task<Installation?> GetUepById(Guid? id);
        Task<List<Installation>> GetInstallationChildrenOfUEP(string uepCode);
        Task SaveChangesAsync();
        Task<Installation?> GetUep(string? cod);
    }
}
