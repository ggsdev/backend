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
        Task<ExportGas?> GetExportGas(string? staticName);
        Task AddExportGas(ExportGas gas);
        Task AddAssistanceGas(AssistanceGas gas);
        Task SaveChangesAsync();
    }
}
