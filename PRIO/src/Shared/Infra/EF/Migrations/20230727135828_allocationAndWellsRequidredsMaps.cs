using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class allocationAndWellsRequidredsMaps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeBaseCoordinate",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "LongitudeDD",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Longitude4C",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "LatitudeDD",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude4C",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "DatumHorizontal",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "CoordY",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "CoordX",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<decimal>(
                name: "AllocationReservoir",
                table: "Completions",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllocationReservoir",
                table: "Completions");

            migrationBuilder.AlterColumn<string>(
                name: "TypeBaseCoordinate",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LongitudeDD",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Longitude4C",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LatitudeDD",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude4C",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DatumHorizontal",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoordY",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoordX",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);
        }
    }
}
