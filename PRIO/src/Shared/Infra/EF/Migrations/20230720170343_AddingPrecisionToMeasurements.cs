using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddingPrecisionToMeasurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_003",
                table: "Measurements",
                type: "decimal(8,4)",
                precision: 8,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,4)",
                oldPrecision: 4,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_002",
                table: "Measurements",
                type: "decimal(8,4)",
                precision: 8,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,4)",
                oldPrecision: 4,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_SUPERIOR_2_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_SUPERIOR_1_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_INFERIOR_2_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_INFERIOR_1_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_PROPANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_PROPANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OCTANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OCTANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NONANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NONANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_METANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_METANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEXANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEXANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HELIO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HELIO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_H2S_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_H2S_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ETANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ETANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_DECANO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_DECANO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO2_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO2_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_AGUA_003",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_AGUA_002",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_ADOTADO_CASO_FALHA_2_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_ADOTADO_CASO_FALHA_1_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_LIQUIDO_MVMDO_001",
                table: "Measurements",
                type: "decimal(11,5)",
                precision: 11,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRUTO_MVMDO_001",
                table: "Measurements",
                type: "decimal(11,5)",
                precision: 11,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRTO_CRRGO_MVMDO_001",
                table: "Measurements",
                type: "decimal(11,5)",
                precision: 11,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_001",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_003",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_FLUIDO_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_003",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_002",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_001",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_RFRNA_003",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_003",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_002",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_003",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_001",
                table: "Measurements",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_5_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_4_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_3_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_2_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_5_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_4_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_3_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_2_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_001",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_3_003",
                table: "Measurements",
                type: "decimal(12,3)",
                maxLength: 50,
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldMaxLength: 50,
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_2_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_1_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_001",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_001",
                table: "Measurements",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,6)",
                oldPrecision: 6,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)",
                oldPrecision: 3,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DMTRO_INTRO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(7,3)",
                precision: 7,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIFERENCIAL_PRESSAO_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIAMETRO_REFERENCIA_003",
                table: "Measurements",
                type: "decimal(7,3)",
                precision: 7,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)",
                oldPrecision: 4,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_002",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_001",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CUTOFF_KPA_2_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CUTOFF_KPA_1_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "decimal(11,5)",
                precision: 11,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_002",
                table: "Measurements",
                type: "decimal(11,5)",
                precision: 11,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_BRUTO_MOVIMENTADO_002",
                table: "Measurements",
                type: "decimal(11,5)",
                precision: 11,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,5)",
                oldPrecision: 6,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_DENSIDADE_RELATIVA_002",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_DENSIDADADE_RELATIVA_001",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CUTOFF_002",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CRRCO_TEMPERATURA_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CORRECAO_PRESSAO_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CORRECAO_BSW_001",
                table: "Measurements",
                type: "decimal(16,8)",
                precision: 16,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CE_LIMITE_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(12,3)",
                precision: 12,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_METER_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QTD_PULSOS_K_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_003",
                table: "Measurements",
                type: "decimal(4,4)",
                precision: 4,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,4)",
                oldPrecision: 8,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRZ_DURACAO_FLUXO_EFETIVO_002",
                table: "Measurements",
                type: "decimal(4,4)",
                precision: 4,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,4)",
                oldPrecision: 8,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_SUPERIOR_2_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_SUPERIOR_1_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_INFERIOR_2_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_LIMITE_INFERIOR_1_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_PROPANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_PROPANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OXIGENIO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OCTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_OCTANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_PENTANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_N_BUTANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NONANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NONANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_NITROGENIO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_METANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_METANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_PENTANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_I_BUTANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HIDROGENIO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEXANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEXANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HEPTANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HELIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_HELIO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_H2S_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_H2S_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ETANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ETANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_DECANO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_DECANO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO2_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_CO2_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_ARGONIO_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_AGUA_003",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_CROMATOGRAFIA_AGUA_002",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_ADOTADO_CASO_FALHA_2_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PCT_ADOTADO_CASO_FALHA_1_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_LIQUIDO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,5)",
                oldPrecision: 11,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRUTO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,5)",
                oldPrecision: 11,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_VOLUME_BRTO_CRRGO_MVMDO_001",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,5)",
                oldPrecision: 11,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_SPRR_ALARME_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_FLUIDO_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TMPTA_ADTTA_FALHA_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_2_002",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_003",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_1_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_TEMPERATURA_001",
                table: "Measurements",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_5_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_4_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_3_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LMTE_INFRR_ALRME_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_5_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_4_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_3_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_LIMITE_SPRR_ALRME_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
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
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldMaxLength: 50,
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRSO_ADOTADA_FALHA_001",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_RFRNA_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ESTATICA_001",
                table: "Measurements",
                type: "decimal(6,6)",
                precision: 6,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_003",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_002",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_PRESSAO_ATMSA_001",
                table: "Measurements",
                type: "decimal(3,3)",
                precision: 3,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DMTRO_INTRO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,3)",
                oldPrecision: 7,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIFERENCIAL_PRESSAO_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DIAMETRO_REFERENCIA_003",
                table: "Measurements",
                type: "decimal(4,3)",
                precision: 4,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,3)",
                oldPrecision: 7,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_002",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_DENSIDADE_RELATIVA_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CUTOFF_KPA_2_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CUTOFF_KPA_1_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,5)",
                oldPrecision: 11,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_002",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,5)",
                oldPrecision: 11,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_BRUTO_MOVIMENTADO_002",
                table: "Measurements",
                type: "decimal(6,5)",
                precision: 6,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,5)",
                oldPrecision: 11,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_METER_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_SPRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_LIMITE_INFRR_ALARME_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_9_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_9_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_8_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_8_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_7_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_7_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_6_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_6_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_5_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_5_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_4_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_4_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_3_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_3_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_2_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_2_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_1_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_15_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_15_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_14_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_14_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_13_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_13_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_12_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_12_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_11_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_11_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_10_002",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_K_FACTOR_10_001",
                table: "Measurements",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_DENSIDADE_RELATIVA_003",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_DENSIDADE_RELATIVA_002",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_DENSIDADADE_RELATIVA_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CUTOFF_002",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CRRCO_TEMPERATURA_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CORRECAO_PRESSAO_LIQUIDO_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ICE_CORRECAO_BSW_001",
                table: "Measurements",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,8)",
                oldPrecision: 16,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CE_LIMITE_SPRR_ALARME_003",
                table: "Measurements",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldPrecision: 12,
                oldScale: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);
        }
    }
}
