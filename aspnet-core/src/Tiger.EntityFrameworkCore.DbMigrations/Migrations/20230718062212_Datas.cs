using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Datas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDatas",
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
                    Name = table.Column<string>(maxLength: 30, nullable: false, comment: "数据字典名称"),
                    Code = table.Column<string>(maxLength: 1024, nullable: false, comment: "编码"),
                    DisplayName = table.Column<string>(maxLength: 128, nullable: false, comment: "数据字典显示名称"),
                    Description = table.Column<string>(maxLength: 1024, nullable: true, comment: "数据字典描述"),
                    ParentId = table.Column<Guid>(nullable: true),
                    IsStatic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDataItems",
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
                    Name = table.Column<string>(maxLength: 30, nullable: false, comment: "字典数据名称"),
                    DisplayName = table.Column<string>(maxLength: 128, nullable: false, comment: "显示名称"),
                    Description = table.Column<string>(maxLength: 1024, nullable: true, comment: "描述"),
                    DefaultValue = table.Column<string>(maxLength: 128, nullable: true, comment: "默认值"),
                    AllowBeNull = table.Column<bool>(nullable: false, defaultValue: true),
                    IsStatic = table.Column<bool>(nullable: false),
                    ValueType = table.Column<int>(nullable: false),
                    DataId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDataItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDataItems_AppDatas_DataId",
                        column: x => x.DataId,
                        principalTable: "AppDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDataItems_DataId",
                table: "AppDataItems",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDataItems_Name",
                table: "AppDataItems",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppDatas_Name",
                table: "AppDatas",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDataItems");

            migrationBuilder.DropTable(
                name: "AppDatas");
        }
    }
}
