using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class purchasereturnheader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPurchaseReturnDetail_AppPurchaseReturnHeader_PurchaseReturnHeaderId",
                table: "AppPurchaseReturnDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleteTime",
                table: "AppPurchaseReturnHeader",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseAt",
                table: "AppPurchaseReturnHeader",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseReturnHeaderId",
                table: "AppPurchaseReturnDetail",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPurchaseReturnDetail_AppPurchaseReturnHeader_PurchaseReturnHeaderId",
                table: "AppPurchaseReturnDetail",
                column: "PurchaseReturnHeaderId",
                principalTable: "AppPurchaseReturnHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPurchaseReturnDetail_AppPurchaseReturnHeader_PurchaseReturnHeaderId",
                table: "AppPurchaseReturnDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleteTime",
                table: "AppPurchaseReturnHeader",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseAt",
                table: "AppPurchaseReturnHeader",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseReturnHeaderId",
                table: "AppPurchaseReturnDetail",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_AppPurchaseReturnDetail_AppPurchaseReturnHeader_PurchaseReturnHeaderId",
                table: "AppPurchaseReturnDetail",
                column: "PurchaseReturnHeaderId",
                principalTable: "AppPurchaseReturnHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
