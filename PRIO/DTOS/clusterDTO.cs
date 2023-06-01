using PRIO.Models;

namespace PRIO.DTOS
{
    public class ClusterDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodCluster { get; set; }
        public string? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
