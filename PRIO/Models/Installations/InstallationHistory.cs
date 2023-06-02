using PRIO.Models.BaseModels;
using PRIO.Models.Clusters;
using PRIO.Models.Users;
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
        public string ClusterName { get; set; } = string.Empty;
        public string? ClusterNameOld { get; set; }
        public Guid? ClusterOldId { get; set; }
        public Installation Installation { get; set; }

    }
}
