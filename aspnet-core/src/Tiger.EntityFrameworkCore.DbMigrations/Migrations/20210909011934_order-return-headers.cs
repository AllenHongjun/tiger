using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class orderreturnheaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderReturnDetails_AppOrderReturnHeader_OrderReturnHeaderId",
                table: "AppOrderReturnDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppOrderReturnHeader",
                table: "AppOrderReturnHeader");

            migrationBuilder.RenameTable(
                name: "AppOrderReturnHeader",
                newName: "AppOrderReturnHeaders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppOrderReturnHeaders",
                table: "AppOrderReturnHeaders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderReturnDetails_AppOrderReturnHeaders_OrderReturnHeaderId",
                table: "AppOrderReturnDetails",
                column: "OrderReturnHeaderId",
                principalTable: "AppOrderReturnHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderReturnDetails_AppOrderReturnHeaders_OrderReturnHeaderId",
                table: "AppOrderReturnDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppOrderReturnHeaders",
                table: "AppOrderReturnHeaders");

            migrationBuilder.RenameTable(
                name: "AppOrderReturnHeaders",
                newName: "AppOrderReturnHeader");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppOrderReturnHeader",
                table: "AppOrderReturnHeader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderReturnDetails_AppOrderReturnHeader_OrderReturnHeaderId",
                table: "AppOrderReturnDetails",
                column: "OrderReturnHeaderId",
                principalTable: "AppOrderReturnHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
