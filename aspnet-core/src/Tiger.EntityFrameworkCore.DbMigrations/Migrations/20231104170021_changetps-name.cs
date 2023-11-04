using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class changetpsname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpTestPaperJudgeSchool_AppTestPapers_SchoolId",
                table: "AbpTestPaperJudgeSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpTestPaperJudgeSchool_AppSchools_SchoolId",
                table: "AbpTestPaperJudgeSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AbpTestPaperJudgeSchool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpTestPaperJudgeSchool",
                table: "AbpTestPaperJudgeSchool");

            migrationBuilder.RenameTable(
                name: "AbpTestPaperJudgeSchool",
                newName: "AppTestPaperJudgeSchool");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTestPaperJudgeSchool_TestPaperId_SchoolId",
                table: "AppTestPaperJudgeSchool",
                newName: "IX_AppTestPaperJudgeSchool_TestPaperId_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTestPaperJudgeSchool_SchoolId",
                table: "AppTestPaperJudgeSchool",
                newName: "IX_AppTestPaperJudgeSchool_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppTestPaperJudgeSchool",
                table: "AppTestPaperJudgeSchool",
                columns: new[] { "TestPaperId", "SchoolId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_SchoolId",
                table: "AppTestPaperJudgeSchool",
                column: "SchoolId",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppSchools_SchoolId",
                table: "AppTestPaperJudgeSchool",
                column: "SchoolId",
                principalTable: "AppSchools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AppTestPaperJudgeSchool",
                column: "TestPaperId",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_SchoolId",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppSchools_SchoolId",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppTestPaperJudgeSchool",
                table: "AppTestPaperJudgeSchool");

            migrationBuilder.RenameTable(
                name: "AppTestPaperJudgeSchool",
                newName: "AbpTestPaperJudgeSchool");

            migrationBuilder.RenameIndex(
                name: "IX_AppTestPaperJudgeSchool_TestPaperId_SchoolId",
                table: "AbpTestPaperJudgeSchool",
                newName: "IX_AbpTestPaperJudgeSchool_TestPaperId_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_AppTestPaperJudgeSchool_SchoolId",
                table: "AbpTestPaperJudgeSchool",
                newName: "IX_AbpTestPaperJudgeSchool_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpTestPaperJudgeSchool",
                table: "AbpTestPaperJudgeSchool",
                columns: new[] { "TestPaperId", "SchoolId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTestPaperJudgeSchool_AppTestPapers_SchoolId",
                table: "AbpTestPaperJudgeSchool",
                column: "SchoolId",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTestPaperJudgeSchool_AppSchools_SchoolId",
                table: "AbpTestPaperJudgeSchool",
                column: "SchoolId",
                principalTable: "AppSchools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTestPaperJudgeSchool_AppTestPapers_TestPaperId",
                table: "AbpTestPaperJudgeSchool",
                column: "TestPaperId",
                principalTable: "AppTestPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
