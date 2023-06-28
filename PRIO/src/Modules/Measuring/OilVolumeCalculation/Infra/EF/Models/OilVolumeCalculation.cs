using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Models
{
    public class OilVolumeCalculation: BaseModel
    {
        public List<Section>? Sections { get; set; }
        public List<TOGRecoveredOil>? TOGRecoveredOils { get; set; }
        public List<DrainVolume>? DrainVolumes { get; set; }
        public List<DOR>? DORs { get; set; }
    }
}
