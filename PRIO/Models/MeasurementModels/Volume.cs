using PRIO.Models.BaseModels;
using System.Text.Json.Serialization;

namespace PRIO.Models.MeasurementModels
{
    public class Volume : BaseModel
    {
        public DateTime? DHA_MEDICAO_039 { get; set; }
        public double? DHA_MED_DECLARADO_039 { get; set; }
        public double? DHA_MED_REGISTRADO_039 { get; set; }
        [JsonIgnore]
        public Measurement? Measurement { get; set; }
    }
}
