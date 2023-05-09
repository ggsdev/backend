using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prio_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Acronym = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    QtdColumns = table.Column<int>(type: "int", nullable: false),
                    Structure = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COD_INSTALACAO_040 = table.Column<int>(type: "int", nullable: false),
                    COD_TAG_PONTO_MEDICAO_040 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PCT_BSW_040 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_MAXIMO_BSW_040 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_MEDICAO_040 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NOM_BOLETIM_ANALISE_040 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_USER_CALCULO_040 = table.Column<bool>(type: "bit", nullable: false),
                    COD_INSTALACAO_041 = table.Column<int>(type: "int", nullable: false),
                    COD_TAG_PONTO_MEDICAO_041 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_ATUALIZACAO_041 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NUM_FATOR_ENCOLHIMENTO_041 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NOM_BOLETIM_ANALISE_041 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_TAG_EQUIPAMENTO_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_FALHA_SUPERIOR_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DSC_TIPO_FALHA_039 = table.Column<short>(type: "smallint", nullable: false),
                    COD_FALHA_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_TIPO_NOTIFICACAO_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_OCORRENCIA_039 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_DETECCAO_039 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_RETORNO_039 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_NUM_PREVISAO_RETORNO_DIAS_039 = table.Column<short>(type: "smallint", nullable: false),
                    DHA_DSC_FALHA_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_DSC_ACAO_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_DSC_METODOLOGIA_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_NOM_RESPONSAVEL_RELATO_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_NUM_SERIE_EQUIPAMENTO_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_COD_INSTALACAO_039 = table.Column<int>(type: "int", nullable: false),
                    DHA_FALHA_BSW_039 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_PCT_BSW_039 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_PCT_MAXIMO_BSW_039 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_MEDICAO_039 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_MED_DECLARADO_039 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_MED_REGISTRADO_039 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_FALHA_CALIBRACAO_039 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_CERTIFICADO_ANTERIOR_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_CERTIFICADO_ATUAL_039 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_INSTALACAO_045 = table.Column<int>(type: "int", nullable: false),
                    COD_TAG_PONTO_MEDICAO_045 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TAG_TANQUE_045 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_045 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_CAPACIDADE_LIQUIDA_045 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_CAPACIDADE_BRUTA_045 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_CAPACIDADE_CORRIGIDA_045 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_MEDICAO_045 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NOM_RELATORIO_BSW_045 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_NAVIO_045 = table.Column<bool>(type: "bit", nullable: false),
                    NUM_SERIE_ELEMENTO_PRIMARIO_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_INSTALACAO_003 = table.Column<int>(type: "int", nullable: false),
                    COD_TAG_PONTO_MEDICAO_003 = table.Column<short>(type: "smallint", nullable: false),
                    NUM_SERIE_COMPUTADOR_VAZAO_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_COLETA_003 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MED_TEMPERATURA_1_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_ATMSA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_RFRNA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_DENSIDADE_RELATIVA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_NORMA_UTILIZADA_CALCULO_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PCT_CROMATOGRAFIA_NITROGENIO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_CO2_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_METANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_ETANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_PROPANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_N_BUTANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_I_BUTANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_N_PENTANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_I_PENTANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HEXANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HEPTANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_OCTANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_NONANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_DECANO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_H2S_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_AGUA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HELIO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_OXIGENIO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_CO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HIDROGENIO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_ARGONIO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_VERSAO_SOFTWARE_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CE_LIMITE_SPRR_ALARME_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_LIMITE_INFRR_ALARME_1_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_LIMITE_INFRR_ALARME_2_003 = table.Column<bool>(type: "bit", nullable: false),
                    NUM_SERIE_1_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_PRSO_LIMITE_SPRR_ALRME_1_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRSO_LMTE_INFRR_ALRME_1_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_1_003 = table.Column<bool>(type: "bit", nullable: false),
                    MED_PRSO_ADOTADA_FALHA_1_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSNO_CASO_FALHA_1_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_TIPO_PRESSAO_CONSIDERADA_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_2_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_TMPTA_SPRR_ALARME_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TMPTA_INFRR_ALRME_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_2_003 = table.Column<bool>(type: "bit", nullable: false),
                    MED_TMPTA_ADTTA_FALHA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSTRUMENTO_FALHA_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_DIAMETRO_REFERENCIA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TEMPERATURA_RFRNA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_MATERIAL_CONTRUCAO_PLACA_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_DMTRO_INTRO_TRCHO_MDCO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TMPTA_TRCHO_MDCO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_MATERIAL_CNSTO_TRCHO_MDCO_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DSC_LCLZO_TMDA_PRSO_DFRNL_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_TOMADA_PRESSAO_ESTATICA_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_3_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_PRSO_LIMITE_SPRR_ALRME_2_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRSO_LMTE_INFRR_ALRME_2_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NUM_SERIE_4_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_PRSO_LIMITE_SPRR_ALRME_3_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRSO_LMTE_INFRR_ALRME_3_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NUM_SERIE_5_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_PRSO_LIMITE_SPRR_ALRME_4_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRSO_LMTE_INFRR_ALRME_4_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_3_003 = table.Column<bool>(type: "bit", nullable: false),
                    MED_PRSO_ADOTADA_FALHA_2_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSNO_CASO_FALHA_2_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_CUTOFF_KPA_1_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NUM_SERIE_6_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_PRSO_LIMITE_SPRR_ALRME_5_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRSO_LMTE_INFRR_ALRME_5_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_4_003 = table.Column<bool>(type: "bit", nullable: false),
                    MED_PRSO_ADOTADA_FALHA_3_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSNO_CASO_FALHA_3_003 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_CUTOFF_KPA_2_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DHA_INICIO_PERIODO_MEDICAO_003 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_FIM_PERIODO_MEDICAO_003 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ICE_DENSIDADE_RELATIVA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_DIFERENCIAL_PRESSAO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_ESTATICA_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TEMPERATURA_2_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PRZ_DURACAO_FLUXO_EFETIVO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_CORRIGIDO_MVMDO_003 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NUM_SERIE_ELEMENTO_PRIMARIO_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_INSTALACAO_002 = table.Column<int>(type: "int", nullable: false),
                    COD_TAG_PONTO_MEDICAO_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_COMPUTADOR_VAZAO_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_COLETA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TEMPERATURA_1_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_ATMSA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_RFRNA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_DENSIDADE_RELATIVA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_NORMA_UTILIZADA_CALCULO_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PCT_CROMATOGRAFIA_NITROGENIO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_CO2_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_METANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_ETANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_PROPANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_N_BUTANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_I_BUTANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_N_PENTANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_I_PENTANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HEXANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HEPTANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_OCTANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_NONANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_DECANO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_H2S_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_AGUA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HELIO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_OXIGENIO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_CO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_HIDROGENIO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_CROMATOGRAFIA_ARGONIO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_VERSAO_SOFTWARE_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICE_METER_FACTOR_1_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_2_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_3_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_4_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_5_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_6_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_7_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_8_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_9_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_10_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_11_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_12_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_13_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_14_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_15_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_1_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_2_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_3_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_4_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_5_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_6_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_7_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_8_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_9_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_10_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_11_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_12_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_13_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_14_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_15_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_1_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_2_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_3_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_4_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_5_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_6_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_7_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_8_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_9_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_10_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_11_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_12_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_13_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_14_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_15_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_1_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_2_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_3_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_4_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_5_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_6_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_7_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_8_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_9_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_10_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_11_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_12_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_13_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_14_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_15_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_CUTOFF_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_LIMITE_SPRR_ALARME_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_LIMITE_INFRR_ALARME_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_1_002 = table.Column<bool>(type: "bit", nullable: false),
                    NUM_SERIE_1_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_PRSO_LIMITE_SPRR_ALRME_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRSO_LMTE_INFRR_ALRME_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_2_002 = table.Column<bool>(type: "bit", nullable: false),
                    MED_PRSO_ADOTADA_FALHA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSNO_CASO_FALHA_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_TIPO_PRESSAO_CONSIDERADA_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_2_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_TMPTA_SPRR_ALARME_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TMPTA_INFRR_ALRME_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_3_002 = table.Column<bool>(type: "bit", nullable: false),
                    MED_TMPTA_ADTTA_FALHA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSTRUMENTO_FALHA_002 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_INICIO_PERIODO_MEDICAO_002 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_FIM_PERIODO_MEDICAO_002 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ICE_DENSIDADE_RELATIVA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_ESTATICA_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TEMPERATURA_2_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PRZ_DURACAO_FLUXO_EFETIVO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_BRUTO_MOVIMENTADO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_CORRIGIDO_MVMDO_002 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NUM_SERIE_ELEMENTO_PRIMARIO_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_INSTALACAO_001 = table.Column<int>(type: "int", nullable: false),
                    COD_TAG_PONTO_MEDICAO_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_COMPUTADOR_VAZAO_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_COLETA_001 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MED_TEMPERATURA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_ATMSA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_RFRNA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_DENSIDADE_RELATIVA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_VERSAO_SOFTWARE_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICE_METER_FACTOR_1_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_2_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_3_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_4_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_5_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_6_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_7_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_8_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_9_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_10_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_11_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_12_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_13_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_14_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_METER_FACTOR_15_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_1_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_2_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_3_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_4_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_5_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_6_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_7_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_8_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_9_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_10_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_11_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_12_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_13_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_14_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_METER_FACTOR_15_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_1_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_2_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_3_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_4_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_5_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_6_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_7_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_8_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_9_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_10_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_11_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_12_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_13_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_14_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_K_FACTOR_15_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_1_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_2_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_3_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_4_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_5_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_6_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_7_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_8_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_9_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_10_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_11_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_12_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_13_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_14_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QTD_PULSOS_K_FACTOR_15_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_CUTOFF_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_LIMITE_SPRR_ALARME_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_LIMITE_INFRR_ALARME_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_1_001 = table.Column<bool>(type: "bit", nullable: false),
                    NUM_SERIE_1_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_PRSO_LIMITE_SPRR_ALRME_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRSO_LMTE_INFRR_ALRME_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_2_001 = table.Column<bool>(type: "bit", nullable: false),
                    MED_PRSO_ADOTADA_FALHA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSNO_CASO_FALHA_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_TIPO_PRESSAO_CONSIDERADA_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_2_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_TMPTA_SPRR_ALARME_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TMPTA_INFRR_ALRME_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_3_001 = table.Column<bool>(type: "bit", nullable: false),
                    MED_TMPTA_ADTTA_FALHA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSTRUMENTO_FALHA_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_SERIE_3_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PCT_LIMITE_SUPERIOR_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_LIMITE_INFERIOR_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_4_001 = table.Column<bool>(type: "bit", nullable: false),
                    PCT_ADOTADO_CASO_FALHA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NUM_SERIE_4_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PCT_LIMITE_SUPERIOR_2_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_LIMITE_INFERIOR_2_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IND_HABILITACAO_ALARME_5_001 = table.Column<bool>(type: "bit", nullable: false),
                    PCT_ADOTADO_CASO_FALHA_2_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSC_ESTADO_INSTRUMENTO_FALHA_3_001 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_INICIO_PERIODO_MEDICAO_001 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_FIM_PERIODO_MEDICAO_001 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ICE_DENSIDADADE_RELATIVA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_CORRECAO_BSW_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_CORRECAO_PRESSAO_LIQUIDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ICE_CRRCO_TEMPERATURA_LIQUIDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_PRESSAO_ESTATICA_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_TMPTA_FLUIDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_VOLUME_BRTO_CRRGO_MVMDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_VOLUME_BRUTO_MVMDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_VOLUME_LIQUIDO_MVMDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_VOLUME_TTLZO_FIM_PRDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_VOLUME_TTLZO_INCO_PRDO_001 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    COD_CADASTRO_POCO_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IND_TIPO_TESTE_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DHA_TESTE_042 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DHA_APLICACAO_042 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IND_VALIDO_042 = table.Column<bool>(type: "bit", nullable: false),
                    MED_POTENCIAL_OLEO_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_POTENCIAL_GAS_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_POTENCIAL_AGUA_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NOM_RELATORIO_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MED_VAZAO_OLEO_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_VAZAO_GAS_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MED_VAZAO_AGUA_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PCT_BSW_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NOM_RELATORIO_BSW_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUM_FATOR_ENCOLHIMENTO_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NOM_RELATORIO_FATOR_ENCLO_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PCT_RAZAO_SOLUBILIDADE_042 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NOM_RELATORIO_RZO_SOLUBILIDADE_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_INSTALACAO_042 = table.Column<int>(type: "int", nullable: false),
                    COD_TAG_PONTO_MEDICAO_OLEO_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COD_TAG_PONTO_MEDICAO_GAS_042 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_FileTypes_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: false),
                    Username = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Sessions_UserId",
                        column: x => x.UserId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clusters_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clusters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodField = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Acronym = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Basin = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Situation = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    WaterDepth = table.Column<decimal>(type: "DECIMAL(6,2)", precision: 6, scale: 2, nullable: false),
                    CorrectedArea = table.Column<decimal>(type: "DECIMAL(6,2)", precision: 6, scale: 2, nullable: false),
                    MainFluid = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    APIGradeOil = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    CalorificPowerGas = table.Column<decimal>(type: "DECIMAL(8,2)", precision: 8, scale: 2, nullable: false),
                    ContractNum = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    ContractOperator = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    ContractType = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    ContractTypeDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Round = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    RoundDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Original = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Location = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    EnviromentDepth = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    DiscoveryDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    ProductionBeginning = table.Column<DateTime>(type: "DATE", nullable: false),
                    Commerciality = table.Column<DateTime>(type: "DATE", nullable: false),
                    ProductionFinishForecast = table.Column<DateTime>(type: "DATE", nullable: false),
                    ProductionFinishDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    QtdWells = table.Column<int>(type: "int", nullable: false),
                    PreSaltWells = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fields_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Installations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodInstallation = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Acronym = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Operator = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Owner = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Environment = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    WaterDepth = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: false),
                    City = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    FieldService = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Latitude = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Longitude = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    GasProcessing = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    OilProcessing = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    BeginningValidity = table.Column<DateTime>(type: "DATE", nullable: false),
                    InclusionDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    PsmQty = table.Column<int>(type: "int", nullable: false),
                    Situation = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installations_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Installations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservoirs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InstallationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservoirs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservoirs_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservoirs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Completions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Completions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Completions_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Completions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAnp = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    RegisterNum = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    WellOperatorName = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    FieldName = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CompanyCodOperator = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Basin = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    DrillingStartDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    DrillingFinishDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    Location = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    WaterDepth = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    SounderDepth = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    InstallationName = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    EnviromentProduction = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Block = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    ClusterName = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CodInstallation = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    ReservoirName = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    ProductionByReservoir = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CompletionName = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    TopOfPerforated = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    BaseOfPerforated = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    MD = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    TVD = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    ArtificialElevation = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CoordX = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CoordY = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    FieldCod = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CurrentSituation = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Latitude = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Longitude = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompletionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wells_Completions_CompletionId",
                        column: x => x.CompletionId,
                        principalTable: "Completions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wells_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_UnitId",
                table: "Clusters",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_UserId",
                table: "Clusters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_ReservoirId",
                table: "Completions",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_UserId",
                table: "Completions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_ClusterId",
                table: "Fields",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_UserId",
                table: "Fields",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_FieldId",
                table: "Installations",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_UserId",
                table: "Installations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_FileTypeId",
                table: "Measurements",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_InstallationId",
                table: "Reservoirs",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_UserId",
                table: "Reservoirs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UserId",
                table: "Units",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wells_CompletionId",
                table: "Wells",
                column: "CompletionId");

            migrationBuilder.CreateIndex(
                name: "IX_Wells_UserId",
                table: "Wells",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Wells");

            migrationBuilder.DropTable(
                name: "FileTypes");

            migrationBuilder.DropTable(
                name: "Completions");

            migrationBuilder.DropTable(
                name: "Reservoirs");

            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
