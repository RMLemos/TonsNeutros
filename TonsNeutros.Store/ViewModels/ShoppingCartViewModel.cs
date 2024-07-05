using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.ViewModels;

public class ShoppingCartViewModel
{
    public ShoppingCart? ShoppingCart { get; set; }
    public decimal ShoppingCartTotal { get; set; }
}
