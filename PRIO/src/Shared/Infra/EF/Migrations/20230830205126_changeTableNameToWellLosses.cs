using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeTableNameToWellLosses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionLosses");

            migrationBuilder.CreateTable(
                name: "WellLosses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EfficienceLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionLost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Downtime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProportionalDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellAllocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellLosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellLosses_WellAllocations_WellAllocationId",
                        column: x => x.WellAllocationId,
                        principalTable: "WellAllocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WellLosses_WellEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellLosses_EventId",
                table: "WellLosses",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_WellLosses_WellAllocationId",
                table: "WellLosses",
                column: "WellAllocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WellLosses");

            migrationBuilder.CreateTable(
                name: "ProductionLosses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellAllocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Downtime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EfficienceLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductionLost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProportionalDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionLosses_WellAllocations_WellAllocationId",
                        column: x => x.WellAllocationId,
                        principalTable: "WellAllocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionLosses_WellEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLosses_EventId",
                table: "ProductionLosses",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLosses_WellAllocationId",
                table: "ProductionLosses",
                column: "WellAllocationId");
        }
    }
}
