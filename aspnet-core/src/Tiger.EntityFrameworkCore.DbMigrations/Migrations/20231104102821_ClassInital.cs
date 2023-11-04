using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class ClassInital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VipLevel",
                table: "AppSchools",
                nullable: false,
                comment: "Vip等级：1.免费客户 2.付费客户",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "AppSchools",
                nullable: false,
                comment: "顺序",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "AppSchools",
                maxLength: 64,
                nullable: true,
                comment: "简称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "AppSchools",
                maxLength: 64,
                nullable: true,
                comment: "编号",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppSchools",
                maxLength: 128,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxPerson",
                table: "AppSchools",
                nullable: false,
                comment: "最大人数",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "AppSchools",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAudit",
                table: "AppSchools",
                nullable: false,
                comment: "是否需要审核帖子",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ImpowerDate",
                table: "AppSchools",
                nullable: true,
                comment: "授权截止时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClassInfoId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    SchoolId = table.Column<Guid>(nullable: false, comment: "学校Id"),
                    Name = table.Column<string>(maxLength: 128, nullable: false, comment: "名称"),
                    Sorting = table.Column<int>(nullable: false, comment: "是否启用"),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppClasses_AppSchools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "AppSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_ClassInfoId",
                table: "AbpUsers",
                column: "ClassInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppClasses_SchoolId",
                table: "AppClasses",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppClasses_ClassInfoId",
                table: "AbpUsers",
                column: "ClassInfoId",
                principalTable: "AppClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppClasses_ClassInfoId",
                table: "AbpUsers");

            migrationBuilder.DropTable(
                name: "AppClasses");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_ClassInfoId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ClassInfoId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<int>(
                name: "VipLevel",
                table: "AppSchools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Vip等级：1.免费客户 2.付费客户");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "AppSchools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "顺序");

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true,
                oldComment: "简称");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true,
                oldComment: "编号");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppSchools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldComment: "名称");

            migrationBuilder.AlterColumn<int>(
                name: "MaxPerson",
                table: "AppSchools",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "最大人数");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnable",
                table: "AppSchools",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAudit",
                table: "AppSchools",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "是否需要审核帖子");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ImpowerDate",
                table: "AppSchools",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "授权截止时间");
        }
    }
}
