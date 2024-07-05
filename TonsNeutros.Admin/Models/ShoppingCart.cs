using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using TonsNeutros.Admin.Context;

namespace TonsNeutros.Admin.Models;

public class ShoppingCart
{
    private readonly AppDbContext _context;

    public ShoppingCart(AppDbContext context)
    {
        _context = context;
    }

    public string? ShoppingCartId { get; set; }
    public List<ShoppingCartItem>? CartItems { get; set; }

    public static ShoppingCart GetShoppingCart(IServiceProvider services)
    {
        // define uma sessão
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        //obtem um serviço do tipo do nosso contexto
        var context = services.GetService<AppDbContext>();

        //obtem ou gera o Id do carrinho
        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

        //atribui o id do carrinho na Sessão
        session.SetString("CartId", cartId);

        //retorna o carrinho com o contexto e o Id atribuido ou obtido
        return new ShoppingCart(context)
        {
            ShoppingCartId = cartId
        };
    }

    public void AddToCart(Product product)
    {
        var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Product != null && s.Product.ProductId == product.ProductId &&
                s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Product = product,
                Quantity = 1,
            };
            _context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Quantity++;
        }
        _context.SaveChanges();
    }

    public int RemoveFromCart(Product product)
    {
        var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Product != null && s.Product.ProductId == product.ProductId &&
                s.ShoppingCartId == ShoppingCartId);

        var localQuantity = 0;

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Quantity > 1)
            {
                shoppingCartItem.Quantity--;
                localQuantity = shoppingCartItem.Quantity;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }
        _context.SaveChanges();
        return localQuantity;
    }

    public List<ShoppingCartItem> GetCartItems()
    {
        if (CartItems != null)
        {
            return CartItems;
        }
        else
        {
            CartItems = _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToList();

            return CartItems;
        }
    }

    public void CleanCart()
    {
        var ItemsCart = _context.ShoppingCartItems
                        .Where(cart => cart.ShoppingCartId == ShoppingCartId);
        _context.ShoppingCartItems.RemoveRange(ItemsCart);
        _context.SaveChanges();
    }

    public decimal GetTotalCart()
    {
        var total = _context.ShoppingCartItems
                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Select(c => c.Product.Price * c.Quantity).Sum();
        return total;
    }
}

