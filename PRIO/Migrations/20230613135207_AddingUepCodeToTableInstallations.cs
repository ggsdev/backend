using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class AddingUepCodeToTableInstallations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Installations_CodInstallation",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "UepCode",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "UepCode",
                table: "ClusterHistories");

            migrationBuilder.DropColumn(
                name: "UepCodeOld",
                table: "ClusterHistories");

            migrationBuilder.RenameColumn(
                name: "CodInstallation",
                table: "Installations",
                newName: "CodInstallationUep");

            migrationBuilder.RenameColumn(
                name: "CodInstallationOld",
                table: "InstallationHistories",
                newName: "CodInstallationUepOld");

            migrationBuilder.RenameColumn(
                name: "CodInstallation",
                table: "InstallationHistories",
                newName: "CodInstallationUep");

            migrationBuilder.AddColumn<string>(
                name: "Cod",
                table: "Installations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cod",
                table: "Installations");

            migrationBuilder.RenameColumn(
                name: "CodInstallationUep",
                table: "Installations",
                newName: "CodInstallation");

            migrationBuilder.RenameColumn(
                name: "CodInstallationUepOld",
                table: "InstallationHistories",
                newName: "CodInstallationOld");

            migrationBuilder.RenameColumn(
                name: "CodInstallationUep",
                table: "InstallationHistories",
                newName: "CodInstallation");

            migrationBuilder.AddColumn<string>(
                name: "UepCode",
                table: "Clusters",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UepCode",
                table: "ClusterHistories",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UepCodeOld",
                table: "ClusterHistories",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installations_CodInstallation",
                table: "Installations",
                column: "CodInstallation",
                unique: true);
        }
    }
}
