using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class NullableDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TAG_TANQUE_045",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_003",
                table: "Measurements",
                type: "decimal(4,4)",
                precision: 4,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,4)",
                oldPrecision: 4,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_002",
                table: "Measurements",
                type: "decimal(4,4)",
                precision: 4,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,4)",
                oldPrecision: 4,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_RAZAO_SOLUBILIDADE_042",
                table: "Measurements",
                type: "decimal(5,4)",
                precision: 5,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)",
                oldPrecision: 5,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_MAXIMO_BSW_040",
                table: "Measurements",
                type: "decimal(3,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_PROPANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OCTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NONANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_METANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEXANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HELIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_H2S_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ETANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_DECANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO2_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_AGUA_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_BSW_042",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_BSW_040",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_ELEMENTO_PRIMARIO_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_ELEMENTO_PRIMARIO_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_ELEMENTO_PRIMARIO_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_COMPUTADOR_VAZAO_003",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_COMPUTADOR_VAZAO_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_COMPUTADOR_VAZAO_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_6_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_5_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_4_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_4_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_3_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_3_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_2_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_2_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_2_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_1_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_1_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_1_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_045",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_FATOR_ENCOLHIMENTO_042",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_FATOR_ENCOLHIMENTO_041",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_TTLZO_INCO_PRDO_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_TTLZO_FIM_PRDO_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_LIQUIDO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRUTO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRTO_CRRGO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_OLEO_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_GAS_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_AGUA_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_FLUIDO_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_001",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_OLEO_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_GAS_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_AGUA_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DMTRO_INTRO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIFERENCIAL_PRESSAO_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIAMETRO_REFERENCIA_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_002",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_002",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_LIQUIDA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_CORRIGIDA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_BRUTA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_BRUTO_MOVIMENTADO_002",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_VALIDO_042",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_USER_CALCULO_040",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IND_TOMADA_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_TESTE_042",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_PRESSAO_CONSIDERADA_003",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_PRESSAO_CONSIDERADA_002",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_PRESSAO_CONSIDERADA_001",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_NOTIFICACAO_039",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_NAVIO_045",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_3_002",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_3_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_002",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_002",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_SPRR_ALARME_001",
                table: "Measurements",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<bool>(
                name: "ICE_LIMITE_INFRR_ALARME_2_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_001",
                table: "Measurements",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CUTOFF_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CUTOFF_001",
                table: "Measurements",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CRRCO_TEMPERATURA_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CORRECAO_PRESSAO_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_VERSAO_SOFTWARE_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_VERSAO_SOFTWARE_002",
                table: "Measurements",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "DSC_VERSAO_SOFTWARE_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<short>(
                name: "DSC_TIPO_FALHA_039",
                table: "Measurements",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "DSC_NORMA_UTILIZADA_CALCULO_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CONTRUCAO_PLACA_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CNSTO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_LCLZO_TMDA_PRSO_DFRNL_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSTRUMENTO_FALHA_1_001",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSTRUMENTO_FALHA_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSTRUMENTO_FALHA_002",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSNO_CASO_FALHA_1_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSNO_CASO_FALHA_002",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSNO_CASO_FALHA_001",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_TESTE_042",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_OCORRENCIA_039",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<short>(
                name: "DHA_NUM_PREVISAO_RETORNO_DIAS_039",
                table: "Measurements",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "DHA_NOM_RESPONSAVEL_RELATO_039",
                table: "Measurements",
                type: "varchar(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(155)",
                oldMaxLength: 155);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_MEDICAO_045",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_MEDICAO_040",
                table: "Measurements",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_INICIO_PERIODO_MEDICAO_003",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_INICIO_PERIODO_MEDICAO_002",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_INICIO_PERIODO_MEDICAO_001",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FIM_PERIODO_MEDICAO_003",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FIM_PERIODO_MEDICAO_002",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FIM_PERIODO_MEDICAO_001",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "DHA_DSC_METODOLOGIA_039",
                table: "Measurements",
                type: "varchar(3000)",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldMaxLength: 3000);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_DSC_FALHA_039",
                table: "Measurements",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_DSC_ACAO_039",
                table: "Measurements",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_DETECCAO_039",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_COLETA_003",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_COLETA_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_COLETA_001",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<int>(
                name: "DHA_COD_INSTALACAO_039",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_ATUALIZACAO_041",
                table: "Measurements",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_APLICACAO_042",
                table: "Measurements",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_OLEO_042",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_GAS_042",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_045",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_041",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_040",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<short>(
                name: "COD_TAG_PONTO_MEDICAO_003",
                table: "Measurements",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_002",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_001",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_045",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_042",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_041",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_040",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_003",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_002",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_001",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "COD_FALHA_039",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "COD_CADASTRO_POCO_042",
                table: "Measurements",
                type: "varchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<decimal>(
                name: "CE_LIMITE_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TAG_TANQUE_045",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_003",
                table: "Measurements",
                type: "decimal(4,4)",
                precision: 4,
                scale: 4,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,4)",
                oldPrecision: 4,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_002",
                table: "Measurements",
                type: "decimal(4,4)",
                precision: 4,
                scale: 4,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,4)",
                oldPrecision: 4,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_RAZAO_SOLUBILIDADE_042",
                table: "Measurements",
                type: "decimal(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)",
                oldPrecision: 5,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_MAXIMO_BSW_040",
                table: "Measurements",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_PROPANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OCTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NONANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_METANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEXANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HELIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_H2S_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ETANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_DECANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO2_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_AGUA_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_BSW_042",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_BSW_040",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_ELEMENTO_PRIMARIO_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_ELEMENTO_PRIMARIO_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_ELEMENTO_PRIMARIO_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_COMPUTADOR_VAZAO_003",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_COMPUTADOR_VAZAO_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_COMPUTADOR_VAZAO_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_6_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_5_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_4_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_4_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_3_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_3_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_2_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_2_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_2_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_1_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_1_002",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_1_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NUM_SERIE_045",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_FATOR_ENCOLHIMENTO_042",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_FATOR_ENCOLHIMENTO_041",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_TTLZO_INCO_PRDO_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_TTLZO_FIM_PRDO_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_LIQUIDO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRUTO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRTO_CRRGO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_OLEO_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_GAS_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VAZAO_AGUA_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_FLUIDO_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_001",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_OLEO_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_GAS_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_POTENCIAL_AGUA_042",
                table: "Measurements",
                type: "decimal(8,5)",
                precision: 8,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,5)",
                oldPrecision: 8,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DMTRO_INTRO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIFERENCIAL_PRESSAO_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIAMETRO_REFERENCIA_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_002",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_002",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_LIQUIDA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_CORRIGIDA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CAPACIDADE_BRUTA_045",
                table: "Measurements",
                type: "decimal(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_BRUTO_MOVIMENTADO_002",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_VALIDO_042",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_USER_CALCULO_040",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TOMADA_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_TESTE_042",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_PRESSAO_CONSIDERADA_003",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_PRESSAO_CONSIDERADA_002",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_PRESSAO_CONSIDERADA_001",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_TIPO_NOTIFICACAO_039",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_NAVIO_045",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_3_002",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_3_001",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_003",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_002",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_001",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_003",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_002",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_001",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_SPRR_ALARME_001",
                table: "Measurements",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ICE_LIMITE_INFRR_ALARME_2_003",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_001",
                table: "Measurements",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CUTOFF_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CUTOFF_001",
                table: "Measurements",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CRRCO_TEMPERATURA_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CORRECAO_PRESSAO_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_VERSAO_SOFTWARE_003",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_VERSAO_SOFTWARE_002",
                table: "Measurements",
                type: "varchar",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_VERSAO_SOFTWARE_001",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "DSC_TIPO_FALHA_039",
                table: "Measurements",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_NORMA_UTILIZADA_CALCULO_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CONTRUCAO_PLACA_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CNSTO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_LCLZO_TMDA_PRSO_DFRNL_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSTRUMENTO_FALHA_1_001",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSTRUMENTO_FALHA_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSTRUMENTO_FALHA_002",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSNO_CASO_FALHA_1_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSNO_CASO_FALHA_002",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_ESTADO_INSNO_CASO_FALHA_001",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_TESTE_042",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_OCORRENCIA_039",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "DHA_NUM_PREVISAO_RETORNO_DIAS_039",
                table: "Measurements",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_NOM_RESPONSAVEL_RELATO_039",
                table: "Measurements",
                type: "varchar(155)",
                maxLength: 155,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(155)",
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_MEDICAO_045",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_MEDICAO_040",
                table: "Measurements",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_INICIO_PERIODO_MEDICAO_003",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_INICIO_PERIODO_MEDICAO_002",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_INICIO_PERIODO_MEDICAO_001",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FIM_PERIODO_MEDICAO_003",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FIM_PERIODO_MEDICAO_002",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FIM_PERIODO_MEDICAO_001",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_DSC_METODOLOGIA_039",
                table: "Measurements",
                type: "varchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_DSC_FALHA_039",
                table: "Measurements",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_DSC_ACAO_039",
                table: "Measurements",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_DETECCAO_039",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_COLETA_003",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_COLETA_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_COLETA_001",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DHA_COD_INSTALACAO_039",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_ATUALIZACAO_041",
                table: "Measurements",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_APLICACAO_042",
                table: "Measurements",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_OLEO_042",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_GAS_042",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_045",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_041",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_040",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "COD_TAG_PONTO_MEDICAO_003",
                table: "Measurements",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_002",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_001",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_045",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_042",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_041",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_040",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_003",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_002",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_INSTALACAO_001",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_FALHA_039",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COD_CADASTRO_POCO_042",
                table: "Measurements",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CE_LIMITE_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);
        }
    }
}
