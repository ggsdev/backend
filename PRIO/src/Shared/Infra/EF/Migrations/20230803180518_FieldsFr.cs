using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class FieldsFr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "FRWater",
                table: "FieldsFRs",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FROil",
                table: "FieldsFRs",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FRGas",
                table: "FieldsFRs",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DailyProductionId",
                table: "FieldsFRs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionInField",
                table: "FieldsFRs",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);


            migrationBuilder.CreateIndex(
                name: "IX_Productions_InstallationId",
                table: "Productions",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldsFRs_DailyProductionId",
                table: "FieldsFRs",
                column: "DailyProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Productions_DailyProductionId",
                table: "FieldsFRs",
                column: "DailyProductionId",
                principalTable: "Productions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Installations_InstallationId",
                table: "Productions",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Productions_DailyProductionId",
                table: "FieldsFRs");



            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Installations_InstallationId",
                table: "Productions");


            migrationBuilder.DropIndex(
                name: "IX_Productions_InstallationId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_FieldsFRs_DailyProductionId",
                table: "FieldsFRs");



            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "DailyProductionId",
                table: "FieldsFRs");

            migrationBuilder.DropColumn(
                name: "ProductionInField",
                table: "FieldsFRs");

            migrationBuilder.AlterColumn<decimal>(
                name: "FRWater",
                table: "FieldsFRs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FROil",
                table: "FieldsFRs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FRGas",
                table: "FieldsFRs",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
