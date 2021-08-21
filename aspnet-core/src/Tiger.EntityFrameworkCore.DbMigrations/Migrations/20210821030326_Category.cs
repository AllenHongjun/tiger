using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    ParentCategoryId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Level = table.Column<int>(maxLength: 10, nullable: false),
                    ProductCount = table.Column<int>(nullable: false),
                    ShowStatus = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Keyword = table.Column<string>(nullable: true, defaultValue: "False"),
                    Description = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCategorys_AppCategorys_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "AppCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_CategoryId",
                table: "AppProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategorys_ParentCategoryId",
                table: "AppCategorys",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts",
                column: "CategoryId",
                principalTable: "AppCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppCategorys");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_CategoryId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppProducts");
        }
    }
}
