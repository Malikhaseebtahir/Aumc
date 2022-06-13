using Microsoft.EntityFrameworkCore.Migrations;

namespace Aumc.Migrations
{
    public partial class PopulatingCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PostCategories (Id, Name) VALUES (1, 'Assignement')");
            migrationBuilder.Sql("INSERT INTO PostCategories (Id, Name) VALUES (2, 'Quiz')");
            migrationBuilder.Sql("INSERT INTO PostCategories (Id, Name) VALUES (3, 'Slides')");
            migrationBuilder.Sql("INSERT INTO PostCategories (Id, Name) VALUES (4, 'Research')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PostCategories WHERE Id IN (1,2,3,4)");
        }
    }
}
