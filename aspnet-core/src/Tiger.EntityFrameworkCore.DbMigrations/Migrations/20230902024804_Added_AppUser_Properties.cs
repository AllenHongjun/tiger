using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Added_AppUser_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AbpUsers",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "AbpUsers",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "AbpUsers");
        }
    }
}
