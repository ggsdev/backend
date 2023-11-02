using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FileTypeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileTypes",
                table: "FileExport.Templates",
                newName: "FileType");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "FileExport.Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "FileExport.Templates");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "FileExport.Templates",
                newName: "FileTypes");
        }
    }
}
