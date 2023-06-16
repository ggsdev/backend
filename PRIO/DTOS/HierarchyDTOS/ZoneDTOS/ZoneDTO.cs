using PRIO.DTOS.HierarchyDTOS.FieldDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.HierarchyDTOS.ZoneDTOS
{
    public class ZoneDTO
    {
        public Guid? Id { get; set; }
        public string? CodZone { get; set; }
        public string? Description { get; set; }
        public UserDTO? User { get; set; }
        public FieldDTO? Field { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
