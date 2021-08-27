using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Stock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockCheckHeader",
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
                    WarehouseCode = table.Column<string>(nullable: true),
                    CheckType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    CloseBy = table.Column<string>(nullable: true),
                    CloseAt = table.Column<DateTime>(nullable: false),
                    ProcessStamp = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCheckHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockReceiptHeader",
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
                    ReceiptType = table.Column<int>(nullable: false),
                    PurchaseOrderId = table.Column<Guid>(nullable: true),
                    ArriveDatetime = table.Column<DateTime>(nullable: false),
                    CloseAt = table.Column<DateTime>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    TotalCases = table.Column<int>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false),
                    TotalVolume = table.Column<decimal>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceiptHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockReverseDetail",
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
                    ProductName = table.Column<string>(nullable: true),
                    WarehouseCode = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    ReverseOrderId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ReverseDetailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReverseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReverseDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReverseDetail_StockReverseDetail_ReverseDetailId",
                        column: x => x.ReverseDetailId,
                        principalTable: "StockReverseDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReverseDetail_StockWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "StockWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockReverseHeader",
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
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    InventorySts = table.Column<int>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    OpenQty = table.Column<int>(nullable: false),
                    BuildQty = table.Column<int>(nullable: false),
                    QuantityUnit = table.Column<string>(nullable: true),
                    ReleaseAt = table.Column<DateTime>(nullable: false),
                    CompleteAt = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    WarehouseCode = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReverseHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockShipmentHeader",
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
                    ShipmentType = table.Column<int>(nullable: false),
                    ShipDateTime = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TotalQty = table.Column<int>(nullable: false),
                    TotalWeight = table.Column<int>(nullable: false),
                    TotalVolume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockShipmentHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferHeader",
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
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    CloasAt = table.Column<DateTime>(nullable: false),
                    CloseBy = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    TransferHeaderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransferHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransferHeader_StockTransferHeader_TransferHeaderId",
                        column: x => x.TransferHeaderId,
                        principalTable: "StockTransferHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockCheckDetail",
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
                    ProductName = table.Column<string>(nullable: true),
                    InventorySts = table.Column<int>(nullable: false),
                    SystemQty = table.Column<int>(nullable: false),
                    LastCheckQty = table.Column<int>(nullable: false),
                    CheckedQty = table.Column<int>(nullable: false),
                    CheckedBy = table.Column<DateTime>(nullable: false),
                    CheckedAt = table.Column<DateTime>(nullable: false),
                    ProcessStamp = table.Column<string>(nullable: true),
                    AdjustQty = table.Column<int>(nullable: false),
                    Batch = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    CheckHeaderId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCheckDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCheckDetail_StockCheckHeader_CheckHeaderId",
                        column: x => x.CheckHeaderId,
                        principalTable: "StockCheckHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCheckDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCheckDetail_StockWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "StockWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockReceiptDetail",
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
                    ReceiptCode = table.Column<string>(nullable: true),
                    WarehouseCode = table.Column<string>(nullable: true),
                    ProductSn = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Batch = table.Column<string>(nullable: true),
                    ManufactureDate = table.Column<DateTime>(nullable: false),
                    AgingDate = table.Column<DateTime>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    OpenQty = table.Column<int>(nullable: false),
                    ProcessStamp = table.Column<string>(nullable: true),
                    QuantityUm = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    ReceiptHeaderId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceiptDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceiptDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceiptDetail_StockReceiptHeader_ReceiptHeaderId",
                        column: x => x.ReceiptHeaderId,
                        principalTable: "StockReceiptHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceiptDetail_StockWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "StockWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockShipmentDetail",
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
                    ProductName = table.Column<string>(nullable: true),
                    ProductSn = table.Column<string>(nullable: true),
                    ShipQty = table.Column<int>(nullable: false),
                    RequestQty = table.Column<int>(nullable: false),
                    Batch = table.Column<string>(nullable: true),
                    AgingDate = table.Column<DateTime>(nullable: false),
                    InventorySts = table.Column<int>(nullable: false),
                    TotalWeight = table.Column<decimal>(nullable: false),
                    TotalVolume = table.Column<decimal>(nullable: false),
                    ProcessStamp = table.Column<string>(nullable: true),
                    QuantityUm = table.Column<string>(nullable: true),
                    CanceledQty = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockShipmentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockShipmentDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockShipmentDetail_StockShipmentHeader_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "StockShipmentHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockShipmentDetail_StockWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "StockWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferDetail",
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
                    TransferCode = table.Column<string>(nullable: true),
                    ProductSn = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    FromWarehouseId = table.Column<Guid>(nullable: false),
                    TotalQty = table.Column<int>(nullable: false),
                    QuantityUnit = table.Column<string>(nullable: true),
                    InventorySts = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ProcessStamp = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    TransferId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransferDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransferDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransferDetail_StockTransferHeader_TransferId",
                        column: x => x.TransferId,
                        principalTable: "StockTransferHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransferDetail_StockWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "StockWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockCheckDetail_CheckHeaderId",
                table: "StockCheckDetail",
                column: "CheckHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCheckDetail_ProductId",
                table: "StockCheckDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCheckDetail_WarehouseId",
                table: "StockCheckDetail",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_ProductId",
                table: "StockReceiptDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_ReceiptHeaderId",
                table: "StockReceiptDetail",
                column: "ReceiptHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiptDetail_WarehouseId",
                table: "StockReceiptDetail",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReverseDetail_ProductId",
                table: "StockReverseDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReverseDetail_ReverseDetailId",
                table: "StockReverseDetail",
                column: "ReverseDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReverseDetail_WarehouseId",
                table: "StockReverseDetail",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockShipmentDetail_ProductId",
                table: "StockShipmentDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockShipmentDetail_ShipmentId",
                table: "StockShipmentDetail",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StockShipmentDetail_WarehouseId",
                table: "StockShipmentDetail",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetail_ProductId",
                table: "StockTransferDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetail_TransferId",
                table: "StockTransferDetail",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetail_WarehouseId",
                table: "StockTransferDetail",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferHeader_TransferHeaderId",
                table: "StockTransferHeader",
                column: "TransferHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockCheckDetail");

            migrationBuilder.DropTable(
                name: "StockReceiptDetail");

            migrationBuilder.DropTable(
                name: "StockReverseDetail");

            migrationBuilder.DropTable(
                name: "StockReverseHeader");

            migrationBuilder.DropTable(
                name: "StockShipmentDetail");

            migrationBuilder.DropTable(
                name: "StockTransferDetail");

            migrationBuilder.DropTable(
                name: "StockCheckHeader");

            migrationBuilder.DropTable(
                name: "StockReceiptHeader");

            migrationBuilder.DropTable(
                name: "StockShipmentHeader");

            migrationBuilder.DropTable(
                name: "StockTransferHeader");
        }
    }
}
