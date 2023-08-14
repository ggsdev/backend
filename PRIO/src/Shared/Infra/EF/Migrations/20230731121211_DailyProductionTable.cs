using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class DailyProductionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductionId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Productions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CalculatedImportedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusGas = table.Column<bool>(type: "bit", nullable: false),
                    StatusOil = table.Column<bool>(type: "bit", nullable: false),
                    StatusWater = table.Column<bool>(type: "bit", nullable: false),
                    CalculatedImportedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalOil = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: true),
                    TotalGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: true),
                    TotalWater = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productions_Users_CalculatedImportedById",
                        column: x => x.CalculatedImportedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_ProductionId",
                table: "Measurements",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_CalculatedImportedById",
                table: "Productions",
                column: "CalculatedImportedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements");

            migrationBuilder.DropTable(
                name: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_ProductionId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "ProductionId",
                table: "Measurements");
        }
    }
}
