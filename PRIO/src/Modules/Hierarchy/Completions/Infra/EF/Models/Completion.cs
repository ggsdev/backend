using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models
{
    public class Completion : BaseModel
    {
        public string? Name { get; set; }
        public string? CodCompletion { get; set; }
        public Reservoir? Reservoir { get; set; }
        public Well? Well { get; set; }
        public User? User { get; set; }
    }
}
