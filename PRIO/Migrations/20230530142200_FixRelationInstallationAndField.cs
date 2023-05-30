using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationInstallationAndField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Fields_FieldId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Installations_InstallationId",
                table: "Reservoirs");

            migrationBuilder.DropIndex(
                name: "IX_Installations_FieldId",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Installations");

            migrationBuilder.RenameColumn(
                name: "InstallationId",
                table: "Reservoirs",
                newName: "FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_InstallationId",
                table: "Reservoirs",
                newName: "IX_Reservoirs_FieldId");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "Fields",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fields_InstallationId",
                table: "Fields",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Installations_InstallationId",
                table: "Fields",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Fields_FieldId",
                table: "Reservoirs",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Installations_InstallationId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Fields_FieldId",
                table: "Reservoirs");

            migrationBuilder.DropIndex(
                name: "IX_Fields_InstallationId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Fields");

            migrationBuilder.RenameColumn(
                name: "FieldId",
                table: "Reservoirs",
                newName: "InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_FieldId",
                table: "Reservoirs",
                newName: "IX_Reservoirs_InstallationId");

            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "Installations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Installations_FieldId",
                table: "Installations",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Fields_FieldId",
                table: "Installations",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Installations_InstallationId",
                table: "Reservoirs",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
