using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeGroupPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "hasChildren",
                table: "GroupPermissions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hasParent",
                table: "GroupPermissions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasChildren",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "hasParent",
                table: "GroupPermissions");
        }
    }
}
