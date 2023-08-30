using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class DecimalIntervalColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Interval",
                table: "WellEvents",
                type: "float(15)",
                precision: 15,
                scale: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Interval",
                table: "WellEvents",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(15)",
                oldPrecision: 15,
                oldScale: 6,
                oldNullable: true);
        }
    }
}
