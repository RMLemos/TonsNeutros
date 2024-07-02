using Microsoft.EntityFrameworkCore;
using TonsNeutros.Admin.Models;

namespace TonsNeutros.Admin.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
}
