using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnZoneProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirProductions_ZoneProductions_WellProductionId",
                table: "ReservoirProductions");

            migrationBuilder.RenameColumn(
                name: "WellProductionId",
                table: "ReservoirProductions",
                newName: "ZoneProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservoirProductions_WellProductionId",
                table: "ReservoirProductions",
                newName: "IX_ReservoirProductions_ZoneProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirProductions_ZoneProductions_ZoneProductionId",
                table: "ReservoirProductions",
                column: "ZoneProductionId",
                principalTable: "ZoneProductions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirProductions_ZoneProductions_ZoneProductionId",
                table: "ReservoirProductions");

            migrationBuilder.RenameColumn(
                name: "ZoneProductionId",
                table: "ReservoirProductions",
                newName: "WellProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservoirProductions_ZoneProductionId",
                table: "ReservoirProductions",
                newName: "IX_ReservoirProductions_WellProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirProductions_ZoneProductions_WellProductionId",
                table: "ReservoirProductions",
                column: "WellProductionId",
                principalTable: "ZoneProductions",
                principalColumn: "Id");
        }
    }
}
