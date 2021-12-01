using System;
using System.ComponentModel.DataAnnotations;

namespace desafio_pontotel_backend.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string IsPis { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string CpfPis { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Password { get; set; }
    }
}
