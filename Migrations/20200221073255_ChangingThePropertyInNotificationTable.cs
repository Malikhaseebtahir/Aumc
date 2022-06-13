using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class ChangingThePropertyInNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrigionalDateTime",
                table: "Notifications");

            migrationBuilder.AddColumn<byte>(
                name: "OrigionalRoom",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrigionalRoom",
                table: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrigionalDateTime",
                table: "Notifications",
                nullable: true);
        }
    }
}
