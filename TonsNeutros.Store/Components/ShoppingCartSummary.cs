using Microsoft.AspNetCore.Mvc;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Components;

public class ShoppingCartSummary : ViewComponent
{
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartSummary(ShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetCartItems();
        _shoppingCart.CartItems = items;

        var shoppingCartVM = new ShoppingCartViewModel
        {
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal = _shoppingCart.GetTotalCart()
        };
        return View(shoppingCartVM);
    }
}
