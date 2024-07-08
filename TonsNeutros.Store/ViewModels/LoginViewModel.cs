using System.ComponentModel.DataAnnotations;

namespace TonsNeutros.Store.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email*")]
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
