using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnNameInOilCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoints_EquipmentId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_EquipmentId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoints_EquipmentId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_EquipmentId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_TOGRecoveredOils_EquipmentId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_Sections_EquipmentId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_DrainVolumes_EquipmentId",
                table: "DrainVolumes");

            migrationBuilder.DropIndex(
                name: "IX_DORs_EquipmentId",
                table: "DORs");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "TOGRecoveredOils",
                newName: "MeasuringPointId");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "Sections",
                newName: "MeasuringPointId");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "DrainVolumes",
                newName: "MeasuringPointId");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "DORs",
                newName: "MeasuringPointId");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_MeasuringPointId",
                table: "TOGRecoveredOils",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_MeasuringPointId",
                table: "Sections",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_MeasuringPointId",
                table: "DrainVolumes",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DORs_MeasuringPointId",
                table: "DORs",
                column: "MeasuringPointId",
                unique: true,
                filter: "[MeasuringPointId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_TOGRecoveredOils_MeasuringPointId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropIndex(
                name: "IX_Sections_MeasuringPointId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_DrainVolumes_MeasuringPointId",
                table: "DrainVolumes");

            migrationBuilder.DropIndex(
                name: "IX_DORs_MeasuringPointId",
                table: "DORs");

            migrationBuilder.RenameColumn(
                name: "MeasuringPointId",
                table: "TOGRecoveredOils",
                newName: "EquipmentId");

            migrationBuilder.RenameColumn(
                name: "MeasuringPointId",
                table: "Sections",
                newName: "EquipmentId");

            migrationBuilder.RenameColumn(
                name: "MeasuringPointId",
                table: "DrainVolumes",
                newName: "EquipmentId");

            migrationBuilder.RenameColumn(
                name: "MeasuringPointId",
                table: "DORs",
                newName: "EquipmentId");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_EquipmentId",
                table: "TOGRecoveredOils",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_EquipmentId",
                table: "Sections",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_EquipmentId",
                table: "DrainVolumes",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DORs_EquipmentId",
                table: "DORs",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

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
    }
}
