using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AddCulumnScorePerQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ScorePerQuestion",
                table: "AppTestPaperStrategies",
                nullable: false,
                defaultValue: 0m,
                comment: "每题分数");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperStrategies_QuestionCategoryId",
                table: "AppTestPaperStrategies",
                column: "QuestionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperStrategies_AppQuestionCategories_QuestionCategoryId",
                table: "AppTestPaperStrategies",
                column: "QuestionCategoryId",
                principalTable: "AppQuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperStrategies_AppQuestionCategories_QuestionCategoryId",
                table: "AppTestPaperStrategies");

            migrationBuilder.DropIndex(
                name: "IX_AppTestPaperStrategies_QuestionCategoryId",
                table: "AppTestPaperStrategies");

            migrationBuilder.DropColumn(
                name: "ScorePerQuestion",
                table: "AppTestPaperStrategies");
        }
    }
}
