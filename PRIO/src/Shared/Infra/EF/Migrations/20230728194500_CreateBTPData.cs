using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class CreateBTPData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BTPBases64",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    FileContent = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BTPBases64", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BTPBases64_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BTPDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    PotencialOil = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    PotencialGas = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    PotencialWater = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    InitialDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    FinalDate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Duration = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    BTPNumber = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    BTPBase64Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BTPDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BTPDatas_BTPBases64_BTPBase64Id",
                        column: x => x.BTPBase64Id,
                        principalTable: "BTPBases64",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BTPDatas_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BTPBases64_UserId",
                table: "BTPBases64",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BTPDatas_BTPBase64Id",
                table: "BTPDatas",
                column: "BTPBase64Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BTPDatas_WellId",
                table: "BTPDatas",
                column: "WellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BTPDatas");

            migrationBuilder.DropTable(
                name: "BTPBases64");
        }
    }
}
