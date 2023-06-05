using PRIO.Models.BaseModels;
using PRIO.Models.Completions;
using PRIO.Models.Users;
using PRIO.Models.Zones;

namespace PRIO.Models.Reservoirs
{
    public class ReservoirHistory : BaseHistoryModel
    {
        public string Name { get; set; } = string.Empty;
        public string? NameOld { get; set; }
        public string CodReservoir { get; set; } = string.Empty;
        public string? CodReservoirOld { get; set; }
        public User? User { get; set; }
        public Reservoir? Reservoir { get; set; }
        public Zone? Zone { get; set; }
        public string ZoneName { get; set; } = string.Empty;
        public string? ZoneNameOld { get; set; }
        public Guid? ZoneOldId { get; set; }
        public List<Completion>? Completions { get; set; }

        public string Type { get; set; } = string.Empty;


    }
}
