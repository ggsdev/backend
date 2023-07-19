using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Mappings
{
    public class MeasurementMap : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.ToTable("Measurements");


            #region 039

            builder.Property(x => x.COD_TAG_EQUIPAMENTO_039)
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Property(x => x.COD_FALHA_SUPERIOR_039)
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Property(x => x.DSC_TIPO_FALHA_039)
                .HasColumnType("smallint")
                ;

            builder.Property(x => x.COD_FALHA_039)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                ;

            builder.Property(x => x.IND_TIPO_NOTIFICACAO_039)
                .HasColumnType("char")
                .HasMaxLength(1)
                ;

            builder.Property(x => x.DHA_OCORRENCIA_039)
                .HasColumnType("datetime")
                ;

            builder.Property(x => x.DHA_DETECCAO_039)
                .HasColumnType("datetime")
                ;

            builder.Property(x => x.DHA_RETORNO_039)
                .HasColumnType("datetime");

            builder.Property(x => x.DHA_NUM_PREVISAO_RETORNO_DIAS_039)
                .HasColumnType("varchar")
                .HasMaxLength(10);

            builder.Property(x => x.DHA_DSC_FALHA_039)
                .HasColumnType("text")
                ;

            builder.Property(x => x.DHA_DSC_ACAO_039)
                .HasColumnType("text")
                ;

            builder.Property(x => x.DHA_DSC_METODOLOGIA_039)
                .HasColumnType("text")
                ;

            builder.Property(x => x.DHA_NOM_RESPONSAVEL_RELATO_039)
                .HasColumnType("varchar")
                .HasMaxLength(155)
                ;

            builder.Property(x => x.DHA_NUM_SERIE_EQUIPAMENTO_039)
                .HasColumnType("varchar")
                .HasMaxLength(30);

            builder.Property(x => x.DHA_COD_INSTALACAO_039)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                ;

            #endregion

            #region 003

            builder.Property(x => x.NUM_SERIE_ELEMENTO_PRIMARIO_003)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                ;

            builder.Property(x => x.COD_INSTALACAO_003).HasColumnType("varchar").HasMaxLength(20)
                ;

            builder.Property(x => x.COD_TAG_PONTO_MEDICAO_003).HasColumnType("varchar").HasMaxLength(20);

            builder.Property(x => x.NUM_SERIE_COMPUTADOR_VAZAO_003)
               .HasColumnType("varchar")
               .HasMaxLength(20)
               ;

            builder.Property(x => x.DHA_COLETA_003)
               .HasColumnType("datetime")
               ;

            builder.Property(x => x.MED_TEMPERATURA_1_003)
               .HasColumnType("float")
               .HasPrecision(3, 2)
               ;

            builder.Property(x => x.MED_PRESSAO_ATMSA_003)
               .HasColumnType("float")
               .HasPrecision(3, 3)
               ;

            builder.Property(x => x.MED_PRESSAO_RFRNA_003)
               .HasColumnType("float")
               .HasPrecision(3, 3)
               ;

            builder.Property(x => x.MED_DENSIDADE_RELATIVA_003)
               .HasColumnType("float")
               .HasPrecision(8, 8)
               ;

            builder.Property(x => x.DSC_NORMA_UTILIZADA_CALCULO_003)
               .HasColumnType("varchar")
               .HasMaxLength(50)
               ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_NITROGENIO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_CO2_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_METANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_ETANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_PROPANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_N_BUTANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_I_BUTANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_N_PENTANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_I_PENTANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_HEXANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_HEPTANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_OCTANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_NONANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_DECANO_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_H2S_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_AGUA_003)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_HELIO_003)
             .HasColumnType("float")
             .HasPrecision(6, 6)
             ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_OXIGENIO_003)
            .HasColumnType("float")
            .HasPrecision(6, 6)
            ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_CO_003)
            .HasColumnType("float")
            .HasPrecision(6, 6)
            ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_HIDROGENIO_003)
            .HasColumnType("float")
            .HasPrecision(6, 6)
            ;

            builder.Property(x => x.PCT_CROMATOGRAFIA_ARGONIO_003)
            .HasColumnType("float")
            .HasPrecision(6, 6)
            ;

            builder.Property(x => x.DSC_VERSAO_SOFTWARE_003)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            ;

            builder.Property(x => x.CE_LIMITE_SPRR_ALARME_003)
           .HasColumnType("float")
           .HasPrecision(6, 3)
           ;

            builder.Property(x => x.ICE_LIMITE_INFRR_ALARME_1_003)
           .HasColumnType("float")
           .HasPrecision(6, 3)
           ;

            builder.Property(x => x.ICE_LIMITE_INFRR_ALARME_2_003)
           .HasColumnType("bit")
           ;

            builder.Property(x => x.NUM_SERIE_1_003)
           .HasColumnType("varchar")
           .HasMaxLength(30)
           ;

            builder.Property(x => x.MED_PRSO_LIMITE_SPRR_ALRME_1_003)
           .HasColumnType("float")
           .HasPrecision(6, 3)
           ;

            builder.Property(x => x.MED_PRSO_LMTE_INFRR_ALRME_1_003)
           .HasColumnType("float")
           .HasPrecision(6, 3)
           ;

            builder.Property(x => x.IND_HABILITACAO_ALARME_1_003)
           .HasColumnType("varchar")
           .HasMaxLength(1)
           ;

            builder.Property(x => x.MED_PRSO_ADOTADA_FALHA_1_003)
            .HasColumnType("float")
           .HasPrecision(6, 3)
            ;

            builder.Property(x => x.DSC_ESTADO_INSNO_CASO_FALHA_1_003)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            ;

            builder.Property(x => x.IND_TIPO_PRESSAO_CONSIDERADA_003)
            .HasColumnType("char")
            .HasMaxLength(1)
            ;

            builder.Property(x => x.NUM_SERIE_2_003)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            ;

            builder.Property(x => x.MED_TMPTA_SPRR_ALARME_003)
            .HasColumnType("float")
            .HasPrecision(3, 2)
            ;

            builder.Property(x => x.MED_TMPTA_INFRR_ALRME_003)
           .HasColumnType("float")
           .HasPrecision(3, 2)
           ;

            builder.Property(x => x.IND_HABILITACAO_ALARME_2_003)
           .HasColumnType("varchar")
           .HasMaxLength(1)
           ;

            builder.Property(x => x.MED_TMPTA_ADTTA_FALHA_003)
           .HasColumnType("float")
           .HasPrecision(3, 2)
           ;

            builder.Property(x => x.DSC_ESTADO_INSTRUMENTO_FALHA_003)
           .HasColumnType("varchar")
           .HasMaxLength(50)
           ;

            builder.Property(x => x.MED_DIAMETRO_REFERENCIA_003)
              .HasColumnType("float")
              .HasPrecision(4, 3)
              ;

            builder.Property(x => x.MED_TEMPERATURA_RFRNA_003)
          .HasColumnType("float")
          .HasPrecision(3, 2)
          ;

            builder.Property(x => x.DSC_MATERIAL_CONTRUCAO_PLACA_003)
          .HasColumnType("varchar")
          .HasMaxLength(50)
          ;

            builder.Property(x => x.MED_DMTRO_INTRO_TRCHO_MDCO_003)
            .HasColumnType("float")
            .HasPrecision(4, 3)
            ;

            builder.Property(x => x.MED_TMPTA_TRCHO_MDCO_003)
             .HasColumnType("float")
             .HasPrecision(3, 2)
             ;

            builder.Property(x => x.DSC_MATERIAL_CNSTO_TRCHO_MDCO_003)
           .HasColumnType("varchar")
           .HasMaxLength(50)
           ;

            builder.Property(x => x.DSC_LCLZO_TMDA_PRSO_DFRNL_003)
           .HasColumnType("varchar")
           .HasMaxLength(50)
           ;

            builder.Property(x => x.IND_TOMADA_PRESSAO_ESTATICA_003)
           .HasColumnType("char")
           .HasMaxLength(1)
           ;

            builder.Property(x => x.NUM_SERIE_3_003)
           .HasColumnType("varchar")
           .HasMaxLength(30)
           ;

            builder.Property(x => x.MED_PRSO_LMTE_INFRR_ALRME_2_003)
           .HasColumnType("float")
           .HasPrecision(6, 3);

            builder.Property(x => x.MED_PRSO_LIMITE_SPRR_ALRME_2_003)
           .HasColumnType("float")
           .HasPrecision(6, 3);

            builder.Property(x => x.NUM_SERIE_4_003)
          .HasColumnType("varchar")
          .HasMaxLength(30)
          ;

            builder.Property(x => x.MED_PRSO_LIMITE_SPRR_ALRME_3_003)
          .HasColumnType("float")
          .HasPrecision(6, 3);

            builder.Property(x => x.MED_PRSO_LMTE_INFRR_ALRME_3_003)
          .HasColumnType("float")
          .HasPrecision(6, 3);

            builder.Property(x => x.NUM_SERIE_5_003)
         .HasColumnType("varchar")
          .HasMaxLength(30)
          ;

            builder.Property(x => x.MED_PRSO_LIMITE_SPRR_ALRME_4_003)
          .HasColumnType("float")
          .HasPrecision(6, 3);

            builder.Property(x => x.MED_PRSO_LMTE_INFRR_ALRME_4_003)
          .HasColumnType("float")
          .HasPrecision(6, 3);

            builder.Property(x => x.IND_HABILITACAO_ALARME_3_003)
          .HasColumnType("varchar")
          .HasMaxLength(1);


            builder.Property(x => x.MED_PRSO_ADOTADA_FALHA_2_003)
           .HasColumnType("float")
           .HasPrecision(6, 3);

            builder.Property(x => x.DSC_ESTADO_INSNO_CASO_FALHA_2_003)
           .HasColumnType("varchar")
           .HasMaxLength(50);

            builder.Property(x => x.MED_CUTOFF_KPA_1_003)
           .HasColumnType("float")
           .HasPrecision(6, 3);

            builder.Property(x => x.NUM_SERIE_6_003)
           .HasColumnType("varchar")
           .HasMaxLength(30)
           ;

            builder.Property(x => x.MED_PRSO_LIMITE_SPRR_ALRME_5_003)
         .HasColumnType("float")
         .HasPrecision(6, 3);

            builder.Property(x => x.MED_PRSO_LMTE_INFRR_ALRME_5_003)
          .HasColumnType("float")
          .HasPrecision(6, 3);

            builder.Property(x => x.IND_HABILITACAO_ALARME_4_003)
          .HasColumnType("varchar")
          .HasMaxLength(1);


            builder.Property(x => x.MED_PRSO_ADOTADA_FALHA_3_003)
          .HasColumnType("float")
          .HasPrecision(6, 3)
          .HasMaxLength(50);


            builder.Property(x => x.DSC_ESTADO_INSNO_CASO_FALHA_3_003)
          .HasColumnType("varchar")
          .HasMaxLength(50);

            builder.Property(x => x.MED_CUTOFF_KPA_2_003)
          .HasColumnType("float")
          .HasPrecision(6, 3);

            builder.Property(x => x.DHA_INICIO_PERIODO_MEDICAO_003)
          .HasColumnType("datetime")
          ;

            builder.Property(x => x.DHA_FIM_PERIODO_MEDICAO_003)
         .HasColumnType("datetime")
         ;

            builder.Property(x => x.ICE_DENSIDADE_RELATIVA_003)
         .HasColumnType("float")
         .HasPrecision(8, 8);

            builder.Property(x => x.MED_DIFERENCIAL_PRESSAO_003)
         .HasColumnType("float")
         .HasPrecision(6, 3)
         ;

            builder.Property(x => x.MED_PRESSAO_ESTATICA_003)
         .HasColumnType("float")
         .HasPrecision(6, 3)
         ;

            builder.Property(x => x.MED_TEMPERATURA_2_003)
        .HasColumnType("float")
        .HasPrecision(3, 2)
        ;

            builder.Property(x => x.PRZ_DURACAO_FLUXO_EFETIVO_003)
        .HasColumnType("float")
        .HasPrecision(4, 4)
        ;

            builder.Property(x => x.MED_CORRIGIDO_MVMDO_003)
          .HasColumnType("float")
          .HasPrecision(6, 5)
          ;

            #endregion

            #region 002

            builder.Property(x => x.NUM_SERIE_ELEMENTO_PRIMARIO_002)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            ;

            builder.Property(x => x.COD_INSTALACAO_002).HasColumnType("varchar").HasMaxLength(20)
            ;

            builder.Property(x => x.COD_TAG_PONTO_MEDICAO_002)
            .HasColumnType("varchar")
            .HasMaxLength(20)
            ;

            builder.Property(x => x.NUM_SERIE_COMPUTADOR_VAZAO_002)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            ;

            builder.Property(x => x.DHA_COLETA_002)
            .HasColumnType("datetime")
            ;

            builder.Property(x => x.MED_TEMPERATURA_1_002)
            .HasColumnType("float")
            .HasPrecision(3, 3)
            ;

            builder.Property(x => x.MED_PRESSAO_ATMSA_002)
            .HasColumnType("float")
            .HasPrecision(3, 3)
            ;

            builder.Property(x => x.MED_PRESSAO_RFRNA_002)
            .HasColumnType("float")
            .HasPrecision(3, 3)
            ;

            builder.Property(x => x.MED_DENSIDADE_RELATIVA_002)
            .HasColumnType("float")
            .HasPrecision(8, 8)
            ;

            builder.Property(x => x.DSC_NORMA_UTILIZADA_CALCULO_002)
            .HasColumnType("varchar")
            .HasMaxLength(50);

            builder.Property(x => x.PCT_CROMATOGRAFIA_NITROGENIO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_CO2_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_METANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_ETANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_PROPANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_N_BUTANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_I_BUTANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);


            builder.Property(x => x.PCT_CROMATOGRAFIA_N_PENTANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_I_PENTANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_HEXANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_HEPTANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_OCTANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_NONANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_DECANO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_H2S_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_AGUA_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_HELIO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_OXIGENIO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_CO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_HIDROGENIO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.PCT_CROMATOGRAFIA_ARGONIO_002)
            .HasColumnType("float")
            .HasPrecision(6, 6);

            builder.Property(x => x.DSC_VERSAO_SOFTWARE_002)
            .HasColumnType("varchar")
            .HasMaxLength(30);
            ;

            builder.Property(x => x.ICE_METER_FACTOR_1_002)
            .HasColumnType("float")
            .HasPrecision(5, 5)
            ;

            builder.Property(x => x.ICE_METER_FACTOR_2_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_3_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_4_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_5_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_6_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_7_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_8_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_9_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_10_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_11_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_12_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_13_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_14_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_15_002)
            .HasColumnType("float")
            .HasPrecision(5, 5);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_1_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_2_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_3_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_4_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_5_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_6_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_7_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_8_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_9_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_10_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_11_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_12_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_13_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_14_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_15_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_1_002)
            .HasColumnType("float")
            .HasPrecision(8, 2)
            ;

            builder.Property(x => x.ICE_K_FACTOR_2_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_3_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_4_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_5_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_6_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_7_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_8_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_9_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_10_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_11_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);
            builder.Property(x => x.ICE_K_FACTOR_12_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_13_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_14_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_15_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_1_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_2_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_3_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_4_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_5_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_6_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_7_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_8_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_9_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_10_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_11_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_12_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_13_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_14_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_15_002)
            .HasColumnType("float")
            .HasPrecision(8, 2);

            builder.Property(x => x.ICE_CUTOFF_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.ICE_LIMITE_SPRR_ALARME_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.ICE_LIMITE_INFRR_ALARME_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.IND_HABILITACAO_ALARME_1_002)
            .HasColumnType("varchar")
            .HasMaxLength(1)
            ;

            builder.Property(x => x.NUM_SERIE_1_002)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            ;

            builder.Property(x => x.MED_PRSO_LIMITE_SPRR_ALRME_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.MED_PRSO_LMTE_INFRR_ALRME_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;


            builder.Property(x => x.IND_HABILITACAO_ALARME_2_002)
            .HasColumnType("varchar")
            .HasMaxLength(1)
            ;

            builder.Property(x => x.MED_PRSO_ADOTADA_FALHA_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.DSC_ESTADO_INSNO_CASO_FALHA_002)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            ;

            builder.Property(x => x.IND_TIPO_PRESSAO_CONSIDERADA_002)
            .HasColumnType("char")
            .HasMaxLength(1)
            ;

            builder.Property(x => x.NUM_SERIE_2_002)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            ;

            builder.Property(x => x.MED_TMPTA_SPRR_ALARME_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.MED_TMPTA_INFRR_ALRME_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.IND_HABILITACAO_ALARME_3_002)
            .HasColumnType("varchar")
            .HasMaxLength(1)
            ;

            builder.Property(x => x.MED_TMPTA_ADTTA_FALHA_002)
            .HasColumnType("float")
            .HasPrecision(3, 2)
            ;

            builder.Property(x => x.DSC_ESTADO_INSTRUMENTO_FALHA_002)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            ;

            builder.Property(x => x.DHA_INICIO_PERIODO_MEDICAO_002)
            .HasColumnType("datetime")
            ;

            builder.Property(x => x.DHA_FIM_PERIODO_MEDICAO_002)
            .HasColumnType("datetime")
            ;

            builder.Property(x => x.ICE_DENSIDADE_RELATIVA_002)
            .HasColumnType("float")
            .HasPrecision(8, 8);

            builder.Property(x => x.MED_PRESSAO_ESTATICA_002)
            .HasColumnType("float")
            .HasPrecision(6, 3)
            ;

            builder.Property(x => x.MED_TEMPERATURA_2_002)
            .HasColumnType("float")
            .HasPrecision(3, 2)
            ;

            builder.Property(x => x.PRZ_DURACAO_FLUXO_EFETIVO_002)
            .HasColumnType("float")
            .HasPrecision(4, 4)
            ;

            builder.Property(x => x.MED_BRUTO_MOVIMENTADO_002)
            .HasColumnType("float")
            .HasPrecision(6, 5)
            ;

            builder.Property(x => x.MED_CORRIGIDO_MVMDO_002)
            .HasColumnType("float")
            .HasPrecision(6, 5)
            ;

            #endregion

            #region 001

            builder.Property(x => x.NUM_SERIE_ELEMENTO_PRIMARIO_001)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                ;

            builder.Property(x => x.COD_INSTALACAO_001).HasColumnType("varchar").HasMaxLength(20)
                ;

            builder.Property(x => x.COD_TAG_PONTO_MEDICAO_001)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                ;

            builder.Property(x => x.NUM_SERIE_COMPUTADOR_VAZAO_001)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                ;

            builder.Property(x => x.DHA_COLETA_001)
                .HasColumnType("datetime")
                ;

            builder.Property(x => x.MED_TEMPERATURA_001)
                .HasColumnType("float")
                .HasPrecision(3, 2)
                ;

            builder.Property(x => x.MED_PRESSAO_ATMSA_001)
               .HasColumnType("float")
               .HasPrecision(3, 3)
               ;

            builder.Property(x => x.MED_PRESSAO_RFRNA_001)
               .HasColumnType("float")
               .HasPrecision(3, 3)
               ;

            builder.Property(x => x.MED_DENSIDADE_RELATIVA_001)
               .HasColumnType("float")
               .HasPrecision(8, 8);

            builder.Property(x => x.DSC_VERSAO_SOFTWARE_001)
               .HasColumnType("varchar")
               .HasMaxLength(30)
               ;

            builder.Property(x => x.ICE_METER_FACTOR_1_001)
              .HasColumnType("float")
              .HasPrecision(5, 5)
              ;

            builder.Property(x => x.ICE_METER_FACTOR_2_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_3_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_4_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_5_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_6_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_7_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_8_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_9_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_10_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_11_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_12_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_13_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_14_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_15_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.ICE_METER_FACTOR_15_001)
              .HasColumnType("float")
              .HasPrecision(5, 5);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_1_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_2_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_3_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_4_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_5_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_6_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_7_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_8_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_9_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_10_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_11_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_12_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_13_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_14_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_METER_FACTOR_15_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_1_001)
              .HasColumnType("float")
              .HasPrecision(8, 2)
              ;

            builder.Property(x => x.ICE_K_FACTOR_2_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_3_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_4_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_5_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_6_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_7_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_8_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_9_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_10_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_11_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_12_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_13_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_14_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_K_FACTOR_15_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_1_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_2_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_3_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_4_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_5_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_6_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_7_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_8_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_9_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_10_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_11_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_12_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_13_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_14_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.QTD_PULSOS_K_FACTOR_15_001)
              .HasColumnType("float")
              .HasPrecision(8, 2);

            builder.Property(x => x.ICE_CUTOFF_001)
              .HasColumnType("float")
              .HasPrecision(6, 2)
              ;

            builder.Property(x => x.ICE_LIMITE_SPRR_ALARME_001)
              .HasColumnType("float")
              .HasPrecision(6, 2)
              ;

            builder.Property(x => x.ICE_LIMITE_INFRR_ALARME_001)
              .HasColumnType("float")
              .HasPrecision(6, 2)
              ;

            builder.Property(x => x.IND_HABILITACAO_ALARME_1_001)
              .HasColumnType("varchar")
          .HasMaxLength(1);

            builder.Property(x => x.NUM_SERIE_1_001)
              .HasColumnType("varchar")
              .HasMaxLength(30)
              ;

            builder.Property(x => x.MED_PRSO_LIMITE_SPRR_ALRME_001)
              .HasColumnType("float")
              .HasPrecision(6, 3)
              ;

            builder.Property(x => x.MED_PRSO_LMTE_INFRR_ALRME_001)
              .HasColumnType("float")
              .HasPrecision(6, 3)
              ;

            builder.Property(x => x.IND_HABILITACAO_ALARME_2_001).HasColumnType("varchar").HasMaxLength(1)
              ;

            builder.Property(x => x.MED_PRSO_ADOTADA_FALHA_001)
              .HasColumnType("float")
              .HasPrecision(6, 3)
              ;

            builder.Property(x => x.DSC_ESTADO_INSNO_CASO_FALHA_001)
              .HasColumnType("varchar")
              .HasMaxLength(50)
              ;

            builder.Property(x => x.IND_TIPO_PRESSAO_CONSIDERADA_001)
              .HasColumnType("char")
              .HasMaxLength(1)
              ;

            builder.Property(x => x.NUM_SERIE_2_001)
              .HasColumnType("varchar")
              .HasMaxLength(30)
              ;

            builder.Property(x => x.MED_TMPTA_SPRR_ALARME_001)
              .HasColumnType("float")
              .HasPrecision(3, 2)
              ;

            builder.Property(x => x.MED_TMPTA_INFRR_ALRME_001)
              .HasColumnType("float")
              .HasPrecision(3, 2)
              ;

            builder.Property(x => x.IND_HABILITACAO_ALARME_3_001)
              .HasColumnType("varchar")
          .HasMaxLength(1);

            builder.Property(x => x.MED_TMPTA_ADTTA_FALHA_001)
              .HasColumnType("float")
              .HasPrecision(3, 2)
              ;

            builder.Property(x => x.DSC_ESTADO_INSTRUMENTO_FALHA_1_001)
              .HasColumnType("varchar")
              .HasMaxLength(50)
              ;

            builder.Property(x => x.NUM_SERIE_3_001)
              .HasColumnType("varchar")
              .HasMaxLength(30)
              ;

            builder.Property(x => x.PCT_LIMITE_SUPERIOR_1_001)
              .HasColumnType("float")
              .HasPrecision(3, 3);

            builder.Property(x => x.PCT_LIMITE_INFERIOR_1_001)
              .HasColumnType("float")
              .HasPrecision(3, 3);

            builder.Property(x => x.IND_HABILITACAO_ALARME_4_001).HasColumnType("varchar").HasMaxLength(1);

            builder.Property(x => x.PCT_ADOTADO_CASO_FALHA_1_001)
              .HasColumnType("float")
              .HasPrecision(3, 3);

            builder.Property(x => x.DSC_ESTADO_INSTRUMENTO_FALHA_2_001)
              .HasColumnType("varchar")
              .HasMaxLength(50);

            builder.Property(x => x.NUM_SERIE_4_001)
              .HasColumnType("varchar")
              .HasMaxLength(30)
              ;

            builder.Property(x => x.PCT_LIMITE_SUPERIOR_2_001)
              .HasColumnType("float")
              .HasPrecision(3, 3);

            builder.Property(x => x.PCT_LIMITE_INFERIOR_2_001)
              .HasColumnType("float")
              .HasPrecision(3, 3);

            builder.Property(x => x.IND_HABILITACAO_ALARME_5_001).HasColumnType("varchar").HasMaxLength(1);

            builder.Property(x => x.PCT_ADOTADO_CASO_FALHA_2_001)
              .HasColumnType("float")
              .HasPrecision(3, 3);

            builder.Property(x => x.DSC_ESTADO_INSTRUMENTO_FALHA_3_001)
              .HasColumnType("varchar")
              .HasMaxLength(50);


            builder.Property(x => x.DHA_INICIO_PERIODO_MEDICAO_001)
              .HasColumnType("datetime")
              ;

            builder.Property(x => x.DHA_FIM_PERIODO_MEDICAO_001)
              .HasColumnType("datetime")
              ;

            builder.Property(x => x.ICE_DENSIDADADE_RELATIVA_001)
              .HasColumnType("float")
              .HasPrecision(8, 8);

            builder.Property(x => x.ICE_CORRECAO_BSW_001)
              .HasColumnType("float")
              .HasPrecision(8, 8);

            builder.Property(x => x.ICE_CORRECAO_PRESSAO_LIQUIDO_001)
              .HasColumnType("float")
              .HasPrecision(8, 8)
              ;

            builder.Property(x => x.ICE_CRRCO_TEMPERATURA_LIQUIDO_001)
              .HasColumnType("float")
              .HasPrecision(8, 8)
              ;

            builder.Property(x => x.MED_PRESSAO_ESTATICA_001)
              .HasColumnType("float")
              .HasPrecision(6, 6)
              ;

            builder.Property(x => x.MED_TMPTA_FLUIDO_001)
              .HasColumnType("float")
              .HasPrecision(5, 5)
              ;

            builder.Property(x => x.MED_VOLUME_BRTO_CRRGO_MVMDO_001)
              .HasColumnType("float")
              .HasPrecision(6, 5)
              ;

            builder.Property(x => x.MED_VOLUME_BRUTO_MVMDO_001)
              .HasColumnType("float")
              .HasPrecision(6, 5)
              ;

            builder.Property(x => x.MED_VOLUME_LIQUIDO_MVMDO_001)
              .HasColumnType("float")
              .HasPrecision(6, 5)
              ;

            builder.Property(x => x.MED_VOLUME_TTLZO_FIM_PRDO_001)
              .HasColumnType("float")
              .HasPrecision(10, 2)
              ;

            builder.Property(x => x.MED_VOLUME_TTLZO_INCO_PRDO_001)
              .HasColumnType("float")
              .HasPrecision(10, 2)
              ;
            #endregion

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(x => x.FileType)
                .WithMany(x => x.Measurements)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(x => x.Installation)
                .WithMany(x => x.Measurements)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Measurements)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

        }
    }
}
