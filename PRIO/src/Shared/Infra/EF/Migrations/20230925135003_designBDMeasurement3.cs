using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDMeasurement3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AC.GroupOperations_GlobalOperations_GlobalOperationId",
                table: "AC.GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.UserOperations_GlobalOperations_GlobalOperationId",
                table: "AC.UserOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Hierachy.Fields_FieldId",
                table: "FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldsFRs_Production.Productions_DailyProductionId",
                table: "FieldsFRs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalOperations",
                table: "GlobalOperations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldsFRs",
                table: "FieldsFRs");

            migrationBuilder.RenameTable(
                name: "GlobalOperations",
                newName: "AC.GlobalOperations");

            migrationBuilder.RenameTable(
                name: "FieldsFRs",
                newName: "Production.FieldsFRs");

            migrationBuilder.RenameIndex(
                name: "IX_FieldsFRs_FieldId",
                table: "Production.FieldsFRs",
                newName: "IX_Production.FieldsFRs_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_FieldsFRs_DailyProductionId",
                table: "Production.FieldsFRs",
                newName: "IX_Production.FieldsFRs_DailyProductionId");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "AC.GlobalOperations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AC.GlobalOperations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.GlobalOperations",
                table: "AC.GlobalOperations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.FieldsFRs",
                table: "Production.FieldsFRs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.GroupOperations_AC.GlobalOperations_GlobalOperationId",
                table: "AC.GroupOperations",
                column: "GlobalOperationId",
                principalTable: "AC.GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.UserOperations_AC.GlobalOperations_GlobalOperationId",
                table: "AC.UserOperations",
                column: "GlobalOperationId",
                principalTable: "AC.GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.FieldsFRs_Hierachy.Fields_FieldId",
                table: "Production.FieldsFRs",
                column: "FieldId",
                principalTable: "Hierachy.Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Production.FieldsFRs_Production.Productions_DailyProductionId",
                table: "Production.FieldsFRs",
                column: "DailyProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AC.GroupOperations_AC.GlobalOperations_GlobalOperationId",
                table: "AC.GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.UserOperations_AC.GlobalOperations_GlobalOperationId",
                table: "AC.UserOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.FieldsFRs_Hierachy.Fields_FieldId",
                table: "Production.FieldsFRs");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.FieldsFRs_Production.Productions_DailyProductionId",
                table: "Production.FieldsFRs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.FieldsFRs",
                table: "Production.FieldsFRs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.GlobalOperations",
                table: "AC.GlobalOperations");

            migrationBuilder.RenameTable(
                name: "Production.FieldsFRs",
                newName: "FieldsFRs");

            migrationBuilder.RenameTable(
                name: "AC.GlobalOperations",
                newName: "GlobalOperations");

            migrationBuilder.RenameIndex(
                name: "IX_Production.FieldsFRs_FieldId",
                table: "FieldsFRs",
                newName: "IX_FieldsFRs_FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.FieldsFRs_DailyProductionId",
                table: "FieldsFRs",
                newName: "IX_FieldsFRs_DailyProductionId");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "GlobalOperations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GlobalOperations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldsFRs",
                table: "FieldsFRs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalOperations",
                table: "GlobalOperations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.GroupOperations_GlobalOperations_GlobalOperationId",
                table: "AC.GroupOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.UserOperations_GlobalOperations_GlobalOperationId",
                table: "AC.UserOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Hierachy.Fields_FieldId",
                table: "FieldsFRs",
                column: "FieldId",
                principalTable: "Hierachy.Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsFRs_Production.Productions_DailyProductionId",
                table: "FieldsFRs",
                column: "DailyProductionId",
                principalTable: "Production.Productions",
                principalColumn: "Id");
        }
    }
}
