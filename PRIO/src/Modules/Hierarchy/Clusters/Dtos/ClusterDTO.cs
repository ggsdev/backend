using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.Hierarchy.Clusters.Dtos
{
    public class ClusterDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodCluster { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
