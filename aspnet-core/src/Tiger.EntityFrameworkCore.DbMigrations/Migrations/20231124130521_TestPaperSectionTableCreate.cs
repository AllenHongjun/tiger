using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TestPaperSectionTableCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestPaperSectionId",
                table: "AppTestPaperQuestions",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cover",
                table: "AppQuestionCategories",
                maxLength: 128,
                nullable: false,
                comment: "封面",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true,
                oldComment: "封面");

            migrationBuilder.CreateTable(
                name: "AppTestPaperSections",
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
                    TestPaperId = table.Column<Guid>(nullable: false, comment: "试卷Id"),
                    Name = table.Column<string>(maxLength: 256, nullable: false, comment: "名称"),
                    Description = table.Column<string>(maxLength: 2048, nullable: true, comment: "大题描述"),
                    QuestionCount = table.Column<int>(nullable: false, comment: "题目数量"),
                    TotalScore = table.Column<decimal>(nullable: false, comment: "题目数量"),
                    Sort = table.Column<int>(nullable: false, comment: "序号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestPaperSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTestPaperSections_AppTestPapers_TestPaperId",
                        column: x => x.TestPaperId,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperQuestions_TestPaperSectionId",
                table: "AppTestPaperQuestions",
                column: "TestPaperSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperSections_TestPaperId",
                table: "AppTestPaperSections",
                column: "TestPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperQuestions_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperQuestions",
                column: "TestPaperSectionId",
                principalTable: "AppTestPaperSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperQuestions_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperQuestions");

            migrationBuilder.DropTable(
                name: "AppTestPaperSections");

            migrationBuilder.DropIndex(
                name: "IX_AppTestPaperQuestions_TestPaperSectionId",
                table: "AppTestPaperQuestions");

            migrationBuilder.DropColumn(
                name: "TestPaperSectionId",
                table: "AppTestPaperQuestions");

            migrationBuilder.AlterColumn<string>(
                name: "Cover",
                table: "AppQuestionCategories",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                comment: "封面",
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldComment: "封面");
        }
    }
}
