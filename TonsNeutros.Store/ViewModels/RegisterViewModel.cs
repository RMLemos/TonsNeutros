using System.ComponentModel.DataAnnotations;

namespace TonsNeutros.Store.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Este campo é de preenchimento obrigatório")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email*")]
    public string? Email { get; set; }


    [Required(ErrorMessage = "Este campo é de preenchimento obrigatório")]
    [DataType(DataType.Password)]
    [Display(Name = "Password*")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Este campo é de preenchimento obrigatório")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmação da Password*")]
    [Compare("Password", ErrorMessage = "Ambas as passwords devem coincidir")]
    public string? ConfirmPassword { get; set; }
}
