using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AnswerSheetAddCulumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalScore",
                table: "AppAnswerSheets",
                nullable: true,
                comment: "总分数",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总分数");

            migrationBuilder.AddColumn<bool>(
                name: "IsPass",
                table: "AppAnswerSheets",
                nullable: true,
                comment: "是否及格1:是; 0:否");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPass",
                table: "AppAnswerSheets");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalScore",
                table: "AppAnswerSheets",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总分数",
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "总分数");
        }
    }
}
