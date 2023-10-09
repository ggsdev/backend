﻿using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Interfaces
{
    public interface IInjectionRepository
    {
        Task<InjectionWaterWell?> GetWaterInjectionById(Guid? id);
        Task<InjectionGasWell?> GetGasInjectionById(Guid? id);
        Task<List<InjectionWaterGasField>> GetInjectionsByInstallationId(Guid installationId);
        //Task<InjectionWaterWell?> GetGasLiftByInstallationId(Guid installationId);
        void UpdateWaterInjection(InjectionWaterWell injection);
        Task AddWellInjectionAsync(InjectionWaterWell injection);
        Task AddGasWellInjectionAsync(InjectionGasWell injection);
        Task Save();
    }
}
