using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class insertMearingPointModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringEquipments_EquipmentId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DORs_OilVolumeCalculations_OilVolumeCalculationId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringEquipments_EquipmentId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_OilVolumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_OilVolumeCalculations_OilVolumeCalculationId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_Installations_InstallationId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringEquipments_EquipmentId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_OilVolumeCalculations_OilVolumeCalculationId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringEquipments_EquipmentId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_OilVolumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropTable(
                name: "OilVolumeCalculations");

            migrationBuilder.DropIndex(
                name: "IX_MeasuringEquipments_InstallationId",
                table: "MeasuringEquipments");

            migrationBuilder.DropIndex(
                name: "IX_Installations_OilVolumeCalculationId",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "MeasuringEquipments");

            migrationBuilder.RenameColumn(
                name: "OilVolumeCalculationId",
                table: "Installations",
                newName: "InstallationId");

            migrationBuilder.AlterColumn<string>(
                name: "TypeEquipment",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<bool>(
                name: "MVS",
                table: "MeasuringEquipments",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationProtocol",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelNumber",
                table: "MeasuringEquipments",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<Guid>(
                name: "MeasuringPointId",
                table: "MeasuringEquipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MeasuringPoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagPointMeasuring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasuringPoint_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OilVoumeCalculations",
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
                    table.PrimaryKey("PK_OilVoumeCalculations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_MeasuringPointId",
                table: "MeasuringEquipments",
                column: "MeasuringPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_InstallationId",
                table: "Installations",
                column: "InstallationId",
                unique: true,
                filter: "[InstallationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringPoint_InstallationId",
                table: "MeasuringPoint",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringPoint_EquipmentId",
                table: "DORs",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringPoint_EquipmentId",
                table: "DrainVolumes",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_OilVoumeCalculations_InstallationId",
                table: "Installations",
                column: "InstallationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoint_MeasuringPointId",
                table: "MeasuringEquipments",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringPoint_EquipmentId",
                table: "Sections",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoint_EquipmentId",
                table: "TOGRecoveredOils",
                column: "EquipmentId",
                principalTable: "MeasuringPoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                column: "OilVolumeCalculationId",
                principalTable: "OilVoumeCalculations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DORs_MeasuringPoint_EquipmentId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DORs");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_MeasuringPoint_EquipmentId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_OilVoumeCalculations_InstallationId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_MeasuringPoint_MeasuringPointId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MeasuringPoint_EquipmentId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringPoint_EquipmentId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropForeignKey(
                name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils");

            migrationBuilder.DropTable(
                name: "MeasuringPoint");

            migrationBuilder.DropTable(
                name: "OilVoumeCalculations");

            migrationBuilder.DropIndex(
                name: "IX_MeasuringEquipments_MeasuringPointId",
                table: "MeasuringEquipments");

            migrationBuilder.DropIndex(
                name: "IX_Installations_InstallationId",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "MeasuringPointId",
                table: "MeasuringEquipments");

            migrationBuilder.RenameColumn(
                name: "InstallationId",
                table: "Installations",
                newName: "OilVolumeCalculationId");

            migrationBuilder.AlterColumn<string>(
                name: "TypeEquipment",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "MVS",
                table: "MeasuringEquipments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationProtocol",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelNumber",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "MeasuringEquipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,17)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OilVolumeCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilVolumeCalculations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_InstallationId",
                table: "MeasuringEquipments",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_OilVolumeCalculationId",
                table: "Installations",
                column: "OilVolumeCalculationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_MeasuringEquipments_EquipmentId",
                table: "DORs",
                column: "EquipmentId",
                principalTable: "MeasuringEquipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DORs_OilVolumeCalculations_OilVolumeCalculationId",
                table: "DORs",
                column: "OilVolumeCalculationId",
                principalTable: "OilVolumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_MeasuringEquipments_EquipmentId",
                table: "DrainVolumes",
                column: "EquipmentId",
                principalTable: "MeasuringEquipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrainVolumes_OilVolumeCalculations_OilVolumeCalculationId",
                table: "DrainVolumes",
                column: "OilVolumeCalculationId",
                principalTable: "OilVolumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_OilVolumeCalculations_OilVolumeCalculationId",
                table: "Installations",
                column: "OilVolumeCalculationId",
                principalTable: "OilVolumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_Installations_InstallationId",
                table: "MeasuringEquipments",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MeasuringEquipments_EquipmentId",
                table: "Sections",
                column: "EquipmentId",
                principalTable: "MeasuringEquipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_OilVolumeCalculations_OilVolumeCalculationId",
                table: "Sections",
                column: "OilVolumeCalculationId",
                principalTable: "OilVolumeCalculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_MeasuringEquipments_EquipmentId",
                table: "TOGRecoveredOils",
                column: "EquipmentId",
                principalTable: "MeasuringEquipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TOGRecoveredOils_OilVolumeCalculations_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                column: "OilVolumeCalculationId",
                principalTable: "OilVolumeCalculations",
                principalColumn: "Id");
        }
    }
}
