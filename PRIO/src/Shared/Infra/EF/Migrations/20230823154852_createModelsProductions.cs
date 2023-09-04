using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class createModelsProductions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZoneProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GasProductionInZone = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    WaterProductionInZone = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    OilProductionInZone = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneProductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservoirProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GasProductionInReservoir = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    WaterProductionInReservoir = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    OilProductionInReservoir = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    WellProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservoirProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservoirProductions_ZoneProductions_WellProductionId",
                        column: x => x.WellProductionId,
                        principalTable: "ZoneProductions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompletionProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompletionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GasProductionInCompletion = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    WaterProductionInCompletion = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    OilProductionInCompletion = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    WellProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservoirProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletionProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletionProductions_ReservoirProductions_ReservoirProductionId",
                        column: x => x.ReservoirProductionId,
                        principalTable: "ReservoirProductions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletionProductions_WellProductions_WellProductionId",
                        column: x => x.WellProductionId,
                        principalTable: "WellProductions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletionProductions_ReservoirProductionId",
                table: "CompletionProductions",
                column: "ReservoirProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionProductions_WellProductionId",
                table: "CompletionProductions",
                column: "WellProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirProductions_WellProductionId",
                table: "ReservoirProductions",
                column: "WellProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletionProductions");

            migrationBuilder.DropTable(
                name: "ReservoirProductions");

            migrationBuilder.DropTable(
                name: "ZoneProductions");
        }
    }
}
