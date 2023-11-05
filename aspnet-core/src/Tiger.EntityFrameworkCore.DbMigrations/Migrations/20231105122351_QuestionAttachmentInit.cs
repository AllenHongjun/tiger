using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class QuestionAttachmentInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppQuestionAttachments",
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
                    QuestionId = table.Column<Guid>(nullable: false, comment: "试题题目Id"),
                    AttachmentType = table.Column<int>(nullable: false, comment: "附件类型：1.内容，2.照片，3.文档，4.本地附件，5.本地视频，6.添加链接"),
                    Name = table.Column<string>(maxLength: 256, nullable: false, comment: "名称"),
                    Content = table.Column<string>(maxLength: 2048, nullable: false, comment: "附件内容"),
                    Sorting = table.Column<int>(nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppQuestionAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppQuestionAttachments_AppQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AppQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppQuestionAttachments_QuestionId",
                table: "AppQuestionAttachments",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppQuestionAttachments");
        }
    }
}
