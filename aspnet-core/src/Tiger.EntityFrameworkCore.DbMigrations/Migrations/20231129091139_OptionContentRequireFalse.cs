using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class OptionContentRequireFalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OptionContent",
                table: "AppQuestions",
                maxLength: 512,
                nullable: true,
                comment: "选项内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldComment: "选项内容");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OptionContent",
                table: "AppQuestions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "选项内容",
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true,
                oldComment: "选项内容");
        }
    }
}
