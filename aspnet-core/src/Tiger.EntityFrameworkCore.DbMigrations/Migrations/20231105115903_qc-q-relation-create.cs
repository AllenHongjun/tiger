using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class qcqrelationcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppQuestions_QuestionCategoryId",
                table: "AppQuestions",
                column: "QuestionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppQuestions_AppQuestionCategories_QuestionCategoryId",
                table: "AppQuestions",
                column: "QuestionCategoryId",
                principalTable: "AppQuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppQuestions_AppQuestionCategories_QuestionCategoryId",
                table: "AppQuestions");

            migrationBuilder.DropIndex(
                name: "IX_AppQuestions_QuestionCategoryId",
                table: "AppQuestions");
        }
    }
}
