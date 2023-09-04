using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models
{
    public class WellLosses : BaseModel
    {
        public DateTime MeasuredAt { get; set; }
        public decimal EfficienceLoss { get; set; }
        public decimal ProductionLost { get; set; }
        public decimal Downtime { get; set; }
        public decimal ProportionalDay { get; set; }
        public WellEvent Event { get; set; }
        public WellProductions WellAllocation { get; set; }
    }
}
