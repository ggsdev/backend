using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UserOperationMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations");

            migrationBuilder.AlterColumn<string>(
                name: "OperationName",
                table: "UserOperations",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations");

            migrationBuilder.AlterColumn<string>(
                name: "OperationName",
                table: "UserOperations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id");
        }
    }
}
