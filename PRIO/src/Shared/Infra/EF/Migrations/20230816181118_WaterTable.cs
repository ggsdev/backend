using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class WaterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WaterId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Waters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusWater = table.Column<bool>(type: "bit", nullable: false),
                    TotalWater = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productions_WaterId",
                table: "Productions",
                column: "WaterId",
                unique: true,
                filter: "[WaterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Waters_WaterId",
                table: "Productions",
                column: "WaterId",
                principalTable: "Waters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Waters_WaterId",
                table: "Productions");

            migrationBuilder.DropTable(
                name: "Waters");

            migrationBuilder.DropIndex(
                name: "IX_Productions_WaterId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "WaterId",
                table: "Productions");
        }
    }
}
