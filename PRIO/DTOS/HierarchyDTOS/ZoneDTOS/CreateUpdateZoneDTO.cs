using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.HierarchyDTOS.ZoneDTOS
{
    public class CreateUpdateZoneDTO
    {
        public Guid Id { get; set; }
        public string? CodZone { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
