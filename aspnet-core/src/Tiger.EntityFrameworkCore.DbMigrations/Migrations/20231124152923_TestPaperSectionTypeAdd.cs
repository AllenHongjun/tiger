using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TestPaperSectionTypeAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppTestPaperSections",
                nullable: false,
                defaultValue: 0,
                comment: "类型:固定题目大题:1,随机题目大题:2,抽题大题:3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppTestPaperSections");
        }
    }
}
