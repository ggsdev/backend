using System.Xml.Serialization;

namespace PRIO.Files._001
{
    [XmlRoot("a001")]
    public class _001PMO
    {
        [XmlArray("LISTA_DADOS_BASICOS")]
        [XmlArrayItem("DADOS_BASICOS")]
        public List<DADOS_BASICOS> LISTA_DADOS_BASICOS { get; set; }
    }

    [XmlRoot("DADOS_BASICOS")]
    public class DADOS_BASICOS
    {
        [XmlAttribute("NUM_SERIE_ELEMENTO_PRIMARIO")]
        public string NUM_SERIE_ELEMENTO_PRIMARIO_001 { get; set; }

        [XmlAttribute("COD_INSTALACAO")]
        public int COD_INSTALACAO_001 { get; set; }

        [XmlAttribute("COD_TAG_PONTO_MEDICAO")]
        public string COD_TAG_PONTO_MEDICAO_001 { get; set; }

        [XmlArray("LISTA_CONFIGURACAO_CV")]
        [XmlArrayItem("CONFIGURACAO_CV")]
        public List<CONFIGURACAO_CV> LISTA_CONFIGURACAO_CV { get; set; }


        [XmlArray("LISTA_ELEMENTO_PRIMARIO")]
        [XmlArrayItem("ELEMENTO_PRIMARIO")]
        public List<ELEMENTO_PRIMARIO> LISTA_ELEMENTO_PRIMARIO { get; set; }

        [XmlArray("LISTA_INSTRUMENTO_PRESSAO")]
        [XmlArrayItem("INSTRUMENTO_PRESSAO")]
        public List<INSTRUMENTO_PRESSAO> LISTA_INSTRUMENTO_PRESSAO { get; set; }

        [XmlArray("LISTA_INSTRUMENTO_TEMPERATURA")]
        [XmlArrayItem("INSTRUMENTO_TEMPERATURA")]
        public List<INSTRUMENTO_TEMPERATURA> LISTA_INSTRUMENTO_TEMPERATURA { get; set; }

        [XmlArray("LISTA_PRODUCAO")]
        [XmlArrayItem("PRODUCAO")]
        public List<PRODUCAO> LISTA_PRODUCAO { get; set; }
    }

    [XmlRoot("CONFIGURACAO_CV")]
    public class CONFIGURACAO_CV
    {
        [XmlElement("NUM_SERIE_COMPUTADOR_VAZAO")]
        public string? NUM_SERIE_COMPUTADOR_VAZAO_001 { get; set; }
        [XmlElement("DHA_COLETA")]
        public DateTime? DHA_COLETA_001 { get; set; }
        [XmlElement("MED_TEMPERATURA")]
        public decimal? MED_TEMPERATURA_001 { get; set; }
        [XmlElement("MED_PRESSAO_ATMSA")]
        public decimal? MED_PRESSAO_ATMSA_001 { get; set; }
        [XmlElement("MED_PRESSAO_RFRNA")]
        public decimal? MED_PRESSAO_RFRNA_001 { get; set; }
        [XmlElement("MED_DENSIDADE_RELATIVA")]
        public decimal? MED_DENSIDADE_RELATIVA_001 { get; set; }
        [XmlElement("DSC_VERSAO_SOFTWARE")]
        public string? DSC_VERSAO_SOFTWARE_001 { get; set; }
    }

