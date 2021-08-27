using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Bom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockBomHeader",
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
                    Status = table.Column<int>(nullable: false),
                    QuantityUnit = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductSn = table.Column<string>(nullable: true),
                    ProcessStamp = table.Column<string>(nullable: true),
                    WarehouseCode = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBomHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockBomDetail",
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
                    BuildSequence = table.Column<int>(nullable: false),
                    BuildLevel = table.Column<int>(nullable: false),
                    QtyNeededPerItem = table.Column<int>(nullable: false),
                    QuantityUnit = table.Column<string>(nullable: true),
                    AllocationRule = table.Column<string>(nullable: true),
                    ProcessStamp = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    BomHeaderId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBomDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockBomDetail_StockBomHeader_BomHeaderId",
                        column: x => x.BomHeaderId,
                        principalTable: "StockBomHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockBomDetail_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockBomDetail_StockWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "StockWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockBomDetail_BomHeaderId",
                table: "StockBomDetail",
                column: "BomHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBomDetail_ProductId",
                table: "StockBomDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBomDetail_WarehouseId",
                table: "StockBomDetail",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockBomDetail");

            migrationBuilder.DropTable(
                name: "StockBomHeader");
        }
    }
}
