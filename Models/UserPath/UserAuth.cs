using System.ComponentModel.DataAnnotations;

namespace desafio_pontotel_backend.Models
{
    public class UserAuth
    {
        [Required(ErrorMessage = "O Campo E-mail é obrigado a estar preenchido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo CPF/PIS é obrigado a estar preenchido")]
        public string CpfPis { get; set; }
        [Required(ErrorMessage = "O Campo Senha é obrigado a estar preenchido")]
        public string Password { get; set; }
    }
}