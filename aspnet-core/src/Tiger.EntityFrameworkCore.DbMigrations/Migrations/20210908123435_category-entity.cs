using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class categoryentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCategorys",
                maxLength: 256,
                nullable: false,
                comment: "分类名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "AppCategorys",
                maxLength: 256,
                nullable: true,
                defaultValue: "False",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "False");

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "AppCategorys",
                maxLength: 512,
                nullable: true,
                comment: "图标地址",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppCategorys",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCategorys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldComment: "分类名称");

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "AppCategorys",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "False",
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldDefaultValue: "False");

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "AppCategorys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true,
                oldComment: "图标地址");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppCategorys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);
        }
    }
}
