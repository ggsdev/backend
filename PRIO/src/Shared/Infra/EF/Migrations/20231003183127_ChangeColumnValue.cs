using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                table: "ManualWellConfiguration.ProductivityIndex",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "ManualWellConfiguration.InjectivityIndex",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "ManualWellConfiguration.BuildUps",
                newName: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ManualWellConfiguration.ProductivityIndex",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ManualWellConfiguration.InjectivityIndex",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ManualWellConfiguration.BuildUps",
                newName: "value");
        }
    }
}
