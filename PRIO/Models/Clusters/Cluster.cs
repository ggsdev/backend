using PRIO.Models.BaseModels;
using PRIO.Models.Installations;
using PRIO.Models.Users;
using System.Text.Json.Serialization;

namespace PRIO.Models.Clusters
{
    public class Cluster : BaseModel
    {
        public string? Name { get; set; }
        public string? CodCluster { get; set; }
        public User? User { get; set; }
        [JsonIgnore]
        public List<Installation>? Installations { get; set; }
        public List<ClusterHistory>? ClusterHistories { get; set; }
        public List<InstallationHistory>? InstallationHistories { get; set; }
    }
}
