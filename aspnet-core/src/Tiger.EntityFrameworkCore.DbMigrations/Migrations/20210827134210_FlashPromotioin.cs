using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class FlashPromotioin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCouponProductRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductSn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCouponProductRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCouponProductRelations_AppCoupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "AppCoupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCouponProductRelations_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFlashPromotionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MemberPhone = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    SubscribeTime = table.Column<DateTime>(nullable: false),
                    Sendtime = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFlashPromotionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFlashPromotionLogs_AppMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AppMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFlashPromotionLogs_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFlashPromotions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFlashPromotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppFlashPromotionSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    FlashPromotionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFlashPromotionSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFlashPromotionSessions_AppFlashPromotions_FlashPromotionId",
                        column: x => x.FlashPromotionId,
                        principalTable: "AppFlashPromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFlashPromotionProductRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    FlashPromotionId = table.Column<Guid>(nullable: false),
                    FlashPromotionSessionId = table.Column<Guid>(nullable: false),
                    PrductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFlashPromotionProductRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFlashPromotionProductRelations_AppFlashPromotions_FlashPromotionId",
                        column: x => x.FlashPromotionId,
                        principalTable: "AppFlashPromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFlashPromotionProductRelations_AppFlashPromotionSessions_FlashPromotionSessionId",
                        column: x => x.FlashPromotionSessionId,
                        principalTable: "AppFlashPromotionSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppFlashPromotionProductRelations_AppProducts_PrductId",
                        column: x => x.PrductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCouponProductRelations_CouponId",
                table: "AppCouponProductRelations",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCouponProductRelations_ProductId",
                table: "AppCouponProductRelations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFlashPromotionLogs_MemberId",
                table: "AppFlashPromotionLogs",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFlashPromotionLogs_ProductId",
                table: "AppFlashPromotionLogs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFlashPromotionProductRelations_FlashPromotionId",
                table: "AppFlashPromotionProductRelations",
                column: "FlashPromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFlashPromotionProductRelations_FlashPromotionSessionId",
                table: "AppFlashPromotionProductRelations",
                column: "FlashPromotionSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFlashPromotionProductRelations_PrductId",
                table: "AppFlashPromotionProductRelations",
                column: "PrductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFlashPromotionSessions_FlashPromotionId",
                table: "AppFlashPromotionSessions",
                column: "FlashPromotionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCouponProductRelations");

            migrationBuilder.DropTable(
                name: "AppFlashPromotionLogs");

            migrationBuilder.DropTable(
                name: "AppFlashPromotionProductRelations");

            migrationBuilder.DropTable(
                name: "AppFlashPromotionSessions");

            migrationBuilder.DropTable(
                name: "AppFlashPromotions");
        }
    }
}
