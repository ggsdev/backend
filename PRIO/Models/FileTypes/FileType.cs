using PRIO.Models.BaseModels;
using PRIO.Models.Measurements;
using System.Text.Json.Serialization;

namespace PRIO.Models.FileTypes
{
    public class FileType : BaseModel
    {
        public string? Name { get; set; }
        public string? Acronym { get; set; }
        [JsonIgnore]
        public List<Measurement> Measurements { get; set; }
    }
}
