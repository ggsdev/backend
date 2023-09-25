using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDMeasurement5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.CommentsInProduction_AC.Users_CommentedById",
                table: "Measurement.CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.CommentsInProduction_Production.Productions_ProductionId",
                table: "Measurement.CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Production.Productions_ProductionId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMsProductions_Production.Productions_ProductionId",
                table: "Measurement.NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.FieldsFRs_Production.Productions_DailyProductionId",
                table: "Production.FieldsFRs");

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
                name: "FK_Production.WellProductions_Production.Productions_ProductionId",
                table: "Production.WellProductions");

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
                name: "PK_Measurement.CommentsInProduction",
                table: "Measurement.CommentsInProduction");

            migrationBuilder.RenameTable(
                name: "Production.Productions",
                newName: "Measurement.Productions");

            migrationBuilder.RenameTable(
                name: "Production.Oils",
                newName: "Measurement.Oils");

            migrationBuilder.RenameTable(
                name: "Production.GasesLinears",
                newName: "Measurement.GasesLinears");

            migrationBuilder.RenameTable(
                name: "Production.GasesDiferencials",
                newName: "Measurement.GasesDiferencials");

            migrationBuilder.RenameTable(
                name: "Production.Gases",
                newName: "Measurement.Gases");

            migrationBuilder.RenameTable(
                name: "Measurement.CommentsInProduction",
                newName: "Production.CommentsInProduction");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_WaterId",
                table: "Measurement.Productions",
                newName: "IX_Measurement.Productions_WaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_OilId",
                table: "Measurement.Productions",
                newName: "IX_Measurement.Productions_OilId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_InstallationId",
                table: "Measurement.Productions",
                newName: "IX_Measurement.Productions_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_GasLinearId",
                table: "Measurement.Productions",
                newName: "IX_Measurement.Productions_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_GasId",
                table: "Measurement.Productions",
                newName: "IX_Measurement.Productions_GasId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_GasDiferencialId",
                table: "Measurement.Productions",
                newName: "IX_Measurement.Productions_GasDiferencialId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Productions_CalculatedImportedById",
                table: "Measurement.Productions",
                newName: "IX_Measurement.Productions_CalculatedImportedById");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Gases_GasLinearId",
                table: "Measurement.Gases",
                newName: "IX_Measurement.Gases_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Gases_GasDiferencialId",
                table: "Measurement.Gases",
                newName: "IX_Measurement.Gases_GasDiferencialId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.CommentsInProduction_ProductionId",
                table: "Production.CommentsInProduction",
                newName: "IX_Production.CommentsInProduction_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.CommentsInProduction_CommentedById",
                table: "Production.CommentsInProduction",
                newName: "IX_Production.CommentsInProduction_CommentedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.Productions",
                table: "Measurement.Productions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.Oils",
                table: "Measurement.Oils",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.GasesLinears",
                table: "Measurement.GasesLinears",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.GasesDiferencials",
                table: "Measurement.GasesDiferencials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.Gases",
                table: "Measurement.Gases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.CommentsInProduction",
                table: "Production.CommentsInProduction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Gases_Measurement.GasesDiferencials_GasDiferencialId",
                table: "Measurement.Gases",
                column: "GasDiferencialId",
                principalTable: "Measurement.GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Gases_Measurement.GasesLinears_GasLinearId",
                table: "Measurement.Gases",
                column: "GasLinearId",
                principalTable: "Measurement.GasesLinears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Measurements_Measurement.Productions_ProductionId",
                table: "Measurement.Measurements",
                column: "ProductionId",
                principalTable: "Measurement.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMsProductions_Measurement.Productions_ProductionId",
                table: "Measurement.NFSMsProductions",
                column: "ProductionId",
                principalTable: "Measurement.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Productions_AC.Users_CalculatedImportedById",
                table: "Measurement.Productions",
                column: "CalculatedImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Productions_Hierachy.Installations_InstallationId",
                table: "Measurement.Productions",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Productions_Measurement.GasesDiferencials_GasDiferencialId",
                table: "Measurement.Productions",
                column: "GasDiferencialId",
                principalTable: "Measurement.GasesDiferencials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Productions_Measurement.GasesLinears_GasLinearId",
                table: "Measurement.Productions",
                column: "GasLinearId",
                principalTable: "Measurement.GasesLinears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Productions_Measurement.Gases_GasId",
                table: "Measurement.Productions",
                column: "GasId",
                principalTable: "Measurement.Gases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Productions_Measurement.Oils_OilId",
                table: "Measurement.Productions",
                column: "OilId",
                principalTable: "Measurement.Oils",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.Productions_Production.Waters_WaterId",
                table: "Measurement.Productions",
                column: "WaterId",
                principalTable: "Production.Waters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.CommentsInProduction_AC.Users_CommentedById",
                table: "Production.CommentsInProduction",
                column: "CommentedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.CommentsInProduction_Measurement.Productions_ProductionId",
                table: "Production.CommentsInProduction",
                column: "ProductionId",
                principalTable: "Measurement.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.FieldsFRs_Measurement.Productions_DailyProductionId",
                table: "Production.FieldsFRs",
                column: "DailyProductionId",
                principalTable: "Measurement.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.WellProductions_Measurement.Productions_ProductionId",
                table: "Production.WellProductions",
                column: "ProductionId",
                principalTable: "Measurement.Productions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Gases_Measurement.GasesDiferencials_GasDiferencialId",
                table: "Measurement.Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Gases_Measurement.GasesLinears_GasLinearId",
                table: "Measurement.Gases");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Measurements_Measurement.Productions_ProductionId",
                table: "Measurement.Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.NFSMsProductions_Measurement.Productions_ProductionId",
                table: "Measurement.NFSMsProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Productions_AC.Users_CalculatedImportedById",
                table: "Measurement.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Productions_Hierachy.Installations_InstallationId",
                table: "Measurement.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Productions_Measurement.GasesDiferencials_GasDiferencialId",
                table: "Measurement.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Productions_Measurement.GasesLinears_GasLinearId",
                table: "Measurement.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Productions_Measurement.Gases_GasId",
                table: "Measurement.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Productions_Measurement.Oils_OilId",
                table: "Measurement.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurement.Productions_Production.Waters_WaterId",
                table: "Measurement.Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.CommentsInProduction_AC.Users_CommentedById",
                table: "Production.CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.CommentsInProduction_Measurement.Productions_ProductionId",
                table: "Production.CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.FieldsFRs_Measurement.Productions_DailyProductionId",
                table: "Production.FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.WellProductions_Measurement.Productions_ProductionId",
                table: "Production.WellProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.CommentsInProduction",
                table: "Production.CommentsInProduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.Productions",
                table: "Measurement.Productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.Oils",
                table: "Measurement.Oils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.GasesLinears",
                table: "Measurement.GasesLinears");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.GasesDiferencials",
                table: "Measurement.GasesDiferencials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurement.Gases",
                table: "Measurement.Gases");

            migrationBuilder.RenameTable(
                name: "Production.CommentsInProduction",
                newName: "Measurement.CommentsInProduction");

            migrationBuilder.RenameTable(
                name: "Measurement.Productions",
                newName: "Production.Productions");

            migrationBuilder.RenameTable(
                name: "Measurement.Oils",
                newName: "Production.Oils");

            migrationBuilder.RenameTable(
                name: "Measurement.GasesLinears",
                newName: "Production.GasesLinears");

            migrationBuilder.RenameTable(
                name: "Measurement.GasesDiferencials",
                newName: "Production.GasesDiferencials");

            migrationBuilder.RenameTable(
                name: "Measurement.Gases",
                newName: "Production.Gases");

            migrationBuilder.RenameIndex(
                name: "IX_Production.CommentsInProduction_ProductionId",
                table: "Measurement.CommentsInProduction",
                newName: "IX_Measurement.CommentsInProduction_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.CommentsInProduction_CommentedById",
                table: "Measurement.CommentsInProduction",
                newName: "IX_Measurement.CommentsInProduction_CommentedById");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Productions_WaterId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_WaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Productions_OilId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_OilId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Productions_InstallationId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Productions_GasLinearId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Productions_GasId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_GasId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Productions_GasDiferencialId",
                table: "Production.Productions",
                newName: "IX_Production.Productions_GasDiferencialId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Productions_CalculatedImportedById",
                table: "Production.Productions",
                newName: "IX_Production.Productions_CalculatedImportedById");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Gases_GasLinearId",
                table: "Production.Gases",
                newName: "IX_Production.Gases_GasLinearId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurement.Gases_GasDiferencialId",
                table: "Production.Gases",
                newName: "IX_Production.Gases_GasDiferencialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurement.CommentsInProduction",
                table: "Measurement.CommentsInProduction",
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
                name: "FK_Measurement.Measurements_Production.Productions_ProductionId",
                table: "Measurement.Measurements",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement.NFSMsProductions_Production.Productions_ProductionId",
                table: "Measurement.NFSMsProductions",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.FieldsFRs_Production.Productions_DailyProductionId",
                table: "Production.FieldsFRs",
                column: "DailyProductionId",
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
                name: "FK_Production.WellProductions_Production.Productions_ProductionId",
                table: "Production.WellProductions",
                column: "ProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");
        }
    }
}