    [XmlRoot("ELEMENTO_PRIMARIO")]
    public class ELEMENTO_PRIMARIO
    {
        [XmlElement("ICE_METER_FACTOR_1")]
        public decimal? ICE_METER_FACTOR_1_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_2")]
        public decimal? ICE_METER_FACTOR_2_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_3")]
        public decimal? ICE_METER_FACTOR_3_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_4")]
        public decimal? ICE_METER_FACTOR_4_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_5")]
        public decimal? ICE_METER_FACTOR_5_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_6")]
        public decimal? ICE_METER_FACTOR_6_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_7")]
        public decimal? ICE_METER_FACTOR_7_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_8")]
        public decimal? ICE_METER_FACTOR_8_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_9")]
        public decimal? ICE_METER_FACTOR_9_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_10")]
        public decimal? ICE_METER_FACTOR_10_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_11")]
        public decimal? ICE_METER_FACTOR_11_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_12")]
        public decimal? ICE_METER_FACTOR_12_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_13")]
        public decimal? ICE_METER_FACTOR_13_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_14")]
        public decimal? ICE_METER_FACTOR_14_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_15")]
        public decimal? ICE_METER_FACTOR_15_001 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_1")]
        public decimal? QTD_PULSOS_METER_FACTOR_1_001 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_2")]
        public decimal? QTD_PULSOS_METER_FACTOR_2_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_3")]
        public decimal? QTD_PULSOS_METER_FACTOR_3_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_4")]
        public decimal? QTD_PULSOS_METER_FACTOR_4_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_5")]
        public decimal? QTD_PULSOS_METER_FACTOR_5_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_6")]
        public decimal? QTD_PULSOS_METER_FACTOR_6_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_7")]
        public decimal? QTD_PULSOS_METER_FACTOR_7_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_8")]
        public decimal? QTD_PULSOS_METER_FACTOR_8_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_9")]
        public decimal? QTD_PULSOS_METER_FACTOR_9_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_10")]
        public decimal? QTD_PULSOS_METER_FACTOR_10_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_11")]
        public decimal? QTD_PULSOS_METER_FACTOR_11_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_12")]
        public decimal? QTD_PULSOS_METER_FACTOR_12_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_13")]
        public decimal? QTD_PULSOS_METER_FACTOR_13_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_14")]
        public decimal? QTD_PULSOS_METER_FACTOR_14_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_15")]
        public decimal? QTD_PULSOS_METER_FACTOR_15_001 { get; set; }
        [XmlElement("ICE_K_FACTOR_1")]
        public decimal? ICE_K_FACTOR_1_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_2")]
        public decimal? ICE_K_FACTOR_2_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_3")]
        public decimal? ICE_K_FACTOR_3_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_4")]
        public decimal? ICE_K_FACTOR_4_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_5")]
        public decimal? ICE_K_FACTOR_5_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_6")]
        public decimal? ICE_K_FACTOR_6_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_7")]
        public decimal? ICE_K_FACTOR_7_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_8")]
        public decimal? ICE_K_FACTOR_8_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_9")]
        public decimal? ICE_K_FACTOR_9_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_10")]
        public decimal? ICE_K_FACTOR_10_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_11")]
        public decimal? ICE_K_FACTOR_11_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_12")]
        public decimal? ICE_K_FACTOR_12_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_13")]
        public decimal? ICE_K_FACTOR_13_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_14")]
        public decimal? ICE_K_FACTOR_14_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_15")]
        public decimal? ICE_K_FACTOR_15_001 { get; set; }

        [XmlElement("IND_HABILITACAO_ALARME")]
        public bool? IND_HABILITACAO_ALARME_1_001 { get; set; }
        [XmlElement("QTD_PULSOS_K_FACTOR_1")]
        public decimal? QTD_PULSOS_K_FACTOR_1_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_2")]
        public decimal? QTD_PULSOS_K_FACTOR_2_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_3")]
        public decimal? QTD_PULSOS_K_FACTOR_3_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_4")]
        public decimal? QTD_PULSOS_K_FACTOR_4_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_5")]
        public decimal? QTD_PULSOS_K_FACTOR_5_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_6")]
        public decimal? QTD_PULSOS_K_FACTOR_6_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_7")]
        public decimal? QTD_PULSOS_K_FACTOR_7_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_8")]
        public decimal? QTD_PULSOS_K_FACTOR_8_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_9")]
        public decimal? QTD_PULSOS_K_FACTOR_9_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_10")]
        public decimal? QTD_PULSOS_K_FACTOR_10_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_11")]
        public decimal? QTD_PULSOS_K_FACTOR_11_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_12")]
        public decimal? QTD_PULSOS_K_FACTOR_12_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_13")]
        public decimal? QTD_PULSOS_K_FACTOR_13_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_14")]
        public decimal? QTD_PULSOS_K_FACTOR_14_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_15")]
        public decimal? QTD_PULSOS_K_FACTOR_15_001 { get; set; }

        [XmlElement("ICE_CUTOFF")]
        public decimal? ICE_CUTOFF_001 { get; set; }

