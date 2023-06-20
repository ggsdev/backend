using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;

namespace PRIO.src.Modules.Hierarchy.Zones.Dtos
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
