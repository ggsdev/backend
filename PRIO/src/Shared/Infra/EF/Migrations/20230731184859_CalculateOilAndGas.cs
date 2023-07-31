using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class CalculateOilAndGas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions");

            migrationBuilder.DropTable(
                name: "Gases");

            migrationBuilder.DropIndex(
                name: "IX_Productions_GasId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_OilId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "GasId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Bsw",
                table: "Oils");

            migrationBuilder.AlterColumn<Guid>(
                name: "OilId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MeasuredAt",
                table: "Productions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CalculatedImportedAt",
                table: "Productions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GasDiferencialId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GasLinearId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOil",
                table: "Oils",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AddColumn<decimal>(
                name: "BswAverage",
                table: "Oils",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GasesDiferencials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusGas = table.Column<bool>(type: "bit", nullable: false),
                    TotalGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ExportedGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ImportedGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    BurntGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    FuelGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasesDiferencials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GasesLinears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusGas = table.Column<bool>(type: "bit", nullable: false),
                    TotalGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ExportedGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ImportedGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    BurntGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    FuelGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasesLinears", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productions_GasDiferencialId",
                table: "Productions",
                column: "GasDiferencialId",
                unique: true,
                filter: "[GasDiferencialId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_GasLinearId",
                table: "Productions",
                column: "GasLinearId",
                unique: true,
                filter: "[GasLinearId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_OilId",
                table: "Productions",
                column: "OilId",
                unique: true,
                filter: "[OilId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_GasesDiferencials_GasDiferencialId",
                table: "Productions",
                column: "GasDiferencialId",
                principalTable: "GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_GasesLinears_GasLinearId",
                table: "Productions",
                column: "GasLinearId",
                principalTable: "GasesLinears",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_GasesDiferencials_GasDiferencialId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_GasesLinears_GasLinearId",
                table: "Productions");

            migrationBuilder.DropTable(
                name: "GasesDiferencials");

            migrationBuilder.DropTable(
                name: "GasesLinears");

            migrationBuilder.DropIndex(
                name: "IX_Productions_GasDiferencialId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_GasLinearId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_OilId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "GasDiferencialId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "GasLinearId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "BswAverage",
                table: "Oils");

            migrationBuilder.AlterColumn<Guid>(
                name: "OilId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MeasuredAt",
                table: "Productions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CalculatedImportedAt",
                table: "Productions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "GasId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOil",
                table: "Oils",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Bsw",
                table: "Oils",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Gases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BurntGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExportedGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    FuelGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ImportedGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StatusGas = table.Column<bool>(type: "bit", nullable: false),
                    TotalGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gases", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productions_GasId",
                table: "Productions",
                column: "GasId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productions_OilId",
                table: "Productions",
                column: "OilId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions",
                column: "GasId",
                principalTable: "Gases",
                principalColumn: "Id");
        }
    }
}
