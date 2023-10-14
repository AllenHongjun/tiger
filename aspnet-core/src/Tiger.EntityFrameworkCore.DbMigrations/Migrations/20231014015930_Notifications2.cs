using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Notifications2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationDefinitionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    AllowSubscriptionToClients = table.Column<bool>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationDefinitionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    GroupName = table.Column<string>(maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    NotificationLifetime = table.Column<int>(nullable: false),
                    NotificationType = table.Column<int>(nullable: false),
                    ContentType = table.Column<int>(nullable: false, defaultValue: 0),
                    Providers = table.Column<string>(maxLength: 200, nullable: true),
                    AllowSubscriptionToClients = table.Column<bool>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(nullable: true),
                    Severity = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ContentType = table.Column<int>(nullable: false, defaultValue: 0),
                    NotificationId = table.Column<long>(nullable: false),
                    NotificationName = table.Column<string>(maxLength: 100, nullable: false, comment: "通知名称"),
                    NotificationTypeName = table.Column<string>(maxLength: 512, nullable: false),
                    ExpirationTime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    NotificationId = table.Column<long>(nullable: false),
                    ReadStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscribes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    NotificationName = table.Column<string>(maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 128, nullable: false, defaultValue: "/")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscribes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TenantId_NotificationName",
                table: "Notifications",
                columns: new[] { "TenantId", "NotificationName" });

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_User_Notification_Id",
                table: "UserNotifications",
                columns: new[] { "TenantId", "UserId", "NotificationId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_User_Notification_Name",
                table: "UserSubscribes",
                columns: new[] { "TenantId", "UserId", "NotificationName" },
                unique: true,
                filter: "[TenantId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationDefinitionGroups");

            migrationBuilder.DropTable(
                name: "NotificationDefinitions");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "UserSubscribes");
        }
    }
}
