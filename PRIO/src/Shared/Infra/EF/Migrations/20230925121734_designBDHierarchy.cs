using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_Users_UserId",
                table: "Clusters");

            migrationBuilder.DropForeignKey(
                name: "FK_Completions_Reservoirs_ReservoirId",
                table: "Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Completions_Users_UserId",
                table: "Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Completions_Wells_WellId",
                table: "Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Installations_InstallationId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Fields_FieldId",
                table: "FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_GasVolumeCalculations_Installations_InstallationId",
                table: "GasVolumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Users_UserId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_IntallationsBTPs_Installations_InstallationId",
                table: "IntallationsBTPs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Installations_InstallationId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoints_Installations_InstallationId",
                table: "MeasuringPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_Installations_InstallationId",
                table: "NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Installations_InstallationId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Users_UserId",
                table: "Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Wells_WellId",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Wells_Fields_FieldId",
                table: "Wells");

            migrationBuilder.DropForeignKey(
                name: "FK_Wells_Users_UserId",
                table: "Wells");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTests_Wells_WellId",
                table: "WellTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Zones_Fields_FieldId",
                table: "Zones");

            migrationBuilder.DropForeignKey(
                name: "FK_Zones_Users_UserId",
                table: "Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zones",
                table: "Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wells",
                table: "Wells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservoirs",
                table: "Reservoirs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Installations",
                table: "Installations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fields",
                table: "Fields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Completions",
                table: "Completions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clusters",
                table: "Clusters");

            migrationBuilder.RenameTable(
                name: "Zones",
                newName: "Hierachy.Zones");

            migrationBuilder.RenameTable(
                name: "Wells",
                newName: "Hierachy.Wells");

            migrationBuilder.RenameTable(
                name: "Reservoirs",
                newName: "Hierachy.Reservoirs");

            migrationBuilder.RenameTable(
                name: "Installations",
                newName: "Hierachy.Installations");

            migrationBuilder.RenameTable(
                name: "Fields",
                newName: "Hierachy.Fields");

            migrationBuilder.RenameTable(
                name: "Completions",
                newName: "Hierachy.Completions");

            migrationBuilder.RenameTable(
                name: "Clusters",
                newName: "Hierachy.Clusters");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_UserId",
                table: "Hierachy.Zones",
                newName: "IX_Hierachy.Zones_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_FieldId",
                table: "Hierachy.Zones",
                newName: "IX_Hierachy.Zones_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Wells_UserId",
                table: "Hierachy.Wells",
                newName: "IX_Hierachy.Wells_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wells_FieldId",
                table: "Hierachy.Wells",
                newName: "IX_Hierachy.Wells_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_ZoneId",
                table: "Hierachy.Reservoirs",
                newName: "IX_Hierachy.Reservoirs_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_UserId",
                table: "Hierachy.Reservoirs",
                newName: "IX_Hierachy.Reservoirs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Installations_UserId",
                table: "Hierachy.Installations",
                newName: "IX_Hierachy.Installations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Installations_ClusterId",
                table: "Hierachy.Installations",
                newName: "IX_Hierachy.Installations_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_Fields_UserId",
                table: "Hierachy.Fields",
                newName: "IX_Hierachy.Fields_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Fields_InstallationId",
                table: "Hierachy.Fields",
                newName: "IX_Hierachy.Fields_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Completions_WellId",
                table: "Hierachy.Completions",
                newName: "IX_Hierachy.Completions_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_Completions_UserId",
                table: "Hierachy.Completions",
                newName: "IX_Hierachy.Completions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Completions_ReservoirId",
                table: "Hierachy.Completions",
                newName: "IX_Hierachy.Completions_ReservoirId");

            migrationBuilder.RenameIndex(
                name: "IX_Clusters_UserId",
                table: "Hierachy.Clusters",
                newName: "IX_Hierachy.Clusters_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierachy.Zones",
                table: "Hierachy.Zones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierachy.Wells",
                table: "Hierachy.Wells",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierachy.Reservoirs",
                table: "Hierachy.Reservoirs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierachy.Installations",
                table: "Hierachy.Installations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierachy.Fields",
                table: "Hierachy.Fields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierachy.Completions",
                table: "Hierachy.Completions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierachy.Clusters",
                table: "Hierachy.Clusters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Hierachy.Fields_FieldId",
                table: "FieldsFRs",
                column: "FieldId",
                principalTable: "Hierachy.Fields",
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
                name: "FK_Hierachy.Clusters_Users_UserId",
                table: "Hierachy.Clusters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Completions_Hierachy.Reservoirs_ReservoirId",
                table: "Hierachy.Completions",
                column: "ReservoirId",
                principalTable: "Hierachy.Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Completions_Hierachy.Wells_WellId",
                table: "Hierachy.Completions",
                column: "WellId",
                principalTable: "Hierachy.Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Completions_Users_UserId",
                table: "Hierachy.Completions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Fields_Hierachy.Installations_InstallationId",
                table: "Hierachy.Fields",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Fields_Users_UserId",
                table: "Hierachy.Fields",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Installations_Hierachy.Clusters_ClusterId",
                table: "Hierachy.Installations",
                column: "ClusterId",
                principalTable: "Hierachy.Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Installations_Users_UserId",
                table: "Hierachy.Installations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Reservoirs_Hierachy.Zones_ZoneId",
                table: "Hierachy.Reservoirs",
                column: "ZoneId",
                principalTable: "Hierachy.Zones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Reservoirs_Users_UserId",
                table: "Hierachy.Reservoirs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Wells_Hierachy.Fields_FieldId",
                table: "Hierachy.Wells",
                column: "FieldId",
                principalTable: "Hierachy.Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Wells_Users_UserId",
                table: "Hierachy.Wells",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Zones_Hierachy.Fields_FieldId",
                table: "Hierachy.Zones",
                column: "FieldId",
                principalTable: "Hierachy.Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Zones_Users_UserId",
                table: "Hierachy.Zones",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntallationsBTPs_Hierachy.Installations_InstallationId",
                table: "IntallationsBTPs",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Hierachy.Installations_InstallationId",
                table: "Measurements",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoints_Hierachy.Installations_InstallationId",
                table: "MeasuringPoints",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_Hierachy.Installations_InstallationId",
                table: "NFSMs",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OilVoumeCalculations_Hierachy.Installations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Hierachy.Installations_InstallationId",
                table: "Productions",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Hierachy.Wells_WellId",
                table: "WellEvents",
                column: "WellId",
                principalTable: "Hierachy.Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellTests_Hierachy.Wells_WellId",
                table: "WellTests",
                column: "WellId",
                principalTable: "Hierachy.Wells",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Hierachy.Fields_FieldId",
                table: "FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_GasVolumeCalculations_Hierachy.Installations_InstallationId",
                table: "GasVolumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Clusters_Users_UserId",
                table: "Hierachy.Clusters");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Completions_Hierachy.Reservoirs_ReservoirId",
                table: "Hierachy.Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Completions_Hierachy.Wells_WellId",
                table: "Hierachy.Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Completions_Users_UserId",
                table: "Hierachy.Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Fields_Hierachy.Installations_InstallationId",
                table: "Hierachy.Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Fields_Users_UserId",
                table: "Hierachy.Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Installations_Hierachy.Clusters_ClusterId",
                table: "Hierachy.Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Installations_Users_UserId",
                table: "Hierachy.Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Reservoirs_Hierachy.Zones_ZoneId",
                table: "Hierachy.Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Reservoirs_Users_UserId",
                table: "Hierachy.Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Wells_Hierachy.Fields_FieldId",
                table: "Hierachy.Wells");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Wells_Users_UserId",
                table: "Hierachy.Wells");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Zones_Hierachy.Fields_FieldId",
                table: "Hierachy.Zones");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Zones_Users_UserId",
                table: "Hierachy.Zones");

            migrationBuilder.DropForeignKey(
                name: "FK_IntallationsBTPs_Hierachy.Installations_InstallationId",
                table: "IntallationsBTPs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Hierachy.Installations_InstallationId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoints_Hierachy.Installations_InstallationId",
                table: "MeasuringPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_Hierachy.Installations_InstallationId",
                table: "NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_OilVoumeCalculations_Hierachy.Installations_InstallationId",
                table: "OilVoumeCalculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Hierachy.Installations_InstallationId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Hierachy.Wells_WellId",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTests_Hierachy.Wells_WellId",
                table: "WellTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierachy.Zones",
                table: "Hierachy.Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierachy.Wells",
                table: "Hierachy.Wells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierachy.Reservoirs",
                table: "Hierachy.Reservoirs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierachy.Installations",
                table: "Hierachy.Installations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierachy.Fields",
                table: "Hierachy.Fields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierachy.Completions",
                table: "Hierachy.Completions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierachy.Clusters",
                table: "Hierachy.Clusters");

            migrationBuilder.RenameTable(
                name: "Hierachy.Zones",
                newName: "Zones");

            migrationBuilder.RenameTable(
                name: "Hierachy.Wells",
                newName: "Wells");

            migrationBuilder.RenameTable(
                name: "Hierachy.Reservoirs",
                newName: "Reservoirs");

            migrationBuilder.RenameTable(
                name: "Hierachy.Installations",
                newName: "Installations");

            migrationBuilder.RenameTable(
                name: "Hierachy.Fields",
                newName: "Fields");

            migrationBuilder.RenameTable(
                name: "Hierachy.Completions",
                newName: "Completions");

            migrationBuilder.RenameTable(
                name: "Hierachy.Clusters",
                newName: "Clusters");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Zones_UserId",
                table: "Zones",
                newName: "IX_Zones_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Zones_FieldId",
                table: "Zones",
                newName: "IX_Zones_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Wells_UserId",
                table: "Wells",
                newName: "IX_Wells_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Wells_FieldId",
                table: "Wells",
                newName: "IX_Wells_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Reservoirs_ZoneId",
                table: "Reservoirs",
                newName: "IX_Reservoirs_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Reservoirs_UserId",
                table: "Reservoirs",
                newName: "IX_Reservoirs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Installations_UserId",
                table: "Installations",
                newName: "IX_Installations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Installations_ClusterId",
                table: "Installations",
                newName: "IX_Installations_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Fields_UserId",
                table: "Fields",
                newName: "IX_Fields_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Fields_InstallationId",
                table: "Fields",
                newName: "IX_Fields_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Completions_WellId",
                table: "Completions",
                newName: "IX_Completions_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Completions_UserId",
                table: "Completions",
                newName: "IX_Completions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Completions_ReservoirId",
                table: "Completions",
                newName: "IX_Completions_ReservoirId");

            migrationBuilder.RenameIndex(
                name: "IX_Hierachy.Clusters_UserId",
                table: "Clusters",
                newName: "IX_Clusters_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zones",
                table: "Zones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wells",
                table: "Wells",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservoirs",
                table: "Reservoirs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Installations",
                table: "Installations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fields",
                table: "Fields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Completions",
                table: "Completions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clusters",
                table: "Clusters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_Users_UserId",
                table: "Clusters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_Reservoirs_ReservoirId",
                table: "Completions",
                column: "ReservoirId",
                principalTable: "Reservoirs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_Users_UserId",
                table: "Completions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_Wells_WellId",
                table: "Completions",
                column: "WellId",
                principalTable: "Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Installations_InstallationId",
                table: "Fields",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Fields_FieldId",
                table: "FieldsFRs",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GasVolumeCalculations_Installations_InstallationId",
                table: "GasVolumeCalculations",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Users_UserId",
                table: "Installations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntallationsBTPs_Installations_InstallationId",
                table: "IntallationsBTPs",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Installations_InstallationId",
                table: "Measurements",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoints_Installations_InstallationId",
                table: "MeasuringPoints",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_Installations_InstallationId",
                table: "NFSMs",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OilVoumeCalculations_Installations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Installations_InstallationId",
                table: "Productions",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Users_UserId",
                table: "Reservoirs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Wells_WellId",
                table: "WellEvents",
                column: "WellId",
                principalTable: "Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wells_Fields_FieldId",
                table: "Wells",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wells_Users_UserId",
                table: "Wells",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellTests_Wells_WellId",
                table: "WellTests",
                column: "WellId",
                principalTable: "Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_Fields_FieldId",
                table: "Zones",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_Users_UserId",
                table: "Zones",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
