using PRIO.DTOS.ClusterDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.InstallationDTOS
{
    public class InstallationHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodInstallation { get; set; }
        public string? CodInstallationOld { get; set; }
        public ClusterDTO? Cluster { get; set; }
        public Guid? ClusterOldId { get; set; }
        public UserDTO? User { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsActiveOld { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TypeOperation { get; set; }
    }
}
