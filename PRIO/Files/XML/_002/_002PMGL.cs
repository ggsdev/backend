using System.Xml.Serialization;

namespace PRIO.Files.XML._002
{
    [XmlRoot("a002")]
    public class _002PMGL
    {
        [XmlArray("LISTA_DADOS_BASICOS")]
        [XmlArrayItem("DADOS_BASICOS")]
        public List<DADOS_BASICOS_002> LISTA_DADOS_BASICOS { get; set; }
    }

    [XmlRoot("LISTA_CONFIGURACAO_CV")]
    public class LISTA_CONFIGURACAO_CV_002
    {
        [XmlElement("CONFIGURACAO_CV")]
        public CONFIGURACAO_CV_002 CONFIGURACAO_CV_002 { get; set; }
    }

    [XmlRoot("LISTA_ELEMENTO_PRIMARIO")]
    public class LISTA_ELEMENTO_PRIMARIO_002
    {
        [XmlElement("ELEMENTO_PRIMARIO")]
        public ELEMENTO_PRIMARIO_002 ELEMENTO_PRIMARIO_002 { get; set; }
    }

    [XmlRoot("LISTA_INSTRUMENTO_PRESSAO")]
    public class LISTA_INSTRUMENTO_PRESSAO_002
    {
        public INSTRUMENTO_PRESSAO_002 INSTRUMENTO_PRESSAO_002 { get; set; }
    }
    [XmlRoot("LISTA_INSTRUMENTO_TEMPERATURA")]
    public class LISTA_INSTRUMENTO_TEMPERATURA_002
    {
        [XmlElement("INSTRUMENTO_TEMPERATURA")]
        public INSTRUMENTO_TEMPERATURA_002 INSTRUMENTO_TEMPERATURA_002 { get; set; }
    }
    [XmlRoot("LISTA_PRODUCAO")]
    public class LISTA_PRODUCAO_002
    {
        [XmlElement("PRODUCAO")]
        public PRODUCAO_002 PRODUCAO_002 { get; set; }
    }

    [XmlRoot("DADOS_BASICOS")]
    public class DADOS_BASICOS_002
    {
        [XmlAttribute("NUM_SERIE_ELEMENTO_PRIMARIO")]
        public string? NUM_SERIE_ELEMENTO_PRIMARIO_002 { get; set; }

        [XmlAttribute("COD_INSTALACAO")]
        public string? COD_INSTALACAO_002 { get; set; }

        [XmlAttribute("COD_TAG_PONTO_MEDICAO")]
        public string? COD_TAG_PONTO_MEDICAO_002 { get; set; }

        [XmlElement("LISTA_CONFIGURACAO_CV")]
        public LISTA_CONFIGURACAO_CV_002 LISTA_CONFIGURACAO_CV_002 { get; set; }

        [XmlElement("LISTA_ELEMENTO_PRIMARIO")]
        public LISTA_ELEMENTO_PRIMARIO_002 LISTA_ELEMENTO_PRIMARIO_002 { get; set; }

        [XmlElement("LISTA_INSTRUMENTO_PRESSAO")]
        public LISTA_INSTRUMENTO_PRESSAO_002 LISTA_INSTRUMENTO_PRESSAO_002 { get; set; }

        [XmlElement("LISTA_INSTRUMENTO_TEMPERATURA")]
        public LISTA_INSTRUMENTO_TEMPERATURA_002 LISTA_INSTRUMENTO_TEMPERATURA_002 { get; set; }

        [XmlElement("LISTA_PRODUCAO")]
        public LISTA_PRODUCAO_002 LISTA_PRODUCAO_002 { get; set; }
    }

    [XmlRoot("CONFIGURACAO_CV")]
    public class CONFIGURACAO_CV_002
    {
        [XmlElement("NUM_SERIE_COMPUTADOR_VAZAO")]
        public string? NUM_SERIE_COMPUTADOR_VAZAO_002 { get; set; }

        [XmlElement("DHA_COLETA")]
        public string? DHA_COLETA_002 { get; set; }

        [XmlElement("MED_TEMPERATURA")]
        public string? MED_TEMPERATURA_1_002 { get; set; }

        [XmlElement("MED_PRESSAO_ATMSA")]
        public string? MED_PRESSAO_ATMSA_002 { get; set; }

        [XmlElement("MED_PRESSAO_RFRNA")]
        public string? MED_PRESSAO_RFRNA_002 { get; set; }

        [XmlElement("MED_DENSIDADE_RELATIVA")]
        public string? MED_DENSIDADE_RELATIVA_002 { get; set; }

