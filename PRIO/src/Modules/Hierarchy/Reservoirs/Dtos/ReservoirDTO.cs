using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Zones.Dtos;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Dtos
{
    public class ReservoirDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public UserDTO? User { get; set; }
        public ZoneDTO? Zone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
