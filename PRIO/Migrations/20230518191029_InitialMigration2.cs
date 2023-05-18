using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Volumes_039",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Calibrations_039",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "BSWS_039",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Volumes_039",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Calibrations_039",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "BSWS_039",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
