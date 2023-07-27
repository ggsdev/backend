using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class Fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_TOGRecoveredOils_MeasuringPointId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_Sections_MeasuringPointId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropIndex(
                name: "IX_DrainVolumes_MeasuringPointId",
                table: "DrainVolumes");

            migrationBuilder.DropIndex(
                name: "IX_DORs_MeasuringPointId",
                table: "DORs");

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "TOGRecoveredOils",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallationId",
                table: "OilVoumeCalculations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagPointMeasuring",
                table: "MeasuringPoints",
                type: "VARCHAR(260)",
                maxLength: 260,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileContent",
                table: "MeasurementsHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ImportId",
                table: "FileTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "DrainVolumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "DrainVolumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "DORs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "DORs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_MeasuringPointId",
                table: "TOGRecoveredOils",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_MeasuringPointId",
                table: "Sections",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_MeasuringPointId",
                table: "DrainVolumes",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DORs_MeasuringPointId",
                table: "DORs",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_TOGRecoveredOils_MeasuringPointId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_Sections_MeasuringPointId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropIndex(
                name: "IX_DrainVolumes_MeasuringPointId",
                table: "DrainVolumes");

            migrationBuilder.DropIndex(
                name: "IX_DORs_MeasuringPointId",
                table: "DORs");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "MeasurementsHistories");

            migrationBuilder.DropColumn(
                name: "ImportId",
                table: "FileTypes");

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "TOGRecoveredOils",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "Sections",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallationId",
                table: "OilVoumeCalculations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "TagPointMeasuring",
                table: "MeasuringPoints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(260)",
                oldMaxLength: 260);

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "DrainVolumes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "DrainVolumes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "DORs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasuringPointId",
                table: "DORs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_MeasuringPointId",
                table: "TOGRecoveredOils",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_MeasuringPointId",
                table: "Sections",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                unique: true,
                filter: "[InstallationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_MeasuringPointId",
                table: "DrainVolumes",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DORs_MeasuringPointId",
                table: "DORs",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");
        }
    }
}
