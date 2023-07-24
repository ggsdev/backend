using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Interfaces
{
    public interface IInstallationRepository
    {
        Task AddAsync(Installation installation);
        void Update(Installation installation);
        void Delete(Installation installation);
        void Restore(Installation installation);
        Task<Installation?> GetByIdAsync(Guid? id);
        Task<Installation?> GetByIdWithCalculationsAsync(Guid? id);
        Task<Installation?> GetByCod(string? cod);
        Task<Installation?> GetByUEPCod(string? cod);
        Task<Installation?> GetInstallationMeasurementByUepAndAnpCodAsync(string codInstallation, string acronym);
        Task<Installation?> GetByIdWithFieldsMeasuringPointsAsync(Guid? id);
        Task<List<Installation>> GetAsync();
        Task<List<Installation>> GetUEPsAsync();
        Task<Installation?> GetInstallationAndChildren(Guid? id);
        Task SaveChangesAsync();
    }
}
