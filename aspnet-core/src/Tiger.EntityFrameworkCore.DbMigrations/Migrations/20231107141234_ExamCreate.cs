using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class ExamCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppExams",
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
                    CourseId = table.Column<Guid>(nullable: true, comment: "课程Id"),
                    TestPaperId = table.Column<Guid>(nullable: false, comment: "考试的试卷"),
                    QuestionCategoryId = table.Column<Guid>(nullable: false, comment: "题目分类"),
                    Name = table.Column<string>(maxLength: 128, nullable: false, comment: "考试名称"),
                    CoverUrl = table.Column<string>(maxLength: 1024, nullable: true, comment: "封面"),
                    Number = table.Column<string>(maxLength: 128, nullable: true, comment: "编号"),
                    ExamType = table.Column<int>(nullable: false, comment: "类型：1.考试 2.练习 , 3 比赛"),
                    StartDate = table.Column<DateTime>(nullable: false, comment: "开始时间"),
                    EndDate = table.Column<DateTime>(nullable: false, comment: "结束时间"),
                    ExamDuration = table.Column<int>(nullable: false, comment: "考试时长 单位：分钟"),
                    IsDifferent = table.Column<bool>(nullable: false, comment: "是否每个人都不同"),
                    IsDifferentOrder = table.Column<bool>(nullable: false, comment: "顺序不同"),
                    IsShowScore = table.Column<bool>(nullable: false, comment: "提交后是否显示成绩"),
                    IsShowError = table.Column<bool>(nullable: false, comment: "是否可以查看错题"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "启用状态"),
                    IsExamAnyTime = table.Column<bool>(nullable: false, comment: "是否随到随考"),
                    IsShowWindowOnblur = table.Column<bool>(nullable: false, comment: "提示切屏次数"),
                    MaxExamCount = table.Column<int>(nullable: false, comment: "考试最大次数"),
                    Sorting = table.Column<int>(nullable: false, comment: "顺序"),
                    OnlyExamDayVisible = table.Column<bool>(nullable: false, comment: "仅考试当天可见"),
                    IsStartSync = table.Column<bool>(nullable: false, comment: "是否启动自动实操评分"),
                    IsShowHelp = table.Column<bool>(nullable: false, comment: "是否显示帮助内容"),
                    HalftimeFlag = table.Column<bool>(nullable: false, comment: "是否中场休息"),
                    HalftimeStart = table.Column<DateTime>(nullable: false, comment: "中场休息开始时间"),
                    HalftimeEnd = table.Column<DateTime>(nullable: false, comment: "中场休息结束时间"),
                    DeductionAmounnt = table.Column<decimal>(nullable: true, comment: "扣款金额"),
                    DeductionInterval = table.Column<int>(nullable: true, comment: "扣款间隔（单位: 分钟）"),
                    Interval = table.Column<int>(nullable: true, comment: "比赛间隔（单位: 分钟）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppExams_AppTestPapers_TestPaperId",
                        column: x => x.TestPaperId,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppExams_TestPaperId",
                table: "AppExams",
                column: "TestPaperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppExams");
        }
    }
}
