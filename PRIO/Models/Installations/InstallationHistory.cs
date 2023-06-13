using PRIO.Models.BaseModels;
using PRIO.Models.Clusters;
using PRIO.Models.Users;
using System.Text.Json.Serialization;

namespace PRIO.Models.Installations
{
    public class InstallationHistory : BaseHistoryModel
    {
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodInstallationUep { get; set; }
        public string? CodInstallationUepOld { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public Cluster? Cluster { get; set; }
        public Guid? ClusterOldId { get; set; }
        public Installation? Installation { get; set; }

    }
}
