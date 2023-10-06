using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixWaterGasInjection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance.FieldsBalance_Balance.InstallationsBalance_installationBalanceId",
                table: "Balance.FieldsBalance");

            migrationBuilder.RenameColumn(
                name: "TOtalWaterInjectedRS",
                table: "Balance.UEPsBalance",
                newName: "TotalWaterInjectedRS");

            migrationBuilder.RenameColumn(
                name: "TOtalWaterInjectedRS",
                table: "Balance.InstallationsBalance",
                newName: "TotalWaterInjectedRS");

            migrationBuilder.RenameColumn(
                name: "installationBalanceId",
                table: "Balance.FieldsBalance",
                newName: "InstallationBalanceId");

            migrationBuilder.RenameColumn(
                name: "TOtalWaterInjectedRS",
                table: "Balance.FieldsBalance",
                newName: "TotalWaterInjectedRS");

            migrationBuilder.RenameIndex(
                name: "IX_Balance.FieldsBalance_installationBalanceId",
                table: "Balance.FieldsBalance",
                newName: "IX_Balance.FieldsBalance_InstallationBalanceId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.UEPsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.InstallationsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjectedRS",
                table: "Balance.FieldsBalance",
                type: "DECIMAL(38,16)",
                precision: 38,
                scale: 16,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Balance.FieldsBalance_Balance.InstallationsBalance_InstallationBalanceId",
                table: "Balance.FieldsBalance",
                column: "InstallationBalanceId",
                principalTable: "Balance.InstallationsBalance",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance.FieldsBalance_Balance.InstallationsBalance_InstallationBalanceId",
                table: "Balance.FieldsBalance");

            migrationBuilder.RenameColumn(
                name: "TotalWaterInjectedRS",
                table: "Balance.UEPsBalance",
                newName: "TOtalWaterInjectedRS");

            migrationBuilder.RenameColumn(
                name: "TotalWaterInjectedRS",
                table: "Balance.InstallationsBalance",
                newName: "TOtalWaterInjectedRS");

            migrationBuilder.RenameColumn(
                name: "TotalWaterInjectedRS",
                table: "Balance.FieldsBalance",
                newName: "TOtalWaterInjectedRS");

            migrationBuilder.RenameColumn(
                name: "InstallationBalanceId",
                table: "Balance.FieldsBalance",
                newName: "installationBalanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Balance.FieldsBalance_InstallationBalanceId",
                table: "Balance.FieldsBalance",
                newName: "IX_Balance.FieldsBalance_installationBalanceId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TOtalWaterInjectedRS",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.UEPsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TOtalWaterInjectedRS",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.InstallationsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterTransferred",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterReceived",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterProduced",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16);

            migrationBuilder.AlterColumn<decimal>(
                name: "TOtalWaterInjectedRS",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterInjected",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterDisposal",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalWaterCaptured",
                table: "Balance.FieldsBalance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(38,16)",
                oldPrecision: 38,
                oldScale: 16,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Balance.FieldsBalance_Balance.InstallationsBalance_installationBalanceId",
                table: "Balance.FieldsBalance",
                column: "installationBalanceId",
                principalTable: "Balance.InstallationsBalance",
                principalColumn: "Id");
        }
    }
}
