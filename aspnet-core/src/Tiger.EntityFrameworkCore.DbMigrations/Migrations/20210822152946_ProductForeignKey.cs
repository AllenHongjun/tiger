using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class ProductForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_CategoryId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "AppOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductAttributeTypeId",
                table: "AppProductAttributes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductAttributeId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProductAttributeValues_AppProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "AppProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProductAttributeValues_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_ProductCategoryId",
                table: "AppProducts",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductAttributes_ProductAttributeTypeId",
                table: "AppProductAttributes",
                column: "ProductAttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCommentReplys_CommentId",
                table: "AppCommentReplys",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductAttributeValues_ProductAttributeId",
                table: "AppProductAttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductAttributeValues_ProductId",
                table: "AppProductAttributeValues",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCommentReplys_AppComments_CommentId",
                table: "AppCommentReplys",
                column: "CommentId",
                principalTable: "AppComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductAttributes_AppProductAttributeTypes_ProductAttributeTypeId",
                table: "AppProductAttributes",
                column: "ProductAttributeTypeId",
                principalTable: "AppProductAttributeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppCategorys_ProductCategoryId",
                table: "AppProducts",
                column: "ProductCategoryId",
                principalTable: "AppCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCommentReplys_AppComments_CommentId",
                table: "AppCommentReplys");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductAttributes_AppProductAttributeTypes_ProductAttributeTypeId",
                table: "AppProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppCategorys_ProductCategoryId",
                table: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_ProductCategoryId",
                table: "AppProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppProductAttributes_ProductAttributeTypeId",
                table: "AppProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_AppCommentReplys_CommentId",
                table: "AppCommentReplys");

            migrationBuilder.DropColumn(
                name: "ProductAttributeTypeId",
                table: "AppProductAttributes");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "AppProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "AppOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_CategoryId",
                table: "AppProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppCategorys_CategoryId",
                table: "AppProducts",
                column: "CategoryId",
                principalTable: "AppCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
