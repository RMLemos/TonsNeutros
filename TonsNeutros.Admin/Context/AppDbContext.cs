using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TonsNeutros.Admin.Models;

namespace TonsNeutros.Admin.Context;

public class AppDbContext : IdentityDbContext<Buyer>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
}
