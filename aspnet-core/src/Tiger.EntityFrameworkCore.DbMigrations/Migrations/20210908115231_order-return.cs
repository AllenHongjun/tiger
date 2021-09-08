using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class orderreturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppOrderReturnHeader",
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
                    Code = table.Column<string>(nullable: true),
                    OrderReturnType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CompleteTime = table.Column<DateTime>(nullable: false),
                    CloseAt = table.Column<DateTime>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false),
                    TotalVolume = table.Column<decimal>(nullable: false),
                    TotalCases = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderReturnHeader", x => x.Id);
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
                    ReceiveAddressId = table.Column<Guid>(nullable: false),
                    MemberReceiveAddressId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    OrderReturnHeaderId = table.Column<Guid>(nullable: true)
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
                        name: "FK_AppOrderReturnDetails_AppOrderReturnHeader_OrderReturnHeaderId",
                        column: x => x.OrderReturnHeaderId,
                        principalTable: "AppOrderReturnHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppOrderReturnDetails_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_MemberId",
                table: "AppOrderReturnDetails",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_MemberReceiveAddressId",
                table: "AppOrderReturnDetails",
                column: "MemberReceiveAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_OrderReturnHeaderId",
                table: "AppOrderReturnDetails",
                column: "OrderReturnHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderReturnDetails_ProductId",
                table: "AppOrderReturnDetails",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOrderReturnDetails");

            migrationBuilder.DropTable(
                name: "AppOrderReturnHeader");
        }
    }
}
