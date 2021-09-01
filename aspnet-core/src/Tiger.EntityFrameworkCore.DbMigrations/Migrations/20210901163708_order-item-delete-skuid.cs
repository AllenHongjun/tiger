using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class orderitemdeleteskuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppSkus_SkuId",
                table: "AppOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderItems_SkuId",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "SkuId",
                table: "AppOrderItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SkuId",
                table: "AppOrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_SkuId",
                table: "AppOrderItems",
                column: "SkuId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppSkus_SkuId",
                table: "AppOrderItems",
                column: "SkuId",
                principalTable: "AppSkus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
