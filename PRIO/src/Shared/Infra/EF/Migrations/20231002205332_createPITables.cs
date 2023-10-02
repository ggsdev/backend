using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class createPITables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PI.Databases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PIId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElementsRoute = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI.Databases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PI.Instances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PIId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElementsRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI.Instances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PI.Instances_PI.Databases_DatabaseId",
                        column: x => x.DatabaseId,
                        principalTable: "PI.Databases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI.Elements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PIId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributesRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI.Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PI.Elements_PI.Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "PI.Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI.Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WellName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperating = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI.Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PI.Attributes_PI.Elements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "PI.Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI.Values",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI.Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PI.Values_PI.Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "PI.Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PI.WellsValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PI.WellsValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PI.WellsValues_Hierachy.Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Hierachy.Wells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PI.WellsValues_PI.Values_ValueId",
                        column: x => x.ValueId,
                        principalTable: "PI.Values",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PI.Attributes_ElementId",
                table: "PI.Attributes",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_PI.Attributes_WebId",
                table: "PI.Attributes",
                column: "WebId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PI.Databases_WebId",
                table: "PI.Databases",
                column: "WebId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PI.Elements_InstanceId",
                table: "PI.Elements",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_PI.Elements_WebId",
                table: "PI.Elements",
                column: "WebId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PI.Instances_DatabaseId",
                table: "PI.Instances",
                column: "DatabaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PI.Instances_WebId",
                table: "PI.Instances",
                column: "WebId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PI.Values_AttributeId",
                table: "PI.Values",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PI.WellsValues_ValueId",
                table: "PI.WellsValues",
                column: "ValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PI.WellsValues_WellId",
                table: "PI.WellsValues",
                column: "WellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PI.WellsValues");

            migrationBuilder.DropTable(
                name: "PI.Values");

            migrationBuilder.DropTable(
                name: "PI.Attributes");

            migrationBuilder.DropTable(
                name: "PI.Elements");

            migrationBuilder.DropTable(
                name: "PI.Instances");

            migrationBuilder.DropTable(
                name: "PI.Databases");
        }
    }
}
