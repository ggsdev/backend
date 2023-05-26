using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class MeasuringEquipmentRelationMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MeasuringEquipmentId",
                table: "Measurements",
                type: "uniqueidentifier",
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
