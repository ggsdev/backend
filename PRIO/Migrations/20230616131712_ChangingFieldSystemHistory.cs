using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ChangingFieldSystemHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedData",
                table: "SystemHistories",
                newName: "CurrentData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentData",
                table: "SystemHistories",
                newName: "UpdatedData");
        }
    }
}
