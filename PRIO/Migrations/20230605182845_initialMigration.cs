using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Acronym = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: false),
                    Username = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    CodCluster = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clusters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExpiresIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserHttpAgent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClusterHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    CodClusterOld = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    CodCluster = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
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
                name: "Installations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodInstallation = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installations_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Installations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodField = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Basin = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Location = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fields_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstallationHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodInstallation = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodInstallationOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterName = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    ClusterNameOld = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    ClusterOldId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", maxLength: 256, nullable: true),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
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
                name: "MeasuringEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    TagEquipment = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    TagMeasuringPoint = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    Fluid = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasuringEquipments_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasuringEquipments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FieldHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodField = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    CodFieldOld = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true),
                    State = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    StateOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Basin = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    BasinOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Location = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    LocationOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationOld = table.Column<Guid>(type: "UniqueIdentifier", maxLength: 120, nullable: true),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
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
                name: "Wells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    WellOperatorName = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWellAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWell = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryReclassificationAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryOperator = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    StatusOperator = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    WaterDepth = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    TopOfPerforated = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    BaseOfPerforated = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    ArtificialLift = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Latitude4C = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Longitude4C = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LatitudeDD = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LongitudeDD = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    DatumHorizontal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeBaseCoordinate = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordX = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordY = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wells_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wells_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodZone = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Zones_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NUM_SERIE_ELEMENTO_PRIMARIO_001 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    COD_INSTALACAO_001 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    COD_TAG_PONTO_MEDICAO_001 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    NUM_SERIE_COMPUTADOR_VAZAO_001 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    DHA_COLETA_001 = table.Column<DateTime>(type: "datetime", nullable: true),
                    MED_TEMPERATURA_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    MED_PRESSAO_ATMSA_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    MED_PRESSAO_RFRNA_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    MED_DENSIDADE_RELATIVA_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    DSC_VERSAO_SOFTWARE_001 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    ICE_METER_FACTOR_1_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_2_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_3_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_4_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_5_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_6_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_7_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_8_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_9_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_10_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_11_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_12_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_13_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_14_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_15_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    QTD_PULSOS_METER_FACTOR_1_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_2_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_3_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_4_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_5_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_6_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_7_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_8_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_9_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_10_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_11_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_12_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_13_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_14_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_15_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_1_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_2_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_3_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_4_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_5_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_6_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_7_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_8_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_9_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_10_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_11_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_12_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_13_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_14_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_15_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_1_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_2_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_3_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_4_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_5_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_6_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_7_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_8_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_9_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_10_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_11_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_12_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_13_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_14_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_15_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_CUTOFF_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 2, nullable: true),
                    ICE_LIMITE_SPRR_ALARME_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 2, nullable: true),
                    ICE_LIMITE_INFRR_ALARME_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 2, nullable: true),
                    IND_HABILITACAO_ALARME_1_001 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    NUM_SERIE_1_001 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_PRSO_LIMITE_SPRR_ALRME_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_LMTE_INFRR_ALRME_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_2_001 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    MED_PRSO_ADOTADA_FALHA_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    DSC_ESTADO_INSNO_CASO_FALHA_001 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IND_TIPO_PRESSAO_CONSIDERADA_001 = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    NUM_SERIE_2_001 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_TMPTA_SPRR_ALARME_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    MED_TMPTA_INFRR_ALRME_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    IND_HABILITACAO_ALARME_3_001 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    MED_TMPTA_ADTTA_FALHA_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    DSC_ESTADO_INSTRUMENTO_FALHA_1_001 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NUM_SERIE_3_001 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    PCT_LIMITE_SUPERIOR_1_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    PCT_LIMITE_INFERIOR_1_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_4_001 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    PCT_ADOTADO_CASO_FALHA_1_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    NUM_SERIE_4_001 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    PCT_LIMITE_SUPERIOR_2_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    PCT_LIMITE_INFERIOR_2_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_5_001 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    PCT_ADOTADO_CASO_FALHA_2_001 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    DSC_ESTADO_INSTRUMENTO_FALHA_3_001 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DSC_ESTADO_INSTRUMENTO_FALHA_2_001 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DHA_INICIO_PERIODO_MEDICAO_001 = table.Column<DateTime>(type: "datetime", nullable: true),
                    DHA_FIM_PERIODO_MEDICAO_001 = table.Column<DateTime>(type: "datetime", nullable: true),
                    ICE_DENSIDADADE_RELATIVA_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    ICE_CORRECAO_BSW_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    ICE_CORRECAO_PRESSAO_LIQUIDO_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    ICE_CRRCO_TEMPERATURA_LIQUIDO_001 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    MED_PRESSAO_ESTATICA_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    MED_TMPTA_FLUIDO_001 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    MED_VOLUME_BRTO_CRRGO_MVMDO_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 5, nullable: true),
                    MED_VOLUME_BRUTO_MVMDO_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 5, nullable: true),
                    MED_VOLUME_LIQUIDO_MVMDO_001 = table.Column<double>(type: "float(6)", precision: 6, scale: 5, nullable: true),
                    MED_VOLUME_TTLZO_FIM_PRDO_001 = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    MED_VOLUME_TTLZO_INCO_PRDO_001 = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    NUM_SERIE_ELEMENTO_PRIMARIO_002 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    COD_INSTALACAO_002 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    COD_TAG_PONTO_MEDICAO_002 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    NUM_SERIE_COMPUTADOR_VAZAO_002 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    DHA_COLETA_002 = table.Column<DateTime>(type: "datetime", nullable: true),
                    MED_TEMPERATURA_1_002 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    MED_PRESSAO_ATMSA_002 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    MED_PRESSAO_RFRNA_002 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    MED_DENSIDADE_RELATIVA_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    DSC_NORMA_UTILIZADA_CALCULO_002 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PCT_CROMATOGRAFIA_NITROGENIO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_CO2_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_METANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_ETANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_PROPANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_N_BUTANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_I_BUTANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_N_PENTANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_I_PENTANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HEXANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HEPTANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_OCTANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_NONANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_DECANO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_H2S_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_AGUA_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HELIO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_OXIGENIO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_CO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HIDROGENIO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_ARGONIO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    DSC_VERSAO_SOFTWARE_002 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    ICE_METER_FACTOR_1_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_2_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_3_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_4_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_5_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_6_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_7_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_8_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_9_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_10_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_11_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_12_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_13_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_14_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    ICE_METER_FACTOR_15_002 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    QTD_PULSOS_METER_FACTOR_1_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_2_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_3_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_4_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_5_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_6_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_7_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_8_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_9_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_10_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_11_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_12_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_13_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_14_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_METER_FACTOR_15_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_1_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_2_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_3_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_4_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_5_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_6_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_7_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_8_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_9_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_10_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_11_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_12_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_13_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_14_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_K_FACTOR_15_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_1_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_2_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_3_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_4_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_5_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_6_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_7_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_8_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_9_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_10_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_11_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_12_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_13_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_14_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    QTD_PULSOS_K_FACTOR_15_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: true),
                    ICE_CUTOFF_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    ICE_LIMITE_SPRR_ALARME_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    ICE_LIMITE_INFRR_ALARME_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_1_002 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    NUM_SERIE_1_002 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_PRSO_LIMITE_SPRR_ALRME_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_LMTE_INFRR_ALRME_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_2_002 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    MED_PRSO_ADOTADA_FALHA_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    DSC_ESTADO_INSNO_CASO_FALHA_002 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IND_TIPO_PRESSAO_CONSIDERADA_002 = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    NUM_SERIE_2_002 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_TMPTA_SPRR_ALARME_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_TMPTA_INFRR_ALRME_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_3_002 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    MED_TMPTA_ADTTA_FALHA_002 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    DSC_ESTADO_INSTRUMENTO_FALHA_002 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DHA_INICIO_PERIODO_MEDICAO_002 = table.Column<DateTime>(type: "datetime", nullable: true),
                    DHA_FIM_PERIODO_MEDICAO_002 = table.Column<DateTime>(type: "datetime", nullable: true),
                    ICE_DENSIDADE_RELATIVA_002 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    MED_PRESSAO_ESTATICA_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_TEMPERATURA_2_002 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    PRZ_DURACAO_FLUXO_EFETIVO_002 = table.Column<double>(type: "float(4)", precision: 4, scale: 4, nullable: true),
                    MED_BRUTO_MOVIMENTADO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 5, nullable: true),
                    MED_CORRIGIDO_MVMDO_002 = table.Column<double>(type: "float(6)", precision: 6, scale: 5, nullable: true),
                    NUM_SERIE_ELEMENTO_PRIMARIO_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    COD_INSTALACAO_003 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    COD_TAG_PONTO_MEDICAO_003 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    NUM_SERIE_COMPUTADOR_VAZAO_003 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    DHA_COLETA_003 = table.Column<DateTime>(type: "datetime", nullable: true),
                    MED_TEMPERATURA_1_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    MED_PRESSAO_ATMSA_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    MED_PRESSAO_RFRNA_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 3, nullable: true),
                    MED_DENSIDADE_RELATIVA_003 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    DSC_NORMA_UTILIZADA_CALCULO_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PCT_CROMATOGRAFIA_NITROGENIO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_CO2_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_METANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_ETANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_PROPANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_N_BUTANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_I_BUTANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_N_PENTANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_I_PENTANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HEXANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HEPTANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_OCTANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_NONANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_DECANO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_H2S_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_AGUA_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HELIO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_OXIGENIO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_CO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_HIDROGENIO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    PCT_CROMATOGRAFIA_ARGONIO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 6, nullable: true),
                    DSC_VERSAO_SOFTWARE_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    CE_LIMITE_SPRR_ALARME_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    ICE_LIMITE_INFRR_ALARME_1_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_1_003 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    ICE_LIMITE_INFRR_ALARME_2_003 = table.Column<bool>(type: "bit", nullable: true),
                    NUM_SERIE_1_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_PRSO_LIMITE_SPRR_ALRME_1_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_LMTE_INFRR_ALRME_1_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_ADOTADA_FALHA_1_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    DSC_ESTADO_INSNO_CASO_FALHA_1_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IND_TIPO_PRESSAO_CONSIDERADA_003 = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    IND_HABILITACAO_ALARME_2_003 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    NUM_SERIE_2_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_TMPTA_SPRR_ALARME_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    MED_TMPTA_INFRR_ALRME_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    IND_HABILITACAO_ALARME_3_003 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    MED_TMPTA_ADTTA_FALHA_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    DSC_ESTADO_INSTRUMENTO_FALHA_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MED_DIAMETRO_REFERENCIA_003 = table.Column<double>(type: "float(4)", precision: 4, scale: 3, nullable: true),
                    MED_TEMPERATURA_RFRNA_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    DSC_MATERIAL_CONTRUCAO_PLACA_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MED_DMTRO_INTRO_TRCHO_MDCO_003 = table.Column<double>(type: "float(4)", precision: 4, scale: 3, nullable: true),
                    MED_TMPTA_TRCHO_MDCO_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    DSC_MATERIAL_CNSTO_TRCHO_MDCO_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DSC_LCLZO_TMDA_PRSO_DFRNL_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IND_TOMADA_PRESSAO_ESTATICA_003 = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    NUM_SERIE_3_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_PRSO_LIMITE_SPRR_ALRME_2_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_LMTE_INFRR_ALRME_2_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    NUM_SERIE_4_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_PRSO_LIMITE_SPRR_ALRME_3_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_LMTE_INFRR_ALRME_3_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    NUM_SERIE_5_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_PRSO_LIMITE_SPRR_ALRME_4_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_LMTE_INFRR_ALRME_4_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_4_003 = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    MED_PRSO_ADOTADA_FALHA_3_003 = table.Column<double>(type: "float(6)", maxLength: 50, precision: 6, scale: 3, nullable: true),
                    DSC_ESTADO_INSNO_CASO_FALHA_3_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MED_CUTOFF_KPA_1_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    NUM_SERIE_6_003 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    MED_PRSO_LIMITE_SPRR_ALRME_5_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_LMTE_INFRR_ALRME_5_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRSO_ADOTADA_FALHA_2_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    IND_HABILITACAO_ALARME_5_003 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DSC_ESTADO_INSNO_CASO_FALHA_2_003 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MED_CUTOFF_KPA_2_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    DHA_INICIO_PERIODO_MEDICAO_003 = table.Column<DateTime>(type: "datetime", nullable: true),
                    DHA_FIM_PERIODO_MEDICAO_003 = table.Column<DateTime>(type: "datetime", nullable: true),
                    ICE_DENSIDADE_RELATIVA_003 = table.Column<double>(type: "float(8)", precision: 8, scale: 8, nullable: true),
                    MED_DIFERENCIAL_PRESSAO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_PRESSAO_ESTATICA_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 3, nullable: true),
                    MED_TEMPERATURA_2_003 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: true),
                    PRZ_DURACAO_FLUXO_EFETIVO_003 = table.Column<double>(type: "float(4)", precision: 4, scale: 4, nullable: true),
                    MED_CORRIGIDO_MVMDO_003 = table.Column<double>(type: "float(6)", precision: 6, scale: 5, nullable: true),
                    COD_TAG_EQUIPAMENTO_039 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    COD_FALHA_SUPERIOR_039 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    DSC_TIPO_FALHA_039 = table.Column<short>(type: "smallint", nullable: true),
                    COD_FALHA_039 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    IND_TIPO_NOTIFICACAO_039 = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    DHA_OCORRENCIA_039 = table.Column<DateTime>(type: "datetime", nullable: true),
                    DHA_DETECCAO_039 = table.Column<DateTime>(type: "datetime", nullable: true),
                    DHA_RETORNO_039 = table.Column<DateTime>(type: "datetime", nullable: true),
                    DHA_NUM_PREVISAO_RETORNO_DIAS_039 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DHA_DSC_FALHA_039 = table.Column<string>(type: "text", nullable: true),
                    DHA_DSC_ACAO_039 = table.Column<string>(type: "text", nullable: true),
                    DHA_DSC_METODOLOGIA_039 = table.Column<string>(type: "text", nullable: true),
                    DHA_NOM_RESPONSAVEL_RELATO_039 = table.Column<string>(type: "varchar(155)", maxLength: 155, nullable: true),
                    DHA_NUM_SERIE_EQUIPAMENTO_039 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    DHA_COD_INSTALACAO_039 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FileTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuringEquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_FileTypes_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                        column: x => x.MeasuringEquipmentId,
                        principalTable: "MeasuringEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Measurements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WellHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    WellOperatorName = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    WellOperatorNameOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWellAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWellAnpOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWell = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CodWellOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryAnpOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryReclassificationAnp = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryReclassificationAnpOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryOperator = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CategoryOperatorOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    StatusOperator = table.Column<bool>(type: "bit", nullable: true),
                    StatusOperatorOld = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    TypeOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    WaterDepth = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    WaterDepthOld = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    TopOfPerforated = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    TopOfPerforatedOld = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    BaseOfPerforated = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    BaseOfPerforatedOld = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: true),
                    ArtificialLift = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    ArtificialLiftOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Latitude4C = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Latitude4COld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Longitude4C = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Longitude4COld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LatitudeDD = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LatitudeDDOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LongitudeDD = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    LongitudeDDOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    DatumHorizontal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumHorizontalOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeBaseCoordinate = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    TypeBaseCoordinateOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordX = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordXOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordY = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    CoordYOld = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldOld = table.Column<Guid>(type: "UniqueIdentifier", maxLength: 120, nullable: true),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    TypeOperation = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Reservoirs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodReservoir = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservoirs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservoirs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservoirs_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ZoneHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodZone = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    CodZoneOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldOldId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldNameOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "BSWS_039",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DHA_FALHA_BSW_039 = table.Column<DateTime>(type: "date", nullable: true),
                    DHA_PCT_BSW_039 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: false),
                    DHA_PCT_MAXIMO_BSW_039 = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: false),
                    MeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BSWS_039", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BSWS_039_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calibrations_039",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DHA_FALHA_CALIBRACAO_039 = table.Column<DateTime>(type: "date", nullable: true),
                    DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 = table.Column<double>(type: "float(5)", precision: 5, scale: 5, nullable: true),
                    DHA_CERTIFICADO_ATUAL_039 = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    DHA_CERTIFICADO_ANTERIOR_039 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibrations_039", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calibrations_039_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volumes_039",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DHA_MEDICAO_039 = table.Column<DateTime>(type: "date", nullable: true),
                    DHA_MED_DECLARADO_039 = table.Column<double>(type: "float(8)", precision: 8, scale: 6, nullable: true),
                    DHA_MED_REGISTRADO_039 = table.Column<double>(type: "float(8)", precision: 8, scale: 6, nullable: true),
                    MeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes_039", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volumes_039_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservoirHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    NameOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodReservoir = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CodReservoirOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneCod = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    ZoneCodOld = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    ZoneOldId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", maxLength: 120, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    TypeOperation = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
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
                name: "Completions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    CodCompletion = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservoirHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WellHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Completions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Completions_ReservoirHistories_ReservoirHistoryId",
                        column: x => x.ReservoirHistoryId,
                        principalTable: "ReservoirHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Completions_Reservoirs_ReservoirId",
                        column: x => x.ReservoirId,
                        principalTable: "Reservoirs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Completions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Completions_WellHistories_WellHistoryId",
                        column: x => x.WellHistoryId,
                        principalTable: "WellHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Completions_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompletionHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodCompletionOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservoirOld = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WellOld = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompletionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActiveOld = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    TypeOperation = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_BSWS_039_MeasurementId",
                table: "BSWS_039",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Calibrations_039_MeasurementId",
                table: "Calibrations_039",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterHistories_ClusterId",
                table: "ClusterHistories",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterHistories_UserId",
                table: "ClusterHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_UserId",
                table: "Clusters",
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
                name: "IX_Completions_ReservoirHistoryId",
                table: "Completions",
                column: "ReservoirHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_ReservoirId",
                table: "Completions",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_UserId",
                table: "Completions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_WellHistoryId",
                table: "Completions",
                column: "WellHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_WellId",
                table: "Completions",
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
                name: "IX_Fields_InstallationId",
                table: "Fields",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_UserId",
                table: "Fields",
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
                name: "IX_Installations_ClusterId",
                table: "Installations",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_UserId",
                table: "Installations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_FileTypeId",
                table: "Measurements",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MeasuringEquipmentId",
                table: "Measurements",
                column: "MeasuringEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_UserId",
                table: "Measurements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_InstallationId",
                table: "MeasuringEquipments",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_UserId",
                table: "MeasuringEquipments",
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
                name: "IX_Reservoirs_UserId",
                table: "Reservoirs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_ZoneId",
                table: "Reservoirs",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_039_MeasurementId",
                table: "Volumes_039",
                column: "MeasurementId");

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
                name: "IX_Wells_FieldId",
                table: "Wells",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Wells_UserId",
                table: "Wells",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Zones_FieldId",
                table: "Zones",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_UserId",
                table: "Zones",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BSWS_039");

            migrationBuilder.DropTable(
                name: "Calibrations_039");

            migrationBuilder.DropTable(
                name: "ClusterHistories");

            migrationBuilder.DropTable(
                name: "CompletionHistories");

            migrationBuilder.DropTable(
                name: "FieldHistories");

            migrationBuilder.DropTable(
                name: "InstallationHistories");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Volumes_039");

            migrationBuilder.DropTable(
                name: "ZoneHistories");

            migrationBuilder.DropTable(
                name: "Completions");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "ReservoirHistories");

            migrationBuilder.DropTable(
                name: "WellHistories");

            migrationBuilder.DropTable(
                name: "FileTypes");

            migrationBuilder.DropTable(
                name: "MeasuringEquipments");

            migrationBuilder.DropTable(
                name: "Reservoirs");

            migrationBuilder.DropTable(
                name: "Wells");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
