using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class createTablePi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PI.Databases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    WebId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    WebId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributesRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    WebId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelfRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WellName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_PI.Attributes_ElementId",
                table: "PI.Attributes",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_PI.Elements_InstanceId",
                table: "PI.Elements",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_PI.Instances_DatabaseId",
                table: "PI.Instances",
                column: "DatabaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
