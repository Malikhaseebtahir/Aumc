using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class AddingLikerIdAndLikeeIdInNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikerId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LikeId",
                table: "Notifications",
                column: "LikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LikerId",
                table: "Notifications",
                column: "LikerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_LikeId",
                table: "Notifications",
                column: "LikeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_LikerId",
                table: "Notifications",
                column: "LikerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_LikeId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_LikerId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_LikeId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_LikerId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "LikerId",
                table: "Notifications");
        }
    }
}
