using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeTableWellProductions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_WellAllocations_WellAllocationId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellLosses_WellAllocations_WellAllocationId",
                table: "WellLosses");

            migrationBuilder.DropTable(
                name: "WellAllocations");

            migrationBuilder.CreateTable(
                name: "WellProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionGasInWellM3 = table.Column<decimal>(type: "DECIMAL(14,5)", precision: 14, scale: 5, nullable: false),
                    ProductionGasInWellSCF = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionWaterInWellM3 = table.Column<decimal>(type: "DECIMAL(14,5)", precision: 14, scale: 5, nullable: false),
                    ProductionWaterInWellBBL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionOilInWellM3 = table.Column<decimal>(type: "DECIMAL(14,5)", precision: 14, scale: 5, nullable: false),
                    ProductionOilInWellBBL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionGasAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionGasAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    EfficienceLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionLost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Downtime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProportionalDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellTestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellProductions_FieldsProductions_FieldProductionId",
                        column: x => x.FieldProductionId,
                        principalTable: "FieldsProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WellProductions_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WellProductions_WellTests_WellTestId",
                        column: x => x.WellTestId,
                        principalTable: "WellTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellProductions_FieldProductionId",
                table: "WellProductions",
                column: "FieldProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_WellProductions_ProductionId",
                table: "WellProductions",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_WellProductions_WellTestId",
                table: "WellProductions",
                column: "WellTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_WellProductions_WellAllocationId",
                table: "CompletionProductions",
                column: "WellAllocationId",
                principalTable: "WellProductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellLosses_WellProductions_WellAllocationId",
                table: "WellLosses",
                column: "WellAllocationId",
                principalTable: "WellProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_WellProductions_WellAllocationId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellLosses_WellProductions_WellAllocationId",
                table: "WellLosses");

            migrationBuilder.DropTable(
                name: "WellProductions");

            migrationBuilder.CreateTable(
                name: "WellAllocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellTestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductionGasAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionGasAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionGasInWell = table.Column<decimal>(type: "DECIMAL(14,5)", precision: 14, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilInWell = table.Column<decimal>(type: "DECIMAL(14,5)", precision: 14, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterInWell = table.Column<decimal>(type: "DECIMAL(14,5)", precision: 14, scale: 5, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellAllocations_FieldsProductions_FieldProductionId",
                        column: x => x.FieldProductionId,
                        principalTable: "FieldsProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WellAllocations_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WellAllocations_WellTests_WellTestId",
                        column: x => x.WellTestId,
                        principalTable: "WellTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellAllocations_FieldProductionId",
                table: "WellAllocations",
                column: "FieldProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_WellAllocations_ProductionId",
                table: "WellAllocations",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_WellAllocations_WellTestId",
                table: "WellAllocations",
                column: "WellTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_WellAllocations_WellAllocationId",
                table: "CompletionProductions",
                column: "WellAllocationId",
                principalTable: "WellAllocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellLosses_WellAllocations_WellAllocationId",
                table: "WellLosses",
                column: "WellAllocationId",
                principalTable: "WellAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