        [XmlElement("DSC_NORMA_UTILIZADA_CALCULO")]
        public string? DSC_NORMA_UTILIZADA_CALCULO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_NITROGENIO")]
        public string? PCT_CROMATOGRAFIA_NITROGENIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_CO2")]
        public string? PCT_CROMATOGRAFIA_CO2_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_METANO")]
        public string? PCT_CROMATOGRAFIA_METANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_ETANO")]
        public string? PCT_CROMATOGRAFIA_ETANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_PROPANO")]
        public string? PCT_CROMATOGRAFIA_PROPANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_N_BUTANO")]
        public string? PCT_CROMATOGRAFIA_N_BUTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_I_BUTANO")]
        public string? PCT_CROMATOGRAFIA_I_BUTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_N_PENTANO")]
        public string? PCT_CROMATOGRAFIA_N_PENTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_I_PENTANO")]
        public string? PCT_CROMATOGRAFIA_I_PENTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HEXANO")]
        public string? PCT_CROMATOGRAFIA_HEXANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HEPTANO")]
        public string? PCT_CROMATOGRAFIA_HEPTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_OCTANO")]
        public string? PCT_CROMATOGRAFIA_OCTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_NONANO")]
        public string? PCT_CROMATOGRAFIA_NONANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_DECANO")]
        public string? PCT_CROMATOGRAFIA_DECANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_H2S")]
        public string? PCT_CROMATOGRAFIA_H2S_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_AGUA")]
        public string? PCT_CROMATOGRAFIA_AGUA_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HELIO")]
        public string? PCT_CROMATOGRAFIA_HELIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_OXIGENIO")]
        public string? PCT_CROMATOGRAFIA_OXIGENIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_CO")]
        public string? PCT_CROMATOGRAFIA_CO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HIDROGENIO")]
        public string? PCT_CROMATOGRAFIA_HIDROGENIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_ARGONIO")]
        public string? PCT_CROMATOGRAFIA_ARGONIO_002 { get; set; }

        [XmlElement("DSC_VERSAO_SOFTWARE")]
        public string? DSC_VERSAO_SOFTWARE_002 { get; set; }
    }

    [XmlRoot("ELEMENTO_PRIMARIO")]
    public class ELEMENTO_PRIMARIO_002
    {
        [XmlElement("ICE_METER_FACTOR_1")]
        public string? ICE_METER_FACTOR_1_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_2")]
        public string? ICE_METER_FACTOR_2_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_3")]
        public string? ICE_METER_FACTOR_3_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_4")]
        public string? ICE_METER_FACTOR_4_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_5")]
        public string? ICE_METER_FACTOR_5_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_6")]
        public string? ICE_METER_FACTOR_6_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_7")]
        public string? ICE_METER_FACTOR_7_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_8")]
        public string? ICE_METER_FACTOR_8_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_9")]
        public string? ICE_METER_FACTOR_9_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_10")]
        public string? ICE_METER_FACTOR_10_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_11")]
        public string? ICE_METER_FACTOR_11_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_12")]
        public string? ICE_METER_FACTOR_12_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_13")]
        public string? ICE_METER_FACTOR_13_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_14")]
        public string? ICE_METER_FACTOR_14_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_15")]
        public string? ICE_METER_FACTOR_15_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_1")]
        public string? QTD_PULSOS_METER_FACTOR_1_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_2")]
        public string? QTD_PULSOS_METER_FACTOR_2_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_3")]
        public string? QTD_PULSOS_METER_FACTOR_3_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_4")]
        public string? QTD_PULSOS_METER_FACTOR_4_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_5")]
        public string? QTD_PULSOS_METER_FACTOR_5_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_6")]
        public string? QTD_PULSOS_METER_FACTOR_6_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_7")]
        public string? QTD_PULSOS_METER_FACTOR_7_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_8")]
        public string? QTD_PULSOS_METER_FACTOR_8_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_9")]
        public string? QTD_PULSOS_METER_FACTOR_9_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_10")]
        public string? QTD_PULSOS_METER_FACTOR_10_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_11")]
        public string? QTD_PULSOS_METER_FACTOR_11_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_12")]
        public string? QTD_PULSOS_METER_FACTOR_12_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_13")]
        public string? QTD_PULSOS_METER_FACTOR_13_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_14")]
        public string? QTD_PULSOS_METER_FACTOR_14_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_15")]
        public string? QTD_PULSOS_METER_FACTOR_15_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_1")]
        public string? ICE_K_FACTOR_1_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_2")]
        public string? ICE_K_FACTOR_2_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_3")]
        public string? ICE_K_FACTOR_3_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_4")]
        public string? ICE_K_FACTOR_4_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_5")]
        public string? ICE_K_FACTOR_5_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_6")]
        public string? ICE_K_FACTOR_6_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_7")]
        public string? ICE_K_FACTOR_7_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_8")]
        public string? ICE_K_FACTOR_8_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_9")]
        public string? ICE_K_FACTOR_9_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_10")]
        public string? ICE_K_FACTOR_10_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_11")]
        public string? ICE_K_FACTOR_11_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_12")]
        public string? ICE_K_FACTOR_12_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_13")]
        public string? ICE_K_FACTOR_13_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_14")]
        public string? ICE_K_FACTOR_14_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_15")]
        public string? ICE_K_FACTOR_15_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_1")]
        public string? QTD_PULSOS_K_FACTOR_1_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_2")]
        public string? QTD_PULSOS_K_FACTOR_2_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_3")]
        public string? QTD_PULSOS_K_FACTOR_3_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_4")]
        public string? QTD_PULSOS_K_FACTOR_4_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_5")]
        public string? QTD_PULSOS_K_FACTOR_5_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_6")]
        public string? QTD_PULSOS_K_FACTOR_6_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_7")]
        public string? QTD_PULSOS_K_FACTOR_7_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_8")]
        public string? QTD_PULSOS_K_FACTOR_8_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_9")]
        public string? QTD_PULSOS_K_FACTOR_9_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_10")]
        public string? QTD_PULSOS_K_FACTOR_10_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_11")]
        public string? QTD_PULSOS_K_FACTOR_11_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_12")]
        public string? QTD_PULSOS_K_FACTOR_12_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_13")]
        public string? QTD_PULSOS_K_FACTOR_13_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_14")]
        public string? QTD_PULSOS_K_FACTOR_14_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_15")]
        public string? QTD_PULSOS_K_FACTOR_15_002 { get; set; }

