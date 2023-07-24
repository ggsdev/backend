using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnsInMeasuringPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "TOGRecoveredOils",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "Sections",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MeasuringPoints",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "DrainVolumes",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "DORs",
                newName: "StaticLocalMeasuringPoint");

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
                name: "StaticLocalMeasuringPoint",
                table: "TOGRecoveredOils",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "Sections",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "DinamicLocalMeasuringPoint",
                table: "MeasuringPoints",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "DrainVolumes",
                newName: "DinamicLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "DORs",
                newName: "DinamicLocalMeasuringPoint");

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
