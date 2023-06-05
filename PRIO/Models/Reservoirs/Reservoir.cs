using PRIO.Models.BaseModels;
using PRIO.Models.Completions;
using PRIO.Models.Users;
using PRIO.Models.Zones;

namespace PRIO.Models.Reservoirs
{
    public class Reservoir : BaseModel
    {
        public string? Name { get; set; }
        public string? CodReservoir { get; set; }
        public User? User { get; set; }
        public Zone? Zone { get; set; }
        public List<Completion>? Completions { get; set; }
        public List<ReservoirHistory>? ReservoirHistories { get; set; }
    }
}
