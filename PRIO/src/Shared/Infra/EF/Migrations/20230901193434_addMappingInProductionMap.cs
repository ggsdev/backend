using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class addMappingInProductionMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProportionalDay",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLost",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellSCF",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EfficienceLoss",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Downtime",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProportionalDay",
                table: "WellProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellBBL",
                table: "WellProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellBBL",
                table: "WellProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionLost",
                table: "WellProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellSCF",
                table: "WellProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "EfficienceLoss",
                table: "WellProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Downtime",
                table: "WellProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);
        }
    }
}
