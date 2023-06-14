using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class startControlAcess4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMenus_Groups_GroupId",
                table: "GroupMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMenus_Menus_MenuId",
                table: "GroupMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissionsOperation_GroupMenus_GroupPermissionsId",
                table: "GroupPermissionsOperation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_GroupMenus_GroupMenuId",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMenus",
                table: "GroupMenus");

            migrationBuilder.RenameTable(
                name: "GroupMenus",
                newName: "GroupPermissions");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMenus_MenuId",
                table: "GroupPermissions",
                newName: "IX_GroupPermissions_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMenus_GroupId",
                table: "GroupPermissions",
                newName: "IX_GroupPermissions_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                table: "GroupPermissions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Menus_MenuId",
                table: "GroupPermissions",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissionsOperation_GroupPermissions_GroupPermissionsId",
                table: "GroupPermissionsOperation",
                column: "GroupPermissionsId",
                principalTable: "GroupPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_GroupPermissions_GroupMenuId",
                table: "UserPermissions",
                column: "GroupMenuId",
                principalTable: "GroupPermissions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Menus_MenuId",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissionsOperation_GroupPermissions_GroupPermissionsId",
                table: "GroupPermissionsOperation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_GroupPermissions_GroupMenuId",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions");

            migrationBuilder.RenameTable(
                name: "GroupPermissions",
                newName: "GroupMenus");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermissions_MenuId",
                table: "GroupMenus",
                newName: "IX_GroupMenus_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermissions_GroupId",
                table: "GroupMenus",
                newName: "IX_GroupMenus_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMenus",
                table: "GroupMenus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMenus_Groups_GroupId",
                table: "GroupMenus",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMenus_Menus_MenuId",
                table: "GroupMenus",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissionsOperation_GroupMenus_GroupPermissionsId",
                table: "GroupPermissionsOperation",
                column: "GroupPermissionsId",
                principalTable: "GroupMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_GroupMenus_GroupMenuId",
                table: "UserPermissions",
                column: "GroupMenuId",
                principalTable: "GroupMenus",
                principalColumn: "Id");
        }
    }
}
