using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class InstallationProcessingUnitField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessingUnit",
                table: "Installations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessingUnit",
                table: "Installations");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
