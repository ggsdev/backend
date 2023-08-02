using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnsInBTPDataToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "BTPDatas",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "RGO",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialWaterPerHour",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialWater",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialOilPerHour",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialOil",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquidPerHour",
                table: "BTPDatas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialLiquid",
                table: "BTPDatas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialGasPerHour",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "PotencialGas",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<decimal>(
                name: "BSW",
                table: "BTPDatas",
                type: "decimal(15,5)",
                precision: 15,
                scale: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<string>(
                name: "RGO",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialWaterPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialWater",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialOilPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialOil",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialLiquidPerHour",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PotencialLiquid",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PotencialGasPerHour",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PotencialGas",
                table: "BTPDatas",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "BSW",
                table: "BTPDatas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,5)",
                oldPrecision: 15,
                oldScale: 5);
        }
    }
}
