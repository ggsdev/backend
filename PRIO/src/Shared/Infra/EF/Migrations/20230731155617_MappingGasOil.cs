using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class MappingGasOil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Users_CalculatedImportedById",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "StatusGas",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "StatusOil",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "TotalGas",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "TotalOil",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "TotalWater",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Productions");

            migrationBuilder.RenameColumn(
                name: "StatusWater",
                table: "Productions",
                newName: "StatusProduction");

            migrationBuilder.AddColumn<Guid>(
                name: "GasId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OilId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Gases",
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
                    table.PrimaryKey("PK_Gases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oils",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusOil = table.Column<bool>(type: "bit", nullable: false),
                    TotalOil = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    Bsw = table.Column<decimal>(type: "DECIMAL(4,2)", precision: 4, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oils", x => x.Id);
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
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions",
                column: "GasId",
                principalTable: "Gases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Oils_OilId",
                table: "Productions",
                column: "OilId",
                principalTable: "Oils",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Users_CalculatedImportedById",
                table: "Productions",
                column: "CalculatedImportedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Oils_OilId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Users_CalculatedImportedById",
                table: "Productions");

            migrationBuilder.DropTable(
                name: "Gases");

            migrationBuilder.DropTable(
                name: "Oils");

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
                name: "OilId",
                table: "Productions");

            migrationBuilder.RenameColumn(
                name: "StatusProduction",
                table: "Productions",
                newName: "StatusWater");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Productions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Productions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Productions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Productions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StatusGas",
                table: "Productions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StatusOil",
                table: "Productions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGas",
                table: "Productions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalOil",
                table: "Productions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalWater",
                table: "Productions",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Productions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Users_CalculatedImportedById",
                table: "Productions",
                column: "CalculatedImportedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
