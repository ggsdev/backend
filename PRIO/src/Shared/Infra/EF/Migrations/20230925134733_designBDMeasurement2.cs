using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDMeasurement2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_Production.ReservoirProductions_ReservoirProductionId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_WellProductions_WellAllocationId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_IntallationsBTPs_BTPs_BTPId",
                table: "IntallationsBTPs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_NFSMs_NFSMId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMImportHistories_AC.Users_ImportedById",
                table: "NFSMImportHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_Hierachy.Installations_InstallationId",
                table: "NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMs_NFSMImportHistories_ImportId",
                table: "NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMsProductions_NFSMs_NFSMId",
                table: "NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMsProductions_Production.Productions_ProductionId",
                table: "NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AC.Users_UserId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellLosses_Event.WellEvents_EventId",
                table: "WellLosses");

            migrationBuilder.DropForeignKey(
                name: "FK_WellLosses_WellProductions_WellAllocationId",
                table: "WellLosses");

            migrationBuilder.DropForeignKey(
                name: "FK_WellProductions_Production.FieldsProductions_FieldProductionId",
                table: "WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellProductions_Production.Productions_ProductionId",
                table: "WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellProductions_WellTest.WellTests_WellTestId",
                table: "WellProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellProductions",
                table: "WellProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellLosses",
                table: "WellLosses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NFSMsProductions",
                table: "NFSMsProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NFSMs",
                table: "NFSMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NFSMImportHistories",
                table: "NFSMImportHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletionProductions",
                table: "CompletionProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BTPs",
                table: "BTPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Backups",
                table: "Backups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auxiliaries",
                table: "Auxiliaries");

            migrationBuilder.RenameTable(
                name: "WellProductions",
                newName: "Production.WellProductions");

            migrationBuilder.RenameTable(
                name: "WellLosses",
                newName: "Production.WellLosses");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "System.Sessions");

            migrationBuilder.RenameTable(
                name: "NFSMsProductions",
                newName: "Measurement.NFSMsProductions");

            migrationBuilder.RenameTable(
                name: "NFSMs",
                newName: "Measurement.NFSMs");

            migrationBuilder.RenameTable(
                name: "NFSMImportHistories",
                newName: "Measurement.NFSMImportHistories");

            migrationBuilder.RenameTable(
                name: "CompletionProductions",
                newName: "Production.CompletionProductions");

            migrationBuilder.RenameTable(
                name: "BTPs",
                newName: "WellTest.BTPs");

            migrationBuilder.RenameTable(
                name: "Backups",
                newName: "System.Backups");

            migrationBuilder.RenameTable(
                name: "Auxiliaries",
                newName: "System.Auxiliary");

            migrationBuilder.RenameIndex(
                name: "IX_WellProductions_WellTestId",
                table: "Production.WellProductions",
                newName: "IX_Production.WellProductions_WellTestId");

            migrationBuilder.RenameIndex(
                name: "IX_WellProductions_ProductionId",
                table: "Production.WellProductions",
                newName: "IX_Production.WellProductions_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_WellProductions_FieldProductionId",
                table: "Production.WellProductions",
                newName: "IX_Production.WellProductions_FieldProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_WellLosses_WellAllocationId",
                table: "Production.WellLosses",
                newName: "IX_Production.WellLosses_WellAllocationId");

            migrationBuilder.RenameIndex(
                name: "IX_WellLosses_EventId",
                table: "Production.WellLosses",
                newName: "IX_Production.WellLosses_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserId",
                table: "System.Sessions",
                newName: "IX_System.Sessions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NFSMsProductions_ProductionId",
                table: "Measurement.NFSMsProductions",
                newName: "IX_Measurement.NFSMsProductions_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_NFSMsProductions_NFSMId",
                table: "Measurement.NFSMsProductions",
                newName: "IX_Measurement.NFSMsProductions_NFSMId");

            migrationBuilder.RenameIndex(
                name: "IX_NFSMs_MeasuringPointId",
                table: "Measurement.NFSMs",
                newName: "IX_Measurement.NFSMs_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_NFSMs_InstallationId",
                table: "Measurement.NFSMs",
                newName: "IX_Measurement.NFSMs_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_NFSMs_ImportId",
                table: "Measurement.NFSMs",
                newName: "IX_Measurement.NFSMs_ImportId");

            migrationBuilder.RenameIndex(
                name: "IX_NFSMImportHistories_ImportedById",
                table: "Measurement.NFSMImportHistories",
                newName: "IX_Measurement.NFSMImportHistories_ImportedById");

            migrationBuilder.RenameIndex(
                name: "IX_CompletionProductions_WellAllocationId",
                table: "Production.CompletionProductions",
                newName: "IX_Production.CompletionProductions_WellAllocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletionProductions_ReservoirProductionId",
                table: "Production.CompletionProductions",
                newName: "IX_Production.CompletionProductions_ReservoirProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Backups_date",
                table: "System.Backups",
                newName: "IX_System.Backups_date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.WellProductions",
                table: "Production.WellProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.WellLosses",
                table: "Production.WellLosses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_System.Sessions",
                table: "System.Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.NFSMsProductions",
                table: "Measurement.NFSMsProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.NFSMs",
                table: "Measurement.NFSMs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.NFSMImportHistories",
                table: "Measurement.NFSMImportHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.CompletionProductions",
                table: "Production.CompletionProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellTest.BTPs",
                table: "WellTest.BTPs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_System.Backups",
                table: "System.Backups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_System.Auxiliary",
                table: "System.Auxiliary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntallationsBTPs_WellTest.BTPs_BTPId",
                table: "IntallationsBTPs",
                column: "BTPId",
                principalTable: "WellTest.BTPs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_Measurement.NFSMs_NFSMId",
                table: "Measurement.Measurements",
                column: "NFSMId",
                principalTable: "Measurement.NFSMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMImportHistories_AC.Users_ImportedById",
                table: "Measurement.NFSMImportHistories",
                column: "ImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMs_Hierachy.Installations_InstallationId",
                table: "Measurement.NFSMs",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Measurement.NFSMs",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMs_Measurement.NFSMImportHistories_ImportId",
                table: "Measurement.NFSMs",
                column: "ImportId",
                principalTable: "Measurement.NFSMImportHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMsProductions_Measurement.NFSMs_NFSMId",
                table: "Measurement.NFSMsProductions",
                column: "NFSMId",
                principalTable: "Measurement.NFSMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMsProductions_Production.Productions_ProductionId",
                table: "Measurement.NFSMsProductions",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.CompletionProductions_Production.ReservoirProductions_ReservoirProductionId",
                table: "Production.CompletionProductions",
                column: "ReservoirProductionId",
                principalTable: "Production.ReservoirProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.CompletionProductions_Production.WellProductions_WellAllocationId",
                table: "Production.CompletionProductions",
                column: "WellAllocationId",
                principalTable: "Production.WellProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.WellLosses_Event.WellEvents_EventId",
                table: "Production.WellLosses",
                column: "EventId",
                principalTable: "Event.WellEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Production.WellLosses_Production.WellProductions_WellAllocationId",
                table: "Production.WellLosses",
                column: "WellAllocationId",
                principalTable: "Production.WellProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Production.WellProductions_Production.FieldsProductions_FieldProductionId",
                table: "Production.WellProductions",
                column: "FieldProductionId",
                principalTable: "Production.FieldsProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Production.WellProductions_Production.Productions_ProductionId",
                table: "Production.WellProductions",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.WellProductions_WellTest.WellTests_WellTestId",
                table: "Production.WellProductions",
                column: "WellTestId",
                principalTable: "WellTest.WellTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_System.Sessions_AC.Users_UserId",
                table: "System.Sessions",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntallationsBTPs_WellTest.BTPs_BTPId",
                table: "IntallationsBTPs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Measurement.NFSMs_NFSMId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMImportHistories_AC.Users_ImportedById",
                table: "Measurement.NFSMImportHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMs_Hierachy.Installations_InstallationId",
                table: "Measurement.NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Measurement.NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMs_Measurement.NFSMImportHistories_ImportId",
                table: "Measurement.NFSMs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMsProductions_Measurement.NFSMs_NFSMId",
                table: "Measurement.NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMsProductions_Production.Productions_ProductionId",
                table: "Measurement.NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.CompletionProductions_Production.ReservoirProductions_ReservoirProductionId",
                table: "Production.CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.CompletionProductions_Production.WellProductions_WellAllocationId",
                table: "Production.CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.WellLosses_Event.WellEvents_EventId",
                table: "Production.WellLosses");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.WellLosses_Production.WellProductions_WellAllocationId",
                table: "Production.WellLosses");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.WellProductions_Production.FieldsProductions_FieldProductionId",
                table: "Production.WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.WellProductions_Production.Productions_ProductionId",
                table: "Production.WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.WellProductions_WellTest.WellTests_WellTestId",
                table: "Production.WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_System.Sessions_AC.Users_UserId",
                table: "System.Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellTest.BTPs",
                table: "WellTest.BTPs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_System.Sessions",
                table: "System.Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_System.Backups",
                table: "System.Backups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_System.Auxiliary",
                table: "System.Auxiliary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.WellProductions",
                table: "Production.WellProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.WellLosses",
                table: "Production.WellLosses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.CompletionProductions",
                table: "Production.CompletionProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.NFSMsProductions",
                table: "Measurement.NFSMsProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.NFSMs",
                table: "Measurement.NFSMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.NFSMImportHistories",
                table: "Measurement.NFSMImportHistories");

            migrationBuilder.RenameTable(
                name: "WellTest.BTPs",
                newName: "BTPs");

            migrationBuilder.RenameTable(
                name: "System.Sessions",
                newName: "Sessions");

            migrationBuilder.RenameTable(
                name: "System.Backups",
                newName: "Backups");

            migrationBuilder.RenameTable(
                name: "System.Auxiliary",
                newName: "Auxiliaries");

            migrationBuilder.RenameTable(
                name: "Production.WellProductions",
                newName: "WellProductions");

            migrationBuilder.RenameTable(
                name: "Production.WellLosses",
                newName: "WellLosses");

            migrationBuilder.RenameTable(
                name: "Production.CompletionProductions",
                newName: "CompletionProductions");

            migrationBuilder.RenameTable(
                name: "Measurement.NFSMsProductions",
                newName: "NFSMsProductions");

            migrationBuilder.RenameTable(
                name: "Measurement.NFSMs",
                newName: "NFSMs");

            migrationBuilder.RenameTable(
                name: "Measurement.NFSMImportHistories",
                newName: "NFSMImportHistories");

            migrationBuilder.RenameIndex(
                name: "IX_System.Sessions_UserId",
                table: "Sessions",
                newName: "IX_Sessions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_System.Backups_date",
                table: "Backups",
                newName: "IX_Backups_date");

            migrationBuilder.RenameIndex(
                name: "IX_Production.WellProductions_WellTestId",
                table: "WellProductions",
                newName: "IX_WellProductions_WellTestId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.WellProductions_ProductionId",
                table: "WellProductions",
                newName: "IX_WellProductions_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.WellProductions_FieldProductionId",
                table: "WellProductions",
                newName: "IX_WellProductions_FieldProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.WellLosses_WellAllocationId",
                table: "WellLosses",
                newName: "IX_WellLosses_WellAllocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.WellLosses_EventId",
                table: "WellLosses",
                newName: "IX_WellLosses_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.CompletionProductions_WellAllocationId",
                table: "CompletionProductions",
                newName: "IX_CompletionProductions_WellAllocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.CompletionProductions_ReservoirProductionId",
                table: "CompletionProductions",
                newName: "IX_CompletionProductions_ReservoirProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.NFSMsProductions_ProductionId",
                table: "NFSMsProductions",
                newName: "IX_NFSMsProductions_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.NFSMsProductions_NFSMId",
                table: "NFSMsProductions",
                newName: "IX_NFSMsProductions_NFSMId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.NFSMs_MeasuringPointId",
                table: "NFSMs",
                newName: "IX_NFSMs_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.NFSMs_InstallationId",
                table: "NFSMs",
                newName: "IX_NFSMs_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.NFSMs_ImportId",
                table: "NFSMs",
                newName: "IX_NFSMs_ImportId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.NFSMImportHistories_ImportedById",
                table: "NFSMImportHistories",
                newName: "IX_NFSMImportHistories_ImportedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BTPs",
                table: "BTPs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Backups",
                table: "Backups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auxiliaries",
                table: "Auxiliaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellProductions",
                table: "WellProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellLosses",
                table: "WellLosses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletionProductions",
                table: "CompletionProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NFSMsProductions",
                table: "NFSMsProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NFSMs",
                table: "NFSMs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NFSMImportHistories",
                table: "NFSMImportHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_Production.ReservoirProductions_ReservoirProductionId",
                table: "CompletionProductions",
                column: "ReservoirProductionId",
                principalTable: "Production.ReservoirProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_WellProductions_WellAllocationId",
                table: "CompletionProductions",
                column: "WellAllocationId",
                principalTable: "WellProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntallationsBTPs_BTPs_BTPId",
                table: "IntallationsBTPs",
                column: "BTPId",
                principalTable: "BTPs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_NFSMs_NFSMId",
                table: "Measurement.Measurements",
                column: "NFSMId",
                principalTable: "NFSMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMImportHistories_AC.Users_ImportedById",
                table: "NFSMImportHistories",
                column: "ImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_Hierachy.Installations_InstallationId",
                table: "NFSMs",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "NFSMs",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMs_NFSMImportHistories_ImportId",
                table: "NFSMs",
                column: "ImportId",
                principalTable: "NFSMImportHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMsProductions_NFSMs_NFSMId",
                table: "NFSMsProductions",
                column: "NFSMId",
                principalTable: "NFSMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMsProductions_Production.Productions_ProductionId",
                table: "NFSMsProductions",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AC.Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellLosses_Event.WellEvents_EventId",
                table: "WellLosses",
                column: "EventId",
                principalTable: "Event.WellEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellLosses_WellProductions_WellAllocationId",
                table: "WellLosses",
                column: "WellAllocationId",
                principalTable: "WellProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellProductions_Production.FieldsProductions_FieldProductionId",
                table: "WellProductions",
                column: "FieldProductionId",
                principalTable: "Production.FieldsProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellProductions_Production.Productions_ProductionId",
                table: "WellProductions",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellProductions_WellTest.WellTests_WellTestId",
                table: "WellProductions",
                column: "WellTestId",
                principalTable: "WellTest.WellTests",
                principalColumn: "Id");
        }
    }
}
