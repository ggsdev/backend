using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class addFieldHistoriesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ClusterId",
                table: "ClusterHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FieldHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodField = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodFieldOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    State = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    StateOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Basin = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    BasinOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Location = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    LocationOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InstallationOld = table.Column<Guid>(type: "UniqueIdentifier", maxLength: 120, nullable: true),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldHistories_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FieldHistories_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FieldHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldHistories_FieldId",
                table: "FieldHistories",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldHistories_InstallationId",
                table: "FieldHistories",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldHistories_UserId",
                table: "FieldHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClusterId",
                table: "ClusterHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
