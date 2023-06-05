using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class wellHistyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WellHistoryId",
                table: "Completions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WellHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WellOperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WellOperatorNameOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodWellAnp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodWellAnpOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodWell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodWellOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryAnp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryAnpOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryReclassificationAnp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryReclassificationAnpOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryOperatorOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusOperator = table.Column<bool>(type: "bit", nullable: true),
                    StatusOperatorOld = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaterDepth = table.Column<double>(type: "float", nullable: true),
                    WaterDepthOld = table.Column<double>(type: "float", nullable: true),
                    TopOfPerforated = table.Column<double>(type: "float", nullable: true),
                    TopOfPerforatedOld = table.Column<double>(type: "float", nullable: true),
                    BaseOfPerforated = table.Column<double>(type: "float", nullable: true),
                    BaseOfPerforatedOld = table.Column<double>(type: "float", nullable: true),
                    ArtificialLift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtificialLiftOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude4C = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude4COld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude4C = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude4COld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatitudeDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatitudeDDOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongitudeDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongitudeDDOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumHorizontal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumHorizontalOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeBaseCoordinate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeBaseCoordinateOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordXOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordYOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FieldOld = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellHistories_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WellHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WellHistories_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Completions_WellHistoryId",
                table: "Completions",
                column: "WellHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WellHistories_FieldId",
                table: "WellHistories",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_WellHistories_UserId",
                table: "WellHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WellHistories_WellId",
                table: "WellHistories",
                column: "WellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_WellHistories_WellHistoryId",
                table: "Completions",
                column: "WellHistoryId",
                principalTable: "WellHistories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Completions_WellHistories_WellHistoryId",
                table: "Completions");

            migrationBuilder.DropTable(
                name: "WellHistories");

            migrationBuilder.DropIndex(
                name: "IX_Completions_WellHistoryId",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "WellHistoryId",
                table: "Completions");
        }
    }
}
