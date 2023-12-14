using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AddTableTestPaperJudge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTestPaperJudges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    TestPaperId = table.Column<int>(nullable: false, comment: "试卷Id"),
                    JudgeId = table.Column<Guid>(nullable: false, comment: "评委Id"),
                    UserName = table.Column<string>(maxLength: 256, nullable: false, comment: "用户名"),
                    FullName = table.Column<string>(maxLength: 64, nullable: true, comment: "姓名"),
                    Email = table.Column<string>(maxLength: 256, nullable: true, comment: "邮箱"),
                    PhoneNumber = table.Column<string>(maxLength: 16, nullable: true, comment: "手机号"),
                    OrganizationUnitId = table.Column<Guid>(nullable: false, comment: "组织Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestPaperJudges", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTestPaperJudges");
        }
    }
}
