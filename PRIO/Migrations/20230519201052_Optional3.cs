using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class Optional3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_5_001",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_4_003",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_4_001",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_3_003",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_3_001",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_2_003",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_2_001",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_1_003",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IND_HABILITACAO_ALARME_1_001",
                table: "Measurements",
                type: "varchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_5_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_4_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_4_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_3_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_3_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_2_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_003",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IND_HABILITACAO_ALARME_1_001",
                table: "Measurements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldNullable: true);
        }
    }
}
