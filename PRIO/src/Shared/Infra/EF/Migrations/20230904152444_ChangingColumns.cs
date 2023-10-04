using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ChangingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemRelated",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "EventReasons");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "WellEvents",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemRelated",
                table: "EventReasons",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "SystemRelated",
                table: "EventReasons");

            migrationBuilder.AddColumn<string>(
                name: "SystemRelated",
                table: "WellEvents",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "EventReasons",
                type: "VARCHAR(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
