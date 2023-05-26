using System.Xml.Serialization;

namespace PRIO.Files.XML._003
{
    [XmlRoot("a003")]
    public class _003PMGD
    {
        public List<DADOS_BASICOS_003> LISTA_DADOS_BASICOS { get; set; }
    }

    [XmlRoot("DADOS_BASICOS")]
    public class DADOS_BASICOS_003
    {
        [XmlAttribute("NUM_SERIE_ELEMENTO_PRIMARIO")]
        public string? NUM_SERIE_ELEMENTO_PRIMARIO_003 { get; set; }
        [XmlAttribute("COD_INSTALACAO")]
        public string? COD_INSTALACAO_003 { get; set; }
        [XmlAttribute("COD_TAG_PONTO_MEDICAO")]
        public string COD_TAG_PONTO_MEDICAO_003 { get; set; }

        [XmlArray("LISTA_CONFIGURACAO_CV")]
        [XmlArrayItem("CONFIGURACAO_CV")]
        public List<CONFIGURACAO_CV_003> LISTA_CONFIGURACAO_CV { get; set; }

        [XmlArray("LISTA_ELEMENTO_PRIMARIO")]
        [XmlArrayItem("ELEMENTO_PRIMARIO")]
        public List<ELEMENTO_PRIMARIO_003> LISTA_ELEMENTO_PRIMARIO { get; set; }

        [XmlArray("LISTA_INSTRUMENTO_PRESSAO")]
        [XmlArrayItem("INSTRUMENTO_PRESSAO")]
        public List<INSTRUMENTO_PRESSAO_003> LISTA_INSTRUMENTO_PRESSAO { get; set; }

        [XmlArray("LISTA_INSTRUMENTO_TEMPERATURA")]
        [XmlArrayItem("INSTRUMENTO_TEMPERATURA")]
        public List<INSTRUMENTO_TEMPERATURA_003> LISTA_INSTRUMENTO_TEMPERATURA { get; set; }

        [XmlArray("LISTA_PLACA_ORIFICIO")]
        [XmlArrayItem("PLACA_ORIFICIO")]
        public List<PLACA_ORIFICIO_003> LISTA_PLACA_ORIFICIO { get; set; }

        [XmlArray("LISTA_INST_DIFEREN_PRESSAO_PRINCIPAL")]
        [XmlArrayItem("INST_DIFEREN_PRESSAO_PRINCIPAL")]
        public List<INST_DIFEREN_PRESSAO_PRINCIPAL_003> LISTA_INST_DIFEREN_PRESSAO_PRINCIPAL { get; set; }

        [XmlArray("LISTA_INST_DIFEREN_PRESSAO_ALTA")]
        [XmlArrayItem("INST_DIFEREN_PRESSAO_ALTA")]
        public List<INST_DIFEREN_PRESSAO_ALTA_003> LISTA_INST_DIFEREN_PRESSAO_ALTA { get; set; }

        [XmlArray("LISTA_INST_DIFEREN_PRESSAO_BAIXA")]
        [XmlArrayItem("INST_DIFEREN_PRESSAO_BAIXA")]
        public List<INST_DIFEREN_PRESSAO_BAIXA_003> LISTA_INST_DIFEREN_PRESSAO_BAIXA { get; set; }

        [XmlArray("LISTA_PRODUCAO")]
        [XmlArrayItem("PRODUCAO")]
        public List<PRODUCAO_003> LISTA_PRODUCAO { get; set; }

    }

    [XmlRoot("CONFIGURACAO_CV")]
    public class CONFIGURACAO_CV_003
    {
        [XmlElement("NUM_SERIE_COMPUTADOR_VAZAO")]
        public string? NUM_SERIE_COMPUTADOR_VAZAO_003 { get; set; }

        [XmlElement("DHA_COLETA")]
        public string? DHA_COLETA_003 { get; set; }

        [XmlElement("MED_TEMPERATURA")]
        public string? MED_TEMPERATURA_1_003 { get; set; }

        [XmlElement("MED_PRESSAO_ATMSA")]
        public string? MED_PRESSAO_ATMSA_003 { get; set; }

        [XmlElement("MED_PRESSAO_RFRNA")]
        public string? MED_PRESSAO_RFRNA_003 { get; set; }

        [XmlElement("MED_DENSIDADE_RELATIVA")]
        public string? MED_DENSIDADE_RELATIVA_003 { get; set; }

