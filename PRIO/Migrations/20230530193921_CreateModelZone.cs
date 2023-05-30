using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class CreateModelZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Fields_FieldId",
                table: "Reservoirs");

            migrationBuilder.RenameColumn(
                name: "FieldId",
                table: "Reservoirs",
                newName: "ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_FieldId",
                table: "Reservoirs",
                newName: "IX_Reservoirs_ZoneId");

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodZone = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zones_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zones_FieldId",
                table: "Zones",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_UserId",
                table: "Zones",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Zones_ZoneId",
                table: "Reservoirs");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.RenameColumn(
                name: "ZoneId",
                table: "Reservoirs",
                newName: "FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservoirs_ZoneId",
                table: "Reservoirs",
                newName: "IX_Reservoirs_FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Fields_FieldId",
                table: "Reservoirs",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
