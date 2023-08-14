using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangingColumnNamesGasCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PurgeGases",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PilotGases",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LPFlares",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LowPressureGases",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ImportGases",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "HpFlares",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "HighPressureGases",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ExportGases",
                newName: "StaticLocalMeasuringPoint");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AssistanceGases",
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
                table: "PurgeGases",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "PilotGases",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "LPFlares",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "LowPressureGases",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "ImportGases",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "HpFlares",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "HighPressureGases",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "ExportGases",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaticLocalMeasuringPoint",
                table: "AssistanceGases",
                newName: "Name");

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
