using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class refactorOilcalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BSW",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "BSW",
                table: "DORs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TOGRecoveredOils",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sections",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DrainVolumes",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DORs",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.AddColumn<bool>(
                name: "IsUser",
                table: "TOGRecoveredOils",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUser",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUser",
                table: "DrainVolumes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUser",
                table: "DORs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUser",
                table: "TOGRecoveredOils");

            migrationBuilder.DropColumn(
                name: "IsUser",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "IsUser",
                table: "DrainVolumes");

            migrationBuilder.DropColumn(
                name: "IsUser",
                table: "DORs");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "TOGRecoveredOils",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "Sections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "DrainVolumes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "DORs",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "BSW",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BSW",
                table: "DORs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
