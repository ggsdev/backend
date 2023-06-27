using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class InstallationInMeasurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                table: "Measurements");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodField",
                table: "Fields",
                type: "VARCHAR(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_InstallationId",
                table: "Measurements",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Installations_InstallationId",
                table: "Measurements",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                table: "Measurements",
                column: "MeasuringEquipmentId",
                principalTable: "MeasuringEquipments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Installations_InstallationId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_InstallationId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Measurements");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodField",
                table: "Fields",
                type: "VARCHAR(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                table: "Measurements",
                column: "MeasuringEquipmentId",
                principalTable: "MeasuringEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
