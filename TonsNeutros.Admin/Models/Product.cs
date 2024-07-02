using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonsNeutros.Admin.Models;


[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, MinimumLength = 10, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Name")]
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Short Description")]
    public string? ShortDescription { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Name")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [Display(Name = "Price")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 999.99, ErrorMessage = "The established price varies from 1 to 999,99")]
    public decimal Price { get; set; }

    [Display(Name = "Image")]
    [StringLength(200, ErrorMessage = "The maximum length is {1} characters.")]
    public string? ImageURL { get; set; }

    [Display(Name = "Path to the small image")]
    [StringLength(200, ErrorMessage = "The maximum length is {1} characters.")]
    public string? ImageThumbnailURL { get; set; }

    [Display(Name = "Stock")]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Stock { get; set; }

    [Display(Name = "Is  Featured?")]
    public bool IsFeatured { get; set; }

    [Display(Name = "In stock")]
    public bool StockInHand { get; set; }

    [DefaultValue(typeof(DateTime), "")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Display(Name = "Categories")]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}
