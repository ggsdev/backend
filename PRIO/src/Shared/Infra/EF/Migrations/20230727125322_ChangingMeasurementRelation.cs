using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangingMeasurementRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementsHistories_Measurements_MeasurementId",
                table: "MeasurementsHistories");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementsHistories_MeasurementId",
                table: "MeasurementsHistories");

            migrationBuilder.DropColumn(
                name: "MeasurementId",
                table: "MeasurementsHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementHistoryId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ImportId",
                table: "FileTypes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MeasurementHistoryId",
                table: "Measurements",
                column: "MeasurementHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasurementsHistories_MeasurementHistoryId",
                table: "Measurements",
                column: "MeasurementHistoryId",
                principalTable: "MeasurementsHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasurementsHistories_MeasurementHistoryId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_MeasurementHistoryId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MeasurementHistoryId",
                table: "Measurements");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementId",
                table: "MeasurementsHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ImportId",
                table: "FileTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementsHistories_MeasurementId",
                table: "MeasurementsHistories",
                column: "MeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementsHistories_Measurements_MeasurementId",
                table: "MeasurementsHistories",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
