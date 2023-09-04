using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeTableNameWellTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellAllocations_BTPDatas_BtpDataId",
                table: "WellAllocations");

            migrationBuilder.DropTable(
                name: "BTPDatas");

            migrationBuilder.RenameColumn(
                name: "BtpDataId",
                table: "WellAllocations",
                newName: "WellTestId");

            migrationBuilder.RenameIndex(
                name: "IX_WellAllocations_BtpDataId",
                table: "WellAllocations",
                newName: "IX_WellAllocations_WellTestId");

            migrationBuilder.CreateTable(
                name: "WellTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    BTPSheet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WellName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WellAlignmentData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WellAlignmentHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotencialLiquid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotencialLiquidPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MPointOil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotencialOil = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialOilPerHour = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    MPointGas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotencialGas = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialGasPerHour = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    MPointWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotencialWater = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialWaterPerHour = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    InitialDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    FinalDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Duration = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    BTPNumber = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    RGO = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    BSW = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    ApplicationDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    FinalApplicationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BTPId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    BTPBase64Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellTests_BTPBases64_BTPBase64Id",
                        column: x => x.BTPBase64Id,
                        principalTable: "BTPBases64",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WellTests_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellTests_BTPBase64Id",
                table: "WellTests",
                column: "BTPBase64Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WellTests_WellId",
                table: "WellTests",
                column: "WellId");

            migrationBuilder.AddForeignKey(
                name: "FK_WellAllocations_WellTests_WellTestId",
                table: "WellAllocations",
                column: "WellTestId",
                principalTable: "WellTests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellAllocations_WellTests_WellTestId",
                table: "WellAllocations");

            migrationBuilder.DropTable(
                name: "WellTests");

            migrationBuilder.RenameColumn(
                name: "WellTestId",
                table: "WellAllocations",
                newName: "BtpDataId");

            migrationBuilder.RenameIndex(
                name: "IX_WellAllocations_WellTestId",
                table: "WellAllocations",
                newName: "IX_WellAllocations_BtpDataId");

            migrationBuilder.CreateTable(
                name: "BTPDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BTPBase64Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    BSW = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    BTPId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BTPNumber = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    BTPSheet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Filename = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    FinalApplicationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    InitialDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    MPointGas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MPointOil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MPointWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotencialGas = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialGasPerHour = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialLiquid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotencialLiquidPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PotencialOil = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialOilPerHour = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialWater = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    PotencialWaterPerHour = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    RGO = table.Column<decimal>(type: "decimal(15,5)", precision: 15, scale: 5, nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WellAlignmentData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WellAlignmentHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WellName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BTPDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BTPDatas_BTPBases64_BTPBase64Id",
                        column: x => x.BTPBase64Id,
                        principalTable: "BTPBases64",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BTPDatas_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BTPDatas_BTPBase64Id",
                table: "BTPDatas",
                column: "BTPBase64Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BTPDatas_WellId",
                table: "BTPDatas",
                column: "WellId");

            migrationBuilder.AddForeignKey(
                name: "FK_WellAllocations_BTPDatas_BtpDataId",
                table: "WellAllocations",
                column: "BtpDataId",
                principalTable: "BTPDatas",
                principalColumn: "Id");
        }
    }
}
