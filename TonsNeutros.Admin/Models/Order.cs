using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TonsNeutros.Admin.Models;

public class Order
{
    public int OrderId { get; set; }

    [ScaffoldColumn(false)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Total Compra")]
    public decimal TotalOrder { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Items Comprados")]
    public int TotalItemsOrder { get; set; }

    [Display(Name = "Data da compra")]
    [DataType(DataType.Text)] //Poderia ser datetime
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Data de envio")]
    [DataType(DataType.Text)] //Poderia ser datetime
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? OrderSentDate { get; set; }
    public string? BuyerId { get; set; }
    public Buyer? Buyer { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
    public List<OrderDetail>? ItemsOrder { get; set; }
}
