using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixingColumnsXlsImport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cod",
                table: "Installations");

            migrationBuilder.RenameColumn(
                name: "CodInstallationUep",
                table: "Installations",
                newName: "UepCod");

            migrationBuilder.AddColumn<string>(
                name: "CodInstallation",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodInstallation",
                table: "Installations");

            migrationBuilder.RenameColumn(
                name: "UepCod",
                table: "Installations",
                newName: "CodInstallationUep");

            migrationBuilder.AddColumn<string>(
                name: "Cod",
                table: "Installations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
