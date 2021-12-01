using AutoMapper;
using desafio_pontotel_backend.Models;

namespace desafio_pontotel_backend.Utils 
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserRegister, User>();
        }
    }
}