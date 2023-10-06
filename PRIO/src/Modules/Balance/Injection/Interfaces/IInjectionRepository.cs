using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Interfaces
{
    public interface IInjectionRepository
    {
        Task<InjectionWaterWell?> GetWaterInjectionById(Guid? id);
        Task<List<InjectionWaterGasField>> GetWaterInjectionsByInstallationId(Guid installationId);
        //Task<InjectionWaterWell?> GetGasLiftByInstallationId(Guid installationId);
        void UpdateWaterInjection(InjectionWaterWell injection);
        Task Save();
    }
}
