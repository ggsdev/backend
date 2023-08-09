using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class RedoingRelationNfsm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NFSMImportHistories_NFSMs_ImportId",
                table: "NFSMImportHistories");

            migrationBuilder.DropIndex(
                name: "IX_NFSMImportHistories_ImportId",
                table: "NFSMImportHistories");

            migrationBuilder.DropColumn(
                name: "ImportId",
                table: "NFSMImportHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "ImportId",
                table: "NFSMs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_NFSMs_ImportId",
                table: "NFSMs",
                column: "ImportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_NFSMImportHistories_ImportId",
                table: "NFSMs",
                column: "ImportId",
                principalTable: "NFSMImportHistories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_NFSMImportHistories_ImportId",
                table: "NFSMs");

            migrationBuilder.DropIndex(
                name: "IX_NFSMs_ImportId",
                table: "NFSMs");

            migrationBuilder.DropColumn(
                name: "ImportId",
                table: "NFSMs");

            migrationBuilder.AddColumn<Guid>(
                name: "ImportId",
                table: "NFSMImportHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_NFSMImportHistories_ImportId",
                table: "NFSMImportHistories",
                column: "ImportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMImportHistories_NFSMs_ImportId",
                table: "NFSMImportHistories",
                column: "ImportId",
                principalTable: "NFSMs",
                principalColumn: "Id");
        }
    }
}
