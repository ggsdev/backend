using PRIO.Models.BaseModels;
using PRIO.Models.Fields;
using PRIO.Models.Users;

namespace PRIO.Models.Zones
{
    public class ZoneHistory : BaseHistoryModel
    {
        public string CodZone { get; set; } = string.Empty;
        public string? CodZoneOld { get; set; }
        public User? User { get; set; }
        public Field Field { get; set; }
        public Guid? FieldOldId { get; set; }
        public Zone Zone { get; set; }
        public string Type { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public string? FieldNameOld { get; set; }
    }
}
