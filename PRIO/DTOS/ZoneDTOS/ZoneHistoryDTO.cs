using PRIO.DTOS.ClusterDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.ZoneDTOS
{
    public class ZoneHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodInstallation { get; set; }
        public string? CodInstallationOld { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsActiveOld { get; set; }
        public string? Type { get; set; }
        public ClusterDTO? Cluster { get; set; }
        public UserDTO? User { get; set; }
        public ZoneDTO? Zone { get; set; }
        public string? ClusterName { get; set; }
        public string? ClusterNameOld { get; set; }
        public Guid? ClusterOldId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
