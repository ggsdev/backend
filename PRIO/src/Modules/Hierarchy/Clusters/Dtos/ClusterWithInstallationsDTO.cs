using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;

namespace PRIO.src.Modules.Hierarchy.Clusters.Dtos
{
    public class ClusterWithInstallationsDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
        public List<InstallationWithoutClusterDTO>? Installations { get; set; }
    }
}
