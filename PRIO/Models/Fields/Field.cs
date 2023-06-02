using System.Text.Json.Serialization;
using PRIO.Models.Installations;

namespace PRIO.Models.Fields
{

    public class Field : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string CodField { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Basin { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public User? User { get; set; }
        [JsonIgnore]
        public Installation Installation { get; set; }
        public List<Zone>? Zones { get; set; }
        public List<Well>? Wells { get; set; }
    }
}
