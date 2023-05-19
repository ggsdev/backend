using System.Xml.Serialization;

namespace PRIO.Files._002
{
    [XmlRoot("a002")]
    public class _002PMGL
    {
        [XmlArray("LISTA_DADOS_BASICOS")]
        [XmlArrayItem("DADOS_BASICOS")]
        public List<DADOS_BASICOS> LISTA_DADOS_BASICOS { get; set; }
    }

    [XmlRoot("DADOS_BASICOS")]
    public class DADOS_BASICOS
    {
        [XmlAttribute("NUM_SERIE_ELEMENTO_PRIMARIO")]
        public string? NUM_SERIE_ELEMENTO_PRIMARIO_002 { get; set; }

        [XmlAttribute("COD_INSTALACAO")]
        public int? COD_INSTALACAO_002 { get; set; }

        [XmlAttribute("COD_TAG_PONTO_MEDICAO")]
        public string? COD_TAG_PONTO_MEDICAO_002 { get; set; }

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
        public string? NUM_SERIE_COMPUTADOR_VAZAO_002 { get; set; }

        [XmlElement("DHA_COLETA")]
        public decimal? DHA_COLETA_002 { get; set; }

        [XmlElement("MED_TEMPERATURA")]
        public decimal? MED_TEMPERATURA_1_002 { get; set; }

        [XmlElement("MED_PRESSAO_ATMSA")]
        public decimal? MED_PRESSAO_ATMSA_002 { get; set; }

        [XmlElement("MED_PRESSAO_RFRNA")]
        public decimal? MED_PRESSAO_RFRNA_002 { get; set; }

        [XmlElement("MED_DENSIDADE_RELATIVA")]
        public decimal? MED_DENSIDADE_RELATIVA_002 { get; set; }

        [XmlElement("DSC_NORMA_UTILIZADA_CALCULO")]
        public string? DSC_NORMA_UTILIZADA_CALCULO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_NITROGENIO")]
        public decimal? PCT_CROMATOGRAFIA_NITROGENIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_CO2")]
        public decimal? PCT_CROMATOGRAFIA_CO2_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_METANO")]
        public decimal? PCT_CROMATOGRAFIA_METANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_ETANO")]
        public decimal? PCT_CROMATOGRAFIA_ETANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_PROPANO")]
        public decimal? PCT_CROMATOGRAFIA_PROPANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_N_BUTANO")]
        public decimal? PCT_CROMATOGRAFIA_N_BUTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_I_BUTANO")]
        public decimal? PCT_CROMATOGRAFIA_I_BUTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_N_PENTANO")]
        public decimal? PCT_CROMATOGRAFIA_N_PENTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_I_PENTANO")]
        public decimal? PCT_CROMATOGRAFIA_I_PENTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HEXANO")]
        public decimal? PCT_CROMATOGRAFIA_HEXANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HEPTANO")]
        public decimal? PCT_CROMATOGRAFIA_HEPTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_OCTANO")]
        public decimal? PCT_CROMATOGRAFIA_OCTANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_NONANO")]
        public decimal? PCT_CROMATOGRAFIA_NONANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_DECANO")]
        public decimal? PCT_CROMATOGRAFIA_DECANO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_H2S")]
        public decimal? PCT_CROMATOGRAFIA_H2S_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_AGUA")]
        public decimal? PCT_CROMATOGRAFIA_AGUA_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HELIO")]
        public decimal? PCT_CROMATOGRAFIA_HELIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_OXIGENIO")]
        public decimal? PCT_CROMATOGRAFIA_OXIGENIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_CO")]
        public decimal? PCT_CROMATOGRAFIA_CO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HIDROGENIO")]
        public decimal? PCT_CROMATOGRAFIA_HIDROGENIO_002 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_ARGONIO")]
        public decimal? PCT_CROMATOGRAFIA_ARGONIO_002 { get; set; }

        [XmlElement("DSC_VERSAO_SOFTWARE")]
        public string? DSC_VERSAO_SOFTWARE_002 { get; set; }
    }

