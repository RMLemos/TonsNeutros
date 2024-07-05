using System.ComponentModel.DataAnnotations;

namespace TonsNeutros.Admin.Models;

public class ShoppingCartItem
{
    public int ShoppingCartItemID { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }

    [StringLength(200)]
    public string? ShoppingCartId { get; set; }
}
