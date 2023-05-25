using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingColumnToMatchDIC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameAnp",
                table: "Wells",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LongitudeBaseDD",
                table: "Wells",
                newName: "LongitudeDD");

            migrationBuilder.RenameColumn(
                name: "LongitudeBase4C",
                table: "Wells",
                newName: "Longitude4C");

            migrationBuilder.RenameColumn(
                name: "LatitudeBaseDD",
                table: "Wells",
                newName: "LatitudeDD");

            migrationBuilder.RenameColumn(
                name: "LatitudeBase4C",
                table: "Wells",
                newName: "Latitude4C");

            migrationBuilder.RenameColumn(
                name: "ArtificialElevation",
                table: "Wells",
                newName: "ArtificialLift");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Wells",
                newName: "NameAnp");

            migrationBuilder.RenameColumn(
                name: "LongitudeDD",
                table: "Wells",
                newName: "LongitudeBaseDD");

            migrationBuilder.RenameColumn(
                name: "Longitude4C",
                table: "Wells",
                newName: "LongitudeBase4C");

            migrationBuilder.RenameColumn(
                name: "LatitudeDD",
                table: "Wells",
                newName: "LatitudeBaseDD");

            migrationBuilder.RenameColumn(
                name: "Latitude4C",
                table: "Wells",
                newName: "LatitudeBase4C");

            migrationBuilder.RenameColumn(
                name: "ArtificialLift",
                table: "Wells",
                newName: "ArtificialElevation");
        }
    }
}
