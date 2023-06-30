using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.src.Shared.Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auxiliaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auxiliaries", x => x.Id);
                });

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
                name: "GlobalOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Route = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Icon = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Table = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TableItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PreviousData = table.Column<string>(type: "varchar(max)", nullable: true),
                    CurrentData = table.Column<string>(type: "varchar(max)", nullable: false),
                    FieldsChanged = table.Column<string>(type: "varchar(max)", nullable: true),
                    TypeOperation = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: true),
                    Username = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPermissionDefault = table.Column<bool>(type: "bit", nullable: true),
                    LastGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupName = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuName = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    MenuRoute = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: false),
                    MenuIcon = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    MenuOrder = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    hasChildren = table.Column<bool>(type: "bit", nullable: true),
                    hasParent = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPermissions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupPermissions_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
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
                    UserHttpAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "GroupOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GlobalOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupOperations_GlobalOperations_GlobalOperationId",
                        column: x => x.GlobalOperationId,
                        principalTable: "GlobalOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupOperations_GroupPermissions_GroupPermissionId",
                        column: x => x.GroupPermissionId,
                        principalTable: "GroupPermissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupName = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuName = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    MenuRoute = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    MenuIcon = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: true),
                    MenuOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hasChildren = table.Column<bool>(type: "bit", nullable: true),
                    hasParent = table.Column<bool>(type: "bit", nullable: true),
                    GroupMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_GroupPermissions_GroupMenuId",
                        column: x => x.GroupMenuId,
                        principalTable: "GroupPermissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Installations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    UepCod = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    UepName = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CodInstallationAnp = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    GasSafetyBurnVolume = table.Column<decimal>(type: "decimal(38,17)", nullable: true),
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
                name: "UserOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationName = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    GroupName = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    UserPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GlobalOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperations_GlobalOperations_GlobalOperationId",
                        column: x => x.GlobalOperationId,
                        principalTable: "GlobalOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOperations_UserPermissions_UserPermissionId",
                        column: x => x.UserPermissionId,
                        principalTable: "UserPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CodField = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    State = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    Basin = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    Location = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
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
                name: "MeasuringPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagPointMeasuring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasuringPoints_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OilVoumeCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilVoumeCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OilVoumeCalculations_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    WellOperatorName = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CodWellAnp = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CodWell = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    CategoryAnp = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CategoryReclassificationAnp = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    CategoryOperator = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    StatusOperator = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    WaterDepth = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    TopOfPerforated = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    BaseOfPerforated = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ArtificialLift = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    Latitude4C = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Longitude4C = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    LatitudeDD = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    LongitudeDD = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    DatumHorizontal = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    TypeBaseCoordinate = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CoordX = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CoordY = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
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
                    CodZone = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
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
                name: "MeasuringEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagEquipment = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    TagMeasuringPoint = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    SerieNumber = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Type = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    TypeEquipment = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Model = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    HasSeal = table.Column<bool>(type: "bit", nullable: false),
                    MVS = table.Column<bool>(type: "bit", nullable: true),
                    CommunicationProtocol = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    TypePoint = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    ChannelNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    InOperation = table.Column<bool>(type: "bit", nullable: false),
                    Fluid = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    MeasuringPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_MeasuringEquipments_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeasuringEquipments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DORs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DORs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DORs_MeasuringPoints_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DORs_OilVoumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVoumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrainVolumes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrainVolumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrainVolumes_MeasuringPoints_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DrainVolumes_OilVoumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVoumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_MeasuringPoints_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sections_OilVoumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVoumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TOGRecoveredOils",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    OilVolumeCalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOGRecoveredOils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOGRecoveredOils_MeasuringPoints_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TOGRecoveredOils_OilVoumeCalculations_OilVolumeCalculationId",
                        column: x => x.OilVolumeCalculationId,
                        principalTable: "OilVoumeCalculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservoirs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CodReservoir = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
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
                    InstallationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_Measurements_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Measurements_MeasuringEquipments_MeasuringEquipmentId",
                        column: x => x.MeasuringEquipmentId,
                        principalTable: "MeasuringEquipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Measurements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Completions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    CodCompletion = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    ReservoirId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_Completions_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
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

            migrationBuilder.CreateIndex(
                name: "IX_BSWS_039_MeasurementId",
                table: "BSWS_039",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Calibrations_039_MeasurementId",
                table: "Calibrations_039",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_UserId",
                table: "Clusters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_ReservoirId",
                table: "Completions",
                column: "ReservoirId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_UserId",
                table: "Completions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_WellId",
                table: "Completions",
                column: "WellId");

            migrationBuilder.CreateIndex(
                name: "IX_DORs_EquipmentId",
                table: "DORs",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DORs_OilVolumeCalculationId",
                table: "DORs",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_EquipmentId",
                table: "DrainVolumes",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DrainVolumes_OilVolumeCalculationId",
                table: "DrainVolumes",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_InstallationId",
                table: "Fields",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_UserId",
                table: "Fields",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOperations_GlobalOperationId",
                table: "GroupOperations",
                column: "GlobalOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOperations_GroupPermissionId",
                table: "GroupOperations",
                column: "GroupPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_GroupId",
                table: "GroupPermissions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_MenuId",
                table: "GroupPermissions",
                column: "MenuId");

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
                name: "IX_Measurements_InstallationId",
                table: "Measurements",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MeasuringEquipmentId",
                table: "Measurements",
                column: "MeasuringEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_UserId",
                table: "Measurements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_MeasuringPointId",
                table: "MeasuringEquipments",
                column: "MeasuringPointId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringEquipments_UserId",
                table: "MeasuringEquipments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringPoints_InstallationId",
                table: "MeasuringPoints",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OilVoumeCalculations_InstallationId",
                table: "OilVoumeCalculations",
                column: "InstallationId",
                unique: true,
                filter: "[InstallationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_UserId",
                table: "Reservoirs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservoirs_ZoneId",
                table: "Reservoirs",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_EquipmentId",
                table: "Sections",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_OilVolumeCalculationId",
                table: "Sections",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_EquipmentId",
                table: "TOGRecoveredOils",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TOGRecoveredOils_OilVolumeCalculationId",
                table: "TOGRecoveredOils",
                column: "OilVolumeCalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperations_GlobalOperationId",
                table: "UserOperations",
                column: "GlobalOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperations_UserPermissionId",
                table: "UserOperations",
                column: "UserPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_GroupMenuId",
                table: "UserPermissions",
                column: "GroupMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_039_MeasurementId",
                table: "Volumes_039",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Wells_FieldId",
                table: "Wells",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Wells_UserId",
                table: "Wells",
                column: "UserId");

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
                name: "Auxiliaries");

            migrationBuilder.DropTable(
                name: "BSWS_039");

            migrationBuilder.DropTable(
                name: "Calibrations_039");

            migrationBuilder.DropTable(
                name: "Completions");

            migrationBuilder.DropTable(
                name: "DORs");

            migrationBuilder.DropTable(
                name: "DrainVolumes");

            migrationBuilder.DropTable(
                name: "GroupOperations");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "SystemHistories");

            migrationBuilder.DropTable(
                name: "TOGRecoveredOils");

            migrationBuilder.DropTable(
                name: "UserOperations");

            migrationBuilder.DropTable(
                name: "Volumes_039");

            migrationBuilder.DropTable(
                name: "Reservoirs");

            migrationBuilder.DropTable(
                name: "Wells");

            migrationBuilder.DropTable(
                name: "OilVoumeCalculations");

            migrationBuilder.DropTable(
                name: "GlobalOperations");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "FileTypes");

            migrationBuilder.DropTable(
                name: "MeasuringEquipments");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "MeasuringPoints");

            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
