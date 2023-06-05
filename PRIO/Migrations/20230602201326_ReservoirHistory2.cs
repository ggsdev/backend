using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ReservoirHistory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservoirHistoryId",
                table: "Completions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReservoirHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodReservoir = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodReservoirOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneName = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    ZoneNameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    ZoneOldId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", maxLength: 120, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservoirHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservoirHistories_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservoirHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ReservoirHistories_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Completions_ReservoirHistoryId",
                table: "Completions",
                column: "ReservoirHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirHistories_ReservoirId",
                table: "ReservoirHistories",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirHistories_UserId",
                table: "ReservoirHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirHistories_ZoneId",
                table: "ReservoirHistories",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_ReservoirHistories_ReservoirHistoryId",
                table: "Completions",
                column: "ReservoirHistoryId",
                principalTable: "ReservoirHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Completions_ReservoirHistories_ReservoirHistoryId",
                table: "Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs");

            migrationBuilder.DropTable(
                name: "ReservoirHistories");

            migrationBuilder.DropIndex(
                name: "IX_Completions_ReservoirHistoryId",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "ReservoirHistoryId",
                table: "Completions");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
