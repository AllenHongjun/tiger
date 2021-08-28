using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Project11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SP1",
                table: "AppSkus");

            migrationBuilder.DropColumn(
                name: "SP2",
                table: "AppSkus");

            migrationBuilder.DropColumn(
                name: "SP3",
                table: "AppSkus");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "StockTransferHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalQty",
                table: "StockTransferHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalVolume",
                table: "StockTransferHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalWeight",
                table: "StockTransferHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductSn",
                table: "StockInventory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SkuId",
                table: "StockInventory",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "StockTransferHeader");

            migrationBuilder.DropColumn(
                name: "TotalQty",
                table: "StockTransferHeader");

            migrationBuilder.DropColumn(
                name: "TotalVolume",
                table: "StockTransferHeader");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "StockTransferHeader");

            migrationBuilder.DropColumn(
                name: "ProductSn",
                table: "StockInventory");

            migrationBuilder.DropColumn(
                name: "SkuId",
                table: "StockInventory");

            migrationBuilder.AddColumn<string>(
                name: "SP1",
                table: "AppSkus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SP2",
                table: "AppSkus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SP3",
                table: "AppSkus",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
