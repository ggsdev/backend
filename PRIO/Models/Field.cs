using System.Text.Json.Serialization;

namespace PRIO.Models
{

    public class Field : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string CodField { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Basin { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Installation Installation { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
    }
}
