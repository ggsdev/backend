using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class WellLossesMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquidPerHour",
                table: "WellTests",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquid",
                table: "WellTests",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProportionalDay",
                table: "WellLosses",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostWater",
                table: "WellLosses",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostOil",
                table: "WellLosses",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostGas",
                table: "WellLosses",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EfficienceLoss",
                table: "WellLosses",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Downtime",
                table: "WellLosses",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BswMax",
                table: "NFSMsProductions",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Bsw",
                table: "NFSMsProductions",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeAfterManualBsw_001",
                table: "Measurements",
                type: "decimal(22,5)",
                precision: 22,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquidPerHour",
                table: "WellTests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquid",
                table: "WellTests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProportionalDay",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostWater",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostOil",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLostGas",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "EfficienceLoss",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "Downtime",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "BswMax",
                table: "NFSMsProductions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Bsw",
                table: "NFSMsProductions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeAfterManualBsw_001",
                table: "Measurements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(22,5)",
                oldPrecision: 22,
                oldScale: 5,
                oldNullable: true);
        }
    }
}
