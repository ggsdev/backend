using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationClusterInstallationField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Clusters_ClusterId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Fields_ClusterId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "Fields");

            migrationBuilder.AddColumn<Guid>(
                name: "ClusterId",
                table: "Installations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installations_ClusterId",
                table: "Installations",
                column: "ClusterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations");

            migrationBuilder.DropIndex(
                name: "IX_Installations_ClusterId",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "Installations");

            migrationBuilder.AddColumn<Guid>(
                name: "ClusterId",
                table: "Fields",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fields_ClusterId",
                table: "Fields",
                column: "ClusterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Clusters_ClusterId",
                table: "Fields",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
