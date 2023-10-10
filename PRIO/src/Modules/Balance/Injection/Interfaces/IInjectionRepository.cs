using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Interfaces
{
    public interface IInjectionRepository
    {
        Task<InjectionWaterWell?> GetWaterInjectionById(Guid? id);
        Task<List<InjectionWaterWell>> GetWaterWellInjectionsByDate(DateTime date, Guid fieldId);
        Task<List<InjectionGasWell>> GetGasWellInjectionsByDate(DateTime date, Guid fieldId);
        Task<InjectionWaterGasField?> GetWaterGasFieldInjectionsById(Guid id);
        Task<InjectionWaterGasField?> GetWaterGasFieldInjectionByDate(DateTime dateInjection);
        Task<InjectionGasWell?> GetGasInjectionById(Guid? id);
        Task<bool> AnyByDate(DateTime date);
        Task<List<InjectionWaterGasField>> GetInjectionsByInstallationId(Guid installationId);
        //Task<InjectionWaterWell?> GetGasLiftByInstallationId(Guid installationId);
        void UpdateWaterInjection(InjectionWaterWell injection);
        Task AddWellInjectionAsync(InjectionWaterWell injection);
        Task AddWellSensorAsync(WellSensor sensor);
        Task AddWaterGasInjection(InjectionWaterGasField injection);
        void UpdateWaterGasInjection(InjectionWaterGasField injection);
        Task AddGasWellInjectionAsync(InjectionGasWell injection);
        Task Save();
    }
}
