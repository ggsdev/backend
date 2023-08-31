using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class EventRelatedCodeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionLosses");

            migrationBuilder.AddColumn<string>(
                name: "EventRelatedCode",
                table: "WellEvents",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventRelatedCode",
                table: "WellEvents");

            migrationBuilder.CreateTable(
                name: "ProductionLosses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Downtime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EfficienceLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductionLost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProportionalDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WellProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLosses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLosses_EventId",
                table: "ProductionLosses",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLosses_WellProductionId",
                table: "ProductionLosses",
                column: "WellProductionId");
        }
    }
}
