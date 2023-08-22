using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class FieldProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FieldProductionId",
                table: "WellAppropriations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsManually",
                table: "FieldsFRs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FieldProduction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GasProductionInField = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterProductionInField = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OilProductionInField = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldProduction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellAppropriations_FieldProductionId",
                table: "WellAppropriations",
                column: "FieldProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WellAppropriations_FieldProduction_FieldProductionId",
                table: "WellAppropriations",
                column: "FieldProductionId",
                principalTable: "FieldProduction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellAppropriations_FieldProduction_FieldProductionId",
                table: "WellAppropriations");

            migrationBuilder.DropTable(
                name: "FieldProduction");

            migrationBuilder.DropIndex(
                name: "IX_WellAppropriations_FieldProductionId",
                table: "WellAppropriations");

            migrationBuilder.DropColumn(
                name: "FieldProductionId",
                table: "WellAppropriations");

            migrationBuilder.DropColumn(
                name: "IsManually",
                table: "FieldsFRs");
        }
    }
}
