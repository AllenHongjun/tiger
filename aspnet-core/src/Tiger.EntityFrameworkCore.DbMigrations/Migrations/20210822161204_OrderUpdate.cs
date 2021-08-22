using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppOrders_OrderId1",
                table: "AppOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppProducts_ProductId1",
                table: "AppOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderItems_OrderId1",
                table: "AppOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderItems_ProductId1",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "ProductSkuId",
                table: "AppOrderItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "AppOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CouponId",
                table: "AppOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "AppOrderItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "AppOrderItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "SkuId",
                table: "AppOrderItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppCoupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Platform = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    PerLimit = table.Column<int>(nullable: false),
                    MinPoint = table.Column<decimal>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    UseType = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PublishCount = table.Column<int>(nullable: false),
                    UseCount = table.Column<int>(nullable: false),
                    ReceiveCount = table.Column<int>(nullable: false),
                    EnableTime = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    MemberLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCoupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMemberReceiveAddresses",
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
                    MemberId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DefaultStatus = table.Column<int>(nullable: false),
                    PostCode = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Reginon = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true),
                    Lon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMemberReceiveAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMembers",
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
                    MemberLevelId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    SourceType = table.Column<int>(nullable: false),
                    Integration = table.Column<int>(nullable: false),
                    Growth = table.Column<int>(nullable: false),
                    LuckeyCount = table.Column<int>(nullable: false),
                    HistoryIntegration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSkus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    SkuCode = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Stock = table.Column<decimal>(nullable: false),
                    LowStock = table.Column<decimal>(nullable: false),
                    SP1 = table.Column<string>(nullable: true),
                    SP2 = table.Column<string>(nullable: true),
                    SP3 = table.Column<string>(nullable: true),
                    Sale = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    PromotionPrice = table.Column<decimal>(nullable: false),
                    LockStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSkus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCouponHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    CouponId = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    CouponCode = table.Column<string>(nullable: true),
                    MemberNickName = table.Column<string>(nullable: true),
                    GetType = table.Column<int>(nullable: false),
                    UseStatus = table.Column<int>(nullable: false),
                    UseTime = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    OrderSn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCouponHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCouponHistories_AppCoupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "AppCoupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCouponHistories_AppMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AppMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppOrderReturnDetails",
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
                    OrderSn = table.Column<string>(nullable: true),
                    ReturnAmount = table.Column<decimal>(nullable: false),
                    ReturnName = table.Column<string>(nullable: true),
                    ReturnPhone = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    HandleTime = table.Column<DateTime>(nullable: false),
                    ProductPic = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductAttr = table.Column<string>(nullable: true),
                    ProductQuantity = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<string>(nullable: true),
                    ProductRealPrice = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProofPics = table.Column<string>(nullable: true),
                    HandleNote = table.Column<string>(nullable: true),
                    HandleMan = table.Column<string>(nullable: true),
                    ReceiveMan = table.Column<string>(nullable: true),
                    ReceiveTime = table.Column<string>(nullable: true),
                    ReceiveNote = table.Column<string>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    ReceiveAddressId = table.Column<Guid>(nullable: false),
                    MemberReceiveAddressId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderReturnDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOrderReturnDetails_AppMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AppMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrderReturnDetails_AppMemberReceiveAddresses_MemberReceiveAddressId",
                        column: x => x.MemberReceiveAddressId,
                        principalTable: "AppMemberReceiveAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppOrderReturnDetails_AppOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "AppOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrderReturnDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCartItems",
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
                    ProductId = table.Column<Guid>(nullable: false),
                    SkuId = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductPic = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductSubTitle = table.Column<string>(nullable: true),
                    SkuCode = table.Column<string>(nullable: true),
                    MemberNickName = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ProductSn = table.Column<string>(nullable: true),
                    ProductAttr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCartItems_AppCategorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AppCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCartItems_AppMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AppMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCartItems_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCartItems_AppSkus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "AppSkus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_CouponId",
                table: "AppOrders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_MemberId",
                table: "AppOrders",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_OrderId",
                table: "AppOrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_ProductId",
                table: "AppOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_SkuId",
                table: "AppOrderItems",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCartItems_CategoryId",
                table: "AppCartItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCartItems_MemberId",
                table: "AppCartItems",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCartItems_ProductId",
                table: "AppCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCartItems_SkuId",
                table: "AppCartItems",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCouponHistories_CouponId",
                table: "AppCouponHistories",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCouponHistories_MemberId",
                table: "AppCouponHistories",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_MemberId",
                table: "AppOrderReturnDetails",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_MemberReceiveAddressId",
                table: "AppOrderReturnDetails",
                column: "MemberReceiveAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_OrderId",
                table: "AppOrderReturnDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_ProductId",
                table: "AppOrderReturnDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSkus_SkuCode",
                table: "AppSkus",
                column: "SkuCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppOrders_OrderId",
                table: "AppOrderItems",
                column: "OrderId",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppProducts_ProductId",
                table: "AppOrderItems",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppSkus_SkuId",
                table: "AppOrderItems",
                column: "SkuId",
                principalTable: "AppSkus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppCoupons_CouponId",
                table: "AppOrders",
                column: "CouponId",
                principalTable: "AppCoupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppMembers_MemberId",
                table: "AppOrders",
                column: "MemberId",
                principalTable: "AppMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppOrders_OrderId",
                table: "AppOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppProducts_ProductId",
                table: "AppOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderItems_AppSkus_SkuId",
                table: "AppOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppCoupons_CouponId",
                table: "AppOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppMembers_MemberId",
                table: "AppOrders");

            migrationBuilder.DropTable(
                name: "AppCartItems");

            migrationBuilder.DropTable(
                name: "AppCouponHistories");

            migrationBuilder.DropTable(
                name: "AppOrderReturnDetails");

            migrationBuilder.DropTable(
                name: "AppSkus");

            migrationBuilder.DropTable(
                name: "AppCoupons");

            migrationBuilder.DropTable(
                name: "AppMembers");

            migrationBuilder.DropTable(
                name: "AppMemberReceiveAddresses");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_CouponId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_MemberId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderItems_OrderId",
                table: "AppOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderItems_ProductId",
                table: "AppOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderItems_SkuId",
                table: "AppOrderItems");

            migrationBuilder.DropColumn(
                name: "SkuId",
                table: "AppOrderItems");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "AppOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "AppOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "AppOrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "AppOrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "AppOrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "AppOrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSkuId",
                table: "AppOrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_OrderId1",
                table: "AppOrderItems",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderItems_ProductId1",
                table: "AppOrderItems",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppOrders_OrderId1",
                table: "AppOrderItems",
                column: "OrderId1",
                principalTable: "AppOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderItems_AppProducts_ProductId1",
                table: "AppOrderItems",
                column: "ProductId1",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
