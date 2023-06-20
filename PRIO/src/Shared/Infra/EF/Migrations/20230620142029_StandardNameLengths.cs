using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class StandardNameLengths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wells",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "CodWell",
                table: "Wells",
                type: "VARCHAR(8)",
                maxLength: 8,
                nullable: true,
                defaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)",
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodReservoir",
                table: "Reservoirs",
                type: "VARCHAR(8)",
                maxLength: 8,
                nullable: true,
                defaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)",
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Completions",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "CodCompletion",
                table: "Completions",
                type: "VARCHAR(8)",
                maxLength: 8,
                nullable: true,
                defaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)",
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clusters",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "CodCluster",
                table: "Clusters",
                type: "VARCHAR(8)",
                maxLength: 8,
                nullable: true,
                defaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "CodWell",
                table: "Wells",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(8)",
                oldMaxLength: 8,
                oldNullable: true,
                oldDefaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)");

            migrationBuilder.AlterColumn<string>(
                name: "CodReservoir",
                table: "Reservoirs",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(8)",
                oldMaxLength: 8,
                oldNullable: true,
                oldDefaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Completions",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "CodCompletion",
                table: "Completions",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(8)",
                oldMaxLength: 8,
                oldNullable: true,
                oldDefaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clusters",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "CodCluster",
                table: "Clusters",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(8)",
                oldMaxLength: 8,
                oldNullable: true,
                oldDefaultValueSql: "PRIO.Utils.GenerateCode.Generate(Name)");
        }
    }
}
