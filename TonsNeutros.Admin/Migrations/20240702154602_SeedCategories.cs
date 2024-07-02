using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TonsNeutros.Admin.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories(CategoryName,CategoryDescription) " +
                "VALUES('Velas','Velas decorativas')");
            migrationBuilder.Sql("INSERT INTO Categories(CategoryName,CategoryDescription) " +
                "VALUES('Acessórios','Acessórios decorativos')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}
