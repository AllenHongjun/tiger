using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class backgroundjobadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBackgroundJobActions",
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
                    JobId = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Paramters = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBackgroundJobActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppBackgroundJobLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(nullable: true),
                    JobId = table.Column<string>(maxLength: 255, nullable: true),
                    JobName = table.Column<string>(maxLength: 100, nullable: true),
                    JobGroup = table.Column<string>(maxLength: 50, nullable: true),
                    JobType = table.Column<string>(maxLength: 1000, nullable: true),
                    Message = table.Column<string>(maxLength: 1000, nullable: true),
                    RunTime = table.Column<DateTime>(nullable: false),
                    Exception = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBackgroundJobLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppBackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Group = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 1000, nullable: false),
                    Result = table.Column<string>(maxLength: 1000, nullable: true),
                    Args = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    LockTimeOut = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    LastRunTime = table.Column<DateTime>(nullable: true),
                    NextRunTime = table.Column<DateTime>(nullable: true),
                    JobType = table.Column<int>(nullable: false),
                    Cron = table.Column<string>(maxLength: 50, nullable: true),
                    Source = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    TriggerCount = table.Column<int>(nullable: false),
                    TryCount = table.Column<int>(nullable: false),
                    MaxTryCount = table.Column<int>(nullable: false),
                    MaxCount = table.Column<int>(nullable: false),
                    Interval = table.Column<int>(nullable: false),
                    IsAbandoned = table.Column<bool>(nullable: false),
                    NodeName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBackgroundJobActions_Name",
                table: "AppBackgroundJobActions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppBackgroundJobLogs_JobGroup_JobName",
                table: "AppBackgroundJobLogs",
                columns: new[] { "JobGroup", "JobName" });

            migrationBuilder.CreateIndex(
                name: "IX_AppBackgroundJobs_Name_Group",
                table: "AppBackgroundJobs",
                columns: new[] { "Name", "Group" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBackgroundJobActions");

            migrationBuilder.DropTable(
                name: "AppBackgroundJobLogs");

            migrationBuilder.DropTable(
                name: "AppBackgroundJobs");
        }
    }
}
