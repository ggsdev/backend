using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ColumnsWellLosses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EfficienceLoss",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProductionLost",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProportionalDay",
                table: "WellProductions");

            migrationBuilder.RenameColumn(
                name: "ProportionalDay",
                table: "WellLosses",
                newName: "ProportionalDayWater");

            migrationBuilder.RenameColumn(
                name: "ProductionLost",
                table: "WellLosses",
                newName: "ProportionalDayOil");

            migrationBuilder.RenameColumn(
                name: "EfficienceLoss",
                table: "WellLosses",
                newName: "ProportionalDayGas");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellM3",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellM3",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellSCF",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellM3",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(14,5)",
                oldPrecision: 14,
                oldScale: 5);

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
                name: "ProductionLostGas",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionLostOil",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionLostWater",
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
                name: "ProportionalDayOil",
                table: "WellProductions",
                type: "DECIMAL(22,5)",
                precision: 22,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProportionalDayWater",
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
                name: "ProductionLostGas",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionLostOil",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionLostWater",
                table: "WellLosses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ProductionLostGas",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProductionLostOil",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProductionLostWater",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProportionalDayGas",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProportionalDayOil",
                table: "WellProductions");

            migrationBuilder.DropColumn(
                name: "ProportionalDayWater",
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
                name: "ProductionLostGas",
                table: "WellLosses");

            migrationBuilder.DropColumn(
                name: "ProductionLostOil",
                table: "WellLosses");

            migrationBuilder.DropColumn(
                name: "ProductionLostWater",
                table: "WellLosses");

            migrationBuilder.RenameColumn(
                name: "ProportionalDayWater",
                table: "WellLosses",
                newName: "ProportionalDay");

            migrationBuilder.RenameColumn(
                name: "ProportionalDayOil",
                table: "WellLosses",
                newName: "ProductionLost");

            migrationBuilder.RenameColumn(
                name: "ProportionalDayGas",
                table: "WellLosses",
                newName: "EfficienceLoss");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellM3",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionWaterInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellM3",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionOilInWellBBL",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellSCF",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductionGasInWellM3",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(22,5)",
                oldPrecision: 22,
                oldScale: 5);

            migrationBuilder.AddColumn<decimal>(
                name: "EfficienceLoss",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionLost",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProportionalDay",
                table: "WellProductions",
                type: "DECIMAL(14,5)",
                precision: 14,
                scale: 5,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
