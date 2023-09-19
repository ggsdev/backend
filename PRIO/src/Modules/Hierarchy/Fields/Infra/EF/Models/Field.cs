using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models
{
    public class Field : BaseModel
    {
        public string? Name { get; set; }
        public string? CodField { get; set; }
        public string? State { get; set; }
        public string? Basin { get; set; }
        public string? Location { get; set; }
        public DateTime? InactivatedAt { get; set; }
        public User? User { get; set; }
        public Installation? Installation { get; set; }
        public List<Zone>? Zones { get; set; }
        public List<Well>? Wells { get; set; }
        public List<FieldFR>? FRs { get; set; }
    }
}
