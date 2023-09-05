using PRIO.src.Shared.Infra.EF.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Models
{
    public class EventReason : BaseModel
    {
        public string Comment { get; set; } = "";
        public string SystemRelated { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Interval { get; set; }
        public WellEvent WellEvent { get; set; }
        [ForeignKey("WellEvent")]
        public Guid WellEventId { get; set; }
    }
}
