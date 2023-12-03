using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class StrategySimpleCountChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EasyCount",
                table: "AppTestPaperStrategies");

            migrationBuilder.AddColumn<int>(
                name: "SimpleCount",
                table: "AppTestPaperStrategies",
                nullable: false,
                defaultValue: 0,
                comment: "简单的数量");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppQuestions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "题目名称");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperQuestions_QuestionId",
                table: "AppTestPaperQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperQuestions_AppQuestions_QuestionId",
                table: "AppTestPaperQuestions",
                column: "QuestionId",
                principalTable: "AppQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperQuestions_AppQuestions_QuestionId",
                table: "AppTestPaperQuestions");

            migrationBuilder.DropIndex(
                name: "IX_AppTestPaperQuestions_QuestionId",
                table: "AppTestPaperQuestions");

            migrationBuilder.DropColumn(
                name: "SimpleCount",
                table: "AppTestPaperStrategies");

            migrationBuilder.AddColumn<int>(
                name: "EasyCount",
                table: "AppTestPaperStrategies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "简单的数量");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppQuestions",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "题目名称",
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
