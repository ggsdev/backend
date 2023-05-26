using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UserMeasuringEquipmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "MeasuringEquipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_UserId",
                table: "MeasuringEquipments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_Users_UserId",
                table: "MeasuringEquipments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_Users_UserId",
                table: "MeasuringEquipments");

            migrationBuilder.DropIndex(
                name: "IX_MeasuringEquipments_UserId",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MeasuringEquipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
