using System.ComponentModel.DataAnnotations;

namespace AdministradorWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
