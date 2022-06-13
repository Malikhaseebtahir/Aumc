using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class ResolveTheMisSpellingOfLikeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_LikeId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "LikeId",
                table: "Notifications",
                newName: "LikeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_LikeId",
                table: "Notifications",
                newName: "IX_Notifications_LikeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_LikeeId",
                table: "Notifications",
                column: "LikeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_LikeeId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "LikeeId",
                table: "Notifications",
                newName: "LikeId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_LikeeId",
                table: "Notifications",
                newName: "IX_Notifications_LikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_LikeId",
                table: "Notifications",
                column: "LikeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
