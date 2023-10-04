using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class createColumnInactivatedAtOnHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Zones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Wells",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Reservoirs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Installations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Fields",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Completions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedAt",
                table: "Clusters",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "InactivatedAt",
                table: "Clusters");
        }
    }
}
