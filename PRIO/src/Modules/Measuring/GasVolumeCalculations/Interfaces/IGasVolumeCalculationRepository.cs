using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces
{
    public interface IGasVolumeCalculationRepository
    {
        Task AddGasVolumeCalculationAsync(GasVolumeCalculation gasVolume);
        Task<GasVolumeCalculation?> GetGasVolumeCalculationByIdAsync(Guid? id);
        Task<List<GasVolumeCalculation>> GetAllGasVolumeCalculationsAsync();
        void UpdateGasVolumeCalculation(GasVolumeCalculation gasVolume);
        void DeleteGasVolumeCalculation(GasVolumeCalculation gasVolume);
        void RestoreGasVolumeCalculation(GasVolumeCalculation gasVolume);
        Task SaveChangesAsync();
    }
}
