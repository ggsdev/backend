using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingPrecisionToMatchUnitChangesOnGasTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWell",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWell",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWell",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWell",
                table: "WellProductions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWell",
                table: "WellProductions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWell",
                table: "WellProductions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);
        }
    }
}
