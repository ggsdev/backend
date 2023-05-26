using System.Xml.Serialization;

namespace PRIO.Files.XML._001
{
    [XmlRoot("a001")]
    public class _001PMO
    {
        [XmlElement("LISTA_DADOS_BASICOS")]
        public LISTA_DADOS_BASICOS_001? LISTA_DADOS_BASICOS_001 { get; set; }
    }

    [XmlRoot("LISTA_DADOS_BASICOS")]
    public class LISTA_DADOS_BASICOS_001
    {
        [XmlElement("DADOS_BASICOS")]
        public DADOS_BASICOS_001? DADOS_BASICOS_001 { get; set; }
    }

    [XmlRoot("DADOS_BASICOS")]
    public class DADOS_BASICOS_001
    {
        [XmlAttribute("NUM_SERIE_ELEMENTO_PRIMARIO")]
        public string? NUM_SERIE_ELEMENTO_PRIMARIO_001 { get; set; }

        [XmlAttribute("COD_INSTALACAO")]
        public string? COD_INSTALACAO_001 { get; set; }

        [XmlAttribute("COD_TAG_PONTO_MEDICAO")]
        public string? COD_TAG_PONTO_MEDICAO_001 { get; set; }

        [XmlElement("LISTA_CONFIGURACAO_CV")]
        public LISTA_CONFIGURACAO_CV_001? LISTA_CONFIGURACAO_CV_001 { get; set; }

        [XmlElement("LISTA_ELEMENTO_PRIMARIO")]
        public LISTA_ELEMENTO_PRIMARIO_001? LISTA_ELEMENTO_PRIMARIO_001 { get; set; }

        [XmlElement("LISTA_INSTRUMENTO_PRESSAO")]
        public LISTA_INSTRUMENTO_PRESSAO_001? LISTA_INSTRUMENTO_PRESSAO_001 { get; set; }

        [XmlElement("LISTA_INSTRUMENTO_TEMPERATURA")]
        public LISTA_INSTRUMENTO_TEMPERATURA_001? LISTA_INSTRUMENTO_TEMPERATURA_001 { get; set; }

        [XmlElement("LISTA_PRODUCAO")]
        public LISTA_PRODUCAO_001? LISTA_PRODUCAO_001 { get; set; }
    }

    [XmlRoot("LISTA_CONFIGURACAO_CV")]
    public class LISTA_CONFIGURACAO_CV_001
    {
        public CONFIGURACAO_CV_001? CONFIGURACAO_CV_001 { get; set; }
    }

    [XmlRoot("LISTA_ELEMENTO_PRIMARIO")]
    public class LISTA_ELEMENTO_PRIMARIO_001
    {
        public ELEMENTO_PRIMARIO_001? ELEMENTO_PRIMARIO_001 { get; set; }
    }

    [XmlRoot("LISTA_INSTRUMENTO_PRESSAO")]
    public class LISTA_INSTRUMENTO_PRESSAO_001
    {
        public INSTRUMENTO_PRESSAO_001? INSTRUMENTO_PRESSAO_001 { get; set; }
    }

    [XmlRoot("LISTA_INSTRUMENTO_TEMPERATURA")]
    public class LISTA_INSTRUMENTO_TEMPERATURA_001
    {
        public INSTRUMENTO_TEMPERATURA_001? INSTRUMENTO_TEMPERATURA_001 { get; set; }
    }

    [XmlRoot("LISTA_PRODUCAO")]
    public class LISTA_PRODUCAO_001
    {
        public PRODUCAO_001? PRODUCAO_001 { get; set; }
    }


    [XmlRoot("CONFIGURACAO_CV")]
    public class CONFIGURACAO_CV_001
    {
        [XmlElement("NUM_SERIE_COMPUTADOR_VAZAO")]
        public string? NUM_SERIE_COMPUTADOR_VAZAO_001 { get; set; }
        [XmlElement("DHA_COLETA")]
        public string? DHA_COLETA_001 { get; set; }
        [XmlElement("MED_TEMPERATURA")]
        public string? MED_TEMPERATURA_001 { get; set; }
        [XmlElement("MED_PRESSAO_ATMSA")]
        public string? MED_PRESSAO_ATMSA_001 { get; set; }
        [XmlElement("MED_PRESSAO_RFRNA")]
        public string? MED_PRESSAO_RFRNA_001 { get; set; }
        [XmlElement("MED_DENSIDADE_RELATIVA")]
        public string? MED_DENSIDADE_RELATIVA_001 { get; set; }
        [XmlElement("DSC_VERSAO_SOFTWARE")]
        public string? DSC_VERSAO_SOFTWARE_001 { get; set; }
    }

