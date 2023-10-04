using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class DecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProportionalDay",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellM3",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostWater",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostOil",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostGas",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellSCF",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "EfficienceLoss",
                table: "WellProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWater",
                table: "Waters",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(20,5)",
                oldPrecision: 20,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInZone",
                table: "ZoneProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProportionalDay",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellM3",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostWater",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostOil",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostGas",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellSCF",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "EfficienceLoss",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWater",
                table: "Waters",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInReservoir",
                table: "ReservoirProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInCompletion",
                table: "CompletionProductions",
                type: "DECIMAL(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,16)",
                oldPrecision: 22,
                oldScale: 16);
        }
    }
}
