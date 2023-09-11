using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models
{
    public class WellLosses : BaseModel
    {
        public DateTime MeasuredAt { get; set; }
        public decimal EfficienceLossOil { get; set; }
        public decimal ProductionLostOil { get; set; }
        public decimal ProportionalDayOil { get; set; }

        public decimal EfficienceLossGas { get; set; }
        public decimal ProductionLostGas { get; set; }
        public decimal ProportionalDayGas { get; set; }

        public decimal EfficienceLossWater { get; set; }
        public decimal ProductionLostWater { get; set; }
        public decimal ProportionalDayWater { get; set; }

        public decimal Downtime { get; set; }
        public WellEvent Event { get; set; }
        public WellProduction WellAllocation { get; set; }
    }
}