    [XmlRoot("ELEMENTO_PRIMARIO")]
    public class ELEMENTO_PRIMARIO_001
    {
        [XmlElement("ICE_METER_FACTOR_1")]
        public string? ICE_METER_FACTOR_1_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_2")]
        public string? ICE_METER_FACTOR_2_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_3")]
        public string? ICE_METER_FACTOR_3_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_4")]
        public string? ICE_METER_FACTOR_4_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_5")]
        public string? ICE_METER_FACTOR_5_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_6")]
        public string? ICE_METER_FACTOR_6_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_7")]
        public string? ICE_METER_FACTOR_7_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_8")]
        public string? ICE_METER_FACTOR_8_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_9")]
        public string? ICE_METER_FACTOR_9_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_10")]
        public string? ICE_METER_FACTOR_10_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_11")]
        public string? ICE_METER_FACTOR_11_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_12")]
        public string? ICE_METER_FACTOR_12_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_13")]
        public string? ICE_METER_FACTOR_13_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_14")]
        public string? ICE_METER_FACTOR_14_001 { get; set; }

        [XmlElement("ICE_METER_FACTOR_15")]
        public string? ICE_METER_FACTOR_15_001 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_1")]
        public string? QTD_PULSOS_METER_FACTOR_1_001 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_2")]
        public string? QTD_PULSOS_METER_FACTOR_2_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_3")]
        public string? QTD_PULSOS_METER_FACTOR_3_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_4")]
        public string? QTD_PULSOS_METER_FACTOR_4_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_5")]
        public string? QTD_PULSOS_METER_FACTOR_5_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_6")]
        public string? QTD_PULSOS_METER_FACTOR_6_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_7")]
        public string? QTD_PULSOS_METER_FACTOR_7_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_8")]
        public string? QTD_PULSOS_METER_FACTOR_8_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_9")]
        public string? QTD_PULSOS_METER_FACTOR_9_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_10")]
        public string? QTD_PULSOS_METER_FACTOR_10_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_11")]
        public string? QTD_PULSOS_METER_FACTOR_11_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_12")]
        public string? QTD_PULSOS_METER_FACTOR_12_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_13")]
        public string? QTD_PULSOS_METER_FACTOR_13_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_14")]
        public string? QTD_PULSOS_METER_FACTOR_14_001 { get; set; }
        [XmlElement("QTD_PULSOS_METER_FACTOR_15")]
        public string? QTD_PULSOS_METER_FACTOR_15_001 { get; set; }
        [XmlElement("ICE_K_FACTOR_1")]
        public string? ICE_K_FACTOR_1_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_2")]
        public string? ICE_K_FACTOR_2_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_3")]
        public string? ICE_K_FACTOR_3_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_4")]
        public string? ICE_K_FACTOR_4_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_5")]
        public string? ICE_K_FACTOR_5_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_6")]
        public string? ICE_K_FACTOR_6_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_7")]
        public string? ICE_K_FACTOR_7_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_8")]
        public string? ICE_K_FACTOR_8_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_9")]
        public string? ICE_K_FACTOR_9_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_10")]
        public string? ICE_K_FACTOR_10_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_11")]
        public string? ICE_K_FACTOR_11_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_12")]
        public string? ICE_K_FACTOR_12_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_13")]
        public string? ICE_K_FACTOR_13_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_14")]
        public string? ICE_K_FACTOR_14_001 { get; set; }

        [XmlElement("ICE_K_FACTOR_15")]
        public string? ICE_K_FACTOR_15_001 { get; set; }

        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_1_001 { get; set; }
        [XmlElement("QTD_PULSOS_K_FACTOR_1")]
        public string? QTD_PULSOS_K_FACTOR_1_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_2")]
        public string? QTD_PULSOS_K_FACTOR_2_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_3")]
        public string? QTD_PULSOS_K_FACTOR_3_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_4")]
        public string? QTD_PULSOS_K_FACTOR_4_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_5")]
        public string? QTD_PULSOS_K_FACTOR_5_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_6")]
        public string? QTD_PULSOS_K_FACTOR_6_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_7")]
        public string? QTD_PULSOS_K_FACTOR_7_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_8")]
        public string? QTD_PULSOS_K_FACTOR_8_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_9")]
        public string? QTD_PULSOS_K_FACTOR_9_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_10")]
        public string? QTD_PULSOS_K_FACTOR_10_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_11")]
        public string? QTD_PULSOS_K_FACTOR_11_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_12")]
        public string? QTD_PULSOS_K_FACTOR_12_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_13")]
        public string? QTD_PULSOS_K_FACTOR_13_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_14")]
        public string? QTD_PULSOS_K_FACTOR_14_001 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_15")]
        public string? QTD_PULSOS_K_FACTOR_15_001 { get; set; }

