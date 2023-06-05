using PRIO.Models.BaseModels;
using PRIO.Models.Clusters;
using PRIO.Models.Fields;
using PRIO.Models.MeasuringEquipments;
using PRIO.Models.Users;
using System.Text.Json.Serialization;

namespace PRIO.Models.Installations
{
    public class Installation : BaseModel
    {
        public string? Name { get; set; }
        public string? CodInstallation { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public Cluster? Cluster { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
        public List<InstallationHistory>? InstallationHistories { get; set; }
        public List<Field>? Fields { get; set; }
        public List<FieldHistory>? FieldHistories { get; set; }
    }
}
