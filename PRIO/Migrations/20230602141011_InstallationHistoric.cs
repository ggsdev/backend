using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class InstallationHistoric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations");

            migrationBuilder.AlterColumn<Guid>(
                name: "FieldId",
                table: "Wells",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationHistoryId",
                table: "MeasuringEquipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClusterId",
                table: "Installations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationHistoryId",
                table: "Fields",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReservoirId",
                table: "Completions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodCompletion",
                table: "Completions",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "InstallationsHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodInstallation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodInstallationOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterOldId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationsHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallationsHistories_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallationsHistories_Clusters_ClusterOldId",
                        column: x => x.ClusterOldId,
                        principalTable: "Clusters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstallationsHistories_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallationsHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_InstallationHistoryId",
                table: "MeasuringEquipments",
                column: "InstallationHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_InstallationHistoryId",
                table: "Fields",
                column: "InstallationHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationsHistories_ClusterId",
                table: "InstallationsHistories",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationsHistories_ClusterOldId",
                table: "InstallationsHistories",
                column: "ClusterOldId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationsHistories_InstallationId",
                table: "InstallationsHistories",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationsHistories_UserId",
                table: "InstallationsHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_InstallationsHistories_InstallationHistoryId",
                table: "Fields",
                column: "InstallationHistoryId",
                principalTable: "InstallationsHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_InstallationsHistories_InstallationHistoryId",
                table: "MeasuringEquipments",
                column: "InstallationHistoryId",
                principalTable: "InstallationsHistories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_InstallationsHistories_InstallationHistoryId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_InstallationsHistories_InstallationHistoryId",
                table: "MeasuringEquipments");

            migrationBuilder.DropTable(
                name: "InstallationsHistories");

            migrationBuilder.DropIndex(
                name: "IX_MeasuringEquipments_InstallationHistoryId",
                table: "MeasuringEquipments");

            migrationBuilder.DropIndex(
                name: "IX_Fields_InstallationHistoryId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "InstallationHistoryId",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "InstallationHistoryId",
                table: "Fields");

            migrationBuilder.AlterColumn<Guid>(
                name: "FieldId",
                table: "Wells",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClusterId",
                table: "Installations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReservoirId",
                table: "Completions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CodCompletion",
                table: "Completions",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Clusters_ClusterId",
                table: "Installations",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
