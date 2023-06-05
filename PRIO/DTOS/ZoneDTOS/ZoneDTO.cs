using PRIO.DTOS.ReservoirDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.ZoneDTOS
{
    public class ZoneDTO : BaseDTO
    {
        public string? CodZone { get; set; }
        public UserDTO? User { get; set; }
        public List<ZoneHistoryDTO>? ZoneHistories { get; set; }
        public List<ReservoirDTO>? Reservoirs { get; set; }
    }
}
