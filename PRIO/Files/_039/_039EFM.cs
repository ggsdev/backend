using System.Xml.Serialization;

namespace PRIO.Files._039
{
    [XmlRoot("a039")]
    public class _039EFM
    {
        [XmlArray("LISTA_DADOS_BASICOS")]
        [XmlArrayItem("DADOS_BASICOS")]
        public List<DADOS_BASICOS> LISTA_DADOS_BASICOS { get; set; }
    }

    [XmlRoot("DADOS_BASICOS")]
    public class DADOS_BASICOS
    {
        [XmlAttribute("COD_FALHA")]
        public string? COD_FALHA_039 { get; set; }
        [XmlAttribute("COD_INSTALACAO")]
        public string? DHA_COD_INSTALACAO_039 { get; set; }
        [XmlElement("COD_TAG_EQUIPAMENTO")]
        public string? COD_TAG_EQUIPAMENTO_039 { get; set; }
        [XmlElement("COD_FALHA_SUPERIOR")]
        public string? COD_FALHA_SUPERIOR_039 { get; set; }
        [XmlElement("DSC_TIPO_FALHA")]
        public string? DSC_TIPO_FALHA_039 { get; set; }
        [XmlElement("IND_TIPO_NOTIFICACAO")]
        public string? IND_TIPO_NOTIFICACAO_039 { get; set; }
        [XmlElement("DHA_OCORRENCIA")]
        public string? DHA_OCORRENCIA_039 { get; set; }
        [XmlElement("DHA_DETECCAO")]
        public string? DHA_DETECCAO_039 { get; set; }
        [XmlElement("DHA_RETORNO")]
        public string? DHA_RETORNO_039 { get; set; }
        [XmlElement("NUM_PREVISAO_RETORNO_DIAS")]
        public string DHA_NUM_PREVISAO_RETORNO_DIAS_039 { get; set; }
        [XmlElement("DSC_FALHA")]
        public string? DHA_DSC_FALHA_039 { get; set; }
        [XmlElement("DSC_ACAO")]
        public string? DHA_DSC_ACAO_039 { get; set; }
        [XmlElement("DSC_METODOLOGIA")]
        public string? DHA_DSC_METODOLOGIA_039 { get; set; }
        [XmlElement("NOM_RESPONSAVEL_RELATO")]
        public string? DHA_NOM_RESPONSAVEL_RELATO_039 { get; set; }
        [XmlElement("NUM_SERIE_EQUIPAMENTO")]
        public string? DHA_NUM_SERIE_EQUIPAMENTO_039 { get; set; }

        [XmlArray("LISTA_CALIBRACAO")]
        [XmlArrayItem("CALIBRACAO")]
        public List<CALIBRACAO>? LISTA_CALIBRACAO { get; set; }

        [XmlArray("LISTA_BSW")]
        [XmlArrayItem("BSW")]
        public List<BSW>? LISTA_BSW { get; set; }

        [XmlArray("LISTA_VOLUME")]
        [XmlArrayItem("VOLUME")]
        public List<VOLUME>? LISTA_VOLUME { get; set; }
    }

    [XmlRoot("BSW")]
    public class BSW
    {
        [XmlElement("DHA_FALHA_BSW")]
        public string? DHA_FALHA_BSW_039 { get; set; }

        [XmlElement("PCT_BSW")]
        public string? DHA_PCT_BSW_039 { get; set; }

        [XmlElement("PCT_MAXIMO_BSW")]
        public string? DHA_PCT_MAXIMO_BSW_039 { get; set; }
    }

    [XmlRoot("VOLUME")]
    public class VOLUME
    {
        [XmlElement("DHA_MEDICAO")]
        public string? DHA_MEDICAO_039 { get; set; }
        [XmlElement("MED_DECLARADO")]
        public string? DHA_MED_DECLARADO_039 { get; set; }
        [XmlElement("MED_REGISTRADO")]
        public string? DHA_MED_REGISTRADO_039 { get; set; }
    }

    [XmlRoot("CALIBRACAO")]
    public class CALIBRACAO
    {
        [XmlElement("DHA_FALHA_CALIBRACAO")]
        public string? DHA_FALHA_CALIBRACAO_039 { get; set; }

        [XmlElement("NUM_FATOR_CALIBRACAO_ANTERIOR")]
        public string? DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 { get; set; }

        [XmlElement("NUM_FATOR_CALIBRACAO_ATUAL")]
        public string? DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 { get; set; }

        [XmlElement("NUM_CERTIFICADO_ANTERIOR")]
        public string? DHA_CERTIFICADO_ANTERIOR_039 { get; set; }

        [XmlElement("NUM_CERTIFICADO_ATUAL")]
        public string? DHA_CERTIFICADO_ATUAL_039 { get; set; }
    }
}


