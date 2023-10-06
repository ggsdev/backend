using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class alterColunInjectionFieldToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterField_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell");

            migrationBuilder.AlterColumn<Guid>(
                name: "InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterField_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                column: "InjectionWaterFieldId",
                principalTable: "Injection.InjectionWaterField",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterField_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell");

            migrationBuilder.AlterColumn<Guid>(
                name: "InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterField_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                column: "InjectionWaterFieldId",
                principalTable: "Injection.InjectionWaterField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
