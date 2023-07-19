using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
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

        public AssistanceGas? AssistanceGas { get; set; }
        public ExportGas? ExportGas { get; set; }
        public HighPressureGas? HighPressureGas { get; set; }
        public HPFlare? HpFlare { get; set; }
        public ImportGas? ImportGas { get; set; }
        public LowPressureGas? LowPressureGas { get; set; }
        public LPFlare? LpFlare { get; set; }
        public PilotGas? PilotGas { get; set; }
        public PurgeGas? PurgeGas { get; set; }
    }
}
