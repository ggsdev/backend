using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletionInstallation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");
        }
    }
}
