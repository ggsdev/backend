using PRIO.Models.BaseModels;
using PRIO.Models.Fields;
using PRIO.Models.Users;

namespace PRIO.Models.Zones
{
    public class ZoneHistory : BaseHistoryModel
    {
        public string? CodZone { get; set; }
        public string? CodZoneOld { get; set; }
        public User? User { get; set; }
        public Field? Field { get; set; }
        public Guid? FieldOldId { get; set; }
        public Zone? Zone { get; set; }
    }
}
