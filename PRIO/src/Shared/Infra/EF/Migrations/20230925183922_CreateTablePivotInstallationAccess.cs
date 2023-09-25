using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablePivotInstallationAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AC.InstallationsAccess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AC.InstallationsAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AC.InstallationsAccess_AC.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "AC.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AC.InstallationsAccess_Hierachy.Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Hierachy.Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AC.InstallationsAccess_InstallationId",
                table: "AC.InstallationsAccess",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_AC.InstallationsAccess_UserId",
                table: "AC.InstallationsAccess",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AC.InstallationsAccess");
        }
    }
}
