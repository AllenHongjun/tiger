using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class menuaddfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Redirect",
                table: "AppMenus",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "AppMenus",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AppMenus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Redirect",
                table: "AppLayouts",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "AppLayouts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AppLayouts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "AppMenus");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppMenus");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "AppLayouts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppLayouts");

            migrationBuilder.AlterColumn<string>(
                name: "Redirect",
                table: "AppMenus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Redirect",
                table: "AppLayouts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
