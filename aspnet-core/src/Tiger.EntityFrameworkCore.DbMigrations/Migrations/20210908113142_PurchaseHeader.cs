using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class PurchaseHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "StockReceiptDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "StockReceiptDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckDate",
                table: "StockCheckHeader",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "StockCheckHeader",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                table: "StockCheckHeader",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "StockReceiptDetail");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "StockReceiptDetail");

            migrationBuilder.DropColumn(
                name: "CheckDate",
                table: "StockCheckHeader");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "StockCheckHeader");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "StockCheckHeader");
        }
    }
}
