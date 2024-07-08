using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonsNeutros.Admin.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Categoria")]
    public string? CategoryName { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(200, ErrorMessage = "The maximum length is 200 characters.")]
    [Display(Name = "Descrição Categoria")]
    public string? CategoryDescription { get; set; }
    public List<Product>? Products { get; set; }
}
