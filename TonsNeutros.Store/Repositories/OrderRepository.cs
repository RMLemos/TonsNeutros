using Microsoft.AspNetCore.Identity;
using System;
using TonsNeutros.Admin.Context;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories.Interfaces;

namespace TonsNeutros.Store.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly AppDbContext _context;
    private readonly IAddressRepository _addressRepository;
    private readonly ShoppingCart _shoppingCart;
    private readonly UserManager<Buyer> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderRepository(IBuyerRepository buyerRepository, AppDbContext context, IAddressRepository addressRepository, ShoppingCart shoppingCart, UserManager<Buyer> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _buyerRepository = buyerRepository;
        _context = context;
        _addressRepository = addressRepository;
        _shoppingCart = shoppingCart;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public void CreateOrder(Order order)
    {
        // Obter o usuário atual
        var user = _httpContextAccessor.HttpContext.User;
        var buyerId = _userManager.GetUserId(user);

        var buyer = _buyerRepository.Buyers.FirstOrDefault(b => b.Id.Equals(buyerId));

        // Atribuir o BuyerId ao pedido
        order.BuyerId = buyerId;

        var addressBuyer = _addressRepository.Addresses.FirstOrDefault(u => u.AddressId == buyer.AddressId);

        order.AddressId = addressBuyer.AddressId;

        order.OrderSentDate = DateTime.Now;
        _context.Orders.Add(order);
        _context.SaveChanges();

        var shoppingCartItems = _shoppingCart.CartItems;
        foreach (var cartItem in shoppingCartItems)
        {
            var orderDetail = new OrderDetail()
            {
                Quantity = cartItem.Quantity,
                ProductId = cartItem.Product.ProductId,
                OrderId = order.OrderId,
                Price = cartItem.Product.Price,
            };
            _context.OrderDetails.Add(orderDetail);
        }
        _context.SaveChanges();
    }
}
