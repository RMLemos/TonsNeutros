using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.ViewModels;

public class OrderProductViewModel
{
    public Order? Order { get; set; }
    public Buyer? Buyer { get; set; }
    public Address? Address { get; set; }
    public IEnumerable<OrderDetail>? OrderDetails { get; set; }
}
