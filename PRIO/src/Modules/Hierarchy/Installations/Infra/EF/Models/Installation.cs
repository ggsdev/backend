using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models
{
    public class Installation : BaseModel
    {
        public string? Name { get; set; }
        public string UepCod { get; set; }
        public string UepName { get; set; }
        public string? CodInstallationAnp { get; set; }
        public decimal? GasSafetyBurnVolume { get; set; }
        public User? User { get; set; }
        public List<InstallationBTP>? BTPs { get; set; }
        public Cluster? Cluster { get; set; }
        public OilVolumeCalculation? OilVolumeCalculation { get; set; }
        public List<MeasuringPoint>? MeasuringPoints { get; set; }
        public List<Field>? Fields { get; set; }
        public List<Measurement>? Measurements { get; set; }
        public GasVolumeCalculation GasVolumeCalculation { get; set; }
        public bool IsProcessingUnit { get; set; }
        public List<Production>? Productions { get; set; }
        public List<NFSM>? NFSMs { get; set; }
    }
}
