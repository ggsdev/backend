using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class EnhancingDecimalsPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalProduction",
                table: "Productions",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOilWithoutBsw",
                table: "Oils",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOil",
                table: "Oils",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeBefore",
                table: "NFSMsProductions",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeAfter",
                table: "NFSMsProductions",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_002",
                table: "Measurements",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_BRUTO_MOVIMENTADO_002",
                table: "Measurements",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalGas",
                table: "GasesLinears",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ImportedGas",
                table: "GasesLinears",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelGas",
                table: "GasesLinears",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExportedGas",
                table: "GasesLinears",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "BurntGas",
                table: "GasesLinears",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalGas",
                table: "GasesDiferencials",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ImportedGas",
                table: "GasesDiferencials",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelGas",
                table: "GasesDiferencials",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExportedGas",
                table: "GasesDiferencials",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "BurntGas",
                table: "GasesDiferencials",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WellTestBurn",
                table: "Gases",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "VentedGas",
                table: "Gases",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ScheduledStopBurn",
                table: "Gases",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OthersBurn",
                table: "Gases",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "LimitOperacionalBurn",
                table: "Gases",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ForCommissioningBurn",
                table: "Gases",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "EmergencialBurn",
                table: "Gases",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalProduction",
                table: "Productions",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOilWithoutBsw",
                table: "Oils",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOil",
                table: "Oils",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeBefore",
                table: "NFSMsProductions",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeAfter",
                table: "NFSMsProductions",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_003",
                table: "Measurements",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_CORRIGIDO_MVMDO_002",
                table: "Measurements",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MED_BRUTO_MOVIMENTADO_002",
                table: "Measurements",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalGas",
                table: "GasesLinears",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ImportedGas",
                table: "GasesLinears",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelGas",
                table: "GasesLinears",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExportedGas",
                table: "GasesLinears",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "BurntGas",
                table: "GasesLinears",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalGas",
                table: "GasesDiferencials",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ImportedGas",
                table: "GasesDiferencials",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelGas",
                table: "GasesDiferencials",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExportedGas",
                table: "GasesDiferencials",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "BurntGas",
                table: "GasesDiferencials",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WellTestBurn",
                table: "Gases",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "VentedGas",
                table: "Gases",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ScheduledStopBurn",
                table: "Gases",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OthersBurn",
                table: "Gases",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "LimitOperacionalBurn",
                table: "Gases",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ForCommissioningBurn",
                table: "Gases",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "EmergencialBurn",
                table: "Gases",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);
        }
    }
}
