using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiger.Migrations
{
    public partial class Member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppMembers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppMemberReceiveAddresses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppMemberLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    GrowthPoint = table.Column<int>(nullable: false),
                    FreeFreightPoint = table.Column<decimal>(nullable: false),
                    CommentGrowthPoint = table.Column<int>(nullable: false),
                    PriviledgeFreeFreight = table.Column<int>(nullable: false),
                    PriviledgeSignIn = table.Column<int>(nullable: false),
                    PrivilegeComment = table.Column<int>(nullable: false),
                    PrivilegePromotin = table.Column<int>(nullable: false),
                    PrivilegeMemberPrice = table.Column<int>(nullable: false),
                    PrivilegeBirthDay = table.Column<int>(nullable: false),
                    Note = table.Column<int>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMemberLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMemberLoginLog",
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
                    IP = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    LoginType = table.Column<int>(nullable: false),
                    Province = table.Column<string>(nullable: true),
                    MemberId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMemberLoginLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMemberLoginLog_AppMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AppMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppMemberStatisticInfo",
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
                    TotalAmount = table.Column<decimal>(nullable: false),
                    OrderCount = table.Column<int>(nullable: false),
                    CouponCount = table.Column<int>(nullable: false),
                    CommentCount = table.Column<int>(nullable: false),
                    ReturnOrderCount = table.Column<int>(nullable: false),
                    LoginCount = table.Column<int>(nullable: false),
                    AttentCount = table.Column<int>(nullable: false),
                    FanCount = table.Column<int>(nullable: false),
                    CollectProductCount = table.Column<int>(nullable: false),
                    CollectSubjectCount = table.Column<int>(nullable: false),
                    CollectTopicCount = table.Column<int>(nullable: false),
                    CollectCommentCount = table.Column<int>(nullable: false),
                    InviteFriendCount = table.Column<int>(nullable: false),
                    RecentOrderTime = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMemberStatisticInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMemberStatisticInfo_AppMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AppMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppMembers_MemberLevelId",
                table: "AppMembers",
                column: "MemberLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMemberReceiveAddresses_MemberId",
                table: "AppMemberReceiveAddresses",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMemberLoginLog_MemberId",
                table: "AppMemberLoginLog",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMemberStatisticInfo_MemberId",
                table: "AppMemberStatisticInfo",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMemberReceiveAddresses_AppMembers_MemberId",
                table: "AppMemberReceiveAddresses",
                column: "MemberId",
                principalTable: "AppMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppMembers_AppMemberLevel_MemberLevelId",
                table: "AppMembers",
                column: "MemberLevelId",
                principalTable: "AppMemberLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMemberReceiveAddresses_AppMembers_MemberId",
                table: "AppMemberReceiveAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMembers_AppMemberLevel_MemberLevelId",
                table: "AppMembers");

            migrationBuilder.DropTable(
                name: "AppMemberLevel");

            migrationBuilder.DropTable(
                name: "AppMemberLoginLog");

            migrationBuilder.DropTable(
                name: "AppMemberStatisticInfo");

            migrationBuilder.DropIndex(
                name: "IX_AppMembers_MemberLevelId",
                table: "AppMembers");

            migrationBuilder.DropIndex(
                name: "IX_AppMemberReceiveAddresses_MemberId",
                table: "AppMemberReceiveAddresses");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppMembers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppMemberReceiveAddresses");
        }
    }
}
