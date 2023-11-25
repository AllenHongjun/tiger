using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class TestPaperSectionRelateStragety : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestPaperSectionId",
                table: "AppTestPaperStrategies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppTestPaperStrategies_TestPaperSectionId",
                table: "AppTestPaperStrategies",
                column: "TestPaperSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTestPaperStrategies_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperStrategies",
                column: "TestPaperSectionId",
                principalTable: "AppTestPaperSections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTestPaperStrategies_AppTestPaperSections_TestPaperSectionId",
                table: "AppTestPaperStrategies");

            migrationBuilder.DropIndex(
                name: "IX_AppTestPaperStrategies_TestPaperSectionId",
                table: "AppTestPaperStrategies");

            migrationBuilder.DropColumn(
                name: "TestPaperSectionId",
                table: "AppTestPaperStrategies");
        }
    }
}
