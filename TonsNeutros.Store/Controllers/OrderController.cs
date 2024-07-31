using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Controllers;

public class OrderController : Controller
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly UserManager<Buyer> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOrderRepository _orderRepository;
    private readonly ShoppingCart _shoppingCart;

    public OrderController(IBuyerRepository buyerRepository, IAddressRepository addressRepository, UserManager<Buyer> userManager, IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, ShoppingCart shoppingCart)
    {
        _buyerRepository = buyerRepository;
        _addressRepository = addressRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _orderRepository = orderRepository;
        _shoppingCart = shoppingCart;
    }

    [Authorize]
    public IActionResult Checkout()
    {
        var user = _httpContextAccessor.HttpContext.User;
        var buyerId = _userManager.GetUserId(user);

        var buyer = _buyerRepository.Buyers.FirstOrDefault(b => b.Id.Equals(buyerId));

        if (buyer == null)
        {
            return RedirectToAction(nameof(Error), new { message = "User not found" });
        }

        var address = _addressRepository.Addresses.FirstOrDefault(u => u.AddressId == buyer.AddressId);

        if (address == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Address not found" });
        }

        var viewModel = new BuyerFormViewModel
        {
            Buyer = buyer,
            Address = address,

        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        var user = _httpContextAccessor.HttpContext.User;
        var buyerId = _userManager.GetUserId(user);

        var buyer = _buyerRepository.Buyers.FirstOrDefault(b => b.Id.Equals(buyerId));
        if (buyer == null)
        {
            return RedirectToAction(nameof(Error), new { message = "User not found" });
        }
        order.BuyerId = buyerId;

        var address = _addressRepository.Addresses.FirstOrDefault(u => u.AddressId == buyer.AddressId);

        if (address == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Address not found" });
        }
        order.AddressId = address.AddressId;


        int orderTotalItems = 0;
        decimal orderTotalPrice = 0.0m;

        //obtem os itens do carrinho de compra do cliente
        List<ShoppingCartItem> items = _shoppingCart.GetCartItems();
        _shoppingCart.CartItems = items;

        //verifica se existem itens no pedido
        if (_shoppingCart.CartItems.Count == 0)
        {
            ModelState.AddModelError("", "Your cart is empty.");
        }

        //calcula o total de itens e o total do pedido
        foreach (var item in items)
        {
            orderTotalItems += item.Quantity;
            orderTotalPrice += (item.Product.Price * item.Quantity);
        }

        //atribui os valores obtidos ao pedido
        order.TotalItemsOrder = orderTotalItems;
        order.TotalOrder = orderTotalPrice;

        //cria o pedido e os detalhes
        _orderRepository.CreateOrder(order);

        //define mensagens ao cliente
        ViewBag.CheckoutCompleteMessage = "Muito obrigado. A sua encomenda está a caminho.";
        ViewBag.TotalOrder = _shoppingCart.GetTotalCart();

        //limpa o carrinho do cliente
        _shoppingCart.CleanCart();

        //exibe a view com dados do cliente e do pedido
        return View("~/Views/Order/CheckoutComplete.cshtml", order);
    }
}
