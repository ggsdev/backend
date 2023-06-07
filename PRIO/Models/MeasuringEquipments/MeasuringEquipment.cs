using PRIO.Models.BaseModels;
using PRIO.Models.Installations;
using PRIO.Models.Measurements;
using PRIO.Models.Users;

namespace PRIO.Models.MeasuringEquipments
{
    public class MeasuringEquipment : BaseModel
    {
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
        public string? InOperation { get; set; } 
        public string? Fluid { get; set; }
        public Installation? Installation { get; set; }
        public List<Measurement>? Measurements { get; set; }
        public User? User { get; set; }

    }
}
