using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Models
{
    public class EventReason : BaseModel
    {
        public string Reason { get; set; } = string.Empty;
        public string Downtime { get; set; } = string.Empty;
        public WellEvent WellEvent { get; set; }
    }
}
