using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class AddingPrecisionToProductionPotencialWellApprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(4,2)",
                oldPrecision: 4,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(4,2)",
                oldPrecision: 4,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);
        }
    }
}
