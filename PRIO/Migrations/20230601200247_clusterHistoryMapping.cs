using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class clusterHistoryMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClustersHistories_Clusters_clusterId",
                table: "ClustersHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ClustersHistories_Users_UserId",
                table: "ClustersHistories");

            migrationBuilder.RenameColumn(
                name: "clusterId",
                table: "ClustersHistories",
                newName: "ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_ClustersHistories_clusterId",
                table: "ClustersHistories",
                newName: "IX_ClustersHistories_ClusterId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ClustersHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "NameOld",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActiveOld",
                table: "ClustersHistories",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ClustersHistories",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_ClustersHistories_Clusters_ClusterId",
                table: "ClustersHistories",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClustersHistories_Users_UserId",
                table: "ClustersHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClustersHistories_Clusters_ClusterId",
                table: "ClustersHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ClustersHistories_Users_UserId",
                table: "ClustersHistories");

            migrationBuilder.RenameColumn(
                name: "ClusterId",
                table: "ClustersHistories",
                newName: "clusterId");

            migrationBuilder.RenameIndex(
                name: "IX_ClustersHistories_ClusterId",
                table: "ClustersHistories",
                newName: "IX_ClustersHistories_clusterId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ClustersHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameOld",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActiveOld",
                table: "ClustersHistories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "ClustersHistories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClustersHistories_Clusters_clusterId",
                table: "ClustersHistories",
                column: "clusterId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClustersHistories_Users_UserId",
                table: "ClustersHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
