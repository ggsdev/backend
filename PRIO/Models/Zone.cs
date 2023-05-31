using System.Text.Json.Serialization;

namespace PRIO.Models
{
    public class Zone : BaseModel
    {
        public string CodZone { get; set; } = string.Empty;
        [JsonIgnore]
        public User? User { get; set; }
        public Field Field { get; set; }
        [JsonIgnore]
        public List<Reservoir>? Reservoirs { get; set; }
    }
}
