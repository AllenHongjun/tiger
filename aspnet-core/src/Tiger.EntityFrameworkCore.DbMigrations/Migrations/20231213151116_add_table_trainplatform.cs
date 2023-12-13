using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class add_table_trainplatform : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTrainPlatforms",
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
                    Name = table.Column<string>(maxLength: 512, nullable: false, comment: "名称"),
                    Description = table.Column<string>(maxLength: 512, nullable: true, comment: "描述"),
                    Icon = table.Column<string>(maxLength: 1024, nullable: false, comment: "logo图标"),
                    Url = table.Column<string>(maxLength: 1024, nullable: false, comment: "实训跳转链接"),
                    CheckCode = table.Column<string>(maxLength: 64, nullable: false, comment: "token校验码"),
                    TokenType = table.Column<int>(nullable: false, comment: "Token传值方式：0、使用旧版Cookie；1、使用旧版Url Token；2、使用新版Url Token；"),
                    Sorting = table.Column<int>(nullable: false, comment: "序号"),
                    Enable = table.Column<bool>(nullable: false, comment: "是否启用")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTrainPlatforms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTrainPlatforms");
        }
    }
}
