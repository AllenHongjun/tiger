using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockInventory",
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
                    OnHandQty = table.Column<int>(nullable: false),
                    LockedQty = table.Column<int>(nullable: false),
                    FrozenQty = table.Column<int>(nullable: false),
                    InventoryStatus = table.Column<int>(nullable: false),
                    AttributeData = table.Column<string>(nullable: true),
                    CsQty = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockInventory_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockInventory_StockWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "StockWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockInventory_ProductId",
                table: "StockInventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInventory_WarehouseId",
                table: "StockInventory",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockInventory");
        }
    }
}
