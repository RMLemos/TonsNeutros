using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories.Interfaces;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
    {
        _productRepository = productRepository;
        _shoppingCart = shoppingCart;
    }

    [Authorize]
    public IActionResult Index()
    {
        var items = _shoppingCart.GetCartItems();
        _shoppingCart.CartItems = items;

        var shoppingCartVM = new ShoppingCartViewModel
        {
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal = _shoppingCart.GetTotalCart(),
        };
        return View(shoppingCartVM);
    }

    [Authorize]
    public IActionResult AddItemShoppingCart(int productId)
    {
        var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
        if (selectedProduct != null)
        {
            _shoppingCart.AddToCart(selectedProduct);
        }
        return RedirectToAction("Index");
    }

    [Authorize]
    public IActionResult RemoveItemShoppingCart(int productId)
    {
        var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
        if (selectedProduct != null)
        {
            _shoppingCart.RemoveFromCart(selectedProduct);
        }
        return RedirectToAction("Index");
    }
}
