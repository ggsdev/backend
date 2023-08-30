using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ProductionIsCalculatedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLoss_WellEvents_EventId",
                table: "ProductionLoss");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLoss_WellProductions_WellProductionId",
                table: "ProductionLoss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionLoss",
                table: "ProductionLoss");

            migrationBuilder.RenameTable(
                name: "ProductionLoss",
                newName: "ProductionLosses");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionLoss_WellProductionId",
                table: "ProductionLosses",
                newName: "IX_ProductionLosses_WellProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionLoss_EventId",
                table: "ProductionLosses",
                newName: "IX_ProductionLosses_EventId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCalculated",
                table: "Productions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionLosses",
                table: "ProductionLosses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLosses_WellEvents_EventId",
                table: "ProductionLosses",
                column: "EventId",
                principalTable: "WellEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLosses_WellProductions_WellProductionId",
                table: "ProductionLosses",
                column: "WellProductionId",
                principalTable: "WellProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLosses_WellEvents_EventId",
                table: "ProductionLosses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLosses_WellProductions_WellProductionId",
                table: "ProductionLosses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionLosses",
                table: "ProductionLosses");

            migrationBuilder.DropColumn(
                name: "IsCalculated",
                table: "Productions");

            migrationBuilder.RenameTable(
                name: "ProductionLosses",
                newName: "ProductionLoss");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionLosses_WellProductionId",
                table: "ProductionLoss",
                newName: "IX_ProductionLoss_WellProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionLosses_EventId",
                table: "ProductionLoss",
                newName: "IX_ProductionLoss_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionLoss",
                table: "ProductionLoss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLoss_WellEvents_EventId",
                table: "ProductionLoss",
                column: "EventId",
                principalTable: "WellEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLoss_WellProductions_WellProductionId",
                table: "ProductionLoss",
                column: "WellProductionId",
                principalTable: "WellProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
