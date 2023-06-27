using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models
{
    public class Installation : BaseModel
    {
        public string? Name { get; set; }
        public string? UepCod { get; set; }
        public string? UepName { get; set; }
        public string? CodInstallationAnp { get; set; }
        public double? GasSafetyBurnVolume { get; set; }
        public User? User { get; set; }
        public Cluster? Cluster { get; set; }
        public OilVolumeCalculation? OilVolumeCalculation { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
        public List<Field>? Fields { get; set; }
        public List<Measurement>? Measurements { get; set; }
    }
}
