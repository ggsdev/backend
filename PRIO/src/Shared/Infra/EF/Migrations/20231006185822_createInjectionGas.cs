using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class createInjectionGas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Injection.InjectionGasWell",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedValue = table.Column<double>(type: "float", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WellValuesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injection.InjectionGasWell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionGasWell_AC.Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AC.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionGasWell_PI.WellsValues_WellValuesId",
                        column: x => x.WellValuesId,
                        principalTable: "PI.WellsValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionGasWell_CreatedById",
                table: "Injection.InjectionGasWell",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionGasWell_WellValuesId",
                table: "Injection.InjectionGasWell",
                column: "WellValuesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Injection.InjectionGasWell");
        }
    }
}
