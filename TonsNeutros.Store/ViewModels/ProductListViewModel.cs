using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.ViewModels;

public class ProductListViewModel
{
    public IEnumerable<Product>? Products { get; set; }
    public string? CurrentCategory { get; set; }
}
