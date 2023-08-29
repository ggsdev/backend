﻿using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Models
{
    public class WellEvent : BaseModel
    {
        public string EventStatus { get; set; } = "A";
        public string IdAutoGenerated { get; set; } = string.Empty;
        public string SystemRelated { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Interval { get; set; }
        public string StatusANP { get; set; } = string.Empty;
        public string StateANP { get; set; } = string.Empty;
        public Well Well { get; set; }
        public WellEvent? EventRelated { get; set; }
        public List<EventReason> EventReasons { get; set; } = new();
        public List<ProductionLoss>? ProductionLosses { get; set; }
    }
}
