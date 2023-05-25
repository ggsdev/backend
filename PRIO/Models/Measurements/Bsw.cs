using System.Text.Json.Serialization;

namespace PRIO.Models.Measurements
{
    public class Bsw : BaseModel
    {
        public DateTime? DHA_FALHA_BSW_039 { get; set; }

        public double DHA_PCT_BSW_039 { get; set; }

        public double DHA_PCT_MAXIMO_BSW_039 { get; set; }
        [JsonIgnore]
        public Measurement Measurement { get; set; }
    }
}
