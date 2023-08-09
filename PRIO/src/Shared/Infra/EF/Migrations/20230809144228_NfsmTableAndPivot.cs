using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class NfsmTableAndPivot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "MeasuredAt",
                table: "MeasurementsHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NFSMId",
                table: "Measurements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NFSMs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeFailure = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    DateOfOcurrence = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionFailure = table.Column<string>(type: "text", nullable: true),
                    Action = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Methodology = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFSMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NFSMs_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NFSMs_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NFSMsProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NFSMId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VolumeAfter = table.Column<decimal>(type: "decimal(10,5)", precision: 10, scale: 5, nullable: false),
                    VolumeBefore = table.Column<decimal>(type: "decimal(10,5)", precision: 10, scale: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFSMsProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NFSMsProductions_NFSMs_NFSMId",
                        column: x => x.NFSMId,
                        principalTable: "NFSMs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NFSMsProductions_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_NFSMId",
                table: "Measurements",
                column: "NFSMId");

            migrationBuilder.CreateIndex(
                name: "IX_NFSMs_InstallationId",
                table: "NFSMs",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_NFSMs_MeasuringPointId",
                table: "NFSMs",
                column: "MeasuringPointId");

            migrationBuilder.CreateIndex(
                name: "IX_NFSMsProductions_NFSMId",
                table: "NFSMsProductions",
                column: "NFSMId");

            migrationBuilder.CreateIndex(
                name: "IX_NFSMsProductions_ProductionId",
                table: "NFSMsProductions",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_NFSMs_NFSMId",
                table: "Measurements",
                column: "NFSMId",
                principalTable: "NFSMs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_NFSMs_NFSMId",
                table: "Measurements");

            migrationBuilder.DropTable(
                name: "NFSMsProductions");

            migrationBuilder.DropTable(
                name: "NFSMs");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_NFSMId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NFSMId",
                table: "Measurements");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MeasuredAt",
                table: "MeasurementsHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
