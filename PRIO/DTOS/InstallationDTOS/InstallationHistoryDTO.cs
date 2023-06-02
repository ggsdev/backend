using PRIO.DTOS.ClusterDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.InstallationDTOS
{
    public class InstallationHistoryDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? NameOld { get; set; }
        public string? CodInstallation { get; set; }
        public string? CodInstallationOld { get; set; }
        public string? Type { get; set; }
        public ClusterDTO? Cluster { get; set; }
        public UserDTO? User { get; set; }
        public string ClusterName { get; set; } = string.Empty;
        public string? ClusterNameOld { get; set; }
        public Guid? ClusterOldId { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
