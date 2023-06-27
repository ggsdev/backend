using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;

namespace PRIO.src.Modules.Hierarchy.Zones.Dtos
{
    public class ZoneWithReservoirsDTO
    {
        public Guid? Id { get; set; }
        public string? CodZone { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public UserDTO? User { get; set; }
        public List<ReservoirWithoutZoneDTO>? Reservoirs { get; set; }
        public FieldWithoutInstallationDTO? Field { get; set; }
    }
}
