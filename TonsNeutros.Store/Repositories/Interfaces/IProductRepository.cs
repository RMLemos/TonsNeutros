using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.Repositories.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> Products { get; }
    IEnumerable<Product> FeaturedProducts { get; }

    Product GetProductById(int productid);
}
