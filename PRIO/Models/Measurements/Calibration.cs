using System.Text.Json.Serialization;

namespace PRIO.Models.Measurements
{
    public class Calibration : BaseModel
    {
        public DateTime? DHA_FALHA_CALIBRACAO_039 { get; set; }

        public double? DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 { get; set; }

        public double? DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 { get; set; }

        public string? DHA_CERTIFICADO_ATUAL_039 { get; set; }

        public string? DHA_CERTIFICADO_ANTERIOR_039 { get; set; }
        [JsonIgnore]
        public Measurement Measurement { get; set; }
    }
}
