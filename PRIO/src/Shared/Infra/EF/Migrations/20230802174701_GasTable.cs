using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class GasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LimitOperacionalBurn = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ScheduledStopBurn = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    ForCommissioningBurn = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    VentedGas = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    WellTestBurn = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    EmergencialBurn = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    OthersBurn = table.Column<decimal>(type: "DECIMAL(10,5)", precision: 10, scale: 5, nullable: false),
                    GasId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gases_GasesDiferencials_GasId",
                        column: x => x.GasId,
                        principalTable: "GasesDiferencials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gases_GasesLinears_GasId",
                        column: x => x.GasId,
                        principalTable: "GasesLinears",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gases_GasId",
                table: "Gases",
                column: "GasId",
                unique: true,
                filter: "[GasId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Gases_GasDiferencialId",
                table: "Productions",
                column: "GasDiferencialId",
                principalTable: "Gases",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Gases_GasDiferencialId",
                table: "Productions");

            migrationBuilder.DropTable(
                name: "Gases");
        }
    }
}
