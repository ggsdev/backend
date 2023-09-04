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
        Task<GasVolumeCalculation?> GetGasVolumeCalculationByInstallationId(Guid id);
        Task<GasVolumeCalculation?> GetGasVolumeCalculationByInstallationUEP(string uepCode);
        Task<List<GasVolumeCalculation>> GetAll();
        Task<ExportGas?> GetExportGas(Guid id);
        Task AddExportGas(ExportGas gas);
        Task<ExportGas?> GetOtherExportGas(Guid mpointId, Guid exportGas);

        Task<AssistanceGas?> GetAssistanceGas(Guid id);
        Task AddAssistanceGas(AssistanceGas gas);
        Task<AssistanceGas?> GetOtherAssistanceGas(Guid mpointId, Guid assistanceGasId);
        Task<GasVolumeCalculation?> GetGasVolumeCalculationByInstallationIdXML(Guid id);

        void RemoveRange<T>(IEnumerable<T> entities) where T : class;

        Task<HighPressureGas?> GetHighPressureGas(Guid id);
        Task AddHighPressureGas(HighPressureGas gas);
        Task<HighPressureGas?> GetOtherHighPressureGas(Guid mpointId, Guid highGas);

        Task<HPFlare?> GetHPFlare(Guid id);
        Task AddHPFlare(HPFlare gas);
        Task<HPFlare?> GetOtherHPFlare(Guid mpointId, Guid hpFlare);

        Task<ImportGas?> GetImportGas(Guid id);
        Task AddImportGas(ImportGas gas);
        Task<ImportGas?> GetOtherImportGas(Guid mpointId, Guid importGas);

        Task<LowPressureGas?> GetLowPressureGas(Guid id);
        Task AddLowPressureGas(LowPressureGas gas);
        Task<LowPressureGas?> GetOtherLowPressureGas(Guid mpointId, Guid low);

        Task<LPFlare?> GetLPFlare(Guid id);
        Task AddLPFlare(LPFlare gas);
        Task<LPFlare?> GetOtherLPFlare(Guid mpointId, Guid lp);

        Task<PilotGas?> GetPilotGas(Guid id);
        Task AddPilotGas(PilotGas gas);
        Task<PilotGas?> GetOtherPilotGas(Guid mpointId, Guid pilot);

        Task<PurgeGas?> GetPurgeGas(Guid id);
        Task AddPurgeGas(PurgeGas gas);
        Task<PurgeGas?> GetOtherPurgeGas(Guid mpointId, Guid purge);

        Task SaveChangesAsync();

    }
}
