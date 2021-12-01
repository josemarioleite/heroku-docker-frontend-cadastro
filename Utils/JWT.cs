using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using desafio_pontotel_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace desafio_pontotel_backend.Utils
{
    public class JWT
    {
        public async Task<string> CreateTokenJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new [] {
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public async Task<int> ReturnIdUser(HttpContext context) {
            var identidade = context.User.Identity as ClaimsIdentity;
            if(identidade != null) {
                IEnumerable<Claim> claims = identidade.Claims;
                int idusuario = Int32.Parse(identidade.FindFirst("id").Value);
                return idusuario;
            } else {
                return 0;
            }
        }
    }
}