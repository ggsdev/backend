using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class GasIdFkFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasDiferencialId",
                table: "Productions");

            migrationBuilder.AddColumn<Guid>(
                name: "GasId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productions_GasId",
                table: "Productions",
                column: "GasId",
                unique: true,
                filter: "[GasId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions",
                column: "GasId",
                principalTable: "Gases",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_GasId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "GasId",
                table: "Productions");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasDiferencialId",
                table: "Productions",
                column: "GasDiferencialId",
                principalTable: "Gases",
                principalColumn: "Id");
        }
    }
}
