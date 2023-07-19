using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class GasVolumeModelsAndMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GasSafetyBurnVolume",
                table: "Installations",
                type: "decimal(38,17)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GasVolumeCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasVolumeCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GasVolumeCalculations_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssistanceGases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceGases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistanceGases_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssistanceGases_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportGases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportGases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportGases_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportGases_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HighPressureGases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighPressureGases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighPressureGases_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HighPressureGases_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HpFlares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HpFlares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HpFlares_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HpFlares_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportGases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportGases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportGases_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportGases_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LowPressureGases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowPressureGases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LowPressureGases_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LowPressureGases_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LPFlares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LPFlares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LPFlares_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LPFlares_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PilotGases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PilotGases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PilotGases_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PilotGases_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurgeGases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(260)", maxLength: 260, nullable: false),
                    GasVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurgeGases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurgeGases_GasVolumeCalculations_GasVolumeCalculationId",
                        column: x => x.GasVolumeCalculationId,
                        principalTable: "GasVolumeCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurgeGases_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceGases_GasVolumeCalculationId",
                table: "AssistanceGases",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceGases_MeasuringPointId",
                table: "AssistanceGases",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportGases_GasVolumeCalculationId",
                table: "ExportGases",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportGases_MeasuringPointId",
                table: "ExportGases",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GasVolumeCalculations_InstallationId",
                table: "GasVolumeCalculations",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_HighPressureGases_GasVolumeCalculationId",
                table: "HighPressureGases",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_HighPressureGases_MeasuringPointId",
                table: "HighPressureGases",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HpFlares_GasVolumeCalculationId",
                table: "HpFlares",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_HpFlares_MeasuringPointId",
                table: "HpFlares",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportGases_GasVolumeCalculationId",
                table: "ImportGases",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportGases_MeasuringPointId",
                table: "ImportGases",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LowPressureGases_GasVolumeCalculationId",
                table: "LowPressureGases",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_LowPressureGases_MeasuringPointId",
                table: "LowPressureGases",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LPFlares_GasVolumeCalculationId",
                table: "LPFlares",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_LPFlares_MeasuringPointId",
                table: "LPFlares",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PilotGases_GasVolumeCalculationId",
                table: "PilotGases",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_PilotGases_MeasuringPointId",
                table: "PilotGases",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurgeGases_GasVolumeCalculationId",
                table: "PurgeGases",
                column: "GasVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurgeGases_MeasuringPointId",
                table: "PurgeGases",
                column: "MeasuringPointId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssistanceGases");

            migrationBuilder.DropTable(
                name: "ExportGases");

            migrationBuilder.DropTable(
                name: "HighPressureGases");

            migrationBuilder.DropTable(
                name: "HpFlares");

            migrationBuilder.DropTable(
                name: "ImportGases");

            migrationBuilder.DropTable(
                name: "LowPressureGases");

            migrationBuilder.DropTable(
                name: "LPFlares");

            migrationBuilder.DropTable(
                name: "PilotGases");

            migrationBuilder.DropTable(
                name: "PurgeGases");

            migrationBuilder.DropTable(
                name: "GasVolumeCalculations");

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
