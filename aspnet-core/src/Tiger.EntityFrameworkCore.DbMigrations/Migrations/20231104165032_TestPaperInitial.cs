using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TestPaperInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sorting",
                table: "AppCourses",
                nullable: false,
                comment: "顺序",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Enable",
                table: "AppCourses",
                nullable: false,
                comment: "启用",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Sorting",
                table: "AppClasses",
                nullable: false,
                comment: "顺序",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "AppClasses",
                nullable: false,
                comment: "顺序",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "AppTestPapers",
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
                    TestPaperMainId = table.Column<Guid>(nullable: true),
                    Number = table.Column<string>(maxLength: 128, nullable: false, comment: "编号"),
                    Name = table.Column<string>(maxLength: 128, nullable: false, comment: "名称"),
                    Type = table.Column<int>(nullable: false, comment: "类型 1.固定题目（手动或自动选题） 2.随机题目（根据策略每个学员的题目都不同） 3.固定题目打乱显示"),
                    IsComposing = table.Column<bool>(nullable: false, comment: "是否已组卷"),
                    Enable = table.Column<bool>(nullable: false, comment: "启用"),
                    IsIncludeAllSchoolTeachers = table.Column<bool>(nullable: false, comment: "是否包含全校老师"),
                    IsLimitJudgeTime = table.Column<bool>(nullable: false, comment: "是否限制评卷时间"),
                    JudgeStartTime = table.Column<DateTime>(nullable: true, comment: "评卷开始时间"),
                    JudgeEndTime = table.Column<DateTime>(nullable: true, comment: "评卷结束时间"),
                    CourseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestPapers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTestPapers_AppCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "AppCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbpTestPaperJudgeSchool",
                columns: table => new
                {
                    TestPaperId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTestPaperJudgeSchool", x => new { x.TestPaperId, x.SchoolId });
                    table.ForeignKey(
                        name: "FK_AbpTestPaperJudgeSchool_AppTestPapers_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpTestPaperJudgeSchool_AppSchools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "AppSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                        column: x => x.TestPaperId,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpTestPaperJudgeSchool_SchoolId",
                table: "AbpTestPaperJudgeSchool",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTestPaperJudgeSchool_TestPaperId_SchoolId",
                table: "AbpTestPaperJudgeSchool",
                columns: new[] { "TestPaperId", "SchoolId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPapers_CourseId",
                table: "AppTestPapers",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpTestPaperJudgeSchool");

            migrationBuilder.DropTable(
                name: "AppTestPapers");

            migrationBuilder.AlterColumn<int>(
                name: "Sorting",
                table: "AppCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "顺序");

            migrationBuilder.AlterColumn<bool>(
                name: "Enable",
                table: "AppCourses",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "启用");

            migrationBuilder.AlterColumn<int>(
                name: "Sorting",
                table: "AppClasses",
                type: "int",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(int),
                oldComment: "顺序");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "AppClasses",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "顺序");
        }
    }
}
