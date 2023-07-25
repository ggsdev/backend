using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class GasIsApplicableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "PurgeGases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "PilotGases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "LPFlares",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "LowPressureGases",
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
                name: "IsApplicable",
                table: "ImportGases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "HpFlares",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "HighPressureGases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "ExportGases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "AssistanceGases",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "PurgeGases");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "PilotGases");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "LPFlares");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "LowPressureGases");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "ImportGases");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "HpFlares");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "HighPressureGases");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "ExportGases");

            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "AssistanceGases");

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
