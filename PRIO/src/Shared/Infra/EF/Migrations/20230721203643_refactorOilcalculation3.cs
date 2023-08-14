using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class refactorOilcalculation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "TOGRecoveredOils",
                newName: "IsApplicable");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "Sections",
                newName: "IsApplicable");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "DrainVolumes",
                newName: "IsApplicable");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "DORs",
                newName: "IsApplicable");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "MeasuringPoints",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "MeasuringPoints");

            migrationBuilder.RenameColumn(
                name: "IsApplicable",
                table: "TOGRecoveredOils",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "IsApplicable",
                table: "Sections",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "IsApplicable",
                table: "DrainVolumes",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "IsApplicable",
                table: "DORs",
                newName: "IsUsed");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);
        }
    }
}
