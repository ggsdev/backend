using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class globalOperations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupOperationsGroupPermissions");

            migrationBuilder.DropTable(
                name: "UserOperationsUserPermissions");

            migrationBuilder.AddColumn<Guid>(
                name: "GlobalOperationId",
                table: "UserOperations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserPermissionId",
                table: "UserOperations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GlobalOperationId",
                table: "GroupOperations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupPermissionId",
                table: "GroupOperations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GlobalOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalOperation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOperations_GlobalOperationId",
                table: "UserOperations",
                column: "GlobalOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperations_UserPermissionId",
                table: "UserOperations",
                column: "UserPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOperations_GlobalOperationId",
                table: "GroupOperations",
                column: "GlobalOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOperations_GroupPermissionId",
                table: "GroupOperations",
                column: "GroupPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOperations_GlobalOperation_GlobalOperationId",
                table: "GroupOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOperations_GroupPermissions_GroupPermissionId",
                table: "GroupOperations",
                column: "GroupPermissionId",
                principalTable: "GroupPermissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperations_GlobalOperation_GlobalOperationId",
                table: "UserOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupOperations_GlobalOperation_GlobalOperationId",
                table: "GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOperations_GroupPermissions_GroupPermissionId",
                table: "GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperations_GlobalOperation_GlobalOperationId",
                table: "UserOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations");

            migrationBuilder.DropTable(
                name: "GlobalOperation");

            migrationBuilder.DropIndex(
                name: "IX_UserOperations_GlobalOperationId",
                table: "UserOperations");

            migrationBuilder.DropIndex(
                name: "IX_UserOperations_UserPermissionId",
                table: "UserOperations");

            migrationBuilder.DropIndex(
                name: "IX_GroupOperations_GlobalOperationId",
                table: "GroupOperations");

            migrationBuilder.DropIndex(
                name: "IX_GroupOperations_GroupPermissionId",
                table: "GroupOperations");

            migrationBuilder.DropColumn(
                name: "GlobalOperationId",
                table: "UserOperations");

            migrationBuilder.DropColumn(
                name: "UserPermissionId",
                table: "UserOperations");

            migrationBuilder.DropColumn(
                name: "GlobalOperationId",
                table: "GroupOperations");

            migrationBuilder.DropColumn(
                name: "GroupPermissionId",
                table: "GroupOperations");

            migrationBuilder.CreateTable(
                name: "GroupOperationsGroupPermissions",
                columns: table => new
                {
                    GroupPermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOperationsGroupPermissions", x => new { x.GroupPermissionsId, x.OperationsId });
                    table.ForeignKey(
                        name: "FK_GroupOperationsGroupPermissions_GroupOperations_OperationsId",
                        column: x => x.OperationsId,
                        principalTable: "GroupOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupOperationsGroupPermissions_GroupPermissions_GroupPermissionsId",
                        column: x => x.GroupPermissionsId,
                        principalTable: "GroupPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationsUserPermissions",
                columns: table => new
                {
                    UserOperationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationsUserPermissions", x => new { x.UserOperationsId, x.UserPermissionId });
                    table.ForeignKey(
                        name: "FK_UserOperationsUserPermissions_UserOperations_UserOperationsId",
                        column: x => x.UserOperationsId,
                        principalTable: "UserOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationsUserPermissions_UserPermissions_UserPermissionId",
                        column: x => x.UserPermissionId,
                        principalTable: "UserPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupOperationsGroupPermissions_OperationsId",
                table: "GroupOperationsGroupPermissions",
                column: "OperationsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationsUserPermissions_UserPermissionId",
                table: "UserOperationsUserPermissions",
                column: "UserPermissionId");
        }
    }
}