    [XmlRoot("ELEMENTO_PRIMARIO")]
    public class ELEMENTO_PRIMARIO
    {
        [XmlElement("ICE_METER_FACTOR_1")]
        public decimal? ICE_METER_FACTOR_1_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_2")]
        public decimal? ICE_METER_FACTOR_2_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_3")]
        public decimal? ICE_METER_FACTOR_3_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_4")]
        public decimal? ICE_METER_FACTOR_4_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_5")]
        public decimal? ICE_METER_FACTOR_5_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_6")]
        public decimal? ICE_METER_FACTOR_6_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_7")]
        public decimal? ICE_METER_FACTOR_7_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_8")]
        public decimal? ICE_METER_FACTOR_8_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_9")]
        public decimal? ICE_METER_FACTOR_9_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_10")]
        public decimal? ICE_METER_FACTOR_10_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_11")]
        public decimal? ICE_METER_FACTOR_11_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_12")]
        public decimal? ICE_METER_FACTOR_12_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_13")]
        public decimal? ICE_METER_FACTOR_13_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_14")]
        public decimal? ICE_METER_FACTOR_14_002 { get; set; }

        [XmlElement("ICE_METER_FACTOR_15")]
        public decimal? ICE_METER_FACTOR_15_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_1")]
        public decimal? QTD_PULSOS_METER_FACTOR_1_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_2")]
        public decimal? QTD_PULSOS_METER_FACTOR_2_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_3")]
        public decimal? QTD_PULSOS_METER_FACTOR_3_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_4")]
        public decimal? QTD_PULSOS_METER_FACTOR_4_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_5")]
        public decimal? QTD_PULSOS_METER_FACTOR_5_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_6")]
        public decimal? QTD_PULSOS_METER_FACTOR_6_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_7")]
        public decimal? QTD_PULSOS_METER_FACTOR_7_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_8")]
        public decimal? QTD_PULSOS_METER_FACTOR_8_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_9")]
        public decimal? QTD_PULSOS_METER_FACTOR_9_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_10")]
        public decimal? QTD_PULSOS_METER_FACTOR_10_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_11")]
        public decimal? QTD_PULSOS_METER_FACTOR_11_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_12")]
        public decimal? QTD_PULSOS_METER_FACTOR_12_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_13")]
        public decimal? QTD_PULSOS_METER_FACTOR_13_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_14")]
        public decimal? QTD_PULSOS_METER_FACTOR_14_002 { get; set; }

        [XmlElement("QTD_PULSOS_METER_FACTOR_15")]
        public decimal? QTD_PULSOS_METER_FACTOR_15_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_1")]
        public decimal? ICE_K_FACTOR_1_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_2")]
        public decimal? ICE_K_FACTOR_2_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_3")]
        public decimal? ICE_K_FACTOR_3_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_4")]
        public decimal? ICE_K_FACTOR_4_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_5")]
        public decimal? ICE_K_FACTOR_5_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_6")]
        public decimal? ICE_K_FACTOR_6_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_7")]
        public decimal? ICE_K_FACTOR_7_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_8")]
        public decimal? ICE_K_FACTOR_8_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_9")]
        public decimal? ICE_K_FACTOR_9_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_10")]
        public decimal? ICE_K_FACTOR_10_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_11")]
        public decimal? ICE_K_FACTOR_11_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_12")]
        public decimal? ICE_K_FACTOR_12_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_13")]
        public decimal? ICE_K_FACTOR_13_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_14")]
        public decimal? ICE_K_FACTOR_14_002 { get; set; }

        [XmlElement("ICE_K_FACTOR_15")]
        public decimal? ICE_K_FACTOR_15_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_1")]
        public decimal? QTD_PULSOS_K_FACTOR_1_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_2")]
        public decimal? QTD_PULSOS_K_FACTOR_2_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_3")]
        public decimal? QTD_PULSOS_K_FACTOR_3_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_4")]
        public decimal? QTD_PULSOS_K_FACTOR_4_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_5")]
        public decimal? QTD_PULSOS_K_FACTOR_5_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_6")]
        public decimal? QTD_PULSOS_K_FACTOR_6_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_7")]
        public decimal? QTD_PULSOS_K_FACTOR_7_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_8")]
        public decimal? QTD_PULSOS_K_FACTOR_8_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_9")]
        public decimal? QTD_PULSOS_K_FACTOR_9_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_10")]
        public decimal? QTD_PULSOS_K_FACTOR_10_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_11")]
        public decimal? QTD_PULSOS_K_FACTOR_11_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_12")]
        public decimal? QTD_PULSOS_K_FACTOR_12_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_13")]
        public decimal? QTD_PULSOS_K_FACTOR_13_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_14")]
        public decimal? QTD_PULSOS_K_FACTOR_14_002 { get; set; }

