using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingToDouble3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ATUAL_039",
                table: "Calibrations_039",
                type: "float(5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039",
                table: "Calibrations_039",
                type: "float(5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ATUAL_039",
                table: "Calibrations_039",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039",
                table: "Calibrations_039",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
