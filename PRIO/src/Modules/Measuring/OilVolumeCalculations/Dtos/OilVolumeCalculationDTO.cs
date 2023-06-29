using PRIO.src.Modules.Hierarchy.Installations.Dtos;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos
{
    public class OilVolumeCalculationDTO
    {
        public List<SectionWithEquipmentDTO>? Sections { get; set; }
        public List<TOGRecoveredOilWithEquipmentDTO>? TOGRecoveredOils { get; set; }
        public List<DrainVolumeWithEquipmentDTO>? DrainVolumes { get; set; }
        public List<DORWithEquipmentDTO>? DORs { get; set; }
        public InstallationWithoutClusterDTO? Installation { get; set; }
    }
}
