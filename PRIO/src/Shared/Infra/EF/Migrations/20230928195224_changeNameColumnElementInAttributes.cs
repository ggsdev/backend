using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeNameColumnElementInAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PI.Attributes_PI.Elements_ElementsInstaceId",
                table: "PI.Attributes");

            migrationBuilder.RenameColumn(
                name: "ElementsInstaceId",
                table: "PI.Attributes",
                newName: "ElementId");

            migrationBuilder.RenameIndex(
                name: "IX_PI.Attributes_ElementsInstaceId",
                table: "PI.Attributes",
                newName: "IX_PI.Attributes_ElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_PI.Attributes_PI.Elements_ElementId",
                table: "PI.Attributes",
                column: "ElementId",
                principalTable: "PI.Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PI.Attributes_PI.Elements_ElementId",
                table: "PI.Attributes");

            migrationBuilder.RenameColumn(
                name: "ElementId",
                table: "PI.Attributes",
                newName: "ElementsInstaceId");

            migrationBuilder.RenameIndex(
                name: "IX_PI.Attributes_ElementId",
                table: "PI.Attributes",
                newName: "IX_PI.Attributes_ElementsInstaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PI.Attributes_PI.Elements_ElementsInstaceId",
                table: "PI.Attributes",
                column: "ElementsInstaceId",
                principalTable: "PI.Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
