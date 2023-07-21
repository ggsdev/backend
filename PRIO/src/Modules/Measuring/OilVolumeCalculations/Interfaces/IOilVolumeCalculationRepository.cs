
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces
{
    public interface IOilVolumeCalculationRepository
    {
        #region OilVolumeCalculation
        Task<OilVolumeCalculation?> AddOilVolumeCalculationAsync(Installation installation);

        Task<OilVolumeCalculation?> GetOilVolumeCalculationByInstallationId(Guid id);
        Task<OilVolumeCalculation?> GetOilVolumeCalculationById(Guid id);
        Task SaveChangesAsync();
        #endregion

        #region Section
        Task<Section?> GetSectionByMeasuringPointIdAsync(Guid? id);
        Task<Section?> GetSectionByIdAsync(Guid? id);
        Task UpdateSection(Section section);
        Task RemoveSectionRange(List<Section> sections);
        Task<Section?> GetSectionOtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);
        Task<Section?> GetSectionInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);

        Task<Section?> AddSection(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, string mpointName);
        #endregion

        #region TOG
        Task<TOGRecoveredOil?> GetTOGByIdAsync(Guid? id);
        Task UpdateTOG(TOGRecoveredOil tog);
        Task RemoveTOGRange(List<TOGRecoveredOil> togs);
        Task<TOGRecoveredOil?> GetTOGByMeasuringPointIdAsync(Guid? id);
        Task<TOGRecoveredOil?> GetTOGOtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);
        Task<TOGRecoveredOil?> GetTOGInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);
        Task<TOGRecoveredOil?> AddTOG(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, string mpointName);
        #endregion

        #region DOR
        Task<DOR?> GetDORByIdAsync(Guid? id);
        Task UpdateDOR(DOR dor);
        Task RemoveDORRange(List<DOR> dors);
        Task<DOR?> GetDORByMeasuringPointIdAsync(Guid? id);
        Task<DOR?> GetDOROtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);
        Task<DOR?> GetDORInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);

        Task<DOR?> AddDOR(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, string mpointName);
        #endregion

        #region Drain
        Task<DrainVolume?> GetDrainByIdAsync(Guid? id);
        Task UpdateDrain(DrainVolume drain);
        Task RemoveDrainRange(List<DrainVolume> drains);
        Task<DrainVolume?> GetDrainByMeasuringPointIdAsync(Guid? id);
        Task<DrainVolume?> GetDrainOtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);
        Task<DrainVolume?> GetDrainInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId);

        Task<DrainVolume?> AddDrain(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, string mpointName);
        #endregion
    }
}
