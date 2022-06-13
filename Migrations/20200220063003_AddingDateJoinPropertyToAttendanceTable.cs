using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class AddingDateJoinPropertyToAttendanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateJoin",
                table: "Attendances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateJoin",
                table: "Attendances");
        }
    }
}
