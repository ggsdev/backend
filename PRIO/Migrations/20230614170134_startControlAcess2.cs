using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class startControlAcess2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_GroupMenus_GroupMenuId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "UserPermissions");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_UserId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_GroupMenuId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_GroupMenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_GroupMenus_GroupMenuId",
                table: "UserPermissions",
                column: "GroupMenuId",
                principalTable: "GroupMenus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_GroupMenus_GroupMenuId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions");

            migrationBuilder.RenameTable(
                name: "UserPermissions",
                newName: "Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UserId",
                table: "Permissions",
                newName: "IX_Permissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_GroupMenuId",
                table: "Permissions",
                newName: "IX_Permissions_GroupMenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_GroupMenus_GroupMenuId",
                table: "Permissions",
                column: "GroupMenuId",
                principalTable: "GroupMenus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
