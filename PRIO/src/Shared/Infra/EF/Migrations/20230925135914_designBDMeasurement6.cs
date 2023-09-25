using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDMeasurement6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Production.CommentsInProduction_AC.Users_CommentedById",
                table: "Production.CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.CommentsInProduction_Measurement.Productions_ProductionId",
                table: "Production.CommentsInProduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.CommentsInProduction",
                table: "Production.CommentsInProduction");

            migrationBuilder.RenameTable(
                name: "Production.CommentsInProduction",
                newName: "Production.Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Production.CommentsInProduction_ProductionId",
                table: "Production.Comments",
                newName: "IX_Production.Comments_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.CommentsInProduction_CommentedById",
                table: "Production.Comments",
                newName: "IX_Production.Comments_CommentedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.Comments",
                table: "Production.Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Comments_AC.Users_CommentedById",
                table: "Production.Comments",
                column: "CommentedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.Comments_Measurement.Productions_ProductionId",
                table: "Production.Comments",
                column: "ProductionId",
                principalTable: "Measurement.Productions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Production.Comments_AC.Users_CommentedById",
                table: "Production.Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Production.Comments_Measurement.Productions_ProductionId",
                table: "Production.Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production.Comments",
                table: "Production.Comments");

            migrationBuilder.RenameTable(
                name: "Production.Comments",
                newName: "Production.CommentsInProduction");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Comments_ProductionId",
                table: "Production.CommentsInProduction",
                newName: "IX_Production.CommentsInProduction_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Production.Comments_CommentedById",
                table: "Production.CommentsInProduction",
                newName: "IX_Production.CommentsInProduction_CommentedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production.CommentsInProduction",
                table: "Production.CommentsInProduction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.CommentsInProduction_AC.Users_CommentedById",
                table: "Production.CommentsInProduction",
                column: "CommentedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Production.CommentsInProduction_Measurement.Productions_ProductionId",
                table: "Production.CommentsInProduction",
                column: "ProductionId",
                principalTable: "Measurement.Productions",
                principalColumn: "Id");
        }
    }
}
