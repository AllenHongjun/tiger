using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AnswerSheetAddCulumnStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AppAnswerSheets",
                nullable: false,
                defaultValue: 0,
                comment: "答卷状态1:未交卷;2:已交卷;3:已阅卷");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppAnswerSheets");
        }
    }
}
