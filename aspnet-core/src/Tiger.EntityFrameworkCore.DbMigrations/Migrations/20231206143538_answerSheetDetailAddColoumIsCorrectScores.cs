using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class answerSheetDetailAddColoumIsCorrectScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "AppAnswerSheetDetails",
                nullable: true,
                comment: "是否正确");

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "AppAnswerSheetDetails",
                nullable: true,
                comment: "题目得分");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "AppAnswerSheetDetails");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AppAnswerSheetDetails");
        }
    }
}
