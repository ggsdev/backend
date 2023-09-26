using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class addNullObjectsInInstallationAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AC.InstallationsAccess_AC.Users_UserId",
                table: "AC.InstallationsAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.InstallationsAccess_Hierachy.Installations_InstallationId",
                table: "AC.InstallationsAccess");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AC.InstallationsAccess",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallationId",
                table: "AC.InstallationsAccess",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.InstallationsAccess_AC.Users_UserId",
                table: "AC.InstallationsAccess",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.InstallationsAccess_Hierachy.Installations_InstallationId",
                table: "AC.InstallationsAccess",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AC.InstallationsAccess_AC.Users_UserId",
                table: "AC.InstallationsAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.InstallationsAccess_Hierachy.Installations_InstallationId",
                table: "AC.InstallationsAccess");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AC.InstallationsAccess",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallationId",
                table: "AC.InstallationsAccess",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AC.InstallationsAccess_AC.Users_UserId",
                table: "AC.InstallationsAccess",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AC.InstallationsAccess_Hierachy.Installations_InstallationId",
                table: "AC.InstallationsAccess",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
