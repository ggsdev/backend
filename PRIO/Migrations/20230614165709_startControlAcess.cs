using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class startControlAcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "MenuOrder",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissionsOperation",
                columns: table => new
                {
                    GroupPermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissionsOperation", x => new { x.GroupPermissionsId, x.OperationsId });
                    table.ForeignKey(
                        name: "FK_GroupPermissionsOperation_GroupMenus_GroupPermissionsId",
                        column: x => x.GroupPermissionsId,
                        principalTable: "GroupMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPermissionsOperation_Operations_OperationsId",
                        column: x => x.OperationsId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissionsOperation_OperationsId",
                table: "GroupPermissionsOperation",
                column: "OperationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "GroupPermissionsOperation");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropColumn(
                name: "MenuOrder",
                table: "Permissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
