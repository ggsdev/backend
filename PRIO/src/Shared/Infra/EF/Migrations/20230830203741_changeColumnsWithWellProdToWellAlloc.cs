using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnsWithWellProdToWellAlloc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_WellAllocations_WellProductionId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLosses_WellAllocations_WellProductionId",
                table: "ProductionLosses");

            migrationBuilder.RenameColumn(
                name: "WellProductionId",
                table: "ProductionLosses",
                newName: "WellAllocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionLosses_WellProductionId",
                table: "ProductionLosses",
                newName: "IX_ProductionLosses_WellAllocationId");

            migrationBuilder.RenameColumn(
                name: "WellProductionId",
                table: "CompletionProductions",
                newName: "WellAllocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletionProductions_WellProductionId",
                table: "CompletionProductions",
                newName: "IX_CompletionProductions_WellAllocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_WellAllocations_WellAllocationId",
                table: "CompletionProductions",
                column: "WellAllocationId",
                principalTable: "WellAllocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLosses_WellAllocations_WellAllocationId",
                table: "ProductionLosses",
                column: "WellAllocationId",
                principalTable: "WellAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletionProductions_WellAllocations_WellAllocationId",
                table: "CompletionProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLosses_WellAllocations_WellAllocationId",
                table: "ProductionLosses");

            migrationBuilder.RenameColumn(
                name: "WellAllocationId",
                table: "ProductionLosses",
                newName: "WellProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionLosses_WellAllocationId",
                table: "ProductionLosses",
                newName: "IX_ProductionLosses_WellProductionId");

            migrationBuilder.RenameColumn(
                name: "WellAllocationId",
                table: "CompletionProductions",
                newName: "WellProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletionProductions_WellAllocationId",
                table: "CompletionProductions",
                newName: "IX_CompletionProductions_WellProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletionProductions_WellAllocations_WellProductionId",
                table: "CompletionProductions",
                column: "WellProductionId",
                principalTable: "WellAllocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLosses_WellAllocations_WellProductionId",
                table: "ProductionLosses",
                column: "WellProductionId",
                principalTable: "WellAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