        [XmlElement("QTD_PULSOS_K_FACTOR_15")]
        public decimal? QTD_PULSOS_K_FACTOR_15_002 { get; set; }

        [XmlElement("ICE_CUTOFF")]
        public decimal? ICE_CUTOFF_002 { get; set; }
        [XmlElement("ICE_LIMITE_SPRR_ALARME")]
        public decimal? ICE_LIMITE_SPRR_ALARME_002 { get; set; }
        [XmlElement("ICE_LIMITE_INFRR_ALARME")]
        public decimal? ICE_LIMITE_INFRR_ALARME_002 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public bool? IND_HABILITACAO_ALARME_1_002 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_PRESSAO")]
    public class INSTRUMENTO_PRESSAO
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_1_002 { get; set; }
        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_002 { get; set; }
        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_002 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public bool? IND_HABILITACAO_ALARME_2_002 { get; set; }
        [XmlElement("MED_PRSO_ADOTADA_FALHA")]
        public decimal? MED_PRSO_ADOTADA_FALHA_002 { get; set; }
        [XmlElement("DSC_ESTADO_INSNO_CASO_FALHA")]
        public string? DSC_ESTADO_INSNO_CASO_FALHA_002 { get; set; }
        [XmlElement("IND_TIPO_PRESSAO_CONSIDERADA")]
        public string? IND_TIPO_PRESSAO_CONSIDERADA_002 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_TEMPERATURA")]
    public class INSTRUMENTO_TEMPERATURA
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_2_002 { get; set; }
        [XmlElement("MED_TMPTA_SPRR_ALARME")]
        public decimal? MED_TMPTA_SPRR_ALARME_002 { get; set; }
        [XmlElement("MED_TMPTA_INFRR_ALRME")]

        public decimal? MED_TMPTA_INFRR_ALRME_002 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]

        public bool? IND_HABILITACAO_ALARME_3_002 { get; set; }
        [XmlElement("MED_TMPTA_ADTTA_FALHA")]

        public decimal? MED_TMPTA_ADTTA_FALHA_002 { get; set; }
        [XmlElement("DSC_ESTADO_INSTRUMENTO_FALHA")]
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_002 { get; set; }
    }

    [XmlRoot("PRODUCAO")]
    public class PRODUCAO
    {

        [XmlElement("DHA_INICIO_PERIODO_MEDICAO")]
        public DateTime? DHA_INICIO_PERIODO_MEDICAO_002 { get; set; }

        [XmlElement("DHA_FIM_PERIODO_MEDICAO")]
        public DateTime? DHA_FIM_PERIODO_MEDICAO_002 { get; set; }

        [XmlElement("ICE_DENSIDADE_RELATIVA")]
        public decimal? ICE_DENSIDADE_RELATIVA_002 { get; set; }

        [XmlElement("MED_PRESSAO_ESTATICA")]
        public decimal? MED_PRESSAO_ESTATICA_002 { get; set; }

        [XmlElement("MED_TEMPERATURA")]
        public decimal? MED_TEMPERATURA_2_002 { get; set; }

        [XmlElement("PRZ_DURACAO_FLUXO_EFETIVO")]
        public decimal? PRZ_DURACAO_FLUXO_EFETIVO_002 { get; set; }

        [XmlElement("MED_BRUTO_MOVIMENTADO")]
        public decimal? MED_BRUTO_MOVIMENTADO_002 { get; set; }

        [XmlElement("MED_CORRIGIDO_MVMDO")]
        public decimal? MED_CORRIGIDO_MVMDO_002 { get; set; }
    }
}
