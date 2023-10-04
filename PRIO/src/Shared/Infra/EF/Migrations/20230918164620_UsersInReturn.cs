using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UsersInReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_Users_UserId",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Users_UserId",
                table: "WellEvents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WellEvents",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_WellEvents_UserId",
                table: "WellEvents",
                newName: "IX_WellEvents_CreatedById");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EventReasons",
                newName: "UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EventReasons_UserId",
                table: "EventReasons",
                newName: "IX_EventReasons_UpdatedById");

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "WellEvents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "EventReasons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WellEvents_UpdatedById",
                table: "WellEvents",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EventReasons_CreatedById",
                table: "EventReasons",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_Users_CreatedById",
                table: "EventReasons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_Users_UpdatedById",
                table: "EventReasons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Users_CreatedById",
                table: "WellEvents",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Users_UpdatedById",
                table: "WellEvents",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_Users_CreatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_Users_UpdatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Users_CreatedById",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Users_UpdatedById",
                table: "WellEvents");

            migrationBuilder.DropIndex(
                name: "IX_WellEvents_UpdatedById",
                table: "WellEvents");

            migrationBuilder.DropIndex(
                name: "IX_EventReasons_CreatedById",
                table: "EventReasons");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "EventReasons");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "WellEvents",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WellEvents_CreatedById",
                table: "WellEvents",
                newName: "IX_WellEvents_UserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "EventReasons",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventReasons_UpdatedById",
                table: "EventReasons",
                newName: "IX_EventReasons_UserId");

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
    }
}
