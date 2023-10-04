using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDMeasurement4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellTest.BTPBases64_AC.Users_UserId",
                table: "WellTest.BTPBases64");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTest.WellTests_WellTest.BTPBases64_BTPBase64Id",
                table: "WellTest.WellTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellTest.BTPBases64",
                table: "WellTest.BTPBases64");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Validates",
                table: "Validates");

            migrationBuilder.RenameTable(
                name: "WellTest.BTPBases64",
                newName: "WellTest.Bases64");

            migrationBuilder.RenameTable(
                name: "Validates",
                newName: "WellTest.Validates");

            migrationBuilder.RenameIndex(
                name: "IX_WellTest.BTPBases64_UserId",
                table: "WellTest.Bases64",
                newName: "IX_WellTest.Bases64_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellTest.Bases64",
                table: "WellTest.Bases64",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellTest.Validates",
                table: "WellTest.Validates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellTest.Bases64_AC.Users_UserId",
                table: "WellTest.Bases64",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellTest.WellTests_WellTest.Bases64_BTPBase64Id",
                table: "WellTest.WellTests",
                column: "BTPBase64Id",
                principalTable: "WellTest.Bases64",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellTest.Bases64_AC.Users_UserId",
                table: "WellTest.Bases64");

            migrationBuilder.DropForeignKey(
                name: "FK_WellTest.WellTests_WellTest.Bases64_BTPBase64Id",
                table: "WellTest.WellTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellTest.Validates",
                table: "WellTest.Validates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WellTest.Bases64",
                table: "WellTest.Bases64");

            migrationBuilder.RenameTable(
                name: "WellTest.Validates",
                newName: "Validates");

            migrationBuilder.RenameTable(
                name: "WellTest.Bases64",
                newName: "WellTest.BTPBases64");

            migrationBuilder.RenameIndex(
                name: "IX_WellTest.Bases64_UserId",
                table: "WellTest.BTPBases64",
                newName: "IX_WellTest.BTPBases64_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Validates",
                table: "Validates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WellTest.BTPBases64",
                table: "WellTest.BTPBases64",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellTest.BTPBases64_AC.Users_UserId",
                table: "WellTest.BTPBases64",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellTest.WellTests_WellTest.BTPBases64_BTPBase64Id",
                table: "WellTest.WellTests",
                column: "BTPBase64Id",
                principalTable: "WellTest.BTPBases64",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
