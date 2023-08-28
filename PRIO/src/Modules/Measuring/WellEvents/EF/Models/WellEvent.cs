using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Models
{
    public class WellEvent : BaseModel
    {
        public bool EventType { get; set; } = true;
        public List<EventReason> EventReasons { get; set; }
        public string Downtime { get; set; }
        public Well Well { get; set; }

    }
}
