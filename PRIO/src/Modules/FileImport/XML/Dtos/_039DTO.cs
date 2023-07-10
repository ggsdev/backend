using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.Dtos
{
    public class _039DTO
    {
        public Guid Id { get; set; }
        public string? COD_TAG_EQUIPAMENTO_039 { get; set; }
        public string? COD_FALHA_SUPERIOR_039 { get; set; }
        public short? DSC_TIPO_FALHA_039 { get; set; }
        public string? COD_FALHA_039 { get; set; }
        public string? IND_TIPO_NOTIFICACAO_039 { get; set; }
        public DateTime? DHA_OCORRENCIA_039 { get; set; }
        public DateTime? DHA_DETECCAO_039 { get; set; }
        public DateTime? DHA_RETORNO_039 { get; set; } = null;
        public string? DHA_NUM_PREVISAO_RETORNO_DIAS_039 { get; set; }
        public string? DHA_DSC_FALHA_039 { get; set; }
        public string? DHA_DSC_ACAO_039 { get; set; }
        public string? DHA_DSC_METODOLOGIA_039 { get; set; }
        public string? DHA_NOM_RESPONSAVEL_RELATO_039 { get; set; }
        public string? DHA_NUM_SERIE_EQUIPAMENTO_039 { get; set; }
        public string? DHA_COD_INSTALACAO_039 { get; set; }

        public List<Calibration>? LISTA_CALIBRACAO { get; set; }
        public List<Bsw>? LISTA_BSW { get; set; }
        public List<Volume>? LISTA_VOLUME { get; set; }
        public Guid? ImportId { get; set; }

    }
}