        [XmlElement("ICE_LIMITE_SPRR_ALARME")]
        public decimal? ICE_LIMITE_SPRR_ALARME_001 { get; set; }

        [XmlElement("ICE_LIMITE_INFRR_ALARME")]
        public decimal? ICE_LIMITE_INFRR_ALARME_001 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_PRESSAO")]
    public class INSTRUMENTO_PRESSAO
    {

        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_1_001 { get; set; }
        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_001 { get; set; }

        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_001 { get; set; }

        [XmlElement("IND_HABILITACAO_ALARME")]
        public bool? IND_HABILITACAO_ALARME_2_001 { get; set; }
        [XmlElement("MED_PRSO_ADOTADA_FALHA")]
        public decimal? MED_PRSO_ADOTADA_FALHA_001 { get; set; }

        [XmlElement("DSC_ESTADO_INSNO_CASO_FALHA")]
        public string? DSC_ESTADO_INSNO_CASO_FALHA_001 { get; set; }

        [XmlElement("IND_TIPO_PRESSAO_CONSIDERADA")]
        public string? IND_TIPO_PRESSAO_CONSIDERADA_001 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_TEMPERATURA")]
    public class INSTRUMENTO_TEMPERATURA
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_2_001 { get; set; }
        [XmlElement("MED_TMPTA_SPRR_ALARME")]
        public decimal? MED_TMPTA_SPRR_ALARME_001 { get; set; }
        [XmlElement("MED_TMPTA_INFRR_ALRME")]
        public decimal? MED_TMPTA_INFRR_ALRME_001 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public bool? IND_HABILITACAO_ALARME_3_001 { get; set; }
        [XmlElement("MED_TMPTA_ADTTA_FALHA")]
        public decimal? MED_TMPTA_ADTTA_FALHA_001 { get; set; }
        [XmlElement("DSC_ESTADO_INSTRUMENTO_FALHA")]
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_1_001 { get; set; }

    }

    [XmlRoot("PRODUCAO")]
    public class PRODUCAO
    {
        [XmlElement("DHA_INICIO_PERIODO_MEDICAO")]
        public DateTime? DHA_INICIO_PERIODO_MEDICAO_001 { get; set; }
        [XmlElement("DHA_FIM_PERIODO_MEDICAO")]
        public DateTime? DHA_FIM_PERIODO_MEDICAO_001 { get; set; }
        [XmlElement("ICE_DENSIDADADE_RELATIVA")]
        public decimal? ICE_DENSIDADADE_RELATIVA_001 { get; set; }
        [XmlElement("ICE_CORRECAO_BSW")]
        public decimal? ICE_CORRECAO_BSW_001 { get; set; }
        [XmlElement("ICE_CORRECAO_PRESSAO_LIQUIDO")]
        public decimal? ICE_CORRECAO_PRESSAO_LIQUIDO_001 { get; set; }
        [XmlElement("ICE_CRRCO_TEMPERATURA_LIQUIDO")]
        public decimal? ICE_CRRCO_TEMPERATURA_LIQUIDO_001 { get; set; }
        [XmlElement("MED_PRESSAO_ESTATICA")]
        public decimal? MED_PRESSAO_ESTATICA_001 { get; set; }
        [XmlElement("MED_TMPTA_FLUIDO")]
        public decimal? MED_TMPTA_FLUIDO_001 { get; set; }
        [XmlElement("MED_VOLUME_BRTO_CRRGO_MVMDO")]
        public decimal? MED_VOLUME_BRTO_CRRGO_MVMDO_001 { get; set; }
        [XmlElement("MED_VOLUME_BRUTO_MVMDO")]
        public decimal? MED_VOLUME_BRUTO_MVMDO_001 { get; set; }
        [XmlElement("MED_VOLUME_LIQUIDO_MVMDO")]
        public decimal? MED_VOLUME_LIQUIDO_MVMDO_001 { get; set; }
        [XmlElement("MED_VOLUME_TTLZO_FIM_PRDO")]
        public decimal? MED_VOLUME_TTLZO_FIM_PRDO_001 { get; set; }
        [XmlElement("MED_VOLUME_TTLZO_INCO_PRDO")]
        public decimal? MED_VOLUME_TTLZO_INCO_PRDO_001 { get; set; }
    }
}
