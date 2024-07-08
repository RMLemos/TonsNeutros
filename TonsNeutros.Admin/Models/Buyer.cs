using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace TonsNeutros.Admin.Models;

public class Buyer : IdentityUser
{
    [StringLength(100, MinimumLength = 5, ErrorMessage = "The {0} must have at least {2} characters and the maximum length is {1} characters.")]
    [Display(Name = "Nome")]
    public string? Name { get; set; }

    [Display(Name = "NIF")]
    public int Nif { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
}
