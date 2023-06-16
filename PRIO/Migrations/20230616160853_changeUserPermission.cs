using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeUserPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsParent",
                table: "UserPermissions",
                newName: "hasParent");

            migrationBuilder.AddColumn<bool>(
                name: "hasChildren",
                table: "UserPermissions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasChildren",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "hasParent",
                table: "UserPermissions",
                newName: "IsParent");
        }
    }
}
