using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TextTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    CultureName = table.Column<string>(maxLength: 128, nullable: false, comment: "语言名称"),
                    UiCultureName = table.Column<string>(maxLength: 128, nullable: false, comment: "Ui语言名称"),
                    DisplayName = table.Column<string>(maxLength: 128, nullable: false, comment: "显示名称"),
                    FlagIcon = table.Column<string>(maxLength: 128, nullable: true, comment: "图标"),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLanguageTexts",
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
                    CultrueName = table.Column<string>(maxLength: 128, nullable: false, comment: "语言名称"),
                    Key = table.Column<string>(maxLength: 256, nullable: false, comment: "键名称"),
                    DefaultValue = table.Column<string>(nullable: true),
                    Value = table.Column<string>(maxLength: 256, nullable: false, comment: "值"),
                    ResourceName = table.Column<string>(maxLength: 128, nullable: false, comment: "资源名称"),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLanguageTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPosts",
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
                    Remark = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    Name = table.Column<bool>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppTextTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false, comment: "名称"),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: true, comment: "显示名称"),
                    Content = table.Column<string>(maxLength: 4096, nullable: true, comment: "模板内容"),
                    Culture = table.Column<string>(maxLength: 256, nullable: true, comment: "文化名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTextTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppLanguages_CultureName",
                table: "AppLanguages",
                column: "CultureName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLanguageTexts_TenantId_ResourceName_CultrueName",
                table: "AppLanguageTexts",
                columns: new[] { "TenantId", "ResourceName", "CultrueName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLanguages");

            migrationBuilder.DropTable(
                name: "AppLanguageTexts");

            migrationBuilder.DropTable(
                name: "AppPosts");

            migrationBuilder.DropTable(
                name: "AppResources");

            migrationBuilder.DropTable(
                name: "AppTextTemplates");
        }
    }
}
