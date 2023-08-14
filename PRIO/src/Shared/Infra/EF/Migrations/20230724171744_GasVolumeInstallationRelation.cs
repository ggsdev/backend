using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class GasVolumeInstallationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GasVolumeCalculations_InstallationId",
                table: "GasVolumeCalculations");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GasVolumeCalculations_InstallationId",
                table: "GasVolumeCalculations",
                column: "InstallationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GasVolumeCalculations_InstallationId",
                table: "GasVolumeCalculations");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GasVolumeCalculations_InstallationId",
                table: "GasVolumeCalculations",
                column: "InstallationId");
        }
    }
}
