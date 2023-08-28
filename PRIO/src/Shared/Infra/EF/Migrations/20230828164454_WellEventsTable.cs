using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class WellEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WellEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<bool>(type: "bit", nullable: false),
                    Downtime = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellEvents_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reason = table.Column<string>(type: "VARCHAR(1000)", maxLength: 1000, nullable: false),
                    Downtime = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    WellEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventReasons_WellEvents_WellEventId",
                        column: x => x.WellEventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventReasons_WellEventId",
                table: "EventReasons",
                column: "WellEventId");

            migrationBuilder.CreateIndex(
                name: "IX_WellEvents_WellId",
                table: "WellEvents",
                column: "WellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventReasons");

            migrationBuilder.DropTable(
                name: "WellEvents");
        }
    }
}
