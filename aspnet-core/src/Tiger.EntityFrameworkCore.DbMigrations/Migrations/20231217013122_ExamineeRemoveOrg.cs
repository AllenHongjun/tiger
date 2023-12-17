using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class ExamineeRemoveOrg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "AppExaminees");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitName",
                table: "AppExaminees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationUnitId",
                table: "AppExaminees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "考生所属组织Id");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationUnitName",
                table: "AppExaminees",
                type: "nvarchar(max)",
                nullable: true,
                comment: "考生所属组织名称");
        }
    }
}
