using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class identityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseHeader_CreatorId",
                table: "AppPurchaseHeader",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseHeader_DeleterId",
                table: "AppPurchaseHeader",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseHeader_LastModifierId",
                table: "AppPurchaseHeader",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPurchaseHeader_AbpUsers_CreatorId",
                table: "AppPurchaseHeader",
                column: "CreatorId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPurchaseHeader_AbpUsers_DeleterId",
                table: "AppPurchaseHeader",
                column: "DeleterId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPurchaseHeader_AbpUsers_LastModifierId",
                table: "AppPurchaseHeader",
                column: "LastModifierId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPurchaseHeader_AbpUsers_CreatorId",
                table: "AppPurchaseHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPurchaseHeader_AbpUsers_DeleterId",
                table: "AppPurchaseHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPurchaseHeader_AbpUsers_LastModifierId",
                table: "AppPurchaseHeader");

            migrationBuilder.DropIndex(
                name: "IX_AppPurchaseHeader_CreatorId",
                table: "AppPurchaseHeader");

            migrationBuilder.DropIndex(
                name: "IX_AppPurchaseHeader_DeleterId",
                table: "AppPurchaseHeader");

            migrationBuilder.DropIndex(
                name: "IX_AppPurchaseHeader_LastModifierId",
                table: "AppPurchaseHeader");
        }
    }
}
