using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class PopulatingTheInterestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Interests VALUES ('Programming')");
            migrationBuilder.Sql("INSERT INTO Interests VALUES ('Study')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Interests WHERE Id IN (1,2)");
        }
    }
}
