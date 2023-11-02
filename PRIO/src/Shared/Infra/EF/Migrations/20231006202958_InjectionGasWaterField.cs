using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class InjectionGasWaterField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterField_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell");

            migrationBuilder.DropTable(
                name: "Injection.InjectionWaterField");

            migrationBuilder.RenameColumn(
                name: "InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                newName: "UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Injection.InjectionWaterWell_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                newName: "IX_Injection.InjectionWaterWell_UpdatedById");

            migrationBuilder.AddColumn<Guid>(
                name: "InjectionWaterGasFieldId",
                table: "Injection.InjectionWaterWell",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InjectionWaterGasFieldId",
                table: "Injection.InjectionGasWell",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallationId",
                table: "Balance.InstallationsBalance",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Injection.InjectionWaterGasField",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BalanceFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injection.InjectionWaterGasField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionWaterGasField_Balance.FieldsBalance_BalanceFieldId",
                        column: x => x.BalanceFieldId,
                        principalTable: "Balance.FieldsBalance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterWell_InjectionWaterGasFieldId",
                table: "Injection.InjectionWaterWell",
                column: "InjectionWaterGasFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionGasWell_InjectionWaterGasFieldId",
                table: "Injection.InjectionGasWell",
                column: "InjectionWaterGasFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance.InstallationsBalance_InstallationId",
                table: "Balance.InstallationsBalance",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterGasField_BalanceFieldId",
                table: "Injection.InjectionWaterGasField",
                column: "BalanceFieldId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Balance.InstallationsBalance_Hierachy.Installations_InstallationId",
                table: "Balance.InstallationsBalance",
                column: "InstallationId",
                principalTable: "Hierachy.Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionGasWell_Injection.InjectionWaterGasField_InjectionWaterGasFieldId",
                table: "Injection.InjectionGasWell",
                column: "InjectionWaterGasFieldId",
                principalTable: "Injection.InjectionWaterGasField",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionWaterWell_AC.Users_UpdatedById",
                table: "Injection.InjectionWaterWell",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterGasField_InjectionWaterGasFieldId",
                table: "Injection.InjectionWaterWell",
                column: "InjectionWaterGasFieldId",
                principalTable: "Injection.InjectionWaterGasField",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance.InstallationsBalance_Hierachy.Installations_InstallationId",
                table: "Balance.InstallationsBalance");

            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionGasWell_Injection.InjectionWaterGasField_InjectionWaterGasFieldId",
                table: "Injection.InjectionGasWell");

            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionWaterWell_AC.Users_UpdatedById",
                table: "Injection.InjectionWaterWell");

            migrationBuilder.DropForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterGasField_InjectionWaterGasFieldId",
                table: "Injection.InjectionWaterWell");

            migrationBuilder.DropTable(
                name: "Injection.InjectionWaterGasField");

            migrationBuilder.DropIndex(
                name: "IX_Injection.InjectionWaterWell_InjectionWaterGasFieldId",
                table: "Injection.InjectionWaterWell");

            migrationBuilder.DropIndex(
                name: "IX_Injection.InjectionGasWell_InjectionWaterGasFieldId",
                table: "Injection.InjectionGasWell");

            migrationBuilder.DropIndex(
                name: "IX_Balance.InstallationsBalance_InstallationId",
                table: "Balance.InstallationsBalance");

            migrationBuilder.DropColumn(
                name: "InjectionWaterGasFieldId",
                table: "Injection.InjectionWaterWell");

            migrationBuilder.DropColumn(
                name: "InjectionWaterGasFieldId",
                table: "Injection.InjectionGasWell");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Balance.InstallationsBalance");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Injection.InjectionWaterWell",
                newName: "InjectionWaterFieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Injection.InjectionWaterWell_UpdatedById",
                table: "Injection.InjectionWaterWell",
                newName: "IX_Injection.InjectionWaterWell_InjectionWaterFieldId");

            migrationBuilder.CreateTable(
                name: "Injection.InjectionWaterField",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BalanceFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    MeasurementAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injection.InjectionWaterField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Injection.InjectionWaterField_Balance.FieldsBalance_BalanceFieldId",
                        column: x => x.BalanceFieldId,
                        principalTable: "Balance.FieldsBalance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Injection.InjectionWaterField_BalanceFieldId",
                table: "Injection.InjectionWaterField",
                column: "BalanceFieldId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Injection.InjectionWaterWell_Injection.InjectionWaterField_InjectionWaterFieldId",
                table: "Injection.InjectionWaterWell",
                column: "InjectionWaterFieldId",
                principalTable: "Injection.InjectionWaterField",
                principalColumn: "Id");
        }
    }
}
