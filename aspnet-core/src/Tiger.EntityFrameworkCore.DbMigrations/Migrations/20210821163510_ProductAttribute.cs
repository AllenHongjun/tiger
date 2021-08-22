using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class ProductAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCategorys_AppCategorys_ParentCategoryId",
                table: "AppCategorys");

            migrationBuilder.DropIndex(
                name: "IX_AppCategorys_ParentCategoryId",
                table: "AppCategorys");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "AppCategorys");

            migrationBuilder.CreateTable(
                name: "AppProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 500, nullable: false),
                    SelectType = table.Column<int>(nullable: false),
                    InputType = table.Column<int>(nullable: false),
                    InputList = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    FilterType = table.Column<int>(nullable: false),
                    SearchType = table.Column<int>(nullable: false),
                    HandAddStatus = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCategorys_ParentId",
                table: "AppCategorys",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductAttributes_Name",
                table: "AppProductAttributes",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCategorys_AppCategorys_ParentId",
                table: "AppCategorys",
                column: "ParentId",
                principalTable: "AppCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCategorys_AppCategorys_ParentId",
                table: "AppCategorys");

            migrationBuilder.DropTable(
                name: "AppProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_AppCategorys_ParentId",
                table: "AppCategorys");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "AppCategorys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCategorys_ParentCategoryId",
                table: "AppCategorys",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCategorys_AppCategorys_ParentCategoryId",
                table: "AppCategorys",
                column: "ParentCategoryId",
                principalTable: "AppCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
