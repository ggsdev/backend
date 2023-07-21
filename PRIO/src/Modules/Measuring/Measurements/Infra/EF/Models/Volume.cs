using PRIO.src.Shared.Infra.EF.Models;
using System.Text.Json.Serialization;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models
{
    public class Volume : BaseModel
    {
        public DateTime? DHA_MEDICAO_039 { get; set; }
        public decimal? DHA_MED_DECLARADO_039 { get; set; }
        public decimal? DHA_MED_REGISTRADO_039 { get; set; }
        [JsonIgnore]
        public Measurement? Measurement { get; set; }
    }
}
