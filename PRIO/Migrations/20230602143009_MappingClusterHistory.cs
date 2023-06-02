using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class MappingClusterHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClustersHistories_Clusters_ClusterId",
                table: "ClustersHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ClustersHistories_Users_UserId",
                table: "ClustersHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_InstallationsHistories_InstallationHistoryId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationsHistories_Clusters_ClusterId",
                table: "InstallationsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationsHistories_Clusters_ClusterOldId",
                table: "InstallationsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationsHistories_Installations_InstallationId",
                table: "InstallationsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationsHistories_Users_UserId",
                table: "InstallationsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_InstallationsHistories_InstallationHistoryId",
                table: "MeasuringEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallationsHistories",
                table: "InstallationsHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClustersHistories",
                table: "ClustersHistories");

            migrationBuilder.RenameTable(
                name: "InstallationsHistories",
                newName: "InstallationHistories");

            migrationBuilder.RenameTable(
                name: "ClustersHistories",
                newName: "ClusterHistories");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationsHistories_UserId",
                table: "InstallationHistories",
                newName: "IX_InstallationHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationsHistories_InstallationId",
                table: "InstallationHistories",
                newName: "IX_InstallationHistories_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationsHistories_ClusterOldId",
                table: "InstallationHistories",
                newName: "IX_InstallationHistories_ClusterOldId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationsHistories_ClusterId",
                table: "InstallationHistories",
                newName: "IX_InstallationHistories_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_ClustersHistories_UserId",
                table: "ClusterHistories",
                newName: "IX_ClusterHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClustersHistories_ClusterId",
                table: "ClusterHistories",
                newName: "IX_ClusterHistories_ClusterId");

            migrationBuilder.AlterColumn<string>(
                name: "NameOld",
                table: "InstallationHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InstallationHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionOld",
                table: "InstallationHistories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InstallationHistories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodInstallationOld",
                table: "InstallationHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodInstallation",
                table: "InstallationHistories",
                type: "VARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "InstallationHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ClusterHistories",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameOld",
                table: "ClusterHistories",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClusterHistories",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionOld",
                table: "ClusterHistories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ClusterHistories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodClusterOld",
                table: "ClusterHistories",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodCluster",
                table: "ClusterHistories",
                type: "VARCHAR(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallationHistories",
                table: "InstallationHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClusterHistories",
                table: "ClusterHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterHistories_Clusters_ClusterId",
                table: "ClusterHistories",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterHistories_Users_UserId",
                table: "ClusterHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_InstallationHistories_InstallationHistoryId",
                table: "Fields",
                column: "InstallationHistoryId",
                principalTable: "InstallationHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationHistories_Clusters_ClusterId",
                table: "InstallationHistories",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationHistories_Clusters_ClusterOldId",
                table: "InstallationHistories",
                column: "ClusterOldId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationHistories_Installations_InstallationId",
                table: "InstallationHistories",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationHistories_Users_UserId",
                table: "InstallationHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_InstallationHistories_InstallationHistoryId",
                table: "MeasuringEquipments",
                column: "InstallationHistoryId",
                principalTable: "InstallationHistories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterHistories_Clusters_ClusterId",
                table: "ClusterHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterHistories_Users_UserId",
                table: "ClusterHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_InstallationHistories_InstallationHistoryId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationHistories_Clusters_ClusterId",
                table: "InstallationHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationHistories_Clusters_ClusterOldId",
                table: "InstallationHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationHistories_Installations_InstallationId",
                table: "InstallationHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallationHistories_Users_UserId",
                table: "InstallationHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_InstallationHistories_InstallationHistoryId",
                table: "MeasuringEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallationHistories",
                table: "InstallationHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClusterHistories",
                table: "ClusterHistories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "InstallationHistories");

            migrationBuilder.RenameTable(
                name: "InstallationHistories",
                newName: "InstallationsHistories");

            migrationBuilder.RenameTable(
                name: "ClusterHistories",
                newName: "ClustersHistories");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationHistories_UserId",
                table: "InstallationsHistories",
                newName: "IX_InstallationsHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationHistories_InstallationId",
                table: "InstallationsHistories",
                newName: "IX_InstallationsHistories_InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationHistories_ClusterOldId",
                table: "InstallationsHistories",
                newName: "IX_InstallationsHistories_ClusterOldId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationHistories_ClusterId",
                table: "InstallationsHistories",
                newName: "IX_InstallationsHistories_ClusterId");

            migrationBuilder.RenameIndex(
                name: "IX_ClusterHistories_UserId",
                table: "ClustersHistories",
                newName: "IX_ClustersHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClusterHistories_ClusterId",
                table: "ClustersHistories",
                newName: "IX_ClustersHistories_ClusterId");

            migrationBuilder.AlterColumn<string>(
                name: "NameOld",
                table: "InstallationsHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InstallationsHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionOld",
                table: "InstallationsHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InstallationsHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodInstallationOld",
                table: "InstallationsHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodInstallation",
                table: "InstallationsHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameOld",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionOld",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodClusterOld",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodCluster",
                table: "ClustersHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallationsHistories",
                table: "InstallationsHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClustersHistories",
                table: "ClustersHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClustersHistories_Clusters_ClusterId",
                table: "ClustersHistories",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClustersHistories_Users_UserId",
                table: "ClustersHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_InstallationsHistories_InstallationHistoryId",
                table: "Fields",
                column: "InstallationHistoryId",
                principalTable: "InstallationsHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationsHistories_Clusters_ClusterId",
                table: "InstallationsHistories",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationsHistories_Clusters_ClusterOldId",
                table: "InstallationsHistories",
                column: "ClusterOldId",
                principalTable: "Clusters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationsHistories_Installations_InstallationId",
                table: "InstallationsHistories",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationsHistories_Users_UserId",
                table: "InstallationsHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_InstallationsHistories_InstallationHistoryId",
                table: "MeasuringEquipments",
                column: "InstallationHistoryId",
                principalTable: "InstallationsHistories",
                principalColumn: "Id");
        }
    }
}
