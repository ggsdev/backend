using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class RemovingFkUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemHistories_Users_UserId",
                table: "SystemHistories");

            migrationBuilder.DropIndex(
                name: "IX_SystemHistories_UserId",
                table: "SystemHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SystemHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SystemHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemHistories_UserId",
                table: "SystemHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemHistories_Users_UserId",
                table: "SystemHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
