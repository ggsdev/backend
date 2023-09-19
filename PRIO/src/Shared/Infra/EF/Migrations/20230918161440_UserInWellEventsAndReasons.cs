using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UserInWellEventsAndReasons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "WellEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "EventReasons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WellEvents_UserId",
                table: "WellEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReasons_UserId",
                table: "EventReasons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_Users_UserId",
                table: "EventReasons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Users_UserId",
                table: "WellEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_Users_UserId",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Users_UserId",
                table: "WellEvents");

            migrationBuilder.DropIndex(
                name: "IX_WellEvents_UserId",
                table: "WellEvents");

            migrationBuilder.DropIndex(
                name: "IX_EventReasons_UserId",
                table: "EventReasons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EventReasons");
        }
    }
}
