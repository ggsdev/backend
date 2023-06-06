using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUnecessaryColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "ZoneHistories");

            migrationBuilder.DropColumn(
                name: "FieldNameOld",
                table: "ZoneHistories");

            migrationBuilder.DropColumn(
                name: "ZoneCod",
                table: "ReservoirHistories");

            migrationBuilder.DropColumn(
                name: "ZoneCodOld",
                table: "ReservoirHistories");

            migrationBuilder.DropColumn(
                name: "ClusterName",
                table: "InstallationHistories");

            migrationBuilder.DropColumn(
                name: "ClusterNameOld",
                table: "InstallationHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "ZoneHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldNameOld",
                table: "ZoneHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZoneCod",
                table: "ReservoirHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZoneCodOld",
                table: "ReservoirHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClusterName",
                table: "InstallationHistories",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClusterNameOld",
                table: "InstallationHistories",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
