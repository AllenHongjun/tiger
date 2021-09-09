using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class deletemember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderReturnDetails_AppMembers_MemberId",
                table: "AppOrderReturnDetails");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderReturnDetails_MemberId",
                table: "AppOrderReturnDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "AppOrderReturnDetails",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "AppOrderReturnDetails",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_MemberId",
                table: "AppOrderReturnDetails",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderReturnDetails_AppMembers_MemberId",
                table: "AppOrderReturnDetails",
                column: "MemberId",
                principalTable: "AppMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
