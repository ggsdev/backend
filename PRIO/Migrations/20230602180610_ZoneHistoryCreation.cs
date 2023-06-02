using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ZoneHistoryCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZoneHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodZone = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodZoneOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldOldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneHistories_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ZoneHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ZoneHistories_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHistories_FieldId",
                table: "ZoneHistories",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHistories_UserId",
                table: "ZoneHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHistories_ZoneId",
                table: "ZoneHistories",
                column: "ZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZoneHistories");
        }
    }
}
