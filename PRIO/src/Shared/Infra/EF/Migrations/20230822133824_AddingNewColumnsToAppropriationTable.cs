using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewColumnsToAppropriationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionInWell",
                table: "WellAppropriations",
                newName: "ProductionWaterInWell");

            migrationBuilder.RenameColumn(
                name: "ProductionAsPercentageOfInstallation",
                table: "WellAppropriations",
                newName: "ProductionWaterAsPercentageOfInstallation");

            migrationBuilder.RenameColumn(
                name: "ProductionAsPercentageOfField",
                table: "WellAppropriations",
                newName: "ProductionWaterAsPercentageOfField");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionGasAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionGasAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionGasInWell",
                table: "WellAppropriations",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionOilAsPercentageOfField",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionOilAsPercentageOfInstallation",
                table: "WellAppropriations",
                type: "DECIMAL(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductionOilInWell",
                table: "WellAppropriations",
                type: "DECIMAL(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionGasAsPercentageOfField",
                table: "WellAppropriations");

            migrationBuilder.DropColumn(
                name: "ProductionGasAsPercentageOfInstallation",
                table: "WellAppropriations");

            migrationBuilder.DropColumn(
                name: "ProductionGasInWell",
                table: "WellAppropriations");

            migrationBuilder.DropColumn(
                name: "ProductionOilAsPercentageOfField",
                table: "WellAppropriations");

            migrationBuilder.DropColumn(
                name: "ProductionOilAsPercentageOfInstallation",
                table: "WellAppropriations");

            migrationBuilder.DropColumn(
                name: "ProductionOilInWell",
                table: "WellAppropriations");

            migrationBuilder.RenameColumn(
                name: "ProductionWaterInWell",
                table: "WellAppropriations",
                newName: "ProductionInWell");

            migrationBuilder.RenameColumn(
                name: "ProductionWaterAsPercentageOfInstallation",
                table: "WellAppropriations",
                newName: "ProductionAsPercentageOfInstallation");

            migrationBuilder.RenameColumn(
                name: "ProductionWaterAsPercentageOfField",
                table: "WellAppropriations",
                newName: "ProductionAsPercentageOfField");
        }
    }
}
