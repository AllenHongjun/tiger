using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class questioninit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppQuestions",
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
                    QuestionCategoryId = table.Column<Guid>(nullable: false),
                    PracticalTrainingId = table.Column<Guid>(nullable: true),
                    Type = table.Column<int>(nullable: false, comment: "类型 1.判断 2.单选 3.多选 4.填空 5.计算题 6.问答题 7.B型题,8.简答题 9.实训任务"),
                    Name = table.Column<string>(maxLength: 1024, nullable: true, comment: "题目名称"),
                    Content = table.Column<string>(nullable: false, comment: "题目内容"),
                    OptionA = table.Column<string>(maxLength: 512, nullable: true, comment: "A 选项"),
                    OptionB = table.Column<string>(maxLength: 512, nullable: true, comment: "B 选项"),
                    OptionC = table.Column<string>(maxLength: 512, nullable: true, comment: "C 选项"),
                    OptionD = table.Column<string>(maxLength: 512, nullable: true, comment: "D 选项"),
                    OptionE = table.Column<string>(maxLength: 512, nullable: true, comment: "E 选项"),
                    Answer = table.Column<string>(nullable: true),
                    Degree = table.Column<int>(nullable: false, comment: "难易度：1.简单 2.普通 3.困难"),
                    Analysis = table.Column<string>(maxLength: 512, nullable: true, comment: "试题解析"),
                    Source = table.Column<string>(maxLength: 256, nullable: true, comment: "出处"),
                    HelpMessage = table.Column<string>(maxLength: 256, nullable: true, comment: "帮助文本"),
                    HelpVideo = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(maxLength: 512, nullable: true, comment: "附件URL"),
                    FileName = table.Column<string>(maxLength: 256, nullable: true, comment: "附件名称"),
                    IsShow = table.Column<bool>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    IsShowTextButton = table.Column<bool>(nullable: false, comment: "是否显示上传文本按钮"),
                    IsShowImageButton = table.Column<bool>(nullable: false, comment: "是否显示上传图片按钮"),
                    IsShowLinkButton = table.Column<bool>(nullable: false, comment: "是否显示上传附件按钮")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppQuestions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppQuestions");
        }
    }
}
