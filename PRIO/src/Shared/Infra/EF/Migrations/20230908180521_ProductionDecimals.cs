using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ProductionDecimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalProductionInField",
                table: "FieldsFRs",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsFRs",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsFRs",
                type: "decimal(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalProductionInField",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldPrecision: 20,
                oldScale: 5);
        }
    }
}
