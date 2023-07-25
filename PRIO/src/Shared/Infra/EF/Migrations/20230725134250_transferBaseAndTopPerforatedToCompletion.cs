using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class transferBaseAndTopPerforatedToCompletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseOfPerforated",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "TopOfPerforated",
                table: "Wells");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseOfPerforated",
                table: "Completions",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TopOfPerforated",
                table: "Completions",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseOfPerforated",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "TopOfPerforated",
                table: "Completions");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseOfPerforated",
                table: "Wells",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TopOfPerforated",
                table: "Wells",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);
        }
    }
}
