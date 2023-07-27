using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class MeasurementRelationWithPointMeasuring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_MeasuringEquipmentId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MeasuringEquipmentId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "CodCompletion",
                table: "Completions");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasuringPointId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MeasuringPointId",
                table: "Measurements",
                column: "MeasuringPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_MeasuringPointId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MeasuringPointId",
                table: "Measurements");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasuringEquipmentId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodCompletion",
                table: "Completions",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MeasuringEquipmentId",
                table: "Measurements",
                column: "MeasuringEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                table: "Measurements",
                column: "MeasuringEquipmentId",
                principalTable: "MeasuringEquipments",
                principalColumn: "Id");
        }
    }
}
