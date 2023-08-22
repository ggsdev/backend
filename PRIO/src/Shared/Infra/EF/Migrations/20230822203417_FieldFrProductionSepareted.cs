using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FieldFrProductionSepareted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProductionInFieldAsPercentage",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionInFieldAsPercentage",
                table: "FieldsFRs");
        }
    }
}
