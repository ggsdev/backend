using System.Text.Json.Serialization;

namespace PRIO.Models
{
    public class Cluster : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? CodCluster { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public List<Field>? Fields { get; set; }
    }
}
