using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "AppProducts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    MemberId = table.Column<int>(nullable: false),
                    CouponId = table.Column<int>(nullable: false),
                    OrderSn = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    PayAmount = table.Column<decimal>(nullable: false),
                    FreightAmount = table.Column<decimal>(nullable: false),
                    PromotionAmount = table.Column<decimal>(nullable: false),
                    IntegrationAmount = table.Column<decimal>(nullable: false),
                    CouponAmount = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    PayType = table.Column<int>(nullable: false),
                    SourceType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OrderType = table.Column<int>(nullable: false),
                    DeliveryCompany = table.Column<string>(nullable: true),
                    DeliverySn = table.Column<string>(nullable: true),
                    AutoConfirmDay = table.Column<int>(nullable: false),
                    Integration = table.Column<int>(nullable: false),
                    Growth = table.Column<int>(nullable: false),
                    PromotionInfo = table.Column<int>(nullable: false),
                    BillType = table.Column<int>(nullable: false),
                    BillHeader = table.Column<string>(nullable: true),
                    BillContent = table.Column<string>(nullable: true),
                    BillReceiverPhone = table.Column<string>(nullable: true),
                    BillReceiverEmail = table.Column<string>(nullable: true),
                    ReceiverName = table.Column<string>(nullable: true),
                    ReceiverPhone = table.Column<string>(nullable: true),
                    ReceiverPostCode = table.Column<string>(nullable: true),
                    ReceiverProvince = table.Column<string>(nullable: true),
                    ReceiverCity = table.Column<string>(nullable: true),
                    ReceiverRegion = table.Column<string>(nullable: true),
                    ReceiverDetailAddress = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ConfirmStatus = table.Column<int>(nullable: false),
                    UseIntegration = table.Column<int>(nullable: false),
                    PaymentTime = table.Column<DateTime>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    ReceiveTime = table.Column<DateTime>(nullable: false),
                    CommentTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppOrderSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    FlashOrderOverTime = table.Column<int>(nullable: false),
                    NormalOrderOverTime = table.Column<int>(nullable: false),
                    ComfirmOvertime = table.Column<int>(nullable: false),
                    CommentOvertime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    OrderId1 = table.Column<Guid>(nullable: true),
                    OrderSn = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ProductId1 = table.Column<Guid>(nullable: true),
                    ProductPic = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductBrand = table.Column<string>(nullable: true),
                    ProductSn = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<decimal>(nullable: false),
                    ProductQuantity = table.Column<int>(nullable: false),
                    ProductSkuId = table.Column<int>(nullable: false),
                    ProductSkuCode = table.Column<string>(nullable: true),
                    ProductCategoryId = table.Column<int>(nullable: false),
                    PromotionName = table.Column<string>(nullable: true),
                    PromotionAmount = table.Column<decimal>(nullable: false),
                    CouponAmount = table.Column<decimal>(nullable: false),
                    IntegrationAmount = table.Column<decimal>(nullable: false),
                    RealAmount = table.Column<decimal>(nullable: false),
                    GiftIntegration = table.Column<decimal>(nullable: false),
                    GiftGrowth = table.Column<decimal>(nullable: false),
                    ProductAttr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOrderItems_AppOrders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "AppOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppOrderItems_AppProducts_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppOrderOperateHistorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    OrderId1 = table.Column<Guid>(nullable: true),
                    OperateMan = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderOperateHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOrderOperateHistorys_AppOrders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "AppOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_OrderId",
                table: "AppProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_OrderId1",
                table: "AppOrderItems",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_ProductId1",
                table: "AppOrderItems",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderOperateHistorys_OrderId1",
                table: "AppOrderOperateHistorys",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppOrders_OrderId",
                table: "AppProducts",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppOrders_OrderId",
                table: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppOrderItems");

            migrationBuilder.DropTable(
                name: "AppOrderOperateHistorys");

            migrationBuilder.DropTable(
                name: "AppOrderSettings");

            migrationBuilder.DropTable(
                name: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_OrderId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AppProducts");
        }
    }
}
