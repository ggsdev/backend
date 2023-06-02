using PRIO.Models.Installations;
using PRIO.Models.Measurements;

namespace PRIO.Models
{
    public class MeasuringEquipment : BaseModel
    {
        public string Type { get; set; } = string.Empty;
        public string TagEquipment { get; set; } = string.Empty;
        public string TagMeasuringPoint { get; set; } = string.Empty;
        public string Fluid { get; set; } = string.Empty;
        public Installation Installation { get; set; }
        public List<Measurement>? Measurements { get; set; }
        public User? User { get; set; }

    }
}
