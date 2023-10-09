using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixWaterGasInjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Injection.InjectionWaterGasField",
                newName: "AmountWater");

            migrationBuilder.AddColumn<double>(
                name: "AmountGasLift",
                table: "Injection.InjectionWaterGasField",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "Injection.InjectionWaterGasField",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterGasField_FieldId",
                table: "Injection.InjectionWaterGasField",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionWaterGasField_Hierachy.Fields_FieldId",
                table: "Injection.InjectionWaterGasField",
                column: "FieldId",
                principalTable: "Hierachy.Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionWaterGasField_Hierachy.Fields_FieldId",
                table: "Injection.InjectionWaterGasField");

            migrationBuilder.DropIndex(
                name: "IX_Injection.InjectionWaterGasField_FieldId",
                table: "Injection.InjectionWaterGasField");

            migrationBuilder.DropColumn(
                name: "AmountGasLift",
                table: "Injection.InjectionWaterGasField");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Injection.InjectionWaterGasField");

            migrationBuilder.RenameColumn(
                name: "AmountWater",
                table: "Injection.InjectionWaterGasField",
                newName: "Amount");
        }
    }
}
