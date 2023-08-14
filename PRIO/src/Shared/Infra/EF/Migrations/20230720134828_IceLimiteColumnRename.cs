using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class IceLimiteColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ICE_LIMITE_INFRR_ALARME_2_003",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "ICE_LIMITE_INFRR_ALARME_1_003",
                table: "Measurements",
                newName: "ICE_LIMITE_INFRR_ALARME_003");

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
                name: "ICE_LIMITE_INFRR_ALARME_003",
                table: "Measurements",
                newName: "ICE_LIMITE_INFRR_ALARME_1_003");

            migrationBuilder.AddColumn<bool>(
                name: "ICE_LIMITE_INFRR_ALARME_2_003",
                table: "Measurements",
                type: "bit",
                nullable: true);

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
