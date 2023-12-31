﻿using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models
{
    public class WellEvent : BaseModel
    {
        public string EventStatus { get; set; } = "A";
        public string IdAutoGenerated { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Interval { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? StatusANP { get; set; }
        public string? StateANP { get; set; }
        public string? EventRelatedCode { get; set; }
        public Well Well { get; set; }
        public WellEvent? EventRelated { get; set; }
        public List<EventReason> EventReasons { get; set; } = new();
        public List<WellLosses>? WellLosses { get; set; }
        public User CreatedBy { get; set; }
        public User? UpdatedBy { get; set; }
        public WellEventXML042Base64? XMLBase64 { get; set; }
    }
}
