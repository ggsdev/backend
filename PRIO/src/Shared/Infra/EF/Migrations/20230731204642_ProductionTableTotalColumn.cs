using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class ProductionTableTotalColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalProduction",
                table: "Productions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOil",
                table: "Oils",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalProduction",
                table: "Productions");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOil",
                table: "Oils",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);
        }
    }
}
