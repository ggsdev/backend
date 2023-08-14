using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class adjusts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasDiferencialId",
                table: "Productions");

            migrationBuilder.AddColumn<Guid>(
                name: "GasId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "FRWater",
                table: "FieldsFRs",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FROil",
                table: "FieldsFRs",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FRGas",
                table: "FieldsFRs",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DailyProductionId",
                table: "FieldsFRs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionInField",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "RGO",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialWaterPerHour",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialWater",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialOilPerHour",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialOil",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquidPerHour",
                table: "BTPDatas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquid",
                table: "BTPDatas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialGasPerHour",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialGas",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "BSW",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_GasId",
                table: "Productions",
                column: "GasId",
                unique: true,
                filter: "[GasId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_InstallationId",
                table: "Productions",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldsFRs_DailyProductionId",
                table: "FieldsFRs",
                column: "DailyProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Productions_DailyProductionId",
                table: "FieldsFRs",
                column: "DailyProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions",
                column: "GasId",
                principalTable: "Gases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Installations_InstallationId",
                table: "Productions",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Productions_DailyProductionId",
                table: "FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Installations_InstallationId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_GasId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_InstallationId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_FieldsFRs_DailyProductionId",
                table: "FieldsFRs");

            migrationBuilder.DropColumn(
                name: "GasId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "DailyProductionId",
                table: "FieldsFRs");

            migrationBuilder.DropColumn(
                name: "ProductionInField",
                table: "FieldsFRs");

            migrationBuilder.AlterColumn<decimal>(
                name: "FRWater",
                table: "FieldsFRs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FROil",
                table: "FieldsFRs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FRGas",
                table: "FieldsFRs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RGO",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialWaterPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialWater",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialOilPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialOil",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialLiquidPerHour",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PotencialLiquid",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PotencialGasPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialGas",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "BSW",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasDiferencialId",
                table: "Productions",
                column: "GasDiferencialId",
                principalTable: "Gases",
                principalColumn: "Id");
        }
    }
}
