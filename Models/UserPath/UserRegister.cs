using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace desafio_pontotel_backend.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "O Campo E-mail é obrigado a estar preenchido")]
        [MaxLength(50)]        
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo Nome é obrigado a estar preenchido")]
        [MaxLength(50)] 
        public string Name { get; set; }
        [Required(ErrorMessage = "O Campo País é obrigado a estar preenchido")]
        [MaxLength(50)] 
        public string Country { get; set; }
        [Required(ErrorMessage = "O Campo Estado é obrigado a estar preenchido")]
        [MaxLength(50)] 
        public string State { get; set; }
        [Required(ErrorMessage = "O Campo Data de Nascimento é obrigado a estar preenchido")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "O Campo Cidade é obrigado a estar preenchido")]
        [MaxLength(50)] 
        public string City { get; set; }
        [Required(ErrorMessage = "O Campo CEP é obrigado a estar preenchido")]
        [MaxLength(10)] 
        public string Cep { get; set; }
        [Required(ErrorMessage = "O Campo Bairro é obrigado a estar preenchido")]
        [MaxLength(50)] 
        public string District { get; set; }
        [Required(ErrorMessage = "O Campo Rua é obrigado a estar preenchido")]
        [MaxLength(30)] 
        public string Street { get; set; }
        [Required(ErrorMessage = "O Campo Número é obrigado a estar preenchido")]
        [MaxLength(10)] 
        public string Number { get; set; }
        public string Complement { get; set; }
        [Required(ErrorMessage = "O Campo CPF/PIS é obrigado a estar preenchido")]
        [MaxLength(20)] 
        public string CpfPis { get; set; }
        public bool IsPis { get; set; }
        public string PasswordString { get; set; }
        [JsonIgnore]
        public byte[] Hash { get; set; }
        [JsonIgnore]
        public byte[] Password { get; set; }
    }
}