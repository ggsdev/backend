using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class alterTableBTPAndBTPData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalProduction",
                table: "Productions",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "BTPSheet",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellMPointGas",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellMPointOil",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellMPointWater",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellPotencialLiquid",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellWellAlignmentData",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellWellAlignmentHour",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellWellName",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BTPSheet",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MPointGas",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MPointOil",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MPointWater",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PotencialLiquid",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WellAlignmentData",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WellAlignmentHour",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WellName",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BTPSheet",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellMPointGas",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellMPointOil",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellMPointWater",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellPotencialLiquid",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellWellAlignmentData",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellWellAlignmentHour",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellWellName",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "BTPSheet",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "MPointGas",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "MPointOil",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "MPointWater",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "PotencialLiquid",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "WellAlignmentData",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "WellAlignmentHour",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "WellName",
                table: "BTPDatas");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalProduction",
                table: "Productions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5);
        }
    }
}
