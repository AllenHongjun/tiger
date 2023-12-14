using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AddTableExamModifyTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppExamModifyTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    ExamId = table.Column<Guid>(nullable: false, comment: "考试Id"),
                    DelayTime = table.Column<int>(nullable: false, comment: "考试推迟时间：单位分钟"),
                    ExtendTime = table.Column<int>(nullable: false, comment: "考试延长时间：单位分钟"),
                    OrganizationUnitId = table.Column<Guid>(nullable: false, comment: "组织Id"),
                    ExamineeId = table.Column<Guid>(nullable: false, comment: "考生Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExamModifyTimes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppExamModifyTimes");
        }
    }
}
