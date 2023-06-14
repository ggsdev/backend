using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class createUserOperations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPermissionsOperation");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.AddColumn<bool>(
                name: "IsParent",
                table: "UserPermissions",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WellOld",
                table: "CompletionHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReservoirOld",
                table: "CompletionHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GroupOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperations", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupOperationsGroupPermissions");

            migrationBuilder.DropTable(
                name: "UserOperationsUserPermissions");

            migrationBuilder.DropTable(
                name: "GroupOperations");

            migrationBuilder.DropTable(
                name: "UserOperations");

            migrationBuilder.DropColumn(
                name: "IsParent",
                table: "UserPermissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "WellOld",
                table: "CompletionHistories",
                type: "UniqueIdentifier",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReservoirOld",
                table: "CompletionHistories",
                type: "UniqueIdentifier",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
                        name: "FK_GroupPermissionsOperation_GroupPermissions_GroupPermissionsId",
                        column: x => x.GroupPermissionsId,
                        principalTable: "GroupPermissions",
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
        }
    }
}
