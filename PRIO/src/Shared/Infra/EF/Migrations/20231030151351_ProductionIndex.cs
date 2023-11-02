using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ProductionIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UniqueMeasuredAtIsActive",
                table: "Measurement.Productions",
                columns: new[] { "MeasuredAt", "IsActive" },
                unique: true,
                filter: "IsActive = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniqueMeasuredAtIsActive",
                table: "Production");
        }
    }
}
