﻿using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models
{
    public class WellLosses : BaseModel
    {
        public DateTime MeasuredAt { get; set; }
        public decimal EfficienceLoss { get; set; }
        public decimal ProductionLostOil { get; set; }
        public decimal ProportionalDay { get; set; }

        public decimal ProductionLostGas { get; set; }

        public decimal ProductionLostWater { get; set; }

        public decimal Downtime { get; set; }
        public WellEvent Event { get; set; }
        public WellProduction WellAllocation { get; set; }
    }
}
