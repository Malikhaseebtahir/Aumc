using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class ResolveMinorBugInFollowingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followings_AspNetUsers_FolloweeId1",
                table: "Followings");

            migrationBuilder.DropIndex(
                name: "IX_Followings_FolloweeId1",
                table: "Followings");

            migrationBuilder.DropColumn(
                name: "FolloweeId1",
                table: "Followings");

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_AspNetUsers_FolloweeId",
                table: "Followings",
                column: "FolloweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followings_AspNetUsers_FolloweeId",
                table: "Followings");

            migrationBuilder.AddColumn<int>(
                name: "FolloweeId1",
                table: "Followings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Followings_FolloweeId1",
                table: "Followings",
                column: "FolloweeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_AspNetUsers_FolloweeId1",
                table: "Followings",
                column: "FolloweeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
