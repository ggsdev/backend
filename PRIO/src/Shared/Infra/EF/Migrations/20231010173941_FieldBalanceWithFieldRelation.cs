using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FieldBalanceWithFieldRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "Balance.FieldsBalance",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Balance.FieldsBalance_FieldId",
                table: "Balance.FieldsBalance",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance.FieldsBalance_Hierachy.Fields_FieldId",
                table: "Balance.FieldsBalance",
                column: "FieldId",
                principalTable: "Hierachy.Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance.FieldsBalance_Hierachy.Fields_FieldId",
                table: "Balance.FieldsBalance");

            migrationBuilder.DropIndex(
                name: "IX_Balance.FieldsBalance_FieldId",
                table: "Balance.FieldsBalance");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Balance.FieldsBalance");
        }
    }
}
