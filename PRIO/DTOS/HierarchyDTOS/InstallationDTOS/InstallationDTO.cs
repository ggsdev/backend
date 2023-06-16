using PRIO.DTOS.HierarchyDTOS.ClusterDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.HierarchyDTOS.InstallationDTOS
{
    public class InstallationDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? CodInstallationUep { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
        public ClusterDTO? Cluster { get; set; }
    }
}
