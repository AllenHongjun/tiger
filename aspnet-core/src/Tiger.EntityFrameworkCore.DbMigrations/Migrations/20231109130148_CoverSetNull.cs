using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class CoverSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTestPaperJudgeSchool");

            migrationBuilder.DropColumn(
                name: "PracticalTrainingId",
                table: "AppQuestions");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainPlatformId",
                table: "AppQuestions",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cover",
                table: "AppQuestionCategories",
                maxLength: 128,
                nullable: true,
                comment: "封面",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldComment: "封面");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainPlatformId",
                table: "AppQuestions");

            migrationBuilder.AddColumn<Guid>(
                name: "PracticalTrainingId",
                table: "AppQuestions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cover",
                table: "AppQuestionCategories",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                comment: "封面",
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true,
                oldComment: "封面");

            migrationBuilder.CreateTable(
                name: "AppTestPaperJudgeSchool",
                columns: table => new
                {
                    TestPaperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TestPaperId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestPaperJudgeSchool", x => new { x.TestPaperId, x.SchoolId });
                    table.ForeignKey(
                        name: "FK_AppTestPaperJudgeSchool_AppSchools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "AppSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppTestPaperJudgeSchool_AppSchools_SchoolId1",
                        column: x => x.SchoolId1,
                        principalTable: "AppSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                        column: x => x.TestPaperId,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId1",
                        column: x => x.TestPaperId1,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperJudgeSchool_SchoolId",
                table: "AppTestPaperJudgeSchool",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperJudgeSchool_SchoolId1",
                table: "AppTestPaperJudgeSchool",
                column: "SchoolId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperJudgeSchool_TestPaperId1",
                table: "AppTestPaperJudgeSchool",
                column: "TestPaperId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperJudgeSchool_TestPaperId_SchoolId",
                table: "AppTestPaperJudgeSchool",
                columns: new[] { "TestPaperId", "SchoolId" });
        }
    }
}