        [XmlElement("ICE_CUTOFF")]
        public string? ICE_CUTOFF_001 { get; set; }

        [XmlElement("ICE_LIMITE_SPRR_ALARME")]
        public string? ICE_LIMITE_SPRR_ALARME_001 { get; set; }

        [XmlElement("ICE_LIMITE_INFRR_ALARME")]
        public string? ICE_LIMITE_INFRR_ALARME_001 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_PRESSAO")]
    public class INSTRUMENTO_PRESSAO_001
    {

        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_1_001 { get; set; }
        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public string? MED_PRSO_LIMITE_SPRR_ALRME_001 { get; set; }

        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public string? MED_PRSO_LMTE_INFRR_ALRME_001 { get; set; }

        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_2_001 { get; set; }
        [XmlElement("MED_PRSO_ADOTADA_FALHA")]
        public string? MED_PRSO_ADOTADA_FALHA_001 { get; set; }

        [XmlElement("DSC_ESTADO_INSNO_CASO_FALHA")]
        public string? DSC_ESTADO_INSNO_CASO_FALHA_001 { get; set; }

        [XmlElement("IND_TIPO_PRESSAO_CONSIDERADA")]
        public string? IND_TIPO_PRESSAO_CONSIDERADA_001 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_TEMPERATURA")]
    public class INSTRUMENTO_TEMPERATURA_001
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_2_001 { get; set; }
        [XmlElement("MED_TMPTA_SPRR_ALARME")]
        public string? MED_TMPTA_SPRR_ALARME_001 { get; set; }
        [XmlElement("MED_TMPTA_INFRR_ALRME")]
        public string? MED_TMPTA_INFRR_ALRME_001 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_3_001 { get; set; }
        [XmlElement("MED_TMPTA_ADTTA_FALHA")]
        public string? MED_TMPTA_ADTTA_FALHA_001 { get; set; }
        [XmlElement("DSC_ESTADO_INSTRUMENTO_FALHA")]
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_1_001 { get; set; }

    }

    [XmlRoot("PRODUCAO")]
    public class PRODUCAO_001
    {
        [XmlElement("DHA_INICIO_PERIODO_MEDICAO")]
        public string? DHA_INICIO_PERIODO_MEDICAO_001 { get; set; }
        [XmlElement("DHA_FIM_PERIODO_MEDICAO")]
        public string? DHA_FIM_PERIODO_MEDICAO_001 { get; set; }
        [XmlElement("ICE_DENSIDADADE_RELATIVA")]
        public string? ICE_DENSIDADADE_RELATIVA_001 { get; set; }
        [XmlElement("ICE_CORRECAO_BSW")]
        public string? ICE_CORRECAO_BSW_001 { get; set; }
        [XmlElement("ICE_CORRECAO_PRESSAO_LIQUIDO")]
        public string? ICE_CORRECAO_PRESSAO_LIQUIDO_001 { get; set; }
        [XmlElement("ICE_CRRCO_TEMPERATURA_LIQUIDO")]
        public string? ICE_CRRCO_TEMPERATURA_LIQUIDO_001 { get; set; }
        [XmlElement("MED_PRESSAO_ESTATICA")]
        public string? MED_PRESSAO_ESTATICA_001 { get; set; }
        [XmlElement("MED_TMPTA_FLUIDO")]
        public string? MED_TMPTA_FLUIDO_001 { get; set; }
        [XmlElement("MED_VOLUME_BRTO_CRRGO_MVMDO")]
        public string? MED_VOLUME_BRTO_CRRGO_MVMDO_001 { get; set; }
        [XmlElement("MED_VOLUME_BRUTO_MVMDO")]
        public string? MED_VOLUME_BRUTO_MVMDO_001 { get; set; }
        [XmlElement("MED_VOLUME_LIQUIDO_MVMDO")]
        public string? MED_VOLUME_LIQUIDO_MVMDO_001 { get; set; }
        [XmlElement("MED_VOLUME_TTLZO_FIM_PRDO")]
        public string? MED_VOLUME_TTLZO_FIM_PRDO_001 { get; set; }
        [XmlElement("MED_VOLUME_TTLZO_INCO_PRDO")]
        public string? MED_VOLUME_TTLZO_INCO_PRDO_001 { get; set; }
    }
}
