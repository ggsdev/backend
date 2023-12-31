﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class ChangingNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WellEventWellProduction");

            migrationBuilder.DropColumn(
                name: "Downtime",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "Downtime",
                table: "EventReasons");

            migrationBuilder.AlterColumn<string>(
                name: "EventStatus",
                table: "WellEvents",
                type: "char(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "WellEvents",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "EventRelatedId",
                table: "WellEvents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdAutoGenerated",
                table: "WellEvents",
                type: "VARCHAR(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Interval",
                table: "WellEvents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateANP",
                table: "WellEvents",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusANP",
                table: "WellEvents",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemRelated",
                table: "WellEvents",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "EventReasons",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "EventReasons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Interval",
                table: "EventReasons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "EventReasons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ProductionLoss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EfficienceLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionLost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Downtime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProportionalDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLoss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionLoss_WellEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionLoss_WellProductions_WellProductionId",
                        column: x => x.WellProductionId,
                        principalTable: "WellProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellEvents_EventRelatedId",
                table: "WellEvents",
                column: "EventRelatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLoss_EventId",
                table: "ProductionLoss",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLoss_WellProductionId",
                table: "ProductionLoss",
                column: "WellProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_WellEvents_EventRelatedId",
                table: "WellEvents",
                column: "EventRelatedId",
                principalTable: "WellEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_WellEvents_EventRelatedId",
                table: "WellEvents");

            migrationBuilder.DropTable(
                name: "ProductionLoss");

            migrationBuilder.DropIndex(
                name: "IX_WellEvents_EventRelatedId",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "EventRelatedId",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "IdAutoGenerated",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "Interval",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "StateANP",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "StatusANP",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "SystemRelated",
                table: "WellEvents");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "EventReasons");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EventReasons");

            migrationBuilder.DropColumn(
                name: "Interval",
                table: "EventReasons");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "EventReasons");

            migrationBuilder.AlterColumn<bool>(
                name: "EventStatus",
                table: "WellEvents",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "WellEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Downtime",
                table: "WellEvents",
                type: "CHAR(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Downtime",
                table: "EventReasons",
                type: "CHAR(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WellEventWellProduction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellEventWellProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellEventWellProduction_WellEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WellEventWellProduction_WellProductions_WellProductionId",
                        column: x => x.WellProductionId,
                        principalTable: "WellProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellEventWellProduction_EventId",
                table: "WellEventWellProduction",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_WellEventWellProduction_WellProductionId",
                table: "WellEventWellProduction",
                column: "WellProductionId");
        }
    }
}
