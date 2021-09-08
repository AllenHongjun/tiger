using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class purchase01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPurchaseHeader",
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
                    TenantId = table.Column<Guid>(nullable: true),
                    WarehouseCode = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    AuditedBy = table.Column<string>(nullable: true),
                    PurchseBy = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: false),
                    SupplyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseReturnHeader",
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
                    table.PrimaryKey("PK_AppPurchaseReturnHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseDetail",
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
                    ProductSn = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TotalQty = table.Column<int>(nullable: false),
                    OpenQty = table.Column<int>(nullable: false),
                    ProcessStamp = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    PurchaseHeaderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPurchaseDetail_AppPurchaseHeader_PurchaseHeaderId",
                        column: x => x.PurchaseHeaderId,
                        principalTable: "AppPurchaseHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppPurchaseDetail_AppWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "AppWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppPurchaseReturnDetail",
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
                    ProductSn = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    OpenQty = table.Column<int>(nullable: false),
                    ProcessStamp = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    PurchaseReturnHeaderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPurchaseReturnDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnDetail_AppPurchaseReturnHeader_PurchaseReturnHeaderId",
                        column: x => x.PurchaseReturnHeaderId,
                        principalTable: "AppPurchaseReturnHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppPurchaseReturnDetail_AppWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "AppWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseDetail_ProductId",
                table: "AppPurchaseDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseDetail_PurchaseHeaderId",
                table: "AppPurchaseDetail",
                column: "PurchaseHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseDetail_WarehouseId",
                table: "AppPurchaseDetail",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnDetail_ProductId",
                table: "AppPurchaseReturnDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnDetail_PurchaseReturnHeaderId",
                table: "AppPurchaseReturnDetail",
                column: "PurchaseReturnHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPurchaseReturnDetail_WarehouseId",
                table: "AppPurchaseReturnDetail",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPurchaseDetail");

            migrationBuilder.DropTable(
                name: "AppPurchaseReturnDetail");

            migrationBuilder.DropTable(
                name: "AppPurchaseHeader");

            migrationBuilder.DropTable(
                name: "AppPurchaseReturnHeader");
        }
    }
}
