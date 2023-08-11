using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnNfsm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DetectionDate",
                table: "NFSMs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReponsibleReport",
                table: "NFSMs",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "NFSMs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetectionDate",
                table: "NFSMs");

            migrationBuilder.DropColumn(
                name: "ReponsibleReport",
                table: "NFSMs");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "NFSMs");
        }
    }
}
