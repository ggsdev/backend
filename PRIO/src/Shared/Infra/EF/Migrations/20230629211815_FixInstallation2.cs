using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixInstallation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_OilVoumeCalculations_InstallationId",
                table: "Installations");

            migrationBuilder.DropIndex(
                name: "IX_Installations_InstallationId",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Installations");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "OilVoumeCalculations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                unique: true,
                filter: "[InstallationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "Installations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installations_InstallationId",
                table: "Installations",
                column: "InstallationId",
                unique: true,
                filter: "[InstallationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_OilVoumeCalculations_InstallationId",
                table: "Installations",
                column: "InstallationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");
        }
    }
}
