using PRIO.Models.BaseModels;
using PRIO.Models.Fields;
using PRIO.Models.Reservoirs;
using PRIO.Models.Users;

namespace PRIO.Models.Zones
{
    public class Zone : BaseModel
    {
        public string? CodZone { get; set; }
        public User? User { get; set; }
        public Field? Field { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
        public List<ZoneHistory>? ZoneHistories { get; set; }
        public List<ReservoirHistory>? ReservoirHistories { get; set; }
    }
}
