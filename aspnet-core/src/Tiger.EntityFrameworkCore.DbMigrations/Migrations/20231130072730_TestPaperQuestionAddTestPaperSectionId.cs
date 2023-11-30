using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TestPaperQuestionAddTestPaperSectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissOptionInvalid",
                table: "AppTestPaperQuestions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MissOptionInvalid",
                table: "AppTestPaperQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "漏选按错误处理");
        }
    }
}
