using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class PopulatingGroupTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO GroupTypes (Id, Name) VALUES (1, 'Study')");
            migrationBuilder.Sql("INSERT INTO GroupTypes (Id, Name) VALUES (2, 'Entertainment')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM GroupTypes WHERE Id IN (1, 2)");
        }
    }
}
