using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingToDouble2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DHA_MED_REGISTRADO_039",
                table: "Volumes_039",
                type: "float(8)",
                precision: 8,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,6)",
                oldPrecision: 7,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_MED_DECLARADO_039",
                table: "Volumes_039",
                type: "float(8)",
                precision: 8,
                scale: 6,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,6)",
                oldPrecision: 7,
                oldScale: 6,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_MED_REGISTRADO_039",
                table: "Volumes_039",
                type: "decimal(7,6)",
                precision: 7,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DHA_MED_DECLARADO_039",
                table: "Volumes_039",
                type: "decimal(7,6)",
                precision: 7,
                scale: 6,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 6,
                oldNullable: true);
        }
    }
}
