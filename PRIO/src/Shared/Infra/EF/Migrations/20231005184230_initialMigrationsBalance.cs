using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class initialMigrationsBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balance.UEPsBalance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalWaterProduced = table.Column<double>(type: "float", nullable: false),
                    TotalWaterInjected = table.Column<double>(type: "float", nullable: false),
                    TOtalWaterInjectedRS = table.Column<double>(type: "float", nullable: false),
                    TotalWaterDisposal = table.Column<double>(type: "float", nullable: false),
                    TotalWaterReceived = table.Column<double>(type: "float", nullable: false),
                    TotalWaterCaptured = table.Column<double>(type: "float", nullable: false),
                    DischargedSurface = table.Column<double>(type: "float", nullable: false),
                    TotalWaterTransferred = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance.UEPsBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Balance.InstallationsBalance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalWaterProduced = table.Column<double>(type: "float", nullable: false),
                    TotalWaterInjected = table.Column<double>(type: "float", nullable: false),
                    TOtalWaterInjectedRS = table.Column<double>(type: "float", nullable: false),
                    TotalWaterDisposal = table.Column<double>(type: "float", nullable: false),
                    TotalWaterReceived = table.Column<double>(type: "float", nullable: false),
                    TotalWaterCaptured = table.Column<double>(type: "float", nullable: false),
                    DischargedSurface = table.Column<double>(type: "float", nullable: false),
                    TotalWaterTransferred = table.Column<double>(type: "float", nullable: false),
                    UEPBalanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance.InstallationsBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance.InstallationsBalance_Balance.UEPsBalance_UEPBalanceId",
                        column: x => x.UEPBalanceId,
                        principalTable: "Balance.UEPsBalance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Balance.FieldsBalance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FIRS = table.Column<double>(type: "float", nullable: false),
                    TotalWaterProduced = table.Column<double>(type: "float", nullable: false),
                    TotalWaterInjected = table.Column<double>(type: "float", nullable: false),
                    TOtalWaterInjectedRS = table.Column<double>(type: "float", nullable: false),
                    TotalWaterDisposal = table.Column<double>(type: "float", nullable: false),
                    TotalWaterReceived = table.Column<double>(type: "float", nullable: false),
                    TotalWaterCaptured = table.Column<double>(type: "float", nullable: false),
                    DischargedSurface = table.Column<double>(type: "float", nullable: false),
                    TotalWaterTransferred = table.Column<double>(type: "float", nullable: false),
                    IsParameterized = table.Column<bool>(type: "bit", nullable: false),
                    installationBalanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FieldProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance.FieldsBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance.FieldsBalance_Balance.InstallationsBalance_installationBalanceId",
                        column: x => x.installationBalanceId,
                        principalTable: "Balance.InstallationsBalance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Balance.FieldsBalance_Production.FieldsProductions_FieldProductionId",
                        column: x => x.FieldProductionId,
                        principalTable: "Production.FieldsProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Injection.InjectionWaterField",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BalanceFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injection.InjectionWaterField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionWaterField_Balance.FieldsBalance_BalanceFieldId",
                        column: x => x.BalanceFieldId,
                        principalTable: "Balance.FieldsBalance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Injection.InjectionWaterWell",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedValue = table.Column<double>(type: "float", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WellValuesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InjectionWaterFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injection.InjectionWaterWell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionWaterWell_AC.Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AC.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterField_InjectionWaterFieldId",
                        column: x => x.InjectionWaterFieldId,
                        principalTable: "Injection.InjectionWaterField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionWaterWell_PI.WellsValues_WellValuesId",
                        column: x => x.WellValuesId,
                        principalTable: "PI.WellsValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balance.FieldsBalance_FieldProductionId",
                table: "Balance.FieldsBalance",
                column: "FieldProductionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balance.FieldsBalance_installationBalanceId",
                table: "Balance.FieldsBalance",
                column: "installationBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance.InstallationsBalance_UEPBalanceId",
                table: "Balance.InstallationsBalance",
                column: "UEPBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterField_BalanceFieldId",
                table: "Injection.InjectionWaterField",
                column: "BalanceFieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterWell_CreatedById",
                table: "Injection.InjectionWaterWell",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterWell_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                column: "InjectionWaterFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterWell_WellValuesId",
                table: "Injection.InjectionWaterWell",
                column: "WellValuesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Injection.InjectionWaterWell");

            migrationBuilder.DropTable(
                name: "Injection.InjectionWaterField");

            migrationBuilder.DropTable(
                name: "Balance.FieldsBalance");

            migrationBuilder.DropTable(
                name: "Balance.InstallationsBalance");

            migrationBuilder.DropTable(
                name: "Balance.UEPsBalance");
        }
    }
}
