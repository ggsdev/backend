using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTypesToMatchXml : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_003",
                table: "Measurements",
                type: "float(4)",
                precision: 4,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,4)",
                oldPrecision: 4,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_RAZAO_SOLUBILIDADE_042",
                table: "Measurements",
                type: "float(5)",
                precision: 5,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)",
                oldPrecision: 5,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_MAXIMO_BSW_040",
                table: "Measurements",
                type: "float(3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_PROPANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_OCTANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_NONANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_METANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_HEXANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_HELIO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_H2S_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_ETANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_DECANO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_CO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_CO2_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_CROMATOGRAFIA_AGUA_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_BSW_042",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PCT_BSW_040",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NUM_FATOR_ENCOLHIMENTO_042",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NUM_FATOR_ENCOLHIMENTO_041",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_VAZAO_OLEO_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_VAZAO_GAS_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_VAZAO_AGUA_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_TMPTA_TRCHO_MDCO_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_TMPTA_SPRR_ALARME_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_TMPTA_INFRR_ALRME_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_TMPTA_ADTTA_FALHA_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_TEMPERATURA_RFRNA_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_TEMPERATURA_2_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_TEMPERATURA_1_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_5_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_4_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_3_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_2_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_1_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_5_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_4_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_3_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_2_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_1_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_ADOTADA_FALHA_3_003",
                table: "Measurements",
                type: "float(6)",
                maxLength: 50,
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldMaxLength: 50,
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_ADOTADA_FALHA_2_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRSO_ADOTADA_FALHA_1_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRESSAO_RFRNA_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_PRESSAO_ATMSA_003",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_POTENCIAL_OLEO_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_POTENCIAL_GAS_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_POTENCIAL_AGUA_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_DMTRO_INTRO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "float(4)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_DIFERENCIAL_PRESSAO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_DIAMETRO_REFERENCIA_003",
                table: "Measurements",
                type: "float(4)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_CUTOFF_KPA_2_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_CUTOFF_KPA_1_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_CAPACIDADE_LIQUIDA_045",
                table: "Measurements",
                type: "float(7)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_CAPACIDADE_CORRIGIDA_045",
                table: "Measurements",
                type: "float(7)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MED_CAPACIDADE_BRUTA_045",
                table: "Measurements",
                type: "float(7)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_5_003",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ICE_LIMITE_INFRR_ALARME_1_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ICE_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_COD_INSTALACAO_039",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_003",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_INSTALACAO_045",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_INSTALACAO_042",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_INSTALACAO_041",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_INSTALACAO_040",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_INSTALACAO_003",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_INSTALACAO_002",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_INSTALACAO_001",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CE_LIMITE_SPRR_ALARME_003",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_003",
                table: "Measurements",
                type: "decimal(4,4)",
                precision: 4,
                scale: 4,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(4)",
                oldPrecision: 4,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_RAZAO_SOLUBILIDADE_042",
                table: "Measurements",
                type: "decimal(5,4)",
                precision: 5,
                scale: 4,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(5)",
                oldPrecision: 5,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_MAXIMO_BSW_040",
                table: "Measurements",
                type: "decimal(3,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_PROPANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OCTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NONANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_METANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEXANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HELIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_H2S_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ETANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_DECANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO2_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_AGUA_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_BSW_042",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_BSW_040",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_FATOR_ENCOLHIMENTO_042",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_FATOR_ENCOLHIMENTO_041",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_OLEO_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_GAS_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_AGUA_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_5_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_4_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_3_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_5_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_4_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_3_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_3_003",
                table: "Measurements",
                type: "decimal(6,3)",
                maxLength: 50,
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldMaxLength: 50,
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_OLEO_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_GAS_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_AGUA_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DMTRO_INTRO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(4)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIFERENCIAL_PRESSAO_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIAMETRO_REFERENCIA_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(4)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CUTOFF_KPA_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CUTOFF_KPA_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_LIQUIDA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(7)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_CORRIGIDA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(7)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_BRUTA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(7)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_5_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DHA_COD_INSTALACAO_039",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "COD_TAG_PONTO_MEDICAO_003",
                table: "Measurements",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_045",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_042",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_041",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_040",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_003",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_002",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_001",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CE_LIMITE_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(6)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);
        }
    }
}
