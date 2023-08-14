using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFkNameGas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gases_GasesDiferencials_GasId",
                table: "Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Gases_GasesLinears_GasId",
                table: "Gases");

            migrationBuilder.DropIndex(
                name: "IX_Gases_GasId",
                table: "Gases");

            migrationBuilder.RenameColumn(
                name: "GasId",
                table: "Gases",
                newName: "GasLinearId");

            migrationBuilder.AddColumn<Guid>(
                name: "GasDiferencialId",
                table: "Gases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gases_GasDiferencialId",
                table: "Gases",
                column: "GasDiferencialId",
                unique: true,
                filter: "[GasDiferencialId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Gases_GasLinearId",
                table: "Gases",
                column: "GasLinearId",
                unique: true,
                filter: "[GasLinearId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Gases_GasesDiferencials_GasDiferencialId",
                table: "Gases",
                column: "GasDiferencialId",
                principalTable: "GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gases_GasesLinears_GasLinearId",
                table: "Gases",
                column: "GasLinearId",
                principalTable: "GasesLinears",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gases_GasesDiferencials_GasDiferencialId",
                table: "Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Gases_GasesLinears_GasLinearId",
                table: "Gases");

            migrationBuilder.DropIndex(
                name: "IX_Gases_GasDiferencialId",
                table: "Gases");

            migrationBuilder.DropIndex(
                name: "IX_Gases_GasLinearId",
                table: "Gases");

            migrationBuilder.DropColumn(
                name: "GasDiferencialId",
                table: "Gases");

            migrationBuilder.RenameColumn(
                name: "GasLinearId",
                table: "Gases",
                newName: "GasId");

            migrationBuilder.CreateIndex(
                name: "IX_Gases_GasId",
                table: "Gases",
                column: "GasId",
                unique: true,
                filter: "[GasId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Gases_GasesDiferencials_GasId",
                table: "Gases",
                column: "GasId",
                principalTable: "GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gases_GasesLinears_GasId",
                table: "Gases",
                column: "GasId",
                principalTable: "GasesLinears",
                principalColumn: "Id");
        }
    }
}