        [XmlElement("DSC_NORMA_UTILIZADA_CALCULO")]
        public string? DSC_NORMA_UTILIZADA_CALCULO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_NITROGENIO")]
        public string? PCT_CROMATOGRAFIA_NITROGENIO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_CO2")]
        public string? PCT_CROMATOGRAFIA_CO2_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_METANO")]
        public string? PCT_CROMATOGRAFIA_METANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_ETANO")]
        public string? PCT_CROMATOGRAFIA_ETANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_PROPANO")]
        public string? PCT_CROMATOGRAFIA_PROPANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_N_BUTANO")]
        public string? PCT_CROMATOGRAFIA_N_BUTANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_I_BUTANO")]
        public string? PCT_CROMATOGRAFIA_I_BUTANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_N_PENTANO")]
        public string? PCT_CROMATOGRAFIA_N_PENTANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_I_PENTANO")]
        public string? PCT_CROMATOGRAFIA_I_PENTANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HEXANO")]
        public string? PCT_CROMATOGRAFIA_HEXANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HEPTANO")]
        public string? PCT_CROMATOGRAFIA_HEPTANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_OCTANO")]
        public string? PCT_CROMATOGRAFIA_OCTANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_NONANO")]
        public string? PCT_CROMATOGRAFIA_NONANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_DECANO")]
        public string? PCT_CROMATOGRAFIA_DECANO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_H2S")]
        public string? PCT_CROMATOGRAFIA_H2S_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_AGUA")]
        public string? PCT_CROMATOGRAFIA_AGUA_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HELIO")]
        public string? PCT_CROMATOGRAFIA_HELIO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_OXIGENIO")]
        public string? PCT_CROMATOGRAFIA_OXIGENIO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_CO")]
        public string? PCT_CROMATOGRAFIA_CO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_HIDROGENIO")]
        public string? PCT_CROMATOGRAFIA_HIDROGENIO_003 { get; set; }

        [XmlElement("PCT_CROMATOGRAFIA_ARGONIO")]
        public string? PCT_CROMATOGRAFIA_ARGONIO_003 { get; set; }

        [XmlElement("DSC_VERSAO_SOFTWARE")]
        public string? DSC_VERSAO_SOFTWARE_003 { get; set; }
    }

    [XmlRoot("ELEMENTO_PRIMARIO")]
    public class ELEMENTO_PRIMARIO_003
    {
        [XmlElement("ICE_LIMITE_SPRR_ALARME")]
        public string? CE_LIMITE_SPRR_ALARME_003 { get; set; }
        [XmlElement("ICE_LIMITE_INFRR_ALARME")]

        public string? ICE_LIMITE_INFRR_ALARME_1_003 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]

