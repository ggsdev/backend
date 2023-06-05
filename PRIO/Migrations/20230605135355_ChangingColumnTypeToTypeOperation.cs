using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ChangingColumnTypeToTypeOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ZoneHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ZoneHistories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ReservoirHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ReservoirHistories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "InstallationHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "InstallationHistories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "FieldHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FieldHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ClusterHistories");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ClusterHistories",
                newName: "TypeOperation");

            migrationBuilder.AddColumn<string>(
                name: "TypeOperation",
                table: "ZoneHistories",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CodReservoir",
                table: "Reservoirs",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "CodReservoir",
                table: "ReservoirHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<string>(
                name: "TypeOperation",
                table: "ReservoirHistories",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeOperation",
                table: "InstallationHistories",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeOperation",
                table: "FieldHistories",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOperation",
                table: "ZoneHistories");

            migrationBuilder.DropColumn(
                name: "TypeOperation",
                table: "ReservoirHistories");

            migrationBuilder.DropColumn(
                name: "TypeOperation",
                table: "InstallationHistories");

            migrationBuilder.DropColumn(
                name: "TypeOperation",
                table: "FieldHistories");

            migrationBuilder.RenameColumn(
                name: "TypeOperation",
                table: "ClusterHistories",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ZoneHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ZoneHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "CodReservoir",
                table: "Reservoirs",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodReservoir",
                table: "ReservoirHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ReservoirHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ReservoirHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "InstallationHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "InstallationHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "FieldHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FieldHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ClusterHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
