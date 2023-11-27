using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class QuestionModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionA",
                table: "AppQuestions");

            migrationBuilder.DropColumn(
                name: "OptionB",
                table: "AppQuestions");

            migrationBuilder.DropColumn(
                name: "OptionC",
                table: "AppQuestions");

            migrationBuilder.DropColumn(
                name: "OptionD",
                table: "AppQuestions");

            migrationBuilder.DropColumn(
                name: "OptionE",
                table: "AppQuestions");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "AppQuestions",
                maxLength: 1024,
                nullable: false,
                comment: "题目内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "题目内容");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "AppQuestions",
                maxLength: 256,
                nullable: true,
                comment: "答案 判断题答案：正确/错误；多个填空之间的答案使用竖线 |分隔，如果一个填空有多个答案请用 & 开隔;",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionContent",
                table: "AppQuestions",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                comment: "选项内容");

            migrationBuilder.AddColumn<int>(
                name: "OptionSize",
                table: "AppQuestions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "AppQuestions",
                maxLength: 512,
                nullable: false,
                defaultValue: 0m,
                comment: "分数");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionContent",
                table: "AppQuestions");

            migrationBuilder.DropColumn(
                name: "OptionSize",
                table: "AppQuestions");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AppQuestions");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "AppQuestions",
                type: "nvarchar(max)",
                nullable: false,
                comment: "题目内容",
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldComment: "题目内容");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "AppQuestions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "答案 判断题答案：正确/错误；多个填空之间的答案使用竖线 |分隔，如果一个填空有多个答案请用 & 开隔;");

            migrationBuilder.AddColumn<string>(
                name: "OptionA",
                table: "AppQuestions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "A 选项");

            migrationBuilder.AddColumn<string>(
                name: "OptionB",
                table: "AppQuestions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "B 选项");

            migrationBuilder.AddColumn<string>(
                name: "OptionC",
                table: "AppQuestions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "C 选项");

            migrationBuilder.AddColumn<string>(
                name: "OptionD",
                table: "AppQuestions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "D 选项");

            migrationBuilder.AddColumn<string>(
                name: "OptionE",
                table: "AppQuestions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                comment: "E 选项");
        }
    }
}
