using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TestPaperQuestionCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTestPaperQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    TestPaperId = table.Column<Guid>(nullable: false, comment: "试卷ID"),
                    QuestionCategoryId = table.Column<Guid>(nullable: false, comment: "题目分类"),
                    QuestionId = table.Column<Guid>(nullable: false, comment: "试题ID"),
                    TestPaperType = table.Column<int>(nullable: false, comment: "选题方式 1.自主选题 2.随机生成"),
                    QuestionDegree = table.Column<int>(nullable: false, comment: "难易度：1.简单 2.普通 3.困难"),
                    Sorting = table.Column<int>(nullable: false, comment: "顺序"),
                    Score = table.Column<decimal>(nullable: false, comment: "每题分数"),
                    MissOptionInvalid = table.Column<bool>(nullable: false, comment: "漏选按错误处理")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestPaperQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTestPaperQuestions_AppTestPapers_TestPaperId",
                        column: x => x.TestPaperId,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperQuestions_TestPaperId",
                table: "AppTestPaperQuestions",
                column: "TestPaperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTestPaperQuestions");
        }
    }
}
