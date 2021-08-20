using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class addProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailDesc",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductSn",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTagId",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublishStatus",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppProductTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProductTagRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductId1 = table.Column<Guid>(nullable: true),
                    ProductTagId = table.Column<int>(nullable: false),
                    ProductTagId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductTagRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProductTagRelation_AppProducts_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppProductTagRelation_AppProductTags_ProductTagId1",
                        column: x => x.ProductTagId1,
                        principalTable: "AppProductTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_ProductTagId",
                table: "AppProducts",
                column: "ProductTagId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductTagRelation_ProductId1",
                table: "AppProductTagRelation",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductTagRelation_ProductTagId1",
                table: "AppProductTagRelation",
                column: "ProductTagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppProductTags_ProductTagId",
                table: "AppProducts",
                column: "ProductTagId",
                principalTable: "AppProductTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppProductTags_ProductTagId",
                table: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppProductTagRelation");

            migrationBuilder.DropTable(
                name: "AppProductTags");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_ProductTagId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "DetailDesc",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "ProductSn",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "ProductTagId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PublishStatus",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "AppProducts");
        }
    }
}
