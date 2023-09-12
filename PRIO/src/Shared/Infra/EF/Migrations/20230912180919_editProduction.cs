using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class editProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellM3",
                table: "WellProductions",
                type: "decimal(22,16)",
                precision: 22,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellM3",
                table: "WellProductions",
                type: "DECIMAL(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellM3",
                table: "WellProductions",
                type: "DECIMAL(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(22,16)",
                oldPrecision: 22,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellM3",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,0)");
        }
    }
}
