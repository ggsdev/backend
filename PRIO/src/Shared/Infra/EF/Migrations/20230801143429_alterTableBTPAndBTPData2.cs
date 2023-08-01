using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class alterTableBTPAndBTPData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CellBSW",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CellRGO",
                table: "BTPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BSW",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RGO",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellBSW",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "CellRGO",
                table: "BTPs");

            migrationBuilder.DropColumn(
                name: "BSW",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "RGO",
                table: "BTPDatas");
        }
    }
}
