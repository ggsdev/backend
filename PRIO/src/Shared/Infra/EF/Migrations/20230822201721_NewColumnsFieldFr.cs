using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnsFieldFr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionInField",
                table: "FieldsFRs",
                newName: "TotalProductionInField");

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsProductions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GasProductionInField",
                table: "FieldsFRs");

            migrationBuilder.DropColumn(
                name: "OilProductionInField",
                table: "FieldsFRs");

            migrationBuilder.RenameColumn(
                name: "TotalProductionInField",
                table: "FieldsFRs",
                newName: "ProductionInField");

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterProductionInField",
                table: "FieldsProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "OilProductionInField",
                table: "FieldsProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasProductionInField",
                table: "FieldsProductions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);
        }
    }
}
