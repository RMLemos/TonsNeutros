using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Product>? FeaturedProducts { get; set; }
}
