using System.Text.Json.Serialization;

namespace PRIO.Models
{
    public class ClusterHistory
    {
        public Guid Id { get; set; }
        public string? NameOld { get; set; } 
        public string? Name { get; set; }
        public string? CodClusterOld { get; set; }
        public string? CodCluster { get; set; }
        public string? DescriptionOld { get; set; }
        public string? Description { get; set; }
        public bool? IsActiveOld { get; set; } = true;
        public bool? IsActive { get; set; } = true;
        public User? User { get; set; }
        public Cluster? Cluster { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
