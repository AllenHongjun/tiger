using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TaperQuestionAddColumnSectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperQuestions_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperQuestions");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestPaperSectionId",
                table: "AppTestPaperQuestions",
                nullable: false,
                comment: "试卷大题Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperQuestions_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperQuestions",
                column: "TestPaperSectionId",
                principalTable: "AppTestPaperSections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperQuestions_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperQuestions");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestPaperSectionId",
                table: "AppTestPaperQuestions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldComment: "试卷大题Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperQuestions_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperQuestions",
                column: "TestPaperSectionId",
                principalTable: "AppTestPaperSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