        [XmlElement("ICE_CUTOFF")]
        public string? ICE_CUTOFF_002 { get; set; }
        [XmlElement("ICE_LIMITE_SPRR_ALARME")]
        public string? ICE_LIMITE_SPRR_ALARME_002 { get; set; }
        [XmlElement("ICE_LIMITE_INFRR_ALARME")]
        public string? ICE_LIMITE_INFRR_ALARME_002 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_1_002 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_PRESSAO")]
    public class INSTRUMENTO_PRESSAO_002
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_1_002 { get; set; }
        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public string? MED_PRSO_LIMITE_SPRR_ALRME_002 { get; set; }
        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public string? MED_PRSO_LMTE_INFRR_ALRME_002 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_2_002 { get; set; }
        [XmlElement("MED_PRSO_ADOTADA_FALHA")]
        public string? MED_PRSO_ADOTADA_FALHA_002 { get; set; }
        [XmlElement("DSC_ESTADO_INSNO_CASO_FALHA")]
        public string? DSC_ESTADO_INSNO_CASO_FALHA_002 { get; set; }
        [XmlElement("IND_TIPO_PRESSAO_CONSIDERADA")]
        public string? IND_TIPO_PRESSAO_CONSIDERADA_002 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_TEMPERATURA")]
    public class INSTRUMENTO_TEMPERATURA_002
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_2_002 { get; set; }
        [XmlElement("MED_TMPTA_SPRR_ALARME")]
        public string? MED_TMPTA_SPRR_ALARME_002 { get; set; }
        [XmlElement("MED_TMPTA_INFRR_ALRME")]

        public string? MED_TMPTA_INFRR_ALRME_002 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]

        public string? IND_HABILITACAO_ALARME_3_002 { get; set; }
        [XmlElement("MED_TMPTA_ADTTA_FALHA")]

        public string? MED_TMPTA_ADTTA_FALHA_002 { get; set; }
        [XmlElement("DSC_ESTADO_INSTRUMENTO_FALHA")]
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_002 { get; set; }
    }

    [XmlRoot("PRODUCAO")]
    public class PRODUCAO_002
    {

        [XmlElement("DHA_INICIO_PERIODO_MEDICAO")]
        public string? DHA_INICIO_PERIODO_MEDICAO_002 { get; set; }

        [XmlElement("DHA_FIM_PERIODO_MEDICAO")]
        public string? DHA_FIM_PERIODO_MEDICAO_002 { get; set; }

        [XmlElement("ICE_DENSIDADE_RELATIVA")]
        public string? ICE_DENSIDADE_RELATIVA_002 { get; set; }

        [XmlElement("MED_PRESSAO_ESTATICA")]
        public string? MED_PRESSAO_ESTATICA_002 { get; set; }

        [XmlElement("MED_TEMPERATURA")]
        public string? MED_TEMPERATURA_2_002 { get; set; }

        [XmlElement("PRZ_DURACAO_FLUXO_EFETIVO")]
        public string? PRZ_DURACAO_FLUXO_EFETIVO_002 { get; set; }

        [XmlElement("MED_BRUTO_MOVIMENTADO")]
        public string? MED_BRUTO_MOVIMENTADO_002 { get; set; }

        [XmlElement("MED_CORRIGIDO_MVMDO")]
        public string? MED_CORRIGIDO_MVMDO_002 { get; set; }
    }
}
