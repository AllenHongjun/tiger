using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumPics",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailTitle",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GiftGrowth",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GiftIntegration",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LowStock",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewStatus",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OriPrice",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PreviewStatus",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductAttributeTypeId",
                table: "AppProducts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductCategoryId",
                table: "AppProducts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "PromotionEndTime",
                table: "AppProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PromotionPerLimit",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PromotionPrice",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "PromotionStartTime",
                table: "AppProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PromotionType",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RecommandStatus",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VerifyStatus",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "AppProducts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppOrders",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppOrderItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumPics",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "DetailTitle",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "GiftGrowth",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "GiftIntegration",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "LowStock",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "NewStatus",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "OriPrice",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PreviewStatus",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "ProductAttributeTypeId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PromotionEndTime",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PromotionPerLimit",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PromotionPrice",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PromotionStartTime",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PromotionType",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "RecommandStatus",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "VerifyStatus",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppOrderItems");
        }
    }
}
