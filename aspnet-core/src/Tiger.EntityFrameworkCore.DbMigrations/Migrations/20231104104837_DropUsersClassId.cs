using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class DropUsersClassId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppClasses_ClassInfoId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_ClassInfoId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ClassInfoId",
                table: "AbpUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClassInfoId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_ClassInfoId",
                table: "AbpUsers",
                column: "ClassInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppClasses_ClassInfoId",
                table: "AbpUsers",
                column: "ClassInfoId",
                principalTable: "AppClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
