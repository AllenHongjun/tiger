using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class CategoryChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
