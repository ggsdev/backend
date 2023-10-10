using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class DecimalNonNullableBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "DischargedSurface",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);
        }
    }
}
