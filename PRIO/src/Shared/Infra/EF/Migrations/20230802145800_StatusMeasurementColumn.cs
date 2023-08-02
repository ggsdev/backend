using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class StatusMeasurementColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StatusMeasuringPoint",
                table: "Measurements",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VolumeAfterManualBsw_001",
                table: "Measurements",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusMeasuringPoint",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "VolumeAfterManualBsw_001",
                table: "Measurements");
        }
    }
}
