using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models
{
    public class Zone : BaseModel
    {
        public string? CodZone { get; set; }
        public User? User { get; set; }
        public Field? Field { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
    }
}
