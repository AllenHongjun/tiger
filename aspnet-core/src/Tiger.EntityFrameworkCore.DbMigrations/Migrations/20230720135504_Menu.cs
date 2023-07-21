using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Path = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Redirect = table.Column<string>(maxLength: 255, nullable: true),
                    Framework = table.Column<string>(maxLength: 64, nullable: false, comment: "框架"),
                    Code = table.Column<string>(maxLength: 23, nullable: false, comment: "菜单编码"),
                    Component = table.Column<string>(maxLength: 256, nullable: false, comment: "组件"),
                    ParentId = table.Column<Guid>(nullable: true),
                    LayoutId = table.Column<Guid>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    MenuId = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 256, nullable: false),
                    Startup = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    MenuId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Startup = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserMenus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleMenus_RoleName_MenuId",
                table: "AppRoleMenus",
                columns: new[] { "RoleName", "MenuId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserMenus_UserId_MenuId",
                table: "AppUserMenus",
                columns: new[] { "UserId", "MenuId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMenus");

            migrationBuilder.DropTable(
                name: "AppRoleMenus");

            migrationBuilder.DropTable(
                name: "AppUserMenus");
        }
    }
}
