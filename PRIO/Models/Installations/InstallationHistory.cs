using PRIO.Models.Clusters;
using System.Text.Json.Serialization;

namespace PRIO.Models.Installations
{
    public class InstallationHistory : BaseHistoryModel
    {
        public string Name { get; set; } = string.Empty;
        public string? NameOld { get; set; }
        public string? CodInstallation { get; set; }
        public string? CodInstallationOld { get; set; }
        public string? Type { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public Cluster Cluster { get; set; }
        public Cluster? ClusterOld { get; set; }

        public Installation Installation { get; set; }
        public List<Field>? Fields { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }

    }
}
