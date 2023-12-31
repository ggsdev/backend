﻿using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models
{
    public class Measurement : BaseModel
    {
        public NFSM? NFSM { get; set; }
        public string FileName { get; set; } = string.Empty;
        public Production Production { get; set; }
        public decimal? VolumeAfterManualBsw_001 { get; set; }
        public bool? StatusMeasuringPoint { get; set; }
        #region 001
        public decimal BswManual_001 { get; set; }
        public string? NUM_SERIE_ELEMENTO_PRIMARIO_001 { get; set; }
        public string? COD_INSTALACAO_001 { get; set; }
        public string? COD_TAG_PONTO_MEDICAO_001 { get; set; }
        public string? NUM_SERIE_COMPUTADOR_VAZAO_001 { get; set; }
        public DateTime? DHA_COLETA_001 { get; set; }
        public decimal? MED_TEMPERATURA_001 { get; set; }
        public decimal? MED_PRESSAO_ATMSA_001 { get; set; }
        public decimal? MED_PRESSAO_RFRNA_001 { get; set; }
        public decimal? MED_DENSIDADE_RELATIVA_001 { get; set; }
        public string? DSC_VERSAO_SOFTWARE_001 { get; set; }
        public decimal? ICE_METER_FACTOR_1_001 { get; set; }
        public decimal? ICE_METER_FACTOR_2_001 { get; set; }
        public decimal? ICE_METER_FACTOR_3_001 { get; set; }
        public decimal? ICE_METER_FACTOR_4_001 { get; set; }
        public decimal? ICE_METER_FACTOR_5_001 { get; set; }
        public decimal? ICE_METER_FACTOR_6_001 { get; set; }
        public decimal? ICE_METER_FACTOR_7_001 { get; set; }
        public decimal? ICE_METER_FACTOR_8_001 { get; set; }
        public decimal? ICE_METER_FACTOR_9_001 { get; set; }
        public decimal? ICE_METER_FACTOR_10_001 { get; set; }
        public decimal? ICE_METER_FACTOR_11_001 { get; set; }
        public decimal? ICE_METER_FACTOR_12_001 { get; set; }
        public decimal? ICE_METER_FACTOR_13_001 { get; set; }
        public decimal? ICE_METER_FACTOR_14_001 { get; set; }
        public decimal? ICE_METER_FACTOR_15_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_1_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_2_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_3_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_4_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_5_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_6_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_7_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_8_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_9_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_10_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_11_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_12_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_13_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_14_001 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_15_001 { get; set; }
        public decimal? ICE_K_FACTOR_1_001 { get; set; }
        public decimal? ICE_K_FACTOR_2_001 { get; set; }
        public decimal? ICE_K_FACTOR_3_001 { get; set; }
        public decimal? ICE_K_FACTOR_4_001 { get; set; }
        public decimal? ICE_K_FACTOR_5_001 { get; set; }
        public decimal? ICE_K_FACTOR_6_001 { get; set; }
        public decimal? ICE_K_FACTOR_7_001 { get; set; }
        public decimal? ICE_K_FACTOR_8_001 { get; set; }
        public decimal? ICE_K_FACTOR_9_001 { get; set; }
        public decimal? ICE_K_FACTOR_10_001 { get; set; }
        public decimal? ICE_K_FACTOR_11_001 { get; set; }
        public decimal? ICE_K_FACTOR_12_001 { get; set; }
        public decimal? ICE_K_FACTOR_13_001 { get; set; }
        public decimal? ICE_K_FACTOR_14_001 { get; set; }
        public decimal? ICE_K_FACTOR_15_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_1_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_2_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_3_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_4_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_5_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_6_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_7_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_8_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_9_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_10_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_11_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_12_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_13_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_14_001 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_15_001 { get; set; }
        public decimal? ICE_CUTOFF_001 { get; set; }
        public decimal? ICE_LIMITE_SPRR_ALARME_001 { get; set; }
        public decimal? ICE_LIMITE_INFRR_ALARME_001 { get; set; }
        public string? IND_HABILITACAO_ALARME_1_001 { get; set; }
        public string? NUM_SERIE_1_001 { get; set; }
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_001 { get; set; }
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_001 { get; set; }
        public string? IND_HABILITACAO_ALARME_2_001 { get; set; }
        public decimal? MED_PRSO_ADOTADA_FALHA_001 { get; set; }

        public string? DSC_ESTADO_INSNO_CASO_FALHA_001 { get; set; }
        public string? IND_TIPO_PRESSAO_CONSIDERADA_001 { get; set; }
        public string? NUM_SERIE_2_001 { get; set; }
        public decimal? MED_TMPTA_SPRR_ALARME_001 { get; set; }
        public decimal? MED_TMPTA_INFRR_ALRME_001 { get; set; }
        public string? IND_HABILITACAO_ALARME_3_001 { get; set; }
        public decimal? MED_TMPTA_ADTTA_FALHA_001 { get; set; }
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_1_001 { get; set; }
        public string? NUM_SERIE_3_001 { get; set; }
        public decimal? PCT_LIMITE_SUPERIOR_1_001 { get; set; }
        public decimal? PCT_LIMITE_INFERIOR_1_001 { get; set; }
        public string? IND_HABILITACAO_ALARME_4_001 { get; set; }
        public decimal? PCT_ADOTADO_CASO_FALHA_1_001 { get; set; }
        public string? NUM_SERIE_4_001 { get; set; }
        public decimal? PCT_LIMITE_SUPERIOR_2_001 { get; set; }
        public decimal? PCT_LIMITE_INFERIOR_2_001 { get; set; }
        public string? IND_HABILITACAO_ALARME_5_001 { get; set; }
        public decimal? PCT_ADOTADO_CASO_FALHA_2_001 { get; set; }
        public decimal? DSC_ESTADO_INSTRUMENTO_FALHA_3_001 { get; set; }
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_2_001 { get; set; }
        public DateTime? DHA_INICIO_PERIODO_MEDICAO_001 { get; set; }
        public DateTime? DHA_FIM_PERIODO_MEDICAO_001 { get; set; }
        public decimal? ICE_DENSIDADADE_RELATIVA_001 { get; set; }
        public decimal? ICE_CORRECAO_BSW_001 { get; set; }
        public decimal? ICE_CORRECAO_PRESSAO_LIQUIDO_001 { get; set; }
        public decimal? ICE_CRRCO_TEMPERATURA_LIQUIDO_001 { get; set; }
        public decimal? MED_PRESSAO_ESTATICA_001 { get; set; }
        public decimal? MED_TMPTA_FLUIDO_001 { get; set; }
        public decimal? MED_VOLUME_BRTO_CRRGO_MVMDO_001 { get; set; }
        public decimal? MED_VOLUME_BRUTO_MVMDO_001 { get; set; }
        public decimal? MED_VOLUME_LIQUIDO_MVMDO_001 { get; set; }
        public decimal? MED_VOLUME_TTLZO_FIM_PRDO_001 { get; set; }
        public decimal? MED_VOLUME_TTLZO_INCO_PRDO_001 { get; set; }
        #endregion

        #region 002
        public string? NUM_SERIE_ELEMENTO_PRIMARIO_002 { get; set; }
        public string? COD_INSTALACAO_002 { get; set; }
        public string? COD_TAG_PONTO_MEDICAO_002 { get; set; }
        public string? NUM_SERIE_COMPUTADOR_VAZAO_002 { get; set; }
        public DateTime? DHA_COLETA_002 { get; set; }
        public decimal? MED_TEMPERATURA_1_002 { get; set; }
        public decimal? MED_PRESSAO_ATMSA_002 { get; set; }
        public decimal? MED_PRESSAO_RFRNA_002 { get; set; }
        public decimal? MED_DENSIDADE_RELATIVA_002 { get; set; }
        public string? DSC_NORMA_UTILIZADA_CALCULO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_NITROGENIO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_CO2_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_METANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_ETANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_PROPANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_N_BUTANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_I_BUTANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_N_PENTANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_I_PENTANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HEXANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HEPTANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_OCTANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_NONANO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_DECANO_002 { get; set; }

        public decimal? PCT_CROMATOGRAFIA_H2S_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_AGUA_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HELIO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_OXIGENIO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_CO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HIDROGENIO_002 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_ARGONIO_002 { get; set; }
        public string? DSC_VERSAO_SOFTWARE_002 { get; set; }

        public decimal? ICE_METER_FACTOR_1_002 { get; set; }
        public decimal? ICE_METER_FACTOR_2_002 { get; set; }
        public decimal? ICE_METER_FACTOR_3_002 { get; set; }
        public decimal? ICE_METER_FACTOR_4_002 { get; set; }
        public decimal? ICE_METER_FACTOR_5_002 { get; set; }
        public decimal? ICE_METER_FACTOR_6_002 { get; set; }
        public decimal? ICE_METER_FACTOR_7_002 { get; set; }
        public decimal? ICE_METER_FACTOR_8_002 { get; set; }
        public decimal? ICE_METER_FACTOR_9_002 { get; set; }
        public decimal? ICE_METER_FACTOR_10_002 { get; set; }
        public decimal? ICE_METER_FACTOR_11_002 { get; set; }
        public decimal? ICE_METER_FACTOR_12_002 { get; set; }
        public decimal? ICE_METER_FACTOR_13_002 { get; set; }
        public decimal? ICE_METER_FACTOR_14_002 { get; set; }
        public decimal? ICE_METER_FACTOR_15_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_1_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_2_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_3_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_4_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_5_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_6_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_7_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_8_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_9_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_10_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_11_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_12_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_13_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_14_002 { get; set; }
        public decimal? QTD_PULSOS_METER_FACTOR_15_002 { get; set; }

        public decimal? ICE_K_FACTOR_1_002 { get; set; }
        public decimal? ICE_K_FACTOR_2_002 { get; set; }
        public decimal? ICE_K_FACTOR_3_002 { get; set; }
        public decimal? ICE_K_FACTOR_4_002 { get; set; }
        public decimal? ICE_K_FACTOR_5_002 { get; set; }
        public decimal? ICE_K_FACTOR_6_002 { get; set; }
        public decimal? ICE_K_FACTOR_7_002 { get; set; }
        public decimal? ICE_K_FACTOR_8_002 { get; set; }
        public decimal? ICE_K_FACTOR_9_002 { get; set; }
        public decimal? ICE_K_FACTOR_10_002 { get; set; }
        public decimal? ICE_K_FACTOR_11_002 { get; set; }
        public decimal? ICE_K_FACTOR_12_002 { get; set; }
        public decimal? ICE_K_FACTOR_13_002 { get; set; }
        public decimal? ICE_K_FACTOR_14_002 { get; set; }
        public decimal? ICE_K_FACTOR_15_002 { get; set; }

        public decimal? QTD_PULSOS_K_FACTOR_1_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_2_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_3_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_4_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_5_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_6_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_7_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_8_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_9_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_10_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_11_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_12_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_13_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_14_002 { get; set; }
        public decimal? QTD_PULSOS_K_FACTOR_15_002 { get; set; }
        public decimal? ICE_CUTOFF_002 { get; set; }
        public decimal? ICE_LIMITE_SPRR_ALARME_002 { get; set; }
        public decimal? ICE_LIMITE_INFRR_ALARME_002 { get; set; }

        public string? IND_HABILITACAO_ALARME_1_002 { get; set; }


        public string? NUM_SERIE_1_002 { get; set; }
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_002 { get; set; }
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_002 { get; set; }
        public string? IND_HABILITACAO_ALARME_2_002 { get; set; }
        public decimal? MED_PRSO_ADOTADA_FALHA_002 { get; set; }
        public string? DSC_ESTADO_INSNO_CASO_FALHA_002 { get; set; }
        public string? IND_TIPO_PRESSAO_CONSIDERADA_002 { get; set; }


        public string? NUM_SERIE_2_002 { get; set; }
        public decimal? MED_TMPTA_SPRR_ALARME_002 { get; set; }
        public decimal? MED_TMPTA_INFRR_ALRME_002 { get; set; }
        public string? IND_HABILITACAO_ALARME_3_002 { get; set; }
        public decimal? MED_TMPTA_ADTTA_FALHA_002 { get; set; }
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_002 { get; set; }


        public DateTime? DHA_INICIO_PERIODO_MEDICAO_002 { get; set; }
        public DateTime? DHA_FIM_PERIODO_MEDICAO_002 { get; set; }
        public decimal? ICE_DENSIDADE_RELATIVA_002 { get; set; }
        public decimal? MED_PRESSAO_ESTATICA_002 { get; set; }
        public decimal? MED_TEMPERATURA_2_002 { get; set; }
        public decimal? PRZ_DURACAO_FLUXO_EFETIVO_002 { get; set; }
        public decimal? MED_BRUTO_MOVIMENTADO_002 { get; set; }
        public decimal? MED_CORRIGIDO_MVMDO_002 { get; set; }
        #endregion

        #region 003
        public string? NUM_SERIE_ELEMENTO_PRIMARIO_003 { get; set; }
        public string? COD_INSTALACAO_003 { get; set; }
        public string? COD_TAG_PONTO_MEDICAO_003 { get; set; }
        public string? NUM_SERIE_COMPUTADOR_VAZAO_003 { get; set; }
        public DateTime? DHA_COLETA_003 { get; set; }
        public decimal? MED_TEMPERATURA_1_003 { get; set; }
        public decimal? MED_PRESSAO_ATMSA_003 { get; set; }
        public decimal? MED_PRESSAO_RFRNA_003 { get; set; }
        public decimal? MED_DENSIDADE_RELATIVA_003 { get; set; }
        public string? DSC_NORMA_UTILIZADA_CALCULO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_NITROGENIO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_CO2_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_METANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_ETANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_PROPANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_N_BUTANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_I_BUTANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_N_PENTANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_I_PENTANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HEXANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HEPTANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_OCTANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_NONANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_DECANO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_H2S_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_AGUA_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HELIO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_OXIGENIO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_CO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_HIDROGENIO_003 { get; set; }
        public decimal? PCT_CROMATOGRAFIA_ARGONIO_003 { get; set; }
        public string? DSC_VERSAO_SOFTWARE_003 { get; set; }
        public decimal? CE_LIMITE_SPRR_ALARME_003 { get; set; }
        public decimal? ICE_LIMITE_INFRR_ALARME_003 { get; set; }
        public string? IND_HABILITACAO_ALARME_1_003 { get; set; }

        #region instrumento pressao 003
        public string? NUM_SERIE_1_003 { get; set; }
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_1_003 { get; set; }
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_1_003 { get; set; }
        public decimal? MED_PRSO_ADOTADA_FALHA_1_003 { get; set; }
        public string? DSC_ESTADO_INSNO_CASO_FALHA_1_003 { get; set; }
        public string? IND_TIPO_PRESSAO_CONSIDERADA_003 { get; set; }
        public string? IND_HABILITACAO_ALARME_2_003 { get; set; }
        #endregion

        #region instrumento temperatura
        public string? NUM_SERIE_2_003 { get; set; }
        public decimal? MED_TMPTA_SPRR_ALARME_003 { get; set; }
        public decimal? MED_TMPTA_INFRR_ALRME_003 { get; set; }
        public string? IND_HABILITACAO_ALARME_3_003 { get; set; }
        public decimal? MED_TMPTA_ADTTA_FALHA_003 { get; set; }
        public string? DSC_ESTADO_INSTRUMENTO_FALHA_003 { get; set; }
        #endregion

        #region placa orificio
        public decimal? MED_DIAMETRO_REFERENCIA_003 { get; set; }
        public decimal? MED_TEMPERATURA_RFRNA_003 { get; set; }
        public string? DSC_MATERIAL_CONTRUCAO_PLACA_003 { get; set; }
        public decimal? MED_DMTRO_INTRO_TRCHO_MDCO_003 { get; set; }
        public decimal? MED_TMPTA_TRCHO_MDCO_003 { get; set; }
        public string? DSC_MATERIAL_CNSTO_TRCHO_MDCO_003 { get; set; }
        public string? DSC_LCLZO_TMDA_PRSO_DFRNL_003 { get; set; }
        public string? IND_TOMADA_PRESSAO_ESTATICA_003 { get; set; }
        #endregion

        #region instrumento pressao alta
        public string? NUM_SERIE_3_003 { get; set; }
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_2_003 { get; set; } //coluna não vem no xml
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_2_003 { get; set; }
        #endregion

        #region instrumento pressao media
        public string? NUM_SERIE_4_003 { get; set; }
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_3_003 { get; set; }
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_3_003 { get; set; }
        #endregion

        #region instrumento pressao baixa
        public string? NUM_SERIE_5_003 { get; set; }
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_4_003 { get; set; }
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_4_003 { get; set; }
        public string? IND_HABILITACAO_ALARME_4_003 { get; set; }
        public decimal? MED_PRSO_ADOTADA_FALHA_3_003 { get; set; }
        public string? DSC_ESTADO_INSNO_CASO_FALHA_3_003 { get; set; }
        public decimal? MED_CUTOFF_KPA_1_003 { get; set; }
        #endregion

        #region instrumento pressao principal
        public string? NUM_SERIE_6_003 { get; set; }
        public decimal? MED_PRSO_LIMITE_SPRR_ALRME_5_003 { get; set; }
        public decimal? MED_PRSO_LMTE_INFRR_ALRME_5_003 { get; set; }
        public decimal? MED_PRSO_ADOTADA_FALHA_2_003 { get; set; }
        public string? IND_HABILITACAO_ALARME_5_003 { get; set; }
        public string? DSC_ESTADO_INSNO_CASO_FALHA_2_003 { get; set; }
        public decimal? MED_CUTOFF_KPA_2_003 { get; set; }
        #endregion

        #region producao
        public DateTime? DHA_INICIO_PERIODO_MEDICAO_003 { get; set; }
        public DateTime? DHA_FIM_PERIODO_MEDICAO_003 { get; set; }
        public decimal? ICE_DENSIDADE_RELATIVA_003 { get; set; }
        public decimal? MED_DIFERENCIAL_PRESSAO_003 { get; set; }
        public decimal? MED_PRESSAO_ESTATICA_003 { get; set; }
        public decimal? MED_TEMPERATURA_2_003 { get; set; }
        public decimal? PRZ_DURACAO_FLUXO_EFETIVO_003 { get; set; }
        public decimal? MED_CORRIGIDO_MVMDO_003 { get; set; }
        #endregion

        #endregion

        #region 039
        public string? COD_TAG_EQUIPAMENTO_039 { get; set; }
        public string? COD_TAG_PONTO_MEDICAO_039 { get; set; }

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
        #endregion

        public FileType? FileType { get; set; }
        public User? User { get; set; }
        public MeasurementHistory MeasurementHistory { get; set; }
        public MeasuringPoint MeasuringPoint { get; set; }
        public Installation Installation { get; set; }
    }
}