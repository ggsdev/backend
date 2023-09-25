using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class designBDAC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BTPBases64_Users_UserId",
                table: "BTPBases64");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsInProduction_Users_CommentedById",
                table: "CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_Users_CreatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_Users_UpdatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOperations_GlobalOperations_GlobalOperationId",
                table: "GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOperations_GroupPermissions_GroupPermissionId",
                table: "GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Menus_MenuId",
                table: "GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Clusters_Users_UserId",
                table: "Hierachy.Clusters");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Completions_Users_UserId",
                table: "Hierachy.Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Fields_Users_UserId",
                table: "Hierachy.Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Installations_Users_UserId",
                table: "Hierachy.Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Reservoirs_Users_UserId",
                table: "Hierachy.Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Wells_Users_UserId",
                table: "Hierachy.Wells");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Zones_Users_UserId",
                table: "Hierachy.Zones");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Users_UserId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementsHistories_Users_ImportedById",
                table: "MeasurementsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_Users_UserId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMImportHistories_Users_ImportedById",
                table: "NFSMImportHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Users_CalculatedImportedById",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperations_GlobalOperations_GlobalOperationId",
                table: "UserOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_GroupPermissions_GroupMenuId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Users_CreatedById",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_Users_UpdatedById",
                table: "WellEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOperations",
                table: "UserOperations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupOperations",
                table: "GroupOperations");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AC.Users");

            migrationBuilder.RenameTable(
                name: "UserPermissions",
                newName: "AC.UserPermissions");

            migrationBuilder.RenameTable(
                name: "UserOperations",
                newName: "AC.UserOperations");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "AC.Menus");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "AC.Groups");

            migrationBuilder.RenameTable(
                name: "GroupPermissions",
                newName: "AC.GroupPermissions");

            migrationBuilder.RenameTable(
                name: "GroupOperations",
                newName: "AC.GroupOperations");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "AC.Users",
                newName: "IX_AC.Users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_GroupId",
                table: "AC.Users",
                newName: "IX_AC.Users_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "AC.Users",
                newName: "IX_AC.Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UserId",
                table: "AC.UserPermissions",
                newName: "IX_AC.UserPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_GroupMenuId",
                table: "AC.UserPermissions",
                newName: "IX_AC.UserPermissions_GroupMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOperations_UserPermissionId",
                table: "AC.UserOperations",
                newName: "IX_AC.UserOperations_UserPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOperations_GlobalOperationId",
                table: "AC.UserOperations",
                newName: "IX_AC.UserOperations_GlobalOperationId");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_ParentId",
                table: "AC.Menus",
                newName: "IX_AC.Menus_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermissions_MenuId",
                table: "AC.GroupPermissions",
                newName: "IX_AC.GroupPermissions_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPermissions_GroupId",
                table: "AC.GroupPermissions",
                newName: "IX_AC.GroupPermissions_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupOperations_GroupPermissionId",
                table: "AC.GroupOperations",
                newName: "IX_AC.GroupOperations_GroupPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupOperations_GlobalOperationId",
                table: "AC.GroupOperations",
                newName: "IX_AC.GroupOperations_GlobalOperationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.Users",
                table: "AC.Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.UserPermissions",
                table: "AC.UserPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.UserOperations",
                table: "AC.UserOperations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.Menus",
                table: "AC.Menus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.Groups",
                table: "AC.Groups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.GroupPermissions",
                table: "AC.GroupPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AC.GroupOperations",
                table: "AC.GroupOperations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.GroupOperations_AC.GroupPermissions_GroupPermissionId",
                table: "AC.GroupOperations",
                column: "GroupPermissionId",
                principalTable: "AC.GroupPermissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.GroupOperations_GlobalOperations_GlobalOperationId",
                table: "AC.GroupOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.GroupPermissions_AC.Groups_GroupId",
                table: "AC.GroupPermissions",
                column: "GroupId",
                principalTable: "AC.Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.GroupPermissions_AC.Menus_MenuId",
                table: "AC.GroupPermissions",
                column: "MenuId",
                principalTable: "AC.Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.Menus_AC.Menus_ParentId",
                table: "AC.Menus",
                column: "ParentId",
                principalTable: "AC.Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.UserOperations_AC.UserPermissions_UserPermissionId",
                table: "AC.UserOperations",
                column: "UserPermissionId",
                principalTable: "AC.UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AC.UserOperations_GlobalOperations_GlobalOperationId",
                table: "AC.UserOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.UserPermissions_AC.GroupPermissions_GroupMenuId",
                table: "AC.UserPermissions",
                column: "GroupMenuId",
                principalTable: "AC.GroupPermissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.UserPermissions_AC.Users_UserId",
                table: "AC.UserPermissions",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AC.Users_AC.Groups_GroupId",
                table: "AC.Users",
                column: "GroupId",
                principalTable: "AC.Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BTPBases64_AC.Users_UserId",
                table: "BTPBases64",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsInProduction_AC.Users_CommentedById",
                table: "CommentsInProduction",
                column: "CommentedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_AC.Users_CreatedById",
                table: "EventReasons",
                column: "CreatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_AC.Users_UpdatedById",
                table: "EventReasons",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Clusters_AC.Users_UserId",
                table: "Hierachy.Clusters",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Completions_AC.Users_UserId",
                table: "Hierachy.Completions",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Fields_AC.Users_UserId",
                table: "Hierachy.Fields",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Installations_AC.Users_UserId",
                table: "Hierachy.Installations",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Reservoirs_AC.Users_UserId",
                table: "Hierachy.Reservoirs",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Wells_AC.Users_UserId",
                table: "Hierachy.Wells",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Zones_AC.Users_UserId",
                table: "Hierachy.Zones",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_AC.Users_UserId",
                table: "Measurements",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementsHistories_AC.Users_ImportedById",
                table: "MeasurementsHistories",
                column: "ImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_AC.Users_UserId",
                table: "MeasuringEquipments",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMImportHistories_AC.Users_ImportedById",
                table: "NFSMImportHistories",
                column: "ImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_AC.Users_CalculatedImportedById",
                table: "Productions",
                column: "CalculatedImportedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AC.Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "AC.Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_AC.Users_CreatedById",
                table: "WellEvents",
                column: "CreatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_AC.Users_UpdatedById",
                table: "WellEvents",
                column: "UpdatedById",
                principalTable: "AC.Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AC.GroupOperations_AC.GroupPermissions_GroupPermissionId",
                table: "AC.GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.GroupOperations_GlobalOperations_GlobalOperationId",
                table: "AC.GroupOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.GroupPermissions_AC.Groups_GroupId",
                table: "AC.GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.GroupPermissions_AC.Menus_MenuId",
                table: "AC.GroupPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.Menus_AC.Menus_ParentId",
                table: "AC.Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.UserOperations_AC.UserPermissions_UserPermissionId",
                table: "AC.UserOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.UserOperations_GlobalOperations_GlobalOperationId",
                table: "AC.UserOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.UserPermissions_AC.GroupPermissions_GroupMenuId",
                table: "AC.UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.UserPermissions_AC.Users_UserId",
                table: "AC.UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AC.Users_AC.Groups_GroupId",
                table: "AC.Users");

            migrationBuilder.DropForeignKey(
                name: "FK_BTPBases64_AC.Users_UserId",
                table: "BTPBases64");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsInProduction_AC.Users_CommentedById",
                table: "CommentsInProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_AC.Users_CreatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReasons_AC.Users_UpdatedById",
                table: "EventReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Clusters_AC.Users_UserId",
                table: "Hierachy.Clusters");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Completions_AC.Users_UserId",
                table: "Hierachy.Completions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Fields_AC.Users_UserId",
                table: "Hierachy.Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Installations_AC.Users_UserId",
                table: "Hierachy.Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Reservoirs_AC.Users_UserId",
                table: "Hierachy.Reservoirs");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Wells_AC.Users_UserId",
                table: "Hierachy.Wells");

            migrationBuilder.DropForeignKey(
                name: "FK_Hierachy.Zones_AC.Users_UserId",
                table: "Hierachy.Zones");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_AC.Users_UserId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementsHistories_AC.Users_ImportedById",
                table: "MeasurementsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringEquipments_AC.Users_UserId",
                table: "MeasuringEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_NFSMImportHistories_AC.Users_ImportedById",
                table: "NFSMImportHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_AC.Users_CalculatedImportedById",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AC.Users_UserId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_AC.Users_CreatedById",
                table: "WellEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WellEvents_AC.Users_UpdatedById",
                table: "WellEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.Users",
                table: "AC.Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.UserPermissions",
                table: "AC.UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.UserOperations",
                table: "AC.UserOperations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.Menus",
                table: "AC.Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.Groups",
                table: "AC.Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.GroupPermissions",
                table: "AC.GroupPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AC.GroupOperations",
                table: "AC.GroupOperations");

            migrationBuilder.RenameTable(
                name: "AC.Users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "AC.UserPermissions",
                newName: "UserPermissions");

            migrationBuilder.RenameTable(
                name: "AC.UserOperations",
                newName: "UserOperations");

            migrationBuilder.RenameTable(
                name: "AC.Menus",
                newName: "Menus");

            migrationBuilder.RenameTable(
                name: "AC.Groups",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "AC.GroupPermissions",
                newName: "GroupPermissions");

            migrationBuilder.RenameTable(
                name: "AC.GroupOperations",
                newName: "GroupOperations");

            migrationBuilder.RenameIndex(
                name: "IX_AC.Users_Username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_AC.Users_GroupId",
                table: "Users",
                newName: "IX_Users_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.Users_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_AC.UserPermissions_UserId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.UserPermissions_GroupMenuId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_GroupMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.UserOperations_UserPermissionId",
                table: "UserOperations",
                newName: "IX_UserOperations_UserPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.UserOperations_GlobalOperationId",
                table: "UserOperations",
                newName: "IX_UserOperations_GlobalOperationId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.Menus_ParentId",
                table: "Menus",
                newName: "IX_Menus_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.GroupPermissions_MenuId",
                table: "GroupPermissions",
                newName: "IX_GroupPermissions_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.GroupPermissions_GroupId",
                table: "GroupPermissions",
                newName: "IX_GroupPermissions_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.GroupOperations_GroupPermissionId",
                table: "GroupOperations",
                newName: "IX_GroupOperations_GroupPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_AC.GroupOperations_GlobalOperationId",
                table: "GroupOperations",
                newName: "IX_GroupOperations_GlobalOperationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOperations",
                table: "UserOperations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupOperations",
                table: "GroupOperations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BTPBases64_Users_UserId",
                table: "BTPBases64",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsInProduction_Users_CommentedById",
                table: "CommentsInProduction",
                column: "CommentedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_Users_CreatedById",
                table: "EventReasons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReasons_Users_UpdatedById",
                table: "EventReasons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOperations_GlobalOperations_GlobalOperationId",
                table: "GroupOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOperations_GroupPermissions_GroupPermissionId",
                table: "GroupOperations",
                column: "GroupPermissionId",
                principalTable: "GroupPermissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Groups_GroupId",
                table: "GroupPermissions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Menus_MenuId",
                table: "GroupPermissions",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Clusters_Users_UserId",
                table: "Hierachy.Clusters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Completions_Users_UserId",
                table: "Hierachy.Completions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Fields_Users_UserId",
                table: "Hierachy.Fields",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Installations_Users_UserId",
                table: "Hierachy.Installations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Reservoirs_Users_UserId",
                table: "Hierachy.Reservoirs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Wells_Users_UserId",
                table: "Hierachy.Wells",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hierachy.Zones_Users_UserId",
                table: "Hierachy.Zones",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Users_UserId",
                table: "Measurements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementsHistories_Users_ImportedById",
                table: "MeasurementsHistories",
                column: "ImportedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringEquipments_Users_UserId",
                table: "MeasuringEquipments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus",
                column: "ParentId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NFSMImportHistories_Users_ImportedById",
                table: "NFSMImportHistories",
                column: "ImportedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Users_CalculatedImportedById",
                table: "Productions",
                column: "CalculatedImportedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperations_GlobalOperations_GlobalOperationId",
                table: "UserOperations",
                column: "GlobalOperationId",
                principalTable: "GlobalOperations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperations_UserPermissions_UserPermissionId",
                table: "UserOperations",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_GroupPermissions_GroupMenuId",
                table: "UserPermissions",
                column: "GroupMenuId",
                principalTable: "GroupPermissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Users_CreatedById",
                table: "WellEvents",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WellEvents_Users_UpdatedById",
                table: "WellEvents",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
