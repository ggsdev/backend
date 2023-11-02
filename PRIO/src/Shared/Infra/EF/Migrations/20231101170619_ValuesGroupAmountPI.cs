using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ValuesGroupAmountPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupAmount",
                table: "PI.Values",
                newName: "GroupAmountPI");

            migrationBuilder.AddColumn<double>(
                name: "GroupAmountAssigned",
                table: "PI.Values",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Potencial",
                table: "PI.Values",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupAmountAssigned",
                table: "PI.Values");

            migrationBuilder.DropColumn(
                name: "Potencial",
                table: "PI.Values");

            migrationBuilder.RenameColumn(
                name: "GroupAmountPI",
                table: "PI.Values",
                newName: "GroupAmount");
        }
    }
}
