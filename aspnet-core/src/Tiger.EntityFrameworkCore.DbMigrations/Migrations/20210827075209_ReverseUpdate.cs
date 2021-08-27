using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class ReverseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReverseDetail_StockReverseDetail_ReverseDetailId",
                table: "StockReverseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferHeader_StockTransferHeader_TransferHeaderId",
                table: "StockTransferHeader");

            migrationBuilder.DropIndex(
                name: "IX_StockTransferHeader_TransferHeaderId",
                table: "StockTransferHeader");

            migrationBuilder.DropIndex(
                name: "IX_StockReverseDetail_ReverseDetailId",
                table: "StockReverseDetail");

            migrationBuilder.DropColumn(
                name: "TransferHeaderId",
                table: "StockTransferHeader");

            migrationBuilder.DropColumn(
                name: "ReverseDetailId",
                table: "StockReverseDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "ReverseHeaderId",
                table: "StockReverseDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockReverseDetail_ReverseHeaderId",
                table: "StockReverseDetail",
                column: "ReverseHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReverseDetail_StockReverseHeader_ReverseHeaderId",
                table: "StockReverseDetail",
                column: "ReverseHeaderId",
                principalTable: "StockReverseHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReverseDetail_StockReverseHeader_ReverseHeaderId",
                table: "StockReverseDetail");

            migrationBuilder.DropIndex(
                name: "IX_StockReverseDetail_ReverseHeaderId",
                table: "StockReverseDetail");

            migrationBuilder.DropColumn(
                name: "ReverseHeaderId",
                table: "StockReverseDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "TransferHeaderId",
                table: "StockTransferHeader",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReverseDetailId",
                table: "StockReverseDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferHeader_TransferHeaderId",
                table: "StockTransferHeader",
                column: "TransferHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReverseDetail_ReverseDetailId",
                table: "StockReverseDetail",
                column: "ReverseDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReverseDetail_StockReverseDetail_ReverseDetailId",
                table: "StockReverseDetail",
                column: "ReverseDetailId",
                principalTable: "StockReverseDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferHeader_StockTransferHeader_TransferHeaderId",
                table: "StockTransferHeader",
                column: "TransferHeaderId",
                principalTable: "StockTransferHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
