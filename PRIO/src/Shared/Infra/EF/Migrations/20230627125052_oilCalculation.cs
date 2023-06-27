using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class oilCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodInstallation",
                table: "Installations",
                newName: "UepName");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodInstallationAnp",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OilVolumeCalculationId",
                table: "Installations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OilVolumeCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilVolumeCalculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DORs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DORs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DORs_MeasuringEquipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringEquipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DORs_OilVolumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVolumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrainVolumes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrainVolumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrainVolumes_MeasuringEquipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringEquipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DrainVolumes_OilVolumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVolumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_MeasuringEquipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringEquipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sections_OilVolumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVolumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TOGRecoveredOils",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOGRecoveredOils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOGRecoveredOils_MeasuringEquipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringEquipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TOGRecoveredOils_OilVolumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVolumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installations_OilVolumeCalculationId",
                table: "Installations",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_DORs_EquipmentId",
                table: "DORs",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DORs_OilVolumeCalculationId",
                table: "DORs",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_EquipmentId",
                table: "DrainVolumes",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_OilVolumeCalculationId",
                table: "DrainVolumes",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_EquipmentId",
                table: "Sections",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_OilVolumeCalculationId",
                table: "Sections",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_EquipmentId",
                table: "TOGRecoveredOils",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                column: "OilVolumeCalculationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_OilVolumeCalculations_OilVolumeCalculationId",
                table: "Installations",
                column: "OilVolumeCalculationId",
                principalTable: "OilVolumeCalculations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_OilVolumeCalculations_OilVolumeCalculationId",
                table: "Installations");

            migrationBuilder.DropTable(
                name: "DORs");

            migrationBuilder.DropTable(
                name: "DrainVolumes");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "TOGRecoveredOils");

            migrationBuilder.DropTable(
                name: "OilVolumeCalculations");

            migrationBuilder.DropIndex(
                name: "IX_Installations_OilVolumeCalculationId",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "CodInstallationAnp",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "OilVolumeCalculationId",
                table: "Installations");

            migrationBuilder.RenameColumn(
                name: "UepName",
                table: "Installations",
                newName: "CodInstallation");

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);
        }
    }
}
