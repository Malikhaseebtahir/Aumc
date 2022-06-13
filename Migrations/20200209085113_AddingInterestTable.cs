using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class AddingInterestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interest",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte>(
                name: "InterestId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InterestId",
                table: "AspNetUsers",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Interests_InterestId",
                table: "AspNetUsers",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Interests_InterestId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InterestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Interest",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
