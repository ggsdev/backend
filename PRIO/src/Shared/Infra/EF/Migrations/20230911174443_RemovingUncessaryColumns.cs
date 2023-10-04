using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUncessaryColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EfficienceLossGas",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "EfficienceLossOil",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "EfficienceLossWater",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProportionalDayGas",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "EfficienceLossGas",
                table: "WellLosses");

            migrationBuilder.DropColumn(
                name: "EfficienceLossOil",
                table: "WellLosses");

            migrationBuilder.DropColumn(
                name: "EfficienceLossWater",
                table: "WellLosses");

            migrationBuilder.DropColumn(
                name: "ProportionalDayGas",
                table: "WellLosses");

            migrationBuilder.RenameColumn(
                name: "ProportionalDayWater",
                table: "WellProductions",
                newName: "ProportionalDay");

            migrationBuilder.RenameColumn(
                name: "ProportionalDayOil",
                table: "WellProductions",
                newName: "EfficienceLoss");

            migrationBuilder.RenameColumn(
                name: "ProportionalDayWater",
                table: "WellLosses",
                newName: "ProportionalDay");

            migrationBuilder.RenameColumn(
                name: "ProportionalDayOil",
                table: "WellLosses",
                newName: "EfficienceLoss");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProportionalDay",
                table: "WellProductions",
                newName: "ProportionalDayWater");

            migrationBuilder.RenameColumn(
                name: "EfficienceLoss",
                table: "WellProductions",
                newName: "ProportionalDayOil");

            migrationBuilder.RenameColumn(
                name: "ProportionalDay",
                table: "WellLosses",
                newName: "ProportionalDayWater");

            migrationBuilder.RenameColumn(
                name: "EfficienceLoss",
                table: "WellLosses",
                newName: "ProportionalDayOil");

            migrationBuilder.AddColumn<decimal>(
                name: "EfficienceLossGas",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EfficienceLossOil",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EfficienceLossWater",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProportionalDayGas",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EfficienceLossGas",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EfficienceLossOil",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EfficienceLossWater",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProportionalDayGas",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
