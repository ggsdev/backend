using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class AddingColumnsToFieldAndWellProduc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WellAppropriations");

            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "FieldsProductions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductionId",
                table: "FieldsProductions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WellProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionGasInWell = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ProductionWaterInWell = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ProductionOilInWell = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ProductionGasAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionGasAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BtpDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_WellProductions_BTPDatas_BtpDataId",
                        column: x => x.BtpDataId,
                        principalTable: "BTPDatas",
                        principalColumn: "Id");
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellProductions_BtpDataId",
                table: "WellProductions",
                column: "BtpDataId");

            migrationBuilder.CreateIndex(
                name: "IX_WellProductions_FieldProductionId",
                table: "WellProductions",
                column: "FieldProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_WellProductions_ProductionId",
                table: "WellProductions",
                column: "ProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WellProductions");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "FieldsProductions");

            migrationBuilder.DropColumn(
                name: "ProductionId",
                table: "FieldsProductions");

            migrationBuilder.CreateTable(
                name: "WellAppropriations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BtpDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductionGasAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionGasAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionGasInWell = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionOilInWell = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfField = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterAsPercentageOfInstallation = table.Column<decimal>(type: "DECIMAL(7,5)", precision: 7, scale: 5, nullable: false),
                    ProductionWaterInWell = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellAppropriations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellAppropriations_BTPDatas_BtpDataId",
                        column: x => x.BtpDataId,
                        principalTable: "BTPDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WellAppropriations_FieldsProductions_FieldProductionId",
                        column: x => x.FieldProductionId,
                        principalTable: "FieldsProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WellAppropriations_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellAppropriations_BtpDataId",
                table: "WellAppropriations",
                column: "BtpDataId");

            migrationBuilder.CreateIndex(
                name: "IX_WellAppropriations_FieldProductionId",
                table: "WellAppropriations",
                column: "FieldProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_WellAppropriations_ProductionId",
                table: "WellAppropriations",
                column: "ProductionId");
        }
    }
}
