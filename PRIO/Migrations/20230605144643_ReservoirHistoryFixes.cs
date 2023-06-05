using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ReservoirHistoryFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WellHistories");

            migrationBuilder.RenameColumn(
                name: "ZoneNameOld",
                table: "ReservoirHistories",
                newName: "ZoneCodOld");

            migrationBuilder.RenameColumn(
                name: "ZoneName",
                table: "ReservoirHistories",
                newName: "ZoneCod");

            migrationBuilder.AddColumn<string>(
                name: "TypeOperation",
                table: "WellHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOperation",
                table: "WellHistories");

            migrationBuilder.RenameColumn(
                name: "ZoneCodOld",
                table: "ReservoirHistories",
                newName: "ZoneNameOld");

            migrationBuilder.RenameColumn(
                name: "ZoneCod",
                table: "ReservoirHistories",
                newName: "ZoneName");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "WellHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
