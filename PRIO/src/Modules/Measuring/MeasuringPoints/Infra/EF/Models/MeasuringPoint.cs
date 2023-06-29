using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models
{
    public class MeasuringPoint : BaseModel
    {
        public string? Name { get; set; }
        public string? TagPointMeasuring { get; set; }
        public Section? Section { get; set; }
        public TOGRecoveredOil? TOGRecoveredOil { get; set; }
        public DrainVolume? DrainVolume { get; set; }
        public DOR? DOR { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
        public Installation? Installation { get; set; }
    }
}
