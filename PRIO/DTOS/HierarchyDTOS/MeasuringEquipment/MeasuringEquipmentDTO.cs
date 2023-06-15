using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.HierarchyDTOS.MeasuringEquipment
{
    public class MeasuringEquipmentDTO
    {
        public Guid Id { get; set; }
        public string? TagEquipment { get; set; }
        public string? TagMeasuringPoint { get; set; }
        public string? SerieNumber { get; set; }
        public string? Type { get; set; }
        public string? TypeEquipment { get; set; }
        public string? Model { get; set; }
        public bool? HasSeal { get; set; }
        public bool? MVS { get; set; }
        public string? CommunicationProtocol { get; set; }
        public string? TypePoint { get; set; }
        public string? ChannelNumber { get; set; }
        public bool? InOperation { get; set; }
        public string? Fluid { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
