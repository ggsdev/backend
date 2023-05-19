using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixingMeasurementRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Volumes_039",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_MED_REGISTRADO_039",
                table: "Volumes_039",
                type: "decimal(7,6)",
                precision: 7,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,6)",
                oldPrecision: 7,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_MED_DECLARADO_039",
                table: "Volumes_039",
                type: "decimal(7,6)",
                precision: 7,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,6)",
                oldPrecision: 7,
                oldScale: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_MEDICAO_039",
                table: "Volumes_039",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Calibrations_039",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ATUAL_039",
                table: "Calibrations_039",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039",
                table: "Calibrations_039",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FALHA_CALIBRACAO_039",
                table: "Calibrations_039",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "DHA_CERTIFICADO_ATUAL_039",
                table: "Calibrations_039",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_CERTIFICADO_ANTERIOR_039",
                table: "Calibrations_039",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "BSWS_039",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FALHA_BSW_039",
                table: "BSWS_039",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039");

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Volumes_039",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_MED_REGISTRADO_039",
                table: "Volumes_039",
                type: "decimal(7,6)",
                precision: 7,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,6)",
                oldPrecision: 7,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_MED_DECLARADO_039",
                table: "Volumes_039",
                type: "decimal(7,6)",
                precision: 7,
                scale: 6,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,6)",
                oldPrecision: 7,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_MEDICAO_039",
                table: "Volumes_039",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "Calibrations_039",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ATUAL_039",
                table: "Calibrations_039",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039",
                table: "Calibrations_039",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FALHA_CALIBRACAO_039",
                table: "Calibrations_039",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_CERTIFICADO_ATUAL_039",
                table: "Calibrations_039",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DHA_CERTIFICADO_ANTERIOR_039",
                table: "Calibrations_039",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MeasurementId",
                table: "BSWS_039",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DHA_FALHA_BSW_039",
                table: "BSWS_039",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id");
        }
    }
}
