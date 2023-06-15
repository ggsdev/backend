using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringHistorics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Completions_ReservoirHistories_ReservoirHistoryId",
                table: "Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Completions_WellHistories_WellHistoryId",
                table: "Completions");

            migrationBuilder.DropTable(
                name: "ClusterHistories");

            migrationBuilder.DropTable(
                name: "CompletionHistories");

            migrationBuilder.DropTable(
                name: "FieldHistories");

            migrationBuilder.DropTable(
                name: "InstallationHistories");

            migrationBuilder.DropTable(
                name: "ReservoirHistories");

            migrationBuilder.DropTable(
                name: "UserHistories");

            migrationBuilder.DropTable(
                name: "WellHistories");

            migrationBuilder.DropTable(
                name: "ZoneHistories");

            migrationBuilder.DropIndex(
                name: "IX_Completions_ReservoirHistoryId",
                table: "Completions");

            migrationBuilder.DropIndex(
                name: "IX_Completions_WellHistoryId",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReservoirHistoryId",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "WellHistoryId",
                table: "Completions");

            migrationBuilder.AlterColumn<string>(
                name: "UserHttpAgent",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Users",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserHttpAgent",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReservoirHistoryId",
                table: "Completions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WellHistoryId",
                table: "Completions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClusterHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodCluster = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    CodClusterOld = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    TypeOperation = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterHistories_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClusterHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompletionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompletionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodCompletion = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    CodCompletionOld = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    ReservoirOld = table.Column<Guid>(type: "UniqueIdentifier", maxLength: 120, nullable: true),
                    TypeOperation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WellOld = table.Column<Guid>(type: "UniqueIdentifier", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Completions_CompletionId",
                        column: x => x.CompletionId,
                        principalTable: "Completions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletionHistories_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FieldHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Basin = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    BasinOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodField = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    CodFieldOld = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    InstallationOld = table.Column<Guid>(type: "UniqueIdentifier", maxLength: 120, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    Location = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    LocationOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    State = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    StateOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    TypeOperation = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstallationHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterOldId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", maxLength: 256, nullable: true),
                    CodInstallationUep = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodInstallationUepOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    TypeOperation = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallationHistories_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstallationHistories_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstallationHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReservoirHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodReservoir = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodReservoirOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    TypeOperation = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    ZoneOldId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservoirHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservoirHistories_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservoirHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservoirHistories_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    EmailOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: false),
                    PasswordOld = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: false),
                    TypeOld = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: true),
                    TypeOperation = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    UserOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Username = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    UsernameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WellHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtificialLift = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    ArtificialLiftOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    BaseOfPerforated = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    BaseOfPerforatedOld = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    CategoryAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    CategoryAnpOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryOperator = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryOperatorOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryReclassificationAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryReclassificationAnpOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWell = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWellAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    CodWellAnpOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWellOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordX = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    CoordXOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordY = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    CoordYOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DatumHorizontal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumHorizontalOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    FieldOld = table.Column<Guid>(type: "UniqueIdentifier", maxLength: 120, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    Latitude4C = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    Latitude4COld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LatitudeDD = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    LatitudeDDOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Longitude4C = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    Longitude4COld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LongitudeDD = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    LongitudeDDOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    StatusOperator = table.Column<bool>(type: "bit", nullable: true),
                    StatusOperatorOld = table.Column<bool>(type: "bit", nullable: true),
                    TopOfPerforated = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    TopOfPerforatedOld = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    TypeBaseCoordinate = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    TypeBaseCoordinateOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    TypeOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    TypeOperation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaterDepth = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    WaterDepthOld = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    WellOperatorName = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    WellOperatorNameOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellHistories_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WellHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WellHistories_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ZoneHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodZone = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodZoneOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    FieldOldId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    TypeOperation = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneHistories_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ZoneHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ZoneHistories_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Completions_ReservoirHistoryId",
                table: "Completions",
                column: "ReservoirHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_WellHistoryId",
                table: "Completions",
                column: "WellHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterHistories_ClusterId",
                table: "ClusterHistories",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterHistories_UserId",
                table: "ClusterHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_CompletionId",
                table: "CompletionHistories",
                column: "CompletionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_ReservoirId",
                table: "CompletionHistories",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_UserId",
                table: "CompletionHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletionHistories_WellId",
                table: "CompletionHistories",
                column: "WellId");

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

            migrationBuilder.CreateIndex(
                name: "IX_InstallationHistories_ClusterId",
                table: "InstallationHistories",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationHistories_InstallationId",
                table: "InstallationHistories",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationHistories_UserId",
                table: "InstallationHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirHistories_ReservoirId",
                table: "ReservoirHistories",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirHistories_UserId",
                table: "ReservoirHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservoirHistories_ZoneId",
                table: "ReservoirHistories",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_UserId",
                table: "UserHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WellHistories_FieldId",
                table: "WellHistories",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_WellHistories_UserId",
                table: "WellHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WellHistories_WellId",
                table: "WellHistories",
                column: "WellId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHistories_FieldId",
                table: "ZoneHistories",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHistories_UserId",
                table: "ZoneHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneHistories_ZoneId",
                table: "ZoneHistories",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_ReservoirHistories_ReservoirHistoryId",
                table: "Completions",
                column: "ReservoirHistoryId",
                principalTable: "ReservoirHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_WellHistories_WellHistoryId",
                table: "Completions",
                column: "WellHistoryId",
                principalTable: "WellHistories",
                principalColumn: "Id");
        }
    }
}
