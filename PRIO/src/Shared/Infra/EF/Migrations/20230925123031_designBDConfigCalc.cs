using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDConfigCalc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssistanceGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "AssistanceGases");

            migrationBuilder.DropForeignKey(
                name: "FK_AssistanceGases_MeasuringPoints_MeasuringPointId",
                table: "AssistanceGases");

            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "ExportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportGases_MeasuringPoints_MeasuringPointId",
                table: "ExportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_GasVolumeCalculations_Hierachy.Installations_InstallationId",
                table: "GasVolumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_HighPressureGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "HighPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_HighPressureGases_MeasuringPoints_MeasuringPointId",
                table: "HighPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_HpFlares_GasVolumeCalculations_GasVolumeCalculationId",
                table: "HpFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_HpFlares_MeasuringPoints_MeasuringPointId",
                table: "HpFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "ImportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportGases_MeasuringPoints_MeasuringPointId",
                table: "ImportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_LowPressureGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "LowPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_LowPressureGases_MeasuringPoints_MeasuringPointId",
                table: "LowPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_LPFlares_GasVolumeCalculations_GasVolumeCalculationId",
                table: "LPFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_LPFlares_MeasuringPoints_MeasuringPointId",
                table: "LPFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_AC.Users_UserId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoints_MeasuringPointId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoints_Hierachy.Installations_InstallationId",
                table: "MeasuringPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_MeasuringPoints_MeasuringPointId",
                table: "NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_OilVoumeCalculations_Hierachy.Installations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "PilotGases");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotGases_MeasuringPoints_MeasuringPointId",
                table: "PilotGases");

            migrationBuilder.DropForeignKey(
                name: "FK_PurgeGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "PurgeGases");

            migrationBuilder.DropForeignKey(
                name: "FK_PurgeGases_MeasuringPoints_MeasuringPointId",
                table: "PurgeGases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TOGRecoveredOils",
                table: "TOGRecoveredOils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurgeGases",
                table: "PurgeGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PilotGases",
                table: "PilotGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OilVoumeCalculations",
                table: "OilVoumeCalculations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasuringPoints",
                table: "MeasuringPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasuringEquipments",
                table: "MeasuringEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LPFlares",
                table: "LPFlares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LowPressureGases",
                table: "LowPressureGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImportGases",
                table: "ImportGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HpFlares",
                table: "HpFlares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HighPressureGases",
                table: "HighPressureGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GasVolumeCalculations",
                table: "GasVolumeCalculations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExportGases",
                table: "ExportGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrainVolumes",
                table: "DrainVolumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DORs",
                table: "DORs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssistanceGases",
                table: "AssistanceGases");

            migrationBuilder.RenameTable(
                name: "TOGRecoveredOils",
                newName: "ConfigCalc.TOGRecoveredOils");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "ConfigCalc.Sections");

            migrationBuilder.RenameTable(
                name: "PurgeGases",
                newName: "ConfigCalc.PurgeGases");

            migrationBuilder.RenameTable(
                name: "PilotGases",
                newName: "ConfigCalc.PilotGases");

            migrationBuilder.RenameTable(
                name: "OilVoumeCalculations",
                newName: "ConfigCalc.OilVoumeCalculations");

            migrationBuilder.RenameTable(
                name: "MeasuringPoints",
                newName: "Hierarchy.MeasuringPoints");

            migrationBuilder.RenameTable(
                name: "MeasuringEquipments",
                newName: "Hierarchy.MeasuringEquipments");

            migrationBuilder.RenameTable(
                name: "LPFlares",
                newName: "ConfigCalc.LPFlares");

            migrationBuilder.RenameTable(
                name: "LowPressureGases",
                newName: "ConfigCalc.LowPressureGases");

            migrationBuilder.RenameTable(
                name: "ImportGases",
                newName: "ConfigCalc.ImportGases");

            migrationBuilder.RenameTable(
                name: "HpFlares",
                newName: "ConfigCalc.HpFlares");

            migrationBuilder.RenameTable(
                name: "HighPressureGases",
                newName: "ConfigCalc.HighPressureGases");

            migrationBuilder.RenameTable(
                name: "GasVolumeCalculations",
                newName: "ConfigCalc.GasVolumeCalculations");

            migrationBuilder.RenameTable(
                name: "ExportGases",
                newName: "ConfigCalc.ExportGases");

            migrationBuilder.RenameTable(
                name: "DrainVolumes",
                newName: "ConfigCalc.DrainVolumes");

            migrationBuilder.RenameTable(
                name: "DORs",
                newName: "ConfigCalc.DORs");

            migrationBuilder.RenameTable(
                name: "AssistanceGases",
                newName: "ConfigCalc.AssistanceGases");

            migrationBuilder.RenameIndex(
                name: "IX_TOGRecoveredOils_OilVolumeCalculationId",
                table: "ConfigCalc.TOGRecoveredOils",
                newName: "IX_ConfigCalc.TOGRecoveredOils_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_TOGRecoveredOils_MeasuringPointId",
                table: "ConfigCalc.TOGRecoveredOils",
                newName: "IX_ConfigCalc.TOGRecoveredOils_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_OilVolumeCalculationId",
                table: "ConfigCalc.Sections",
                newName: "IX_ConfigCalc.Sections_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_MeasuringPointId",
                table: "ConfigCalc.Sections",
                newName: "IX_ConfigCalc.Sections_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_PurgeGases_MeasuringPointId",
                table: "ConfigCalc.PurgeGases",
                newName: "IX_ConfigCalc.PurgeGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_PurgeGases_GasVolumeCalculationId",
                table: "ConfigCalc.PurgeGases",
                newName: "IX_ConfigCalc.PurgeGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_PilotGases_MeasuringPointId",
                table: "ConfigCalc.PilotGases",
                newName: "IX_ConfigCalc.PilotGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_PilotGases_GasVolumeCalculationId",
                table: "ConfigCalc.PilotGases",
                newName: "IX_ConfigCalc.PilotGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "ConfigCalc.OilVoumeCalculations",
                newName: "IX_ConfigCalc.OilVoumeCalculations_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasuringPoints_InstallationId",
                table: "Hierarchy.MeasuringPoints",
                newName: "IX_Hierarchy.MeasuringPoints_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasuringEquipments_UserId",
                table: "Hierarchy.MeasuringEquipments",
                newName: "IX_Hierarchy.MeasuringEquipments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasuringEquipments_MeasuringPointId",
                table: "Hierarchy.MeasuringEquipments",
                newName: "IX_Hierarchy.MeasuringEquipments_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_LPFlares_MeasuringPointId",
                table: "ConfigCalc.LPFlares",
                newName: "IX_ConfigCalc.LPFlares_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_LPFlares_GasVolumeCalculationId",
                table: "ConfigCalc.LPFlares",
                newName: "IX_ConfigCalc.LPFlares_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_LowPressureGases_MeasuringPointId",
                table: "ConfigCalc.LowPressureGases",
                newName: "IX_ConfigCalc.LowPressureGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_LowPressureGases_GasVolumeCalculationId",
                table: "ConfigCalc.LowPressureGases",
                newName: "IX_ConfigCalc.LowPressureGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ImportGases_MeasuringPointId",
                table: "ConfigCalc.ImportGases",
                newName: "IX_ConfigCalc.ImportGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ImportGases_GasVolumeCalculationId",
                table: "ConfigCalc.ImportGases",
                newName: "IX_ConfigCalc.ImportGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_HpFlares_MeasuringPointId",
                table: "ConfigCalc.HpFlares",
                newName: "IX_ConfigCalc.HpFlares_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_HpFlares_GasVolumeCalculationId",
                table: "ConfigCalc.HpFlares",
                newName: "IX_ConfigCalc.HpFlares_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_HighPressureGases_MeasuringPointId",
                table: "ConfigCalc.HighPressureGases",
                newName: "IX_ConfigCalc.HighPressureGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_HighPressureGases_GasVolumeCalculationId",
                table: "ConfigCalc.HighPressureGases",
                newName: "IX_ConfigCalc.HighPressureGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_GasVolumeCalculations_InstallationId",
                table: "ConfigCalc.GasVolumeCalculations",
                newName: "IX_ConfigCalc.GasVolumeCalculations_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_ExportGases_MeasuringPointId",
                table: "ConfigCalc.ExportGases",
                newName: "IX_ConfigCalc.ExportGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ExportGases_GasVolumeCalculationId",
                table: "ConfigCalc.ExportGases",
                newName: "IX_ConfigCalc.ExportGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_DrainVolumes_OilVolumeCalculationId",
                table: "ConfigCalc.DrainVolumes",
                newName: "IX_ConfigCalc.DrainVolumes_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_DrainVolumes_MeasuringPointId",
                table: "ConfigCalc.DrainVolumes",
                newName: "IX_ConfigCalc.DrainVolumes_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_DORs_OilVolumeCalculationId",
                table: "ConfigCalc.DORs",
                newName: "IX_ConfigCalc.DORs_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_DORs_MeasuringPointId",
                table: "ConfigCalc.DORs",
                newName: "IX_ConfigCalc.DORs_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_AssistanceGases_MeasuringPointId",
                table: "ConfigCalc.AssistanceGases",
                newName: "IX_ConfigCalc.AssistanceGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_AssistanceGases_GasVolumeCalculationId",
                table: "ConfigCalc.AssistanceGases",
                newName: "IX_ConfigCalc.AssistanceGases_GasVolumeCalculationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.TOGRecoveredOils",
                table: "ConfigCalc.TOGRecoveredOils",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.Sections",
                table: "ConfigCalc.Sections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.PurgeGases",
                table: "ConfigCalc.PurgeGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.PilotGases",
                table: "ConfigCalc.PilotGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.OilVoumeCalculations",
                table: "ConfigCalc.OilVoumeCalculations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierarchy.MeasuringPoints",
                table: "Hierarchy.MeasuringPoints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierarchy.MeasuringEquipments",
                table: "Hierarchy.MeasuringEquipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.LPFlares",
                table: "ConfigCalc.LPFlares",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.LowPressureGases",
                table: "ConfigCalc.LowPressureGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.ImportGases",
                table: "ConfigCalc.ImportGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.HpFlares",
                table: "ConfigCalc.HpFlares",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.HighPressureGases",
                table: "ConfigCalc.HighPressureGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.GasVolumeCalculations",
                table: "ConfigCalc.GasVolumeCalculations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.ExportGases",
                table: "ConfigCalc.ExportGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.DrainVolumes",
                table: "ConfigCalc.DrainVolumes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.DORs",
                table: "ConfigCalc.DORs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigCalc.AssistanceGases",
                table: "ConfigCalc.AssistanceGases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.AssistanceGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.AssistanceGases",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.AssistanceGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.AssistanceGases",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.DORs_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.DORs",
                column: "OilVolumeCalculationId",
                principalTable: "ConfigCalc.OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.DORs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.DORs",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.DrainVolumes_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.DrainVolumes",
                column: "OilVolumeCalculationId",
                principalTable: "ConfigCalc.OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.DrainVolumes_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.DrainVolumes",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.ExportGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.ExportGases",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.ExportGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.ExportGases",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.GasVolumeCalculations_Hierachy.Installations_InstallationId",
                table: "ConfigCalc.GasVolumeCalculations",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.HighPressureGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.HighPressureGases",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.HighPressureGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.HighPressureGases",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.HpFlares_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.HpFlares",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.HpFlares_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.HpFlares",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.ImportGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.ImportGases",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.ImportGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.ImportGases",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.LowPressureGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.LowPressureGases",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.LowPressureGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.LowPressureGases",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.LPFlares_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.LPFlares",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.LPFlares_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.LPFlares",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.OilVoumeCalculations_Hierachy.Installations_InstallationId",
                table: "ConfigCalc.OilVoumeCalculations",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.PilotGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.PilotGases",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.PilotGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.PilotGases",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.PurgeGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.PurgeGases",
                column: "GasVolumeCalculationId",
                principalTable: "ConfigCalc.GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.PurgeGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.PurgeGases",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.Sections_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.Sections",
                column: "OilVolumeCalculationId",
                principalTable: "ConfigCalc.OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.Sections_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.Sections",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.TOGRecoveredOils_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.TOGRecoveredOils",
                column: "OilVolumeCalculationId",
                principalTable: "ConfigCalc.OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCalc.TOGRecoveredOils_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.TOGRecoveredOils",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hierarchy.MeasuringEquipments_AC.Users_UserId",
                table: "Hierarchy.MeasuringEquipments",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierarchy.MeasuringEquipments_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Hierarchy.MeasuringEquipments",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierarchy.MeasuringPoints_Hierachy.Installations_InstallationId",
                table: "Hierarchy.MeasuringPoints",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Measurements",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "NFSMs",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.AssistanceGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.AssistanceGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.AssistanceGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.AssistanceGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.DORs_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.DORs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.DrainVolumes_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.DrainVolumes_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.ExportGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.ExportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.ExportGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.ExportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.GasVolumeCalculations_Hierachy.Installations_InstallationId",
                table: "ConfigCalc.GasVolumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.HighPressureGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.HighPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.HighPressureGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.HighPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.HpFlares_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.HpFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.HpFlares_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.HpFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.ImportGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.ImportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.ImportGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.ImportGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.LowPressureGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.LowPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.LowPressureGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.LowPressureGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.LPFlares_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.LPFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.LPFlares_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.LPFlares");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.OilVoumeCalculations_Hierachy.Installations_InstallationId",
                table: "ConfigCalc.OilVoumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.PilotGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.PilotGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.PilotGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.PilotGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.PurgeGases_ConfigCalc.GasVolumeCalculations_GasVolumeCalculationId",
                table: "ConfigCalc.PurgeGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.PurgeGases_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.PurgeGases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.Sections_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.Sections_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.TOGRecoveredOils_ConfigCalc.OilVoumeCalculations_OilVolumeCalculationId",
                table: "ConfigCalc.TOGRecoveredOils");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCalc.TOGRecoveredOils_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "ConfigCalc.TOGRecoveredOils");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierarchy.MeasuringEquipments_AC.Users_UserId",
                table: "Hierarchy.MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierarchy.MeasuringEquipments_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Hierarchy.MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierarchy.MeasuringPoints_Hierachy.Installations_InstallationId",
                table: "Hierarchy.MeasuringPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "NFSMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierarchy.MeasuringPoints",
                table: "Hierarchy.MeasuringPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierarchy.MeasuringEquipments",
                table: "Hierarchy.MeasuringEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.TOGRecoveredOils",
                table: "ConfigCalc.TOGRecoveredOils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.Sections",
                table: "ConfigCalc.Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.PurgeGases",
                table: "ConfigCalc.PurgeGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.PilotGases",
                table: "ConfigCalc.PilotGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.OilVoumeCalculations",
                table: "ConfigCalc.OilVoumeCalculations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.LPFlares",
                table: "ConfigCalc.LPFlares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.LowPressureGases",
                table: "ConfigCalc.LowPressureGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.ImportGases",
                table: "ConfigCalc.ImportGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.HpFlares",
                table: "ConfigCalc.HpFlares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.HighPressureGases",
                table: "ConfigCalc.HighPressureGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.GasVolumeCalculations",
                table: "ConfigCalc.GasVolumeCalculations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.ExportGases",
                table: "ConfigCalc.ExportGases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.DrainVolumes",
                table: "ConfigCalc.DrainVolumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.DORs",
                table: "ConfigCalc.DORs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigCalc.AssistanceGases",
                table: "ConfigCalc.AssistanceGases");

            migrationBuilder.RenameTable(
                name: "Hierarchy.MeasuringPoints",
                newName: "MeasuringPoints");

            migrationBuilder.RenameTable(
                name: "Hierarchy.MeasuringEquipments",
                newName: "MeasuringEquipments");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.TOGRecoveredOils",
                newName: "TOGRecoveredOils");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.Sections",
                newName: "Sections");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.PurgeGases",
                newName: "PurgeGases");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.PilotGases",
                newName: "PilotGases");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.OilVoumeCalculations",
                newName: "OilVoumeCalculations");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.LPFlares",
                newName: "LPFlares");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.LowPressureGases",
                newName: "LowPressureGases");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.ImportGases",
                newName: "ImportGases");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.HpFlares",
                newName: "HpFlares");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.HighPressureGases",
                newName: "HighPressureGases");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.GasVolumeCalculations",
                newName: "GasVolumeCalculations");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.ExportGases",
                newName: "ExportGases");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.DrainVolumes",
                newName: "DrainVolumes");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.DORs",
                newName: "DORs");

            migrationBuilder.RenameTable(
                name: "ConfigCalc.AssistanceGases",
                newName: "AssistanceGases");

            migrationBuilder.RenameIndex(
                name: "IX_Hierarchy.MeasuringPoints_InstallationId",
                table: "MeasuringPoints",
                newName: "IX_MeasuringPoints_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierarchy.MeasuringEquipments_UserId",
                table: "MeasuringEquipments",
                newName: "IX_MeasuringEquipments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierarchy.MeasuringEquipments_MeasuringPointId",
                table: "MeasuringEquipments",
                newName: "IX_MeasuringEquipments_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.TOGRecoveredOils_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                newName: "IX_TOGRecoveredOils_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.TOGRecoveredOils_MeasuringPointId",
                table: "TOGRecoveredOils",
                newName: "IX_TOGRecoveredOils_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.Sections_OilVolumeCalculationId",
                table: "Sections",
                newName: "IX_Sections_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.Sections_MeasuringPointId",
                table: "Sections",
                newName: "IX_Sections_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.PurgeGases_MeasuringPointId",
                table: "PurgeGases",
                newName: "IX_PurgeGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.PurgeGases_GasVolumeCalculationId",
                table: "PurgeGases",
                newName: "IX_PurgeGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.PilotGases_MeasuringPointId",
                table: "PilotGases",
                newName: "IX_PilotGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.PilotGases_GasVolumeCalculationId",
                table: "PilotGases",
                newName: "IX_PilotGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations",
                newName: "IX_OilVoumeCalculations_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.LPFlares_MeasuringPointId",
                table: "LPFlares",
                newName: "IX_LPFlares_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.LPFlares_GasVolumeCalculationId",
                table: "LPFlares",
                newName: "IX_LPFlares_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.LowPressureGases_MeasuringPointId",
                table: "LowPressureGases",
                newName: "IX_LowPressureGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.LowPressureGases_GasVolumeCalculationId",
                table: "LowPressureGases",
                newName: "IX_LowPressureGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.ImportGases_MeasuringPointId",
                table: "ImportGases",
                newName: "IX_ImportGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.ImportGases_GasVolumeCalculationId",
                table: "ImportGases",
                newName: "IX_ImportGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.HpFlares_MeasuringPointId",
                table: "HpFlares",
                newName: "IX_HpFlares_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.HpFlares_GasVolumeCalculationId",
                table: "HpFlares",
                newName: "IX_HpFlares_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.HighPressureGases_MeasuringPointId",
                table: "HighPressureGases",
                newName: "IX_HighPressureGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.HighPressureGases_GasVolumeCalculationId",
                table: "HighPressureGases",
                newName: "IX_HighPressureGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.GasVolumeCalculations_InstallationId",
                table: "GasVolumeCalculations",
                newName: "IX_GasVolumeCalculations_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.ExportGases_MeasuringPointId",
                table: "ExportGases",
                newName: "IX_ExportGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.ExportGases_GasVolumeCalculationId",
                table: "ExportGases",
                newName: "IX_ExportGases_GasVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.DrainVolumes_OilVolumeCalculationId",
                table: "DrainVolumes",
                newName: "IX_DrainVolumes_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.DrainVolumes_MeasuringPointId",
                table: "DrainVolumes",
                newName: "IX_DrainVolumes_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.DORs_OilVolumeCalculationId",
                table: "DORs",
                newName: "IX_DORs_OilVolumeCalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.DORs_MeasuringPointId",
                table: "DORs",
                newName: "IX_DORs_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.AssistanceGases_MeasuringPointId",
                table: "AssistanceGases",
                newName: "IX_AssistanceGases_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigCalc.AssistanceGases_GasVolumeCalculationId",
                table: "AssistanceGases",
                newName: "IX_AssistanceGases_GasVolumeCalculationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasuringPoints",
                table: "MeasuringPoints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasuringEquipments",
                table: "MeasuringEquipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TOGRecoveredOils",
                table: "TOGRecoveredOils",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurgeGases",
                table: "PurgeGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PilotGases",
                table: "PilotGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OilVoumeCalculations",
                table: "OilVoumeCalculations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LPFlares",
                table: "LPFlares",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LowPressureGases",
                table: "LowPressureGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImportGases",
                table: "ImportGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HpFlares",
                table: "HpFlares",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HighPressureGases",
                table: "HighPressureGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GasVolumeCalculations",
                table: "GasVolumeCalculations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExportGases",
                table: "ExportGases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrainVolumes",
                table: "DrainVolumes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DORs",
                table: "DORs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssistanceGases",
                table: "AssistanceGases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "AssistanceGases",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceGases_MeasuringPoints_MeasuringPointId",
                table: "AssistanceGases",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringPoints_MeasuringPointId",
                table: "DORs",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringPoints_MeasuringPointId",
                table: "DrainVolumes",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "ExportGases",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportGases_MeasuringPoints_MeasuringPointId",
                table: "ExportGases",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GasVolumeCalculations_Hierachy.Installations_InstallationId",
                table: "GasVolumeCalculations",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HighPressureGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "HighPressureGases",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HighPressureGases_MeasuringPoints_MeasuringPointId",
                table: "HighPressureGases",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HpFlares_GasVolumeCalculations_GasVolumeCalculationId",
                table: "HpFlares",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HpFlares_MeasuringPoints_MeasuringPointId",
                table: "HpFlares",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "ImportGases",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportGases_MeasuringPoints_MeasuringPointId",
                table: "ImportGases",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LowPressureGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "LowPressureGases",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LowPressureGases_MeasuringPoints_MeasuringPointId",
                table: "LowPressureGases",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LPFlares_GasVolumeCalculations_GasVolumeCalculationId",
                table: "LPFlares",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LPFlares_MeasuringPoints_MeasuringPointId",
                table: "LPFlares",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_AC.Users_UserId",
                table: "MeasuringEquipments",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoints_MeasuringPointId",
                table: "MeasuringEquipments",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoints_Hierachy.Installations_InstallationId",
                table: "MeasuringPoints",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_MeasuringPoints_MeasuringPointId",
                table: "NFSMs",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OilVoumeCalculations_Hierachy.Installations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PilotGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "PilotGases",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PilotGases_MeasuringPoints_MeasuringPointId",
                table: "PilotGases",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurgeGases_GasVolumeCalculations_GasVolumeCalculationId",
                table: "PurgeGases",
                column: "GasVolumeCalculationId",
                principalTable: "GasVolumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurgeGases_MeasuringPoints_MeasuringPointId",
                table: "PurgeGases",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringPoints_MeasuringPointId",
                table: "Sections",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoints_MeasuringPointId",
                table: "TOGRecoveredOils",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
