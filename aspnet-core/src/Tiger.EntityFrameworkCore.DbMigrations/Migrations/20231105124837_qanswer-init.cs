using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class qanswerinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "AppQuestionAttachments",
                nullable: false,
                comment: "题目Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "试题题目Id");

            migrationBuilder.CreateTable(
                name: "AppQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false, comment: "题目Id"),
                    Answer = table.Column<string>(maxLength: 256, nullable: false, comment: "答案")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppQuestionAnswers_AppQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AppQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppQuestionAnswers_QuestionId",
                table: "AppQuestionAnswers",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppQuestionAnswers");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "AppQuestionAttachments",
                type: "uniqueidentifier",
                nullable: false,
                comment: "试题题目Id",
                oldClrType: typeof(Guid),
                oldComment: "题目Id");
        }
    }
}
