using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class NfsmProductionMoreDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeBefore",
                table: "NFSMsProductions",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeAfter",
                table: "NFSMsProductions",
                type: "decimal(14,5)",
                precision: 14,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeBefore",
                table: "NFSMsProductions",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeAfter",
                table: "NFSMsProductions",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,5)",
                oldPrecision: 14,
                oldScale: 5,
                oldNullable: true);
        }
    }
}
