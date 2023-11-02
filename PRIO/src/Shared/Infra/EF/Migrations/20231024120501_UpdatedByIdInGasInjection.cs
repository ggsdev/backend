using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedByIdInGasInjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Injection.InjectionGasWell",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionGasWell_UpdatedById",
                table: "Injection.InjectionGasWell",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionGasWell_AC.Users_UpdatedById",
                table: "Injection.InjectionGasWell",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionGasWell_AC.Users_UpdatedById",
                table: "Injection.InjectionGasWell");

            migrationBuilder.DropIndex(
                name: "IX_Injection.InjectionGasWell_UpdatedById",
                table: "Injection.InjectionGasWell");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Injection.InjectionGasWell");
        }
    }
}
