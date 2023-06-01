using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class addColumnCodWellInWell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodWell",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "Wells",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wells_FieldId",
                table: "Wells",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wells_Fields_FieldId",
                table: "Wells",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wells_Fields_FieldId",
                table: "Wells");

            migrationBuilder.DropIndex(
                name: "IX_Wells_FieldId",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CodWell",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Wells");
        }
    }
}
