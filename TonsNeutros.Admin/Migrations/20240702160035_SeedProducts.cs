using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TonsNeutros.Admin.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(ProductName,ShortDescription,Description,Price,ImageURL,ImageThumbnailURL,Stock,IsFeatured,StockInHand,CreatedAt,CategoryId) VALUES('Vela','Vela decorativa','Vela decorativa com aroma a baunilha',6.90,'~/images/vela.jpg', ' ',10,0,1,GETDATE(),1)");
            migrationBuilder.Sql("INSERT INTO Products(ProductName,ShortDescription,Description,Price,ImageURL,ImageThumbnailURL,Stock,IsFeatured,StockInHand,CreatedAt,CategoryId) VALUES('Jarra','Jarra decorativa','Jarra decorativa de cor azul',6.90,'~/images/jarra.jpg', ' ',10,0,1,GETDATE(),2)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
