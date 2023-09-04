using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class EventTableWithPivot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventType",
                table: "WellEvents",
                newName: "EventStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "WellEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WellEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "WellEventWellProduction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellEventWellProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellEventWellProduction_WellEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WellEventWellProduction_WellProductions_WellProductionId",
                        column: x => x.WellProductionId,
                        principalTable: "WellProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellEventWellProduction_EventId",
                table: "WellEventWellProduction",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_WellEventWellProduction_WellProductionId",
                table: "WellEventWellProduction",
                column: "WellProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WellEventWellProduction");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WellEvents");

            migrationBuilder.RenameColumn(
                name: "EventStatus",
                table: "WellEvents",
                newName: "EventType");
        }
    }
}
