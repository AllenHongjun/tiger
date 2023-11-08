using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AnswerSheetCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAnswerSheets",
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
                    TestPaperMainId = table.Column<int>(nullable: false, comment: "主试卷、固定题目时0，随机题目或打乱顺序时录入主试卷的ID"),
                    TestPaperId = table.Column<Guid>(nullable: false, comment: "试卷ID"),
                    ExamId = table.Column<Guid>(nullable: false, comment: "考试ID"),
                    StudentId = table.Column<Guid>(nullable: false, comment: "学员Id"),
                    TotalScore = table.Column<decimal>(nullable: false, comment: "总分数"),
                    IsSubmit = table.Column<bool>(nullable: false, comment: "是否交卷 True为交卷"),
                    SubmitDateTime = table.Column<DateTime>(nullable: true, comment: "交卷时间"),
                    IP = table.Column<string>(maxLength: 64, nullable: true, comment: "客户端IP"),
                    DeviceType = table.Column<int>(nullable: false, comment: "设备类型： 1.电脑 2.手机 3.平板"),
                    ExamDuration = table.Column<int>(nullable: false, comment: "考试总时长"),
                    AnswerTotalDuration = table.Column<int>(nullable: false, comment: "答题总时长（分钟）"),
                    WindowOnblur = table.Column<int>(nullable: false, comment: "考试切屏次数"),
                    ScoreTime = table.Column<DateTime>(nullable: true),
                    OperateAutoScore = table.Column<decimal>(nullable: true, comment: "实操题自动评分"),
                    OperateAutoScoreTime = table.Column<DateTime>(nullable: true, comment: "实操自动评分时间"),
                    OperateManualScore = table.Column<decimal>(nullable: true, comment: "实操题人工打分"),
                    OperateManualScoreTime = table.Column<DateTime>(nullable: true, comment: "实操题自动评分时间"),
                    ObjectiveScore = table.Column<decimal>(nullable: true, comment: "客观题评分"),
                    ObjectiveScoreTime = table.Column<DateTime>(nullable: true, comment: "客观题评分时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAnswerSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAnswerSheets_AppExams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "AppExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAnswerSheets_ExamId",
                table: "AppAnswerSheets",
                column: "ExamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAnswerSheets");
        }
    }
}
