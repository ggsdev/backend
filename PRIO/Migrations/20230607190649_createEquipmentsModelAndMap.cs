using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class createEquipmentsModelAndMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MeasuringEquipments",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "TagMeasuringPoint",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "TagEquipment",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<string>(
                name: "ChannelNumber",
                table: "MeasuringEquipments",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationProtocol",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasSeal",
                table: "MeasuringEquipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InOperation",
                table: "MeasuringEquipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MVS",
                table: "MeasuringEquipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerieNumber",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeEquipment",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypePoint",
                table: "MeasuringEquipments",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelNumber",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "CommunicationProtocol",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "HasSeal",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "InOperation",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "MVS",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "SerieNumber",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "TypeEquipment",
                table: "MeasuringEquipments");

            migrationBuilder.DropColumn(
                name: "TypePoint",
                table: "MeasuringEquipments");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MeasuringEquipments",
                type: "varchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "TagMeasuringPoint",
                table: "MeasuringEquipments",
                type: "varchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "TagEquipment",
                table: "MeasuringEquipments",
                type: "varchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60);
        }
    }
}
