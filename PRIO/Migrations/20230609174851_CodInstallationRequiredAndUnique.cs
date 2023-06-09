using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class CodInstallationRequiredAndUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodInstallation",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodInstallation",
                table: "InstallationHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installations_CodInstallation",
                table: "Installations",
                column: "CodInstallation",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Installations_CodInstallation",
                table: "Installations");

            migrationBuilder.AlterColumn<string>(
                name: "CodInstallation",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "CodInstallation",
                table: "InstallationHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);
        }
    }
}
