using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixWaterGasInjection3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);
        }
    }
}
