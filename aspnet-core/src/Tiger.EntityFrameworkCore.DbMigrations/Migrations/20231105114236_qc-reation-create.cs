using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class qcreationcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppQuestionCategories_ParentId",
                table: "AppQuestionCategories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppQuestionCategories_AppQuestionCategories_ParentId",
                table: "AppQuestionCategories",
                column: "ParentId",
                principalTable: "AppQuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppQuestionCategories_AppQuestionCategories_ParentId",
                table: "AppQuestionCategories");

            migrationBuilder.DropIndex(
                name: "IX_AppQuestionCategories_ParentId",
                table: "AppQuestionCategories");
        }
    }
}
