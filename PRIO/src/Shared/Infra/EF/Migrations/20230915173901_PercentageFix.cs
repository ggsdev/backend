using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class PercentageFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfInstallation",
                table: "WellProductions",
                type: "DECIMAL(38,36)",
                precision: 38,
                scale: 36,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfField",
                table: "WellProductions",
                type: "DECIMAL(38,36)",
                precision: 38,
                scale: 36,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfInstallation",
                table: "WellProductions",
                type: "DECIMAL(38,36)",
                precision: 38,
                scale: 36,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfField",
                table: "WellProductions",
                type: "DECIMAL(38,36)",
                precision: 38,
                scale: 36,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfInstallation",
                table: "WellProductions",
                type: "DECIMAL(38,36)",
                precision: 38,
                scale: 36,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfField",
                table: "WellProductions",
                type: "DECIMAL(38,36)",
                precision: 38,
                scale: 36,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(7,5)",
                oldPrecision: 7,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfInstallation",
                table: "WellProductions",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,36)",
                oldPrecision: 38,
                oldScale: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterAsPercentageOfField",
                table: "WellProductions",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,36)",
                oldPrecision: 38,
                oldScale: 36);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfInstallation",
                table: "WellProductions",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,36)",
                oldPrecision: 38,
                oldScale: 36);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilAsPercentageOfField",
                table: "WellProductions",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,36)",
                oldPrecision: 38,
                oldScale: 36);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfInstallation",
                table: "WellProductions",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,36)",
                oldPrecision: 38,
                oldScale: 36);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasAsPercentageOfField",
                table: "WellProductions",
                type: "DECIMAL(7,5)",
                precision: 7,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,36)",
                oldPrecision: 38,
                oldScale: 36);
        }
    }
}
