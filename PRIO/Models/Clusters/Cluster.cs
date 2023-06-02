using PRIO.Models.Installations;
using System.Text.Json.Serialization;

namespace PRIO.Models.Clusters
{
    public class Cluster : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? CodCluster { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public List<Installation>? Installations { get; set; }
        public List<ClusterHistory>? ClusterHistories { get; set; }
    }
}
