using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ProductionFieldTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellAppropriations_FieldProduction_FieldProductionId",
                table: "WellAppropriations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldProduction",
                table: "FieldProduction");

            migrationBuilder.RenameTable(
                name: "FieldProduction",
                newName: "FieldsProductions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldsProductions",
                table: "FieldsProductions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellAppropriations_FieldsProductions_FieldProductionId",
                table: "WellAppropriations",
                column: "FieldProductionId",
                principalTable: "FieldsProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellAppropriations_FieldsProductions_FieldProductionId",
                table: "WellAppropriations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldsProductions",
                table: "FieldsProductions");

            migrationBuilder.RenameTable(
                name: "FieldsProductions",
                newName: "FieldProduction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldProduction",
                table: "FieldProduction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellAppropriations_FieldProduction_FieldProductionId",
                table: "WellAppropriations",
                column: "FieldProductionId",
                principalTable: "FieldProduction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