        public string? IND_HABILITACAO_ALARME_1_003 { get; set; }
    }

    [XmlRoot("INSTRUMENTO_PRESSAO")]
    public class INSTRUMENTO_PRESSAO_003
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_1_003 { get; set; }
        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public string? MED_PRSO_LIMITE_SPRR_ALRME_1_003 { get; set; }
        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public string? MED_PRSO_LMTE_INFRR_ALRME_1_003 { get; set; }
        [XmlElement("MED_PRSO_ADOTADA_FALHA")]
        public string? MED_PRSO_ADOTADA_FALHA_1_003 { get; set; }
        [XmlElement("DSC_ESTADO_INSNO_CASO_FALHA")]
        public string? DSC_ESTADO_INSNO_CASO_FALHA_1_003 { get; set; }
        [XmlElement("IND_TIPO_PRESSAO_CONSIDERADA")]
        public string? IND_TIPO_PRESSAO_CONSIDERADA_003 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_2_003 { get; set; }

    }

    [XmlRoot("INSTRUMENTO_TEMPERATURA")]
    public class INSTRUMENTO_TEMPERATURA_003
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_2_003 { get; set; }

        [XmlElement("MED_TMPTA_SPRR_ALARME")]
        public string? MED_TMPTA_SPRR_ALARME_003 { get; set; }

        [XmlElement("MED_TMPTA_INFRR_ALRME")]
        public string? MED_TMPTA_INFRR_ALRME_003 { get; set; }

        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_3_003 { get; set; }

        [XmlElement("MED_TMPTA_ADTTA_FALHA")]
        public string? MED_TMPTA_ADTTA_FALHA_003 { get; set; }

        [XmlElement("DSC_ESTADO_INSTRUMENTO_FALHA")]
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_003 { get; set; }
    }

    [XmlRoot("INST_DIFEREN_PRESSAO_ALTA")]
    public class INST_DIFEREN_PRESSAO_ALTA_003
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_3_003 { get; set; }

        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public string? MED_PRSO_LIMITE_SPRR_ALRME_2_003 { get; set; }

        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public string? MED_PRSO_LMTE_INFRR_ALRME_2_003 { get; set; }
    }

    [XmlRoot("INST_DIFEREN_PRESSAO_MEDIA")]
    public class INST_DIFEREN_PRESSAO_MEDIA_003
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_4_003 { get; set; }

        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public string? MED_PRSO_LIMITE_SPRR_ALRME_3_003 { get; set; }

        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public string? MED_PRSO_LMTE_INFRR_ALRME_3_003 { get; set; }
    }

    [XmlRoot("INST_DIFEREN_PRESSAO_BAIXA")]
    public class INST_DIFEREN_PRESSAO_BAIXA_003
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_5_003 { get; set; }

        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public string? MED_PRSO_LIMITE_SPRR_ALRME_4_003 { get; set; }

        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public string? MED_PRSO_LMTE_INFRR_ALRME_4_003 { get; set; }

        [XmlElement("MED_PRSO_ADOTADA_FALHA")]
        public string? MED_PRSO_ADOTADA_FALHA_2_003 { get; set; }

        [XmlElement("DSC_ESTADO_INSNO_CASO_FALHA")]

        public string? DSC_ESTADO_INSNO_CASO_FALHA_2_003 { get; set; }
        [XmlElement("MED_CUTOFF_KPA")]

        public string? MED_CUTOFF_KPA_1_003 { get; set; }
        [XmlElement("IND_HABILITACAO_ALARME")]
        public string? IND_HABILITACAO_ALARME_4_003 { get; set; }
    }

    [XmlRoot("INST_DIFEREN_PRESSAO_PRINCIPAL")]
    public class INST_DIFEREN_PRESSAO_PRINCIPAL_003
    {
        [XmlElement("NUM_SERIE")]
        public string? NUM_SERIE_6_003 { get; set; }

        [XmlElement("MED_PRSO_LIMITE_SPRR_ALRME")]
        public string? MED_PRSO_LIMITE_SPRR_ALRME_5_003 { get; set; }

        [XmlElement("MED_PRSO_LMTE_INFRR_ALRME")]
        public string? MED_PRSO_LMTE_INFRR_ALRME_5_003 { get; set; }

        [XmlElement("IND_HABILITACAO_ALARME")]

        public string? IND_HABILITACAO_ALARME_5_003 { get; set; }
        [XmlElement("MED_PRSO_ADOTADA_FALHA")]

        public string? MED_PRSO_ADOTADA_FALHA_3_003 { get; set; }

        [XmlElement("DSC_ESTADO_INSNO_CASO_FALHA")]
        public string? DSC_ESTADO_INSNO_CASO_FALHA_3_003 { get; set; }

        [XmlElement("MED_CUTOFF_KPA")]
        public string? MED_CUTOFF_KPA_2_003 { get; set; }

    }

    [XmlRoot("PLACA_ORIFICIO")]
    public class PLACA_ORIFICIO_003
    {

        [XmlElement("MED_DIAMETRO_REFERENCIA")]
        public string? MED_DIAMETRO_REFERENCIA_003 { get; set; }

        [XmlElement("MED_TEMPERATURA_RFRNA")]
        public string? MED_TEMPERATURA_RFRNA_003 { get; set; }

        [XmlElement("DSC_MATERIAL_CONTRUCAO_PLACA")]
        public string? DSC_MATERIAL_CONTRUCAO_PLACA_003 { get; set; }

        [XmlElement("MED_DMTRO_INTRO_TRCHO_MDCO")]
        public string? MED_DMTRO_INTRO_TRCHO_MDCO_003 { get; set; }

        [XmlElement("MED_TMPTA_TRCHO_MDCO")]
        public string? MED_TMPTA_TRCHO_MDCO_003 { get; set; }

        [XmlElement("DSC_MATERIAL_CNSTO_TRCHO_MDCO")]
        public string? DSC_MATERIAL_CNSTO_TRCHO_MDCO_003 { get; set; }

        [XmlElement("DSC_LCLZO_TMDA_PRSO_DFRNL")]
        public string? DSC_LCLZO_TMDA_PRSO_DFRNL_003 { get; set; }

        [XmlElement("IND_TOMADA_PRESSAO_ESTATICA")]
        public string? IND_TOMADA_PRESSAO_ESTATICA_003 { get; set; }

    }

    [XmlRoot("PRODUCAO")]
    public class PRODUCAO_003
    {
        [XmlElement("DHA_INICIO_PERIODO_MEDICAO")]
        public string? DHA_INICIO_PERIODO_MEDICAO_003 { get; set; }
        [XmlElement("DHA_FIM_PERIODO_MEDICAO")]
        public string? DHA_FIM_PERIODO_MEDICAO_003 { get; set; }
        [XmlElement("ICE_DENSIDADE_RELATIVA")]
        public string? ICE_DENSIDADE_RELATIVA_003 { get; set; }
        [XmlElement("MED_DIFERENCIAL_PRESSAO")]
        public string? MED_DIFERENCIAL_PRESSAO_003 { get; set; }
        [XmlElement("MED_PRESSAO_ESTATICA")]
        public string? MED_PRESSAO_ESTATICA_003 { get; set; }
        [XmlElement("MED_TEMPERATURA")]
        public string? MED_TEMPERATURA_2_003 { get; set; }
        [XmlElement("PRZ_DURACAO_FLUXO_EFETIVO")]
        public string? PRZ_DURACAO_FLUXO_EFETIVO_003 { get; set; }
        [XmlElement("MED_CORRIGIDO_MVMDO")]
        public string? MED_CORRIGIDO_MVMDO_003 { get; set; }
    }
}
