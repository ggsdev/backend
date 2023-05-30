using System.Text.Json.Serialization;

namespace PRIO.Models
{
    public class Installation : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? CodInstallation { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public Cluster? Cluster { get; set; }
        public List<Field>? Fields { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
    }
}
