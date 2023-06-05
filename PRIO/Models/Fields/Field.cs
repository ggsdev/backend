using PRIO.Models.BaseModels;
using PRIO.Models.Installations;
using PRIO.Models.Users;
using PRIO.Models.Wells;
using PRIO.Models.Zones;
using System.Text.Json.Serialization;
namespace PRIO.Models.Fields
{
    public class Field : BaseModel
    {
        public string? Name { get; set; }
        public string? CodField { get; set; }
        public string? State { get; set; }
        public string? Basin { get; set; }
        public string? Location { get; set; }
        public User? User { get; set; }
        [JsonIgnore]
        public Installation? Installation { get; set; }
        public List<Zone>? Zones { get; set; }
        public List<Well>? Wells { get; set; }
        public List<FieldHistory>? FieldHistories { get; set; }
        public List<ZoneHistory>? ZoneHistories { get; set; }
        public List<WellHistory>? WellHistories { get; set; }
    }
}
