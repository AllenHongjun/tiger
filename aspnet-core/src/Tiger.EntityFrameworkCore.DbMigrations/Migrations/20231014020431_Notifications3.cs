using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Notifications3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscribes",
                table: "UserSubscribes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNotifications",
                table: "UserNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationDefinitions",
                table: "NotificationDefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationDefinitionGroups",
                table: "NotificationDefinitionGroups");

            migrationBuilder.RenameTable(
                name: "UserSubscribes",
                newName: "AppUserSubscribes");

            migrationBuilder.RenameTable(
                name: "UserNotifications",
                newName: "AppUserNotifications");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "AppNotifications");

            migrationBuilder.RenameTable(
                name: "NotificationDefinitions",
                newName: "AppNotificationDefinitions");

            migrationBuilder.RenameTable(
                name: "NotificationDefinitionGroups",
                newName: "AppNotificationDefinitionGroups");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_TenantId_NotificationName",
                table: "AppNotifications",
                newName: "IX_AppNotifications_TenantId_NotificationName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserSubscribes",
                table: "AppUserSubscribes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserNotifications",
                table: "AppUserNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppNotifications",
                table: "AppNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppNotificationDefinitions",
                table: "AppNotificationDefinitions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppNotificationDefinitionGroups",
                table: "AppNotificationDefinitionGroups",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserSubscribes",
                table: "AppUserSubscribes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserNotifications",
                table: "AppUserNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppNotifications",
                table: "AppNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppNotificationDefinitions",
                table: "AppNotificationDefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppNotificationDefinitionGroups",
                table: "AppNotificationDefinitionGroups");

            migrationBuilder.RenameTable(
                name: "AppUserSubscribes",
                newName: "UserSubscribes");

            migrationBuilder.RenameTable(
                name: "AppUserNotifications",
                newName: "UserNotifications");

            migrationBuilder.RenameTable(
                name: "AppNotifications",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "AppNotificationDefinitions",
                newName: "NotificationDefinitions");

            migrationBuilder.RenameTable(
                name: "AppNotificationDefinitionGroups",
                newName: "NotificationDefinitionGroups");

            migrationBuilder.RenameIndex(
                name: "IX_AppNotifications_TenantId_NotificationName",
                table: "Notifications",
                newName: "IX_Notifications_TenantId_NotificationName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscribes",
                table: "UserSubscribes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNotifications",
                table: "UserNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationDefinitions",
                table: "NotificationDefinitions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationDefinitionGroups",
                table: "NotificationDefinitionGroups",
                column: "Id");
        }
    }
}
