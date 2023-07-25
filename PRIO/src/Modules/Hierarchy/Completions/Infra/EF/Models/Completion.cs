using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models
{
    public class Completion : BaseModel
    {
        public string? Name { get; set; }
        public decimal? TopOfPerforated { get; set; }
        public decimal? BaseOfPerforated { get; set; }
        public decimal? AllocationReservoir { get; set; } = 1;
        public Reservoir? Reservoir { get; set; }
        public Well? Well { get; set; }
        public User? User { get; set; }
    }
}
