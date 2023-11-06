using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TestPaperStrategyCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_SchoolId",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId1",
                table: "AppTestPaperJudgeSchool",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TestPaperId1",
                table: "AppTestPaperJudgeSchool",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppTestPaperStrategies",
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
                    TenantId = table.Column<Guid>(nullable: true),
                    TestPaperId = table.Column<Guid>(nullable: false, comment: "试卷Id"),
                    QuestionCategoryId = table.Column<Guid>(nullable: true, comment: "题目分类Id"),
                    QuestionType = table.Column<int>(nullable: false, comment: "题型"),
                    UnlimitedDifficultyCount = table.Column<int>(nullable: false, comment: "不限难度数量"),
                    EasyCount = table.Column<int>(nullable: false, comment: "简单的数量"),
                    OrdinaryCount = table.Column<int>(nullable: false, comment: "普通的数量"),
                    DifficultCount = table.Column<int>(nullable: false, comment: "困难的数量")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestPaperStrategies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTestPaperStrategies_AppTestPapers_TestPaperId",
                        column: x => x.TestPaperId,
                        principalTable: "AppTestPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperJudgeSchool_SchoolId1",
                table: "AppTestPaperJudgeSchool",
                column: "SchoolId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperJudgeSchool_TestPaperId1",
                table: "AppTestPaperJudgeSchool",
                column: "TestPaperId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperStrategies_TestPaperId",
                table: "AppTestPaperStrategies",
                column: "TestPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppSchools_SchoolId1",
                table: "AppTestPaperJudgeSchool",
                column: "SchoolId1",
                principalTable: "AppSchools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AppTestPaperJudgeSchool",
                column: "TestPaperId",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId1",
                table: "AppTestPaperJudgeSchool",
                column: "TestPaperId1",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppSchools_SchoolId1",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId1",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropTable(
                name: "AppTestPaperStrategies");

            migrationBuilder.DropIndex(
                name: "IX_AppTestPaperJudgeSchool_SchoolId1",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropIndex(
                name: "IX_AppTestPaperJudgeSchool_TestPaperId1",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropColumn(
                name: "SchoolId1",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropColumn(
                name: "TestPaperId1",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_SchoolId",
                table: "AppTestPaperJudgeSchool",
                column: "SchoolId",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AppTestPaperJudgeSchool",
                column: "TestPaperId",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
