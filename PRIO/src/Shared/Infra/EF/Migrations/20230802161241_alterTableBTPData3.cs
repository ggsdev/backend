using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class alterTableBTPData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationDate",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "BTPDatas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PotencialGasPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PotencialLiquidPerHour",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PotencialOilPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PotencialWaterPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationDate",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "PotencialGasPerHour",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "PotencialLiquidPerHour",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "PotencialOilPerHour",
                table: "BTPDatas");

            migrationBuilder.DropColumn(
                name: "PotencialWaterPerHour",
                table: "BTPDatas");
        }
    }
}
