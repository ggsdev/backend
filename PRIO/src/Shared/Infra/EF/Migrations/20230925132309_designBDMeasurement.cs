using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039");

            migrationBuilder.DropForeignKey(
                name: "FK_BTPBases64_AC.Users_UserId",
                table: "BTPBases64");

            migrationBuilder.DropForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsInProduction_AC.Users_CommentedById",
                table: "CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsInProduction_Productions_ProductionId",
                table: "CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_ReservoirProductions_ReservoirProductionId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_AC.Users_CreatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_AC.Users_UpdatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_WellEvents_WellEventId",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Productions_DailyProductionId",
                table: "FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_Gases_GasesDiferencials_GasDiferencialId",
                table: "Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Gases_GasesLinears_GasLinearId",
                table: "Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_AC.Users_UserId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_FileTypes_FileTypeId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Hierachy.Installations_InstallationId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasurementsHistories_MeasurementHistoryId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_NFSMs_NFSMId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementsHistories_AC.Users_ImportedById",
                table: "MeasurementsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMsProductions_Productions_ProductionId",
                table: "NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_AC.Users_CalculatedImportedById",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_GasesDiferencials_GasDiferencialId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_GasesLinears_GasLinearId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Hierachy.Installations_InstallationId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Oils_OilId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Waters_WaterId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservoirProductions_ZoneProductions_ZoneProductionId",
                table: "ReservoirProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_AC.Users_CreatedById",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_AC.Users_UpdatedById",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Hierachy.Wells_WellId",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_WellEvents_EventRelatedId",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellLosses_WellEvents_EventId",
                table: "WellLosses");

            migrationBuilder.DropForeignKey(
                name: "FK_WellProductions_FieldsProductions_FieldProductionId",
                table: "WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellProductions_Productions_ProductionId",
                table: "WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellProductions_WellTests_WellTestId",
                table: "WellProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTests_BTPBases64_BTPBase64Id",
                table: "WellTests");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTests_Hierachy.Wells_WellId",
                table: "WellTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneProductions",
                table: "ZoneProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellTests",
                table: "WellTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellEvents",
                table: "WellEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Waters",
                table: "Waters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volumes_039",
                table: "Volumes_039");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservoirProductions",
                table: "ReservoirProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productions",
                table: "Productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oils",
                table: "Oils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementsHistories",
                table: "MeasurementsHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GasesLinears",
                table: "GasesLinears");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GasesDiferencials",
                table: "GasesDiferencials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gases",
                table: "Gases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldsProductions",
                table: "FieldsProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventReasons",
                table: "EventReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentsInProduction",
                table: "CommentsInProduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calibrations_039",
                table: "Calibrations_039");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BTPBases64",
                table: "BTPBases64");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BSWS_039",
                table: "BSWS_039");

            migrationBuilder.RenameTable(
                name: "ZoneProductions",
                newName: "Production.ZoneProductions");

            migrationBuilder.RenameTable(
                name: "WellTests",
                newName: "WellTest.WellTests");

            migrationBuilder.RenameTable(
                name: "WellEvents",
                newName: "Event.WellEvents");

            migrationBuilder.RenameTable(
                name: "Waters",
                newName: "Production.Waters");

            migrationBuilder.RenameTable(
                name: "Volumes_039",
                newName: "Measurement.Volumes_039");

            migrationBuilder.RenameTable(
                name: "ReservoirProductions",
                newName: "Production.ReservoirProductions");

            migrationBuilder.RenameTable(
                name: "Productions",
                newName: "Production.Productions");

            migrationBuilder.RenameTable(
                name: "Oils",
                newName: "Production.Oils");

            migrationBuilder.RenameTable(
                name: "MeasurementsHistories",
                newName: "Measurement.MeasurementsHistories");

            migrationBuilder.RenameTable(
                name: "Measurements",
                newName: "Measurement.Measurements");

            migrationBuilder.RenameTable(
                name: "GasesLinears",
                newName: "Production.GasesLinears");

            migrationBuilder.RenameTable(
                name: "GasesDiferencials",
                newName: "Production.GasesDiferencials");

            migrationBuilder.RenameTable(
                name: "Gases",
                newName: "Production.Gases");

            migrationBuilder.RenameTable(
                name: "FileTypes",
                newName: "Measurement.FileTypes");

            migrationBuilder.RenameTable(
                name: "FieldsProductions",
                newName: "Production.FieldsProductions");

            migrationBuilder.RenameTable(
                name: "EventReasons",
                newName: "Event.EventReasons");

            migrationBuilder.RenameTable(
                name: "CommentsInProduction",
                newName: "Measurement.CommentsInProduction");

            migrationBuilder.RenameTable(
                name: "Calibrations_039",
                newName: "Measurement.Calibrations_039");

            migrationBuilder.RenameTable(
                name: "BTPBases64",
                newName: "WellTest.BTPBases64");

            migrationBuilder.RenameTable(
                name: "BSWS_039",
                newName: "Measurement.BSWS_039");

            migrationBuilder.RenameIndex(
                name: "IX_WellTests_WellId",
                table: "WellTest.WellTests",
                newName: "IX_WellTest.WellTests_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_WellTests_BTPBase64Id",
                table: "WellTest.WellTests",
                newName: "IX_WellTest.WellTests_BTPBase64Id");

            migrationBuilder.RenameIndex(
                name: "IX_WellEvents_WellId",
                table: "Event.WellEvents",
                newName: "IX_Event.WellEvents_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_WellEvents_UpdatedById",
                table: "Event.WellEvents",
                newName: "IX_Event.WellEvents_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_WellEvents_EventRelatedId",
                table: "Event.WellEvents",
                newName: "IX_Event.WellEvents_EventRelatedId");

            migrationBuilder.RenameIndex(
                name: "IX_WellEvents_CreatedById",
                table: "Event.WellEvents",
                newName: "IX_Event.WellEvents_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Volumes_039_MeasurementId",
                table: "Measurement.Volumes_039",
                newName: "IX_Measurement.Volumes_039_MeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservoirProductions_ZoneProductionId",
                table: "Production.ReservoirProductions",
                newName: "IX_Production.ReservoirProductions_ZoneProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_WaterId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_WaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_OilId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_OilId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_InstallationId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_GasLinearId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_GasId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_GasId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_GasDiferencialId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_GasDiferencialId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_CalculatedImportedById",
                table: "Production.Productions",
                newName: "IX_Production.Productions_CalculatedImportedById");

            migrationBuilder.RenameIndex(
                name: "IX_MeasurementsHistories_ImportedById",
                table: "Measurement.MeasurementsHistories",
                newName: "IX_Measurement.MeasurementsHistories_ImportedById");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_UserId",
                table: "Measurement.Measurements",
                newName: "IX_Measurement.Measurements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_ProductionId",
                table: "Measurement.Measurements",
                newName: "IX_Measurement.Measurements_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_NFSMId",
                table: "Measurement.Measurements",
                newName: "IX_Measurement.Measurements_NFSMId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_MeasuringPointId",
                table: "Measurement.Measurements",
                newName: "IX_Measurement.Measurements_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_MeasurementHistoryId",
                table: "Measurement.Measurements",
                newName: "IX_Measurement.Measurements_MeasurementHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_InstallationId",
                table: "Measurement.Measurements",
                newName: "IX_Measurement.Measurements_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_FileTypeId",
                table: "Measurement.Measurements",
                newName: "IX_Measurement.Measurements_FileTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Gases_GasLinearId",
                table: "Production.Gases",
                newName: "IX_Production.Gases_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Gases_GasDiferencialId",
                table: "Production.Gases",
                newName: "IX_Production.Gases_GasDiferencialId");

            migrationBuilder.RenameIndex(
                name: "IX_EventReasons_WellEventId",
                table: "Event.EventReasons",
                newName: "IX_Event.EventReasons_WellEventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventReasons_UpdatedById",
                table: "Event.EventReasons",
                newName: "IX_Event.EventReasons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EventReasons_CreatedById",
                table: "Event.EventReasons",
                newName: "IX_Event.EventReasons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_CommentsInProduction_ProductionId",
                table: "Measurement.CommentsInProduction",
                newName: "IX_Measurement.CommentsInProduction_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentsInProduction_CommentedById",
                table: "Measurement.CommentsInProduction",
                newName: "IX_Measurement.CommentsInProduction_CommentedById");

            migrationBuilder.RenameIndex(
                name: "IX_Calibrations_039_MeasurementId",
                table: "Measurement.Calibrations_039",
                newName: "IX_Measurement.Calibrations_039_MeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_BTPBases64_UserId",
                table: "WellTest.BTPBases64",
                newName: "IX_WellTest.BTPBases64_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BSWS_039_MeasurementId",
                table: "Measurement.BSWS_039",
                newName: "IX_Measurement.BSWS_039_MeasurementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.ZoneProductions",
                table: "Production.ZoneProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellTest.WellTests",
                table: "WellTest.WellTests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event.WellEvents",
                table: "Event.WellEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.Waters",
                table: "Production.Waters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.Volumes_039",
                table: "Measurement.Volumes_039",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.ReservoirProductions",
                table: "Production.ReservoirProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.Productions",
                table: "Production.Productions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.Oils",
                table: "Production.Oils",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.MeasurementsHistories",
                table: "Measurement.MeasurementsHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.Measurements",
                table: "Measurement.Measurements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.GasesLinears",
                table: "Production.GasesLinears",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.GasesDiferencials",
                table: "Production.GasesDiferencials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.Gases",
                table: "Production.Gases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.FileTypes",
                table: "Measurement.FileTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.FieldsProductions",
                table: "Production.FieldsProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event.EventReasons",
                table: "Event.EventReasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.CommentsInProduction",
                table: "Measurement.CommentsInProduction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.Calibrations_039",
                table: "Measurement.Calibrations_039",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellTest.BTPBases64",
                table: "WellTest.BTPBases64",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.BSWS_039",
                table: "Measurement.BSWS_039",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_Production.ReservoirProductions_ReservoirProductionId",
                table: "CompletionProductions",
                column: "ReservoirProductionId",
                principalTable: "Production.ReservoirProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event.EventReasons_AC.Users_CreatedById",
                table: "Event.EventReasons",
                column: "CreatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event.EventReasons_AC.Users_UpdatedById",
                table: "Event.EventReasons",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event.EventReasons_Event.WellEvents_WellEventId",
                table: "Event.EventReasons",
                column: "WellEventId",
                principalTable: "Event.WellEvents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event.WellEvents_AC.Users_CreatedById",
                table: "Event.WellEvents",
                column: "CreatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event.WellEvents_AC.Users_UpdatedById",
                table: "Event.WellEvents",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event.WellEvents_Event.WellEvents_EventRelatedId",
                table: "Event.WellEvents",
                column: "EventRelatedId",
                principalTable: "Event.WellEvents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event.WellEvents_Hierachy.Wells_WellId",
                table: "Event.WellEvents",
                column: "WellId",
                principalTable: "Hierachy.Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Production.Productions_DailyProductionId",
                table: "FieldsFRs",
                column: "DailyProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.BSWS_039_Measurement.Measurements_MeasurementId",
                table: "Measurement.BSWS_039",
                column: "MeasurementId",
                principalTable: "Measurement.Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Calibrations_039_Measurement.Measurements_MeasurementId",
                table: "Measurement.Calibrations_039",
                column: "MeasurementId",
                principalTable: "Measurement.Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.CommentsInProduction_AC.Users_CommentedById",
                table: "Measurement.CommentsInProduction",
                column: "CommentedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.CommentsInProduction_Production.Productions_ProductionId",
                table: "Measurement.CommentsInProduction",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_AC.Users_UserId",
                table: "Measurement.Measurements",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_Hierachy.Installations_InstallationId",
                table: "Measurement.Measurements",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Measurement.Measurements",
                column: "MeasuringPointId",
                principalTable: "Hierarchy.MeasuringPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_Measurement.FileTypes_FileTypeId",
                table: "Measurement.Measurements",
                column: "FileTypeId",
                principalTable: "Measurement.FileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_Measurement.MeasurementsHistories_MeasurementHistoryId",
                table: "Measurement.Measurements",
                column: "MeasurementHistoryId",
                principalTable: "Measurement.MeasurementsHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_NFSMs_NFSMId",
                table: "Measurement.Measurements",
                column: "NFSMId",
                principalTable: "NFSMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_Production.Productions_ProductionId",
                table: "Measurement.Measurements",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.MeasurementsHistories_AC.Users_ImportedById",
                table: "Measurement.MeasurementsHistories",
                column: "ImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Volumes_039_Measurement.Measurements_MeasurementId",
                table: "Measurement.Volumes_039",
                column: "MeasurementId",
                principalTable: "Measurement.Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMsProductions_Production.Productions_ProductionId",
                table: "NFSMsProductions",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Gases_Production.GasesDiferencials_GasDiferencialId",
                table: "Production.Gases",
                column: "GasDiferencialId",
                principalTable: "Production.GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Gases_Production.GasesLinears_GasLinearId",
                table: "Production.Gases",
                column: "GasLinearId",
                principalTable: "Production.GasesLinears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Productions_AC.Users_CalculatedImportedById",
                table: "Production.Productions",
                column: "CalculatedImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Productions_Hierachy.Installations_InstallationId",
                table: "Production.Productions",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Productions_Production.GasesDiferencials_GasDiferencialId",
                table: "Production.Productions",
                column: "GasDiferencialId",
                principalTable: "Production.GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Productions_Production.GasesLinears_GasLinearId",
                table: "Production.Productions",
                column: "GasLinearId",
                principalTable: "Production.GasesLinears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Productions_Production.Gases_GasId",
                table: "Production.Productions",
                column: "GasId",
                principalTable: "Production.Gases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Productions_Production.Oils_OilId",
                table: "Production.Productions",
                column: "OilId",
                principalTable: "Production.Oils",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Productions_Production.Waters_WaterId",
                table: "Production.Productions",
                column: "WaterId",
                principalTable: "Production.Waters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.ReservoirProductions_Production.ZoneProductions_ZoneProductionId",
                table: "Production.ReservoirProductions",
                column: "ZoneProductionId",
                principalTable: "Production.ZoneProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellLosses_Event.WellEvents_EventId",
                table: "WellLosses",
                column: "EventId",
                principalTable: "Event.WellEvents",
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

            migrationBuilder.AddForeignKey(
                name: "FK_WellTest.BTPBases64_AC.Users_UserId",
                table: "WellTest.BTPBases64",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellTest.WellTests_Hierachy.Wells_WellId",
                table: "WellTest.WellTests",
                column: "WellId",
                principalTable: "Hierachy.Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellTest.WellTests_WellTest.BTPBases64_BTPBase64Id",
                table: "WellTest.WellTests",
                column: "BTPBase64Id",
                principalTable: "WellTest.BTPBases64",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_Production.ReservoirProductions_ReservoirProductionId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Event.EventReasons_AC.Users_CreatedById",
                table: "Event.EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Event.EventReasons_AC.Users_UpdatedById",
                table: "Event.EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Event.EventReasons_Event.WellEvents_WellEventId",
                table: "Event.EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Event.WellEvents_AC.Users_CreatedById",
                table: "Event.WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Event.WellEvents_AC.Users_UpdatedById",
                table: "Event.WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Event.WellEvents_Event.WellEvents_EventRelatedId",
                table: "Event.WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Event.WellEvents_Hierachy.Wells_WellId",
                table: "Event.WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Production.Productions_DailyProductionId",
                table: "FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.BSWS_039_Measurement.Measurements_MeasurementId",
                table: "Measurement.BSWS_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Calibrations_039_Measurement.Measurements_MeasurementId",
                table: "Measurement.Calibrations_039");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.CommentsInProduction_AC.Users_CommentedById",
                table: "Measurement.CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.CommentsInProduction_Production.Productions_ProductionId",
                table: "Measurement.CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_AC.Users_UserId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Hierachy.Installations_InstallationId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Hierarchy.MeasuringPoints_MeasuringPointId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Measurement.FileTypes_FileTypeId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Measurement.MeasurementsHistories_MeasurementHistoryId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_NFSMs_NFSMId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Production.Productions_ProductionId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.MeasurementsHistories_AC.Users_ImportedById",
                table: "Measurement.MeasurementsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Volumes_039_Measurement.Measurements_MeasurementId",
                table: "Measurement.Volumes_039");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMsProductions_Production.Productions_ProductionId",
                table: "NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Gases_Production.GasesDiferencials_GasDiferencialId",
                table: "Production.Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Gases_Production.GasesLinears_GasLinearId",
                table: "Production.Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Productions_AC.Users_CalculatedImportedById",
                table: "Production.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Productions_Hierachy.Installations_InstallationId",
                table: "Production.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Productions_Production.GasesDiferencials_GasDiferencialId",
                table: "Production.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Productions_Production.GasesLinears_GasLinearId",
                table: "Production.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Productions_Production.Gases_GasId",
                table: "Production.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Productions_Production.Oils_OilId",
                table: "Production.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Productions_Production.Waters_WaterId",
                table: "Production.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.ReservoirProductions_Production.ZoneProductions_ZoneProductionId",
                table: "Production.ReservoirProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellLosses_Event.WellEvents_EventId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_WellTest.BTPBases64_AC.Users_UserId",
                table: "WellTest.BTPBases64");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTest.WellTests_Hierachy.Wells_WellId",
                table: "WellTest.WellTests");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTest.WellTests_WellTest.BTPBases64_BTPBase64Id",
                table: "WellTest.WellTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellTest.WellTests",
                table: "WellTest.WellTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellTest.BTPBases64",
                table: "WellTest.BTPBases64");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.ZoneProductions",
                table: "Production.ZoneProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.Waters",
                table: "Production.Waters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.ReservoirProductions",
                table: "Production.ReservoirProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.Productions",
                table: "Production.Productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.Oils",
                table: "Production.Oils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.GasesLinears",
                table: "Production.GasesLinears");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.GasesDiferencials",
                table: "Production.GasesDiferencials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.Gases",
                table: "Production.Gases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.FieldsProductions",
                table: "Production.FieldsProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.Volumes_039",
                table: "Measurement.Volumes_039");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.MeasurementsHistories",
                table: "Measurement.MeasurementsHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.Measurements",
                table: "Measurement.Measurements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.FileTypes",
                table: "Measurement.FileTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.CommentsInProduction",
                table: "Measurement.CommentsInProduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.Calibrations_039",
                table: "Measurement.Calibrations_039");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.BSWS_039",
                table: "Measurement.BSWS_039");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event.WellEvents",
                table: "Event.WellEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event.EventReasons",
                table: "Event.EventReasons");

            migrationBuilder.RenameTable(
                name: "WellTest.WellTests",
                newName: "WellTests");

            migrationBuilder.RenameTable(
                name: "WellTest.BTPBases64",
                newName: "BTPBases64");

            migrationBuilder.RenameTable(
                name: "Production.ZoneProductions",
                newName: "ZoneProductions");

            migrationBuilder.RenameTable(
                name: "Production.Waters",
                newName: "Waters");

            migrationBuilder.RenameTable(
                name: "Production.ReservoirProductions",
                newName: "ReservoirProductions");

            migrationBuilder.RenameTable(
                name: "Production.Productions",
                newName: "Productions");

            migrationBuilder.RenameTable(
                name: "Production.Oils",
                newName: "Oils");

            migrationBuilder.RenameTable(
                name: "Production.GasesLinears",
                newName: "GasesLinears");

            migrationBuilder.RenameTable(
                name: "Production.GasesDiferencials",
                newName: "GasesDiferencials");

            migrationBuilder.RenameTable(
                name: "Production.Gases",
                newName: "Gases");

            migrationBuilder.RenameTable(
                name: "Production.FieldsProductions",
                newName: "FieldsProductions");

            migrationBuilder.RenameTable(
                name: "Measurement.Volumes_039",
                newName: "Volumes_039");

            migrationBuilder.RenameTable(
                name: "Measurement.MeasurementsHistories",
                newName: "MeasurementsHistories");

            migrationBuilder.RenameTable(
                name: "Measurement.Measurements",
                newName: "Measurements");

            migrationBuilder.RenameTable(
                name: "Measurement.FileTypes",
                newName: "FileTypes");

            migrationBuilder.RenameTable(
                name: "Measurement.CommentsInProduction",
                newName: "CommentsInProduction");

            migrationBuilder.RenameTable(
                name: "Measurement.Calibrations_039",
                newName: "Calibrations_039");

            migrationBuilder.RenameTable(
                name: "Measurement.BSWS_039",
                newName: "BSWS_039");

            migrationBuilder.RenameTable(
                name: "Event.WellEvents",
                newName: "WellEvents");

            migrationBuilder.RenameTable(
                name: "Event.EventReasons",
                newName: "EventReasons");

            migrationBuilder.RenameIndex(
                name: "IX_WellTest.WellTests_WellId",
                table: "WellTests",
                newName: "IX_WellTests_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_WellTest.WellTests_BTPBase64Id",
                table: "WellTests",
                newName: "IX_WellTests_BTPBase64Id");

            migrationBuilder.RenameIndex(
                name: "IX_WellTest.BTPBases64_UserId",
                table: "BTPBases64",
                newName: "IX_BTPBases64_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.ReservoirProductions_ZoneProductionId",
                table: "ReservoirProductions",
                newName: "IX_ReservoirProductions_ZoneProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_WaterId",
                table: "Productions",
                newName: "IX_Productions_WaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_OilId",
                table: "Productions",
                newName: "IX_Productions_OilId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_InstallationId",
                table: "Productions",
                newName: "IX_Productions_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_GasLinearId",
                table: "Productions",
                newName: "IX_Productions_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_GasId",
                table: "Productions",
                newName: "IX_Productions_GasId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_GasDiferencialId",
                table: "Productions",
                newName: "IX_Productions_GasDiferencialId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_CalculatedImportedById",
                table: "Productions",
                newName: "IX_Productions_CalculatedImportedById");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Gases_GasLinearId",
                table: "Gases",
                newName: "IX_Gases_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Gases_GasDiferencialId",
                table: "Gases",
                newName: "IX_Gases_GasDiferencialId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Volumes_039_MeasurementId",
                table: "Volumes_039",
                newName: "IX_Volumes_039_MeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.MeasurementsHistories_ImportedById",
                table: "MeasurementsHistories",
                newName: "IX_MeasurementsHistories_ImportedById");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Measurements_UserId",
                table: "Measurements",
                newName: "IX_Measurements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Measurements_ProductionId",
                table: "Measurements",
                newName: "IX_Measurements_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Measurements_NFSMId",
                table: "Measurements",
                newName: "IX_Measurements_NFSMId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Measurements_MeasuringPointId",
                table: "Measurements",
                newName: "IX_Measurements_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Measurements_MeasurementHistoryId",
                table: "Measurements",
                newName: "IX_Measurements_MeasurementHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Measurements_InstallationId",
                table: "Measurements",
                newName: "IX_Measurements_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Measurements_FileTypeId",
                table: "Measurements",
                newName: "IX_Measurements_FileTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.CommentsInProduction_ProductionId",
                table: "CommentsInProduction",
                newName: "IX_CommentsInProduction_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.CommentsInProduction_CommentedById",
                table: "CommentsInProduction",
                newName: "IX_CommentsInProduction_CommentedById");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Calibrations_039_MeasurementId",
                table: "Calibrations_039",
                newName: "IX_Calibrations_039_MeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.BSWS_039_MeasurementId",
                table: "BSWS_039",
                newName: "IX_BSWS_039_MeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_Event.WellEvents_WellId",
                table: "WellEvents",
                newName: "IX_WellEvents_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_Event.WellEvents_UpdatedById",
                table: "WellEvents",
                newName: "IX_WellEvents_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Event.WellEvents_EventRelatedId",
                table: "WellEvents",
                newName: "IX_WellEvents_EventRelatedId");

            migrationBuilder.RenameIndex(
                name: "IX_Event.WellEvents_CreatedById",
                table: "WellEvents",
                newName: "IX_WellEvents_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Event.EventReasons_WellEventId",
                table: "EventReasons",
                newName: "IX_EventReasons_WellEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Event.EventReasons_UpdatedById",
                table: "EventReasons",
                newName: "IX_EventReasons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Event.EventReasons_CreatedById",
                table: "EventReasons",
                newName: "IX_EventReasons_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellTests",
                table: "WellTests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BTPBases64",
                table: "BTPBases64",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneProductions",
                table: "ZoneProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Waters",
                table: "Waters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservoirProductions",
                table: "ReservoirProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productions",
                table: "Productions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oils",
                table: "Oils",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GasesLinears",
                table: "GasesLinears",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GasesDiferencials",
                table: "GasesDiferencials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gases",
                table: "Gases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldsProductions",
                table: "FieldsProductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volumes_039",
                table: "Volumes_039",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementsHistories",
                table: "MeasurementsHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentsInProduction",
                table: "CommentsInProduction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calibrations_039",
                table: "Calibrations_039",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BSWS_039",
                table: "BSWS_039",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellEvents",
                table: "WellEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventReasons",
                table: "EventReasons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BSWS_039_Measurements_MeasurementId",
                table: "BSWS_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BTPBases64_AC.Users_UserId",
                table: "BTPBases64",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calibrations_039_Measurements_MeasurementId",
                table: "Calibrations_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsInProduction_AC.Users_CommentedById",
                table: "CommentsInProduction",
                column: "CommentedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsInProduction_Productions_ProductionId",
                table: "CommentsInProduction",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_ReservoirProductions_ReservoirProductionId",
                table: "CompletionProductions",
                column: "ReservoirProductionId",
                principalTable: "ReservoirProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_AC.Users_CreatedById",
                table: "EventReasons",
                column: "CreatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_AC.Users_UpdatedById",
                table: "EventReasons",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_WellEvents_WellEventId",
                table: "EventReasons",
                column: "WellEventId",
                principalTable: "WellEvents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Productions_DailyProductionId",
                table: "FieldsFRs",
                column: "DailyProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gases_GasesDiferencials_GasDiferencialId",
                table: "Gases",
                column: "GasDiferencialId",
                principalTable: "GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gases_GasesLinears_GasLinearId",
                table: "Gases",
                column: "GasLinearId",
                principalTable: "GasesLinears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_AC.Users_UserId",
                table: "Measurements",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_FileTypes_FileTypeId",
                table: "Measurements",
                column: "FileTypeId",
                principalTable: "FileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Hierachy.Installations_InstallationId",
                table: "Measurements",
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
                name: "FK_Measurements_MeasurementsHistories_MeasurementHistoryId",
                table: "Measurements",
                column: "MeasurementHistoryId",
                principalTable: "MeasurementsHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_NFSMs_NFSMId",
                table: "Measurements",
                column: "NFSMId",
                principalTable: "NFSMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Productions_ProductionId",
                table: "Measurements",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementsHistories_AC.Users_ImportedById",
                table: "MeasurementsHistories",
                column: "ImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMsProductions_Productions_ProductionId",
                table: "NFSMsProductions",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_AC.Users_CalculatedImportedById",
                table: "Productions",
                column: "CalculatedImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_GasesDiferencials_GasDiferencialId",
                table: "Productions",
                column: "GasDiferencialId",
                principalTable: "GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_GasesLinears_GasLinearId",
                table: "Productions",
                column: "GasLinearId",
                principalTable: "GasesLinears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasId",
                table: "Productions",
                column: "GasId",
                principalTable: "Gases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Hierachy.Installations_InstallationId",
                table: "Productions",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Oils_OilId",
                table: "Productions",
                column: "OilId",
                principalTable: "Oils",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Waters_WaterId",
                table: "Productions",
                column: "WaterId",
                principalTable: "Waters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservoirProductions_ZoneProductions_ZoneProductionId",
                table: "ReservoirProductions",
                column: "ZoneProductionId",
                principalTable: "ZoneProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Volumes_039_Measurements_MeasurementId",
                table: "Volumes_039",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_AC.Users_CreatedById",
                table: "WellEvents",
                column: "CreatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_AC.Users_UpdatedById",
                table: "WellEvents",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Hierachy.Wells_WellId",
                table: "WellEvents",
                column: "WellId",
                principalTable: "Hierachy.Wells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_WellEvents_EventRelatedId",
                table: "WellEvents",
                column: "EventRelatedId",
                principalTable: "WellEvents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellLosses_WellEvents_EventId",
                table: "WellLosses",
                column: "EventId",
                principalTable: "WellEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellProductions_FieldsProductions_FieldProductionId",
                table: "WellProductions",
                column: "FieldProductionId",
                principalTable: "FieldsProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellProductions_Productions_ProductionId",
                table: "WellProductions",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellProductions_WellTests_WellTestId",
                table: "WellProductions",
                column: "WellTestId",
                principalTable: "WellTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellTests_BTPBases64_BTPBase64Id",
                table: "WellTests",
                column: "BTPBase64Id",
                principalTable: "BTPBases64",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellTests_Hierachy.Wells_WellId",
                table: "WellTests",
                column: "WellId",
                principalTable: "Hierachy.Wells",
                principalColumn: "Id");
        }
    }
}
