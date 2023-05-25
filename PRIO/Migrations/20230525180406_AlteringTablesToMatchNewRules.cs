using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class AlteringTablesToMatchNewRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_Units_UnitId",
                table: "Clusters");

            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_Users_UserId",
                table: "Clusters");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Users_UserId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_FileTypes_FileTypeId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Users_UserId",
                table: "Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Wells_Completions_CompletionId",
                table: "Wells");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Wells_CompletionId",
                table: "Wells");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Clusters_UnitId",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "Basin",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "Block",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "ClusterName",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CodInstallation",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CompanyCodOperator",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CompletionId",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CompletionName",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CurrentSituation",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "DrillingFinishDate",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "DrillingStartDate",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "EnviromentProduction",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "FieldCod",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "InstallationName",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "MD",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "ProductionByReservoir",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "RegisterNum",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "ReservoirName",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "SounderDepth",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "TVD",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "COD_CADASTRO_POCO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_INSTALACAO_040",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_INSTALACAO_041",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_INSTALACAO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_INSTALACAO_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_TAG_PONTO_MEDICAO_040",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_TAG_PONTO_MEDICAO_041",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_TAG_PONTO_MEDICAO_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_TAG_PONTO_MEDICAO_GAS_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "COD_TAG_PONTO_MEDICAO_OLEO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DHA_APLICACAO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DHA_ATUALIZACAO_041",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DHA_MEDICAO_040",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DHA_MEDICAO_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DHA_TESTE_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "IND_NAVIO_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "IND_TIPO_TESTE_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "IND_USER_CALCULO_040",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "IND_VALIDO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_CAPACIDADE_BRUTA_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_CAPACIDADE_CORRIGIDA_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_CAPACIDADE_LIQUIDA_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_POTENCIAL_AGUA_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_POTENCIAL_GAS_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_POTENCIAL_OLEO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_VAZAO_AGUA_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_VAZAO_GAS_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MED_VAZAO_OLEO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NOM_BOLETIM_ANALISE_040",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NOM_BOLETIM_ANALISE_041",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NOM_RELATORIO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NOM_RELATORIO_BSW_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NOM_RELATORIO_BSW_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NOM_RELATORIO_FATOR_ENCLO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NOM_RELATORIO_RZO_SOLUBILIDADE_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NUM_FATOR_ENCOLHIMENTO_041",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NUM_FATOR_ENCOLHIMENTO_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "NUM_SERIE_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PCT_BSW_040",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PCT_BSW_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PCT_MAXIMO_BSW_040",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PCT_RAZAO_SOLUBILIDADE_042",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "TAG_TANQUE_045",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Acronym",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "BeginningValidity",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "FieldService",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "GasProcessing",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "InclusionDate",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "OilProcessing",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Operator",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "PsmQty",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Situation",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "WaterDepth",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "APIGradeOil",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Acronym",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CalorificPowerGas",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Commerciality",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ContractNum",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ContractOperator",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ContractTypeDescription",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CorrectedArea",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "DiscoveryDate",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "EnviromentDepth",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "MainFluid",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Original",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "PreSaltWells",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ProductionBeginning",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ProductionFinishDate",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ProductionFinishForecast",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "QtdWells",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "RoundDescription",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Situation",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "WaterDepth",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Clusters");

            migrationBuilder.AlterColumn<string>(
                name: "WellOperatorName",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<double>(
                name: "WaterDepth",
                table: "Wells",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<double>(
                name: "TopOfPerforated",
                table: "Wells",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "NameAnp",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CoordY",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CoordX",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<double>(
                name: "BaseOfPerforated",
                table: "Wells",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ArtificialElevation",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "CategoryAnp",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CategoryOperator",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CategoryReclassificationAnp",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodWellAnp",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DatumHorizontal",
                table: "Wells",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Wells",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Wells",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LatitudeBase4C",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LatitudeBaseDD",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LongitudeBase4C",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LongitudeBaseDD",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeBaseCoordinate",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Volumes_039",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Volumes_039",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Reservoirs",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AddColumn<string>(
                name: "CodReservoir",
                table: "Reservoirs",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Reservoirs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reservoirs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Measurements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Installations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Installations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "FileTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Fields",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Fields",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodCompletion",
                table: "Completions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompletionId",
                table: "Completions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Completions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Completions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WellId",
                table: "Completions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clusters",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AddColumn<string>(
                name: "CodCluster",
                table: "Clusters",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Clusters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Clusters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Calibrations_039",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Calibrations_039",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BSWS_039",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BSWS_039",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Completions_WellId",
                table: "Completions",
                column: "WellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_Users_UserId",
                table: "Clusters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Completions_Wells_WellId",
                table: "Completions",
                column: "WellId",
                principalTable: "Wells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Users_UserId",
                table: "Installations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_FileTypes_FileTypeId",
                table: "Measurements",
                column: "FileTypeId",
                principalTable: "FileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Users_UserId",
                table: "Reservoirs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_Users_UserId",
                table: "Clusters");

            migrationBuilder.DropForeignKey(
                name: "FK_Completions_Wells_WellId",
                table: "Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_Users_UserId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_FileTypes_FileTypeId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservoirs_Users_UserId",
                table: "Reservoirs");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Completions_WellId",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "CategoryAnp",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CategoryOperator",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CategoryReclassificationAnp",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "CodWellAnp",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "DatumHorizontal",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "LatitudeBase4C",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "LatitudeBaseDD",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "LongitudeBase4C",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "LongitudeBaseDD",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "TypeBaseCoordinate",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Volumes_039");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Volumes_039");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CodReservoir",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reservoirs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FileTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CodCompletion",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "CompletionId",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "WellId",
                table: "Completions");

            migrationBuilder.DropColumn(
                name: "CodCluster",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Calibrations_039");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Calibrations_039");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BSWS_039");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BSWS_039");

            migrationBuilder.AlterColumn<string>(
                name: "WellOperatorName",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<decimal>(
                name: "WaterDepth",
                table: "Wells",
                type: "DECIMAL(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "TopOfPerforated",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "NameAnp",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "CoordY",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "CoordX",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "BaseOfPerforated",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "ArtificialElevation",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "Basin",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Block",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClusterName",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodInstallation",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyCodOperator",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Wells",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CompletionId",
                table: "Wells",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CompletionName",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentSituation",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DrillingFinishDate",
                table: "Wells",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DrillingStartDate",
                table: "Wells",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EnviromentProduction",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FieldCod",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstallationName",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Wells",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MD",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductionByReservoir",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegisterNum",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReservoirName",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SounderDepth",
                table: "Wells",
                type: "DECIMAL(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TVD",
                table: "Wells",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Reservoirs",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<string>(
                name: "COD_CADASTRO_POCO_042",
                table: "Measurements",
                type: "varchar(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_INSTALACAO_040",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_INSTALACAO_041",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_INSTALACAO_042",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_INSTALACAO_045",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_040",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_041",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_045",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_GAS_042",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COD_TAG_PONTO_MEDICAO_OLEO_042",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DHA_APLICACAO_042",
                table: "Measurements",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DHA_ATUALIZACAO_041",
                table: "Measurements",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DHA_MEDICAO_040",
                table: "Measurements",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DHA_MEDICAO_045",
                table: "Measurements",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DHA_TESTE_042",
                table: "Measurements",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IND_NAVIO_045",
                table: "Measurements",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IND_TIPO_TESTE_042",
                table: "Measurements",
                type: "char(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IND_USER_CALCULO_040",
                table: "Measurements",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IND_VALIDO_042",
                table: "Measurements",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_CAPACIDADE_BRUTA_045",
                table: "Measurements",
                type: "float(7)",
                precision: 7,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_CAPACIDADE_CORRIGIDA_045",
                table: "Measurements",
                type: "float(7)",
                precision: 7,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_CAPACIDADE_LIQUIDA_045",
                table: "Measurements",
                type: "float(7)",
                precision: 7,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_POTENCIAL_AGUA_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_POTENCIAL_GAS_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_POTENCIAL_OLEO_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_VAZAO_AGUA_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_VAZAO_GAS_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MED_VAZAO_OLEO_042",
                table: "Measurements",
                type: "float(8)",
                precision: 8,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_BOLETIM_ANALISE_040",
                table: "Measurements",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_BOLETIM_ANALISE_041",
                table: "Measurements",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_RELATORIO_042",
                table: "Measurements",
                type: "varchar",
                precision: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_RELATORIO_BSW_042",
                table: "Measurements",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_RELATORIO_BSW_045",
                table: "Measurements",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_RELATORIO_FATOR_ENCLO_042",
                table: "Measurements",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_RELATORIO_RZO_SOLUBILIDADE_042",
                table: "Measurements",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NUM_FATOR_ENCOLHIMENTO_041",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NUM_FATOR_ENCOLHIMENTO_042",
                table: "Measurements",
                type: "float(6)",
                precision: 6,
                scale: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NUM_SERIE_045",
                table: "Measurements",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PCT_BSW_040",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PCT_BSW_042",
                table: "Measurements",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PCT_MAXIMO_BSW_040",
                table: "Measurements",
                type: "float(3)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PCT_RAZAO_SOLUBILIDADE_042",
                table: "Measurements",
                type: "float(5)",
                precision: 5,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TAG_TANQUE_045",
                table: "Measurements",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Acronym",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginningValidity",
                table: "Installations",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FieldService",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "GasProcessing",
                table: "Installations",
                type: "DECIMAL(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "InclusionDate",
                table: "Installations",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Installations",
                type: "VARCHAR(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Installations",
                type: "VARCHAR(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OilProcessing",
                table: "Installations",
                type: "DECIMAL(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PsmQty",
                table: "Installations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Situation",
                table: "Installations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Installations",
                type: "VARCHAR(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Installations",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "WaterDepth",
                table: "Installations",
                type: "DECIMAL(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "APIGradeOil",
                table: "Fields",
                type: "DECIMAL(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Acronym",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CalorificPowerGas",
                table: "Fields",
                type: "DECIMAL(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Commerciality",
                table: "Fields",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContractNum",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractOperator",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractTypeDescription",
                table: "Fields",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CorrectedArea",
                table: "Fields",
                type: "DECIMAL(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DiscoveryDate",
                table: "Fields",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EnviromentDepth",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainFluid",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Original",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PreSaltWells",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionBeginning",
                table: "Fields",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionFinishDate",
                table: "Fields",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionFinishForecast",
                table: "Fields",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QtdWells",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Round",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoundDescription",
                table: "Fields",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Situation",
                table: "Fields",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "WaterDepth",
                table: "Fields",
                type: "DECIMAL(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clusters",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "Clusters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_MAXIMO_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "DHA_PCT_BSW_039",
                table: "BSWS_039",
                type: "float(3)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wells_CompletionId",
                table: "Wells",
                column: "CompletionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_UnitId",
                table: "Clusters",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UserId",
                table: "Units",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_Units_UnitId",
                table: "Clusters",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_Users_UserId",
                table: "Clusters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_UserId",
                table: "Fields",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_Users_UserId",
                table: "Installations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_FileTypes_FileTypeId",
                table: "Measurements",
                column: "FileTypeId",
                principalTable: "FileTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservoirs_Users_UserId",
                table: "Reservoirs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wells_Completions_CompletionId",
                table: "Wells",
                column: "CompletionId",
                principalTable: "Completions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
