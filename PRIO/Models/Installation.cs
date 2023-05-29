using System.Text.Json.Serialization;

namespace PRIO.Models
{
    public class Installation : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? CodInstallation { get; set; }
        public Field Field { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
    }
}
