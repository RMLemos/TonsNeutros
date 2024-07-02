using Microsoft.EntityFrameworkCore;
using TonsNeutros.Admin.Context;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories.Interfaces;

namespace TonsNeutros.Store.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> Products => _context.Products.Include(c => c.Category);

    public IEnumerable<Product> FeaturedProducts => _context.Products.Where(p => p.IsFeatured).Include(p => p.Category);

    public Product GetProductById(int productid)
    {
        return _context.Products.FirstOrDefault(p => p.ProductId == productid);
    }
}
