using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class newRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserOperations",
                newName: "OperationName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GroupOperations",
                newName: "OperationName");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "GroupPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuIcon",
                table: "GroupPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuName",
                table: "GroupPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuOrder",
                table: "GroupPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuRoute",
                table: "GroupPermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GlobalOperations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "GlobalOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GlobalOperations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "GlobalOperations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GlobalOperations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "MenuIcon",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "MenuName",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "MenuOrder",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "MenuRoute",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GlobalOperations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "GlobalOperations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GlobalOperations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "GlobalOperations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GlobalOperations");

            migrationBuilder.RenameColumn(
                name: "OperationName",
                table: "UserOperations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "OperationName",
                table: "GroupOperations",
                newName: "Name");
        }
    }
}
