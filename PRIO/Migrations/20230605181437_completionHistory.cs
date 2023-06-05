using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class completionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZoneNameOld",
                table: "ReservoirHistories",
                newName: "ZoneCodOld");

            migrationBuilder.RenameColumn(
                name: "ZoneName",
                table: "ReservoirHistories",
                newName: "ZoneCod");

            migrationBuilder.CreateTable(
                name: "CompletionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodCompletionOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservoirOld = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WellOld = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompletionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    TypeOperation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Completions_CompletionId",
                        column: x => x.CompletionId,
                        principalTable: "Completions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_CompletionId",
                table: "CompletionHistories",
                column: "CompletionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_ReservoirId",
                table: "CompletionHistories",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_UserId",
                table: "CompletionHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_WellId",
                table: "CompletionHistories",
                column: "WellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletionHistories");

            migrationBuilder.RenameColumn(
                name: "ZoneCodOld",
                table: "ReservoirHistories",
                newName: "ZoneNameOld");

            migrationBuilder.RenameColumn(
                name: "ZoneCod",
                table: "ReservoirHistories",
                newName: "ZoneName");
        }
    }
}
