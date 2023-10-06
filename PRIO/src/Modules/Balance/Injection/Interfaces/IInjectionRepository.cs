using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Interfaces
{
    public interface IInjectionRepository
    {
        Task<InjectionWaterWell?> GetWaterInjectionById(Guid id);
        void UpdateWaterInjection(InjectionWaterWell injection);
        Task AddWellInjectionAsync(InjectionWaterWell injection);
        Task AddGasWellInjectionAsync(InjectionGasWell injection);
        Task Save();
    }
}
