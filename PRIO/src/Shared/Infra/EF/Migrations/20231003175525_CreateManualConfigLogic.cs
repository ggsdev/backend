using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class CreateManualConfigLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PI.Attributes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ManualWellConfiguration.ManualWellConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualWellConfiguration.ManualWellConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualWellConfiguration.ManualWellConfigurations_Hierachy.Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Hierachy.Wells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualWellConfiguration.BuildUps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    IsOperating = table.Column<bool>(type: "bit", nullable: false),
                    ManualWellConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualWellConfiguration.BuildUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualWellConfiguration.BuildUps_ManualWellConfiguration.ManualWellConfigurations_ManualWellConfigurationId",
                        column: x => x.ManualWellConfigurationId,
                        principalTable: "ManualWellConfiguration.ManualWellConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualWellConfiguration.InjectivityIndex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    IsOperating = table.Column<bool>(type: "bit", nullable: false),
                    ManualWellConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualWellConfiguration.InjectivityIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualWellConfiguration.InjectivityIndex_ManualWellConfiguration.ManualWellConfigurations_ManualWellConfigurationId",
                        column: x => x.ManualWellConfigurationId,
                        principalTable: "ManualWellConfiguration.ManualWellConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualWellConfiguration.ProductivityIndex",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    IsOperating = table.Column<bool>(type: "bit", nullable: false),
                    ManualWellConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualWellConfiguration.ProductivityIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualWellConfiguration.ProductivityIndex_ManualWellConfiguration.ManualWellConfigurations_ManualWellConfigurationId",
                        column: x => x.ManualWellConfigurationId,
                        principalTable: "ManualWellConfiguration.ManualWellConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManualWellConfiguration.BuildUps_ManualWellConfigurationId",
                table: "ManualWellConfiguration.BuildUps",
                column: "ManualWellConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualWellConfiguration.InjectivityIndex_ManualWellConfigurationId",
                table: "ManualWellConfiguration.InjectivityIndex",
                column: "ManualWellConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualWellConfiguration.ManualWellConfigurations_WellId",
                table: "ManualWellConfiguration.ManualWellConfigurations",
                column: "WellId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManualWellConfiguration.ProductivityIndex_ManualWellConfigurationId",
                table: "ManualWellConfiguration.ProductivityIndex",
                column: "ManualWellConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManualWellConfiguration.BuildUps");

            migrationBuilder.DropTable(
                name: "ManualWellConfiguration.InjectivityIndex");

            migrationBuilder.DropTable(
                name: "ManualWellConfiguration.ProductivityIndex");

            migrationBuilder.DropTable(
                name: "ManualWellConfiguration.ManualWellConfigurations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PI.Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
