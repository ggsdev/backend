using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models
{
    public class Reservoir : BaseModel
    {
        public string? Name { get; set; }
        public User? User { get; set; }
        public Zone? Zone { get; set; }
        public List<Completion>? Completions { get; set; }
    }
}
