using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class PathNoRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrainPlatformPath",
                table: "AppQuestions",
                maxLength: 512,
                nullable: true,
                comment: "资源路径 例如: /path/to/myfile.html",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldComment: "资源路径 例如: /path/to/myfile.html");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrainPlatformPath",
                table: "AppQuestions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "资源路径 例如: /path/to/myfile.html",
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true,
                oldComment: "资源路径 例如: /path/to/myfile.html");
        }
    }
}
