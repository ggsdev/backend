using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class refactorOilcalculation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUser",
                table: "TOGRecoveredOils",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "IsUser",
                table: "Sections",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "IsUser",
                table: "DrainVolumes",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "IsUser",
                table: "DORs",
                newName: "IsUsed");

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
            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "TOGRecoveredOils",
                newName: "IsUser");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "Sections",
                newName: "IsUser");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "DrainVolumes",
                newName: "IsUser");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "DORs",
                newName: "IsUser");

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
