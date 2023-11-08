using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AnswerSheetDetailCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAnswerSheetDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    AnswerSheetId = table.Column<Guid>(nullable: false, comment: "答卷Id"),
                    TestPaperId = table.Column<Guid>(nullable: false, comment: "试卷Id"),
                    TestPaperDetailId = table.Column<Guid>(nullable: false, comment: "试卷详情Id"),
                    QuestionId = table.Column<Guid>(nullable: false, comment: "试题Id"),
                    Answer = table.Column<string>(maxLength: 512, nullable: true, comment: "答案"),
                    ObjectiveScore = table.Column<decimal>(nullable: true, comment: "客观题评分"),
                    OperateAutoScore = table.Column<decimal>(nullable: true),
                    OperateManualScore = table.Column<decimal>(nullable: true, comment: "实操题自动评分分数"),
                    OperateId = table.Column<string>(nullable: true, comment: "实操Id"),
                    SyncTime = table.Column<DateTime>(nullable: true, comment: "上次自动评分同步时间"),
                    SyncMessage = table.Column<string>(nullable: true, comment: "上次自动评分同步结果")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAnswerSheetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAnswerSheetDetails_AppAnswerSheets_AnswerSheetId",
                        column: x => x.AnswerSheetId,
                        principalTable: "AppAnswerSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAnswerSheetDetails_AnswerSheetId",
                table: "AppAnswerSheetDetails",
                column: "AnswerSheetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAnswerSheetDetails");
        }
    }
}
