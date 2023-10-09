using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UepBalanceRealtionWithUep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UepId",
                table: "Balance.UEPsBalance",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balance.UEPsBalance_UepId",
                table: "Balance.UEPsBalance",
                column: "UepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance.UEPsBalance_Hierachy.Installations_UepId",
                table: "Balance.UEPsBalance",
                column: "UepId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance.UEPsBalance_Hierachy.Installations_UepId",
                table: "Balance.UEPsBalance");

            migrationBuilder.DropIndex(
                name: "IX_Balance.UEPsBalance_UepId",
                table: "Balance.UEPsBalance");

            migrationBuilder.DropColumn(
                name: "UepId",
                table: "Balance.UEPsBalance");
        }
    }
}
