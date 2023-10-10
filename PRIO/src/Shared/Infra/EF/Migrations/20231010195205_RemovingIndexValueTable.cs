using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class RemovingIndexValueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PI.Values_Date_AttributeId",
                table: "PI.Values");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PI.Values_Date_AttributeId",
                table: "PI.Values",
                columns: new[] { "Date", "AttributeId" },
                unique: true);
        }
    }
}
