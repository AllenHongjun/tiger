using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class UpdateBasic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppProductTags_ProductTagId",
                table: "AppProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductTagRelation_AppProductTags_ProductTagId1",
                table: "AppProductTagRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProductTags",
                table: "AppProductTags");

            migrationBuilder.RenameTable(
                name: "AppProductTags",
                newName: "ProductTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppCommentReplys",
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
                    CommentId = table.Column<Guid>(nullable: false),
                    MemberNickName = table.Column<string>(nullable: true),
                    MemberIcon = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCommentReplys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppComments",
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
                    OrderId = table.Column<Guid>(nullable: false),
                    Pics = table.Column<string>(nullable: true),
                    Star = table.Column<int>(nullable: false),
                    ShowStatus = table.Column<int>(nullable: false),
                    MemberNickName = table.Column<string>(nullable: true),
                    MemberIP = table.Column<string>(nullable: true),
                    MemberIcon = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ProductAttribute = table.Column<string>(nullable: true),
                    CollectCount = table.Column<int>(nullable: false),
                    ReadCount = table.Column<int>(nullable: false),
                    ReplayCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppComments_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppProductAttributeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    AttributeCount = table.Column<int>(nullable: false),
                    ParamCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductAttributeTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppComments_ProductId",
                table: "AppComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductAttributeTypes_Name",
                table: "AppProductAttributeTypes",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_ProductTag_ProductTagId",
                table: "AppProducts",
                column: "ProductTagId",
                principalTable: "ProductTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductTagRelation_ProductTag_ProductTagId1",
                table: "AppProductTagRelation",
                column: "ProductTagId1",
                principalTable: "ProductTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_ProductTag_ProductTagId",
                table: "AppProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductTagRelation_ProductTag_ProductTagId1",
                table: "AppProductTagRelation");

            migrationBuilder.DropTable(
                name: "AppCommentReplys");

            migrationBuilder.DropTable(
                name: "AppComments");

            migrationBuilder.DropTable(
                name: "AppProductAttributeTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag");

            migrationBuilder.RenameTable(
                name: "ProductTag",
                newName: "AppProductTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProductTags",
                table: "AppProductTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppProductTags_ProductTagId",
                table: "AppProducts",
                column: "ProductTagId",
                principalTable: "AppProductTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductTagRelation_AppProductTags_ProductTagId1",
                table: "AppProductTagRelation",
                column: "ProductTagId1",
                principalTable: "AppProductTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
