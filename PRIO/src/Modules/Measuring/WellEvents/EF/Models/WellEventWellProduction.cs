using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Models
{
    public class ProductionLoss : BaseModel
    {
        public DateTime MeasuredAt { get; set; }
        public decimal EfficienceLoss { get; set; }
        public decimal ProductionLost { get; set; }
        public decimal Downtime { get; set; }
        public decimal ProportionalDay { get; set; }
        public WellEvent Event { get; set; }
        public WellProduction WellProduction { get; set; }
    }
}
