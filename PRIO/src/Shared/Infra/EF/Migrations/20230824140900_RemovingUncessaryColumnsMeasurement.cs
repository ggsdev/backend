using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUncessaryColumnsMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCalculated",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "VolumeAfterManualBsw_001",
                table: "Measurements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCalculated",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "VolumeAfterManualBsw_001",
                table: "Measurements",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
