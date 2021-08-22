using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class updateorder1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppCoupons_CouponId",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppMembers_MemberId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_CouponId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_MemberId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "AppOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "AppOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "AppOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_CouponId",
                table: "AppOrders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_MemberId",
                table: "AppOrders",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppCoupons_CouponId",
                table: "AppOrders",
                column: "CouponId",
                principalTable: "AppCoupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppMembers_MemberId",
                table: "AppOrders",
                column: "MemberId",
                principalTable: "AppMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
