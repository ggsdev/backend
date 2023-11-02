using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class WellTestXmlBase64Fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event.XML042Base64",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event.XML042Base64", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event.XML042Base64_Event.WellEvents_WellEventId",
                        column: x => x.WellEventId,
                        principalTable: "Event.WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileExport.ClosingOpeningFilesXLSX",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    FileContent = table.Column<string>(type: "TEXT", nullable: false),
                    FileExtension = table.Column<string>(type: "CHAR(4)", maxLength: 4, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileExport.ClosingOpeningFilesXLSX", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileExport.ClosingOpeningFilesXLSX_Hierachy.Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Hierachy.Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WellTests.XML042Base64",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellTestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellTests.XML042Base64", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellTests.XML042Base64_WellTest.WellTests_WellTestId",
                        column: x => x.WellTestId,
                        principalTable: "WellTest.WellTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event.XML042Base64_WellEventId",
                table: "Event.XML042Base64",
                column: "WellEventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileExport.ClosingOpeningFilesXLSX_FieldId",
                table: "FileExport.ClosingOpeningFilesXLSX",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_WellTests.XML042Base64_WellTestId",
                table: "WellTests.XML042Base64",
                column: "WellTestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event.XML042Base64");

            migrationBuilder.DropTable(
                name: "FileExport.ClosingOpeningFilesXLSX");

            migrationBuilder.DropTable(
                name: "WellTests.XML042Base64");
        }
    }
}
