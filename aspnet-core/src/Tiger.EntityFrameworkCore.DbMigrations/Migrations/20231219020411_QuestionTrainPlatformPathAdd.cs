using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class QuestionTrainPlatformPathAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrainPlatformPath",
                table: "AppQuestions",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                comment: "资源路径 例如: /path/to/myfile.html");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainPlatformPath",
                table: "AppQuestions");
        }
    }
}
