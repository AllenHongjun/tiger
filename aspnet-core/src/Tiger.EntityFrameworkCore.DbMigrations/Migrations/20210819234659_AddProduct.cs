using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "AppBooks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProducts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_AuthorId1",
                table: "AppBooks",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_Name",
                table: "AppProducts",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppAuthors_AuthorId1",
                table: "AppBooks",
                column: "AuthorId1",
                principalTable: "AppAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppAuthors_AuthorId1",
                table: "AppBooks");

            migrationBuilder.DropTable(
                name: "AppProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_AuthorId1",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "AppBooks");
        }
    }
}
