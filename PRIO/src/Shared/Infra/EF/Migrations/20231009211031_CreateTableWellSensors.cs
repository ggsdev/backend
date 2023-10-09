using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableWellSensors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Injection.WellSensors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedValue = table.Column<double>(type: "float", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WellValuesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injection.WellSensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Injection.WellSensors_AC.Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AC.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Injection.WellSensors_AC.Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AC.Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Injection.WellSensors_PI.WellsValues_WellValuesId",
                        column: x => x.WellValuesId,
                        principalTable: "PI.WellsValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Injection.WellSensors_CreatedById",
                table: "Injection.WellSensors",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.WellSensors_UpdatedById",
                table: "Injection.WellSensors",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.WellSensors_WellValuesId",
                table: "Injection.WellSensors",
                column: "WellValuesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Injection.WellSensors");
        }
    }
}
