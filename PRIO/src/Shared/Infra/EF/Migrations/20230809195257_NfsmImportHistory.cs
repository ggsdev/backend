using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class NfsmImportHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NFSMImportHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImportedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOperation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FileName = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: false),
                    FileType = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    FileAcronym = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    FileContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFSMImportHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NFSMImportHistories_NFSMs_ImportId",
                        column: x => x.ImportId,
                        principalTable: "NFSMs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NFSMImportHistories_Users_ImportedById",
                        column: x => x.ImportedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NFSMImportHistories_ImportedById",
                table: "NFSMImportHistories",
                column: "ImportedById");

            migrationBuilder.CreateIndex(
                name: "IX_NFSMImportHistories_ImportId",
                table: "NFSMImportHistories",
                column: "ImportId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NFSMImportHistories");
        }
    }
}
