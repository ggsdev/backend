using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class IndexEventReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventReasons_WellEventId",
                table: "EventReasons");

            migrationBuilder.CreateIndex(
                name: "IX_EventReasons_WellEventId_StartDate",
                table: "EventReasons",
                columns: new[] { "WellEventId", "StartDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventReasons_WellEventId_StartDate",
                table: "EventReasons");

            migrationBuilder.CreateIndex(
                name: "IX_EventReasons_WellEventId",
                table: "EventReasons",
                column: "WellEventId");
        }
    }
}
