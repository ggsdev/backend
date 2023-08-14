using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChanginColumnOrificioType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CONTRUCAO_PLACA_003",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(22,14)",
                oldPrecision: 22,
                oldScale: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DSC_MATERIAL_CNSTO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(22,14)",
                oldPrecision: 22,
                oldScale: 14,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DSC_MATERIAL_CONTRUCAO_PLACA_003",
                table: "Measurements",
                type: "decimal(22,14)",
                precision: 22,
                scale: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DSC_MATERIAL_CNSTO_TRCHO_MDCO_003",
                table: "Measurements",
                type: "decimal(22,14)",
                precision: 22,
                scale: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
