using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableBTPAndPVTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ImportedAt",
                table: "MeasurementsHistories",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BTPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Mutable = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    FileContent = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    CellPotencialOil = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    CellPotencialGas = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    CellPotencialWater = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    CellInitialDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellFinalDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellBTPNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BTPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntallationsBTPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BTPId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntallationsBTPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntallationsBTPs_BTPs_BTPId",
                        column: x => x.BTPId,
                        principalTable: "BTPs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IntallationsBTPs_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntallationsBTPs_BTPId",
                table: "IntallationsBTPs",
                column: "BTPId");

            migrationBuilder.CreateIndex(
                name: "IX_IntallationsBTPs_InstallationId",
                table: "IntallationsBTPs",
                column: "InstallationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntallationsBTPs");

            migrationBuilder.DropTable(
                name: "BTPs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ImportedAt",
                table: "MeasurementsHistories",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
