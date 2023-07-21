using PRIO.src.Shared.Infra.EF.Models;
using System.Text.Json.Serialization;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models
{
    public class Bsw : BaseModel
    {
        public DateTime? DHA_FALHA_BSW_039 { get; set; }

        public decimal? DHA_PCT_BSW_039 { get; set; }

        public decimal? DHA_PCT_MAXIMO_BSW_039 { get; set; }
        [JsonIgnore]
        public Measurement? Measurement { get; set; }
    }
}
