using PRIO.DTOS.UserDTOS;
using PRIO.DTOS.ZoneDTOS;

namespace PRIO.DTOS.ReservoirDTOS
{
    public class ReservoirHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodReservoir { get; set; }
        public string? CodReservoirOld { get; set; }
        public ZoneDTO? Zone { get; set; }
        public Guid? ZoneOldId { get; set; }
        public UserDTO? User { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsActiveOld { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TypeOperation { get; set; }
    }
}
