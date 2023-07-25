using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces
{
    public interface IGasVolumeCalculationRepository
    {
        Task<GasVolumeCalculation> AddGasVolumeCalculationAsync(Installation installation);
        Task<GasVolumeCalculation?> GetGasVolumeCalculationByIdAsync(Guid? id);
        Task<List<GasVolumeCalculation>> GetAllGasVolumeCalculationsAsync();
        void UpdateGasVolumeCalculation(GasVolumeCalculation gasVolume);
        void DeleteGasVolumeCalculation(GasVolumeCalculation gasVolume);
        void RestoreGasVolumeCalculation(GasVolumeCalculation gasVolume);
        Task<AssistanceGas?> GetAssistanceGas(Guid id);
        Task<ExportGas?> GetExportGas(Guid id);
        Task AddExportGas(ExportGas gas);
        Task AddAssistanceGas(AssistanceGas gas);

        Task<HighPressureGas?> GetHighPressureGas(Guid id);
        Task AddHighPressureGas(HighPressureGas gas);

        Task<HPFlare?> GetHPFlare(Guid id);
        Task AddHPFlare(HPFlare gas);

        Task<ImportGas?> GetImportGas(Guid id);
        Task AddImportGas(ImportGas gas);

        Task<LowPressureGas?> GetLowPressureGas(Guid id);
        Task AddLowPressureGas(LowPressureGas gas);

        Task<LPFlare?> GetLPFlare(Guid id);
        Task AddLPFlare(LPFlare gas);
        Task<PilotGas?> GetPilotGas(Guid id);
        Task AddPilotGas(PilotGas gas);
        Task<PurgeGas?> GetPurgeGas(Guid id);
        Task AddPurgeGas(PurgeGas gas);
        Task SaveChangesAsync();

        Task<GasVolumeCalculation?> GetGasVolumeCalculationByInstallationId(Guid id);
    }
}
