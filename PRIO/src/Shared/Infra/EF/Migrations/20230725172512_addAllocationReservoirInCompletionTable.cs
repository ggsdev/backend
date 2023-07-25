using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class addAllocationReservoirInCompletionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodCompletion",
                table: "Completions");

            migrationBuilder.AddColumn<decimal>(
                name: "AllocationReservoir",
                table: "Completions",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllocationReservoir",
                table: "Completions");

            migrationBuilder.AddColumn<string>(
                name: "CodCompletion",
                table: "Completions",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true);
        }
    }
}
