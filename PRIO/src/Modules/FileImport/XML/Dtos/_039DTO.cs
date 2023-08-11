using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.Dtos
{
    public class _039DTO
    {
        public Guid Id { get; set; }
        public string COD_TAG_EQUIPAMENTO_039 { get; set; }
        public string COD_TAG_PONTO_MEDICAO_039 { get; set; }
        public string COD_FALHA_SUPERIOR_039 { get; set; }
        public short? DSC_TIPO_FALHA_039 { get; set; }
        public string COD_FALHA_039 { get; set; }
        public string IND_TIPO_NOTIFICACAO_039 { get; set; }
        public string FileName { get; set; } = string.Empty;

        public DateTime DHA_OCORRENCIA_039 { get; set; }
        public DateTime DHA_DETECCAO_039 { get; set; }
        public DateTime DHA_RETORNO_039 { get; set; }
        public string DHA_NUM_PREVISAO_RETORNO_DIAS_039 { get; set; }
        public string DHA_DSC_FALHA_039 { get; set; }
        public string DHA_DSC_ACAO_039 { get; set; }
        public string DHA_DSC_METODOLOGIA_039 { get; set; }
        public string DHA_NOM_RESPONSAVEL_RELATO_039 { get; set; }
        public string DHA_NUM_SERIE_EQUIPAMENTO_039 { get; set; }
        public string DHA_COD_INSTALACAO_039 { get; set; } = string.Empty;

        public List<Calibration> LISTA_CALIBRACAO { get; set; } = new();
        public List<Bsw> LISTA_BSW { get; set; } = new();
        public List<Volume> LISTA_VOLUME { get; set; } = new();
        public Guid ImportId { get; set; }

    }

    public class ResponseNFSMDTO
    {
        public List<Client039DTO> NFSMs { get; set; } = new();
        public MeasurementHistoryDto File { get; set; }
    }
    public class Client039DTO
    {

        public SummaryNfsmDto Summary { get; set; }

        public Guid Id { get; set; }
        public string COD_TAG_PONTO_MEDICAO_039 { get; set; } = string.Empty;

        public string COD_TAG_EQUIPAMENTO_039 { get; set; } = string.Empty;
        public string COD_FALHA_SUPERIOR_039 { get; set; }
        public short? DSC_TIPO_FALHA_039 { get; set; }
        public string COD_FALHA_039 { get; set; }
        public string IND_TIPO_NOTIFICACAO_039 { get; set; }
        public string FileName { get; set; } = string.Empty;

        public DateTime DHA_OCORRENCIA_039 { get; set; }
        public DateTime DHA_DETECCAO_039 { get; set; }
        public DateTime DHA_RETORNO_039 { get; set; }
        public string DHA_NUM_PREVISAO_RETORNO_DIAS_039 { get; set; }
        public string DHA_DSC_FALHA_039 { get; set; }
        public string DHA_DSC_ACAO_039 { get; set; }
        public string DHA_DSC_METODOLOGIA_039 { get; set; }
        public string DHA_NOM_RESPONSAVEL_RELATO_039 { get; set; }
        public string DHA_NUM_SERIE_EQUIPAMENTO_039 { get; set; }
        public string DHA_COD_INSTALACAO_039 { get; set; } = string.Empty;

        public List<CalibrationDto> LISTA_CALIBRACAO { get; set; } = new();
        public List<BswDto> LISTA_BSW { get; set; } = new();
        public List<VolumeDto> LISTA_VOLUME { get; set; } = new();
        public Guid ImportId { get; set; }

    }
    public class CalibrationDto
    {
        public DateTime? DHA_FALHA_CALIBRACAO_039 { get; set; }

        public decimal? DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 { get; set; }

        public decimal? DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 { get; set; }

        public string? DHA_CERTIFICADO_ATUAL_039 { get; set; }

        public string? DHA_CERTIFICADO_ANTERIOR_039 { get; set; }
    }

    public class VolumeDto
    {
        public DateTime? DHA_MEDICAO_039 { get; set; }
        public decimal? DHA_MED_DECLARADO_039 { get; set; }
        public decimal? DHA_MED_REGISTRADO_039 { get; set; }
    }

    public class BswDto
    {
        public DateTime? DHA_FALHA_BSW_039 { get; set; }

        public decimal? DHA_PCT_BSW_039 { get; set; }

        public decimal? DHA_PCT_MAXIMO_BSW_039 { get; set; }
    }

    public class SummaryNfsmDto
    {
        public string UepName { get; set; } = string.Empty;
        public string MeasuringPoint { get; set; } = string.Empty;
        public string Equipment { get; set; } = string.Empty;
        public string ResponsibleReport { get; set; } = string.Empty;
        public short? TypeOfFailure { get; set; }
        public string? TypeOfNotification { get; set; }
        public string CodeFailure { get; set; } = string.Empty;
        public DateTime DateOfOcurrence { get; set; }
        public DateTime DetectionDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string DescriptionFailure { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Methodology { get; set; } = string.Empty;
        public List<VolumeFixedNfsm> MeasurementsFixed { get; set; } = new();
        public List<BswFixedNfsm> BswsFixed { get; set; } = new();
    }

    public class VolumeFixedNfsm
    {
        public DateTime? MeasuredAt { get; set; }
        public decimal? VolumeAfter { get; set; }
        public decimal? VolumeBefore { get; set; }
    }

    public class BswFixedNfsm
    {
        public decimal? MaxBsw { get; set; }
        public decimal? Bsw { get; set; }
        public DateTime? Date { get; set; }

    }
}
