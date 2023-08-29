using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Models
{
    public class EventReason : BaseModel
    {
        public string Reason { get; set; } = string.Empty;
        public string Comment { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Interval { get; set; }
        public WellEvent WellEvent { get; set; }
    }
}
