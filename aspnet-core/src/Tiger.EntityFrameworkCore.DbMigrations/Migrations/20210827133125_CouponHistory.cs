using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class CouponHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppCoupons_CouponId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_CouponId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "GetType",
                table: "AppCouponHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponHistoryId",
                table: "AppOrders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCoupons",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCouponHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppCouponHistories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppCouponCategoryRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                    ParentCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCouponCategoryRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCouponCategoryRelations_AppCategorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AppCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCouponCategoryRelations_AppCoupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "AppCoupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCouponHistories_OrderId",
                table: "AppCouponHistories",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCouponCategoryRelations_CategoryId",
                table: "AppCouponCategoryRelations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCouponCategoryRelations_CouponId",
                table: "AppCouponCategoryRelations",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCouponHistories_AppOrders_OrderId",
                table: "AppCouponHistories",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCouponHistories_AppOrders_OrderId",
                table: "AppCouponHistories");

            migrationBuilder.DropTable(
                name: "AppCouponCategoryRelations");

            migrationBuilder.DropIndex(
                name: "IX_AppCouponHistories_OrderId",
                table: "AppCouponHistories");

            migrationBuilder.DropColumn(
                name: "CouponHistoryId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCoupons");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCouponHistories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppCouponHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "AppOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "GetType",
                table: "AppCouponHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_CouponId",
                table: "AppOrders",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppCoupons_CouponId",
                table: "AppOrders",
                column: "CouponId",
                principalTable: "AppCoupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
