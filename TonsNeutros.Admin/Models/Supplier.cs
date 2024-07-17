using System.ComponentModel.DataAnnotations;

namespace TonsNeutros.Admin.Models;

public class Supplier
{
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [StringLength(50, ErrorMessage = "O número de caracteres máximo é de 50")]
    [Display(Name = "Nome")]
    public string? SupplierName { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [StringLength(150, ErrorMessage = "O número de caracteres máximo é de 150")]
    [Display(Name = "Morada")]
    public string? Address { get; set; }

    [StringLength(150, ErrorMessage = "O número de caracteres máximo é de 150")]
    [Display(Name = "Produtos")]
    public string? SupplerProducts { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [Display(Name = "NIPC")]
    public int Nipc { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [StringLength(50, ErrorMessage = "O número de caracteres máximo é de 50")]
    [Display(Name = "Nome do comercial responsável")]
    public string? ComercialName { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [StringLength(25, ErrorMessage = "The maximum length is 25 characters.")]
    [Display(Name = "Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
           ErrorMessage = "Write a correct email.")]
    public string? Email { get; set; }

}
