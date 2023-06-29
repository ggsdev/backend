using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FixInstallation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoint_EquipmentId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoint_EquipmentId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoint_MeasuringPointId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoint_Installations_InstallationId",
                table: "MeasuringPoint");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoint_EquipmentId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoint_EquipmentId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasuringPoint",
                table: "MeasuringPoint");

            migrationBuilder.RenameTable(
                name: "MeasuringPoint",
                newName: "MeasuringPoints");

            migrationBuilder.RenameIndex(
                name: "IX_MeasuringPoint_InstallationId",
                table: "MeasuringPoints",
                newName: "IX_MeasuringPoints_InstallationId");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasuringPoints",
                table: "MeasuringPoints",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringPoints_EquipmentId",
                table: "DORs",
                column: "EquipmentId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_EquipmentId",
                table: "DrainVolumes",
                column: "EquipmentId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoints_MeasuringPointId",
                table: "MeasuringEquipments",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoints_Installations_InstallationId",
                table: "MeasuringPoints",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringPoints_EquipmentId",
                table: "Sections",
                column: "EquipmentId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_EquipmentId",
                table: "TOGRecoveredOils",
                column: "EquipmentId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoints_EquipmentId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_EquipmentId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoints_MeasuringPointId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoints_Installations_InstallationId",
                table: "MeasuringPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoints_EquipmentId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_EquipmentId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasuringPoints",
                table: "MeasuringPoints");

            migrationBuilder.RenameTable(
                name: "MeasuringPoints",
                newName: "MeasuringPoint");

            migrationBuilder.RenameIndex(
                name: "IX_MeasuringPoints_InstallationId",
                table: "MeasuringPoint",
                newName: "IX_MeasuringPoint_InstallationId");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasuringPoint",
                table: "MeasuringPoint",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringPoint_EquipmentId",
                table: "DORs",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringPoint_EquipmentId",
                table: "DrainVolumes",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoint_MeasuringPointId",
                table: "MeasuringEquipments",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoint_Installations_InstallationId",
                table: "MeasuringPoint",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringPoint_EquipmentId",
                table: "Sections",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoint_EquipmentId",
                table: "TOGRecoveredOils",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");
        }
    }
}
