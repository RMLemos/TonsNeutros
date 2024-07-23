using System.ComponentModel.DataAnnotations;

namespace TonsNeutros.Admin.Models;

public class Address
{
    public int AddressId { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Morada")]
    public string? Street { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Localidade")]
    public string? Place { get; set; }

    public int ZipCode { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Cidade")]
    public string? City { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "País")]
    public string? Country { get; set; }

    public Buyer? Buyer { get; set; }
}
