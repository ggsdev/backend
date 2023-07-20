using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class DscMaterialToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DSC_MATERIAL_CONTRUCAO_PLACA_003",
                table: "Measurements",
                type: "decimal(22,14)",
                precision: 22,
                scale: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DSC_MATERIAL_CNSTO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(22,14)",
                precision: 22,
                scale: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

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
            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CONTRUCAO_PLACA_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(22,14)",
                oldPrecision: 22,
                oldScale: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CNSTO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(22,14)",
                oldPrecision: 22,
                oldScale: 14,
                oldNullable: true);

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
