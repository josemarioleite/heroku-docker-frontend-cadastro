using System;
using System.Threading.Tasks;
using AutoMapper;
using desafio_pontotel_backend.DAO;
using desafio_pontotel_backend.Models;
using desafio_pontotel_backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_pontotel_backend.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DAOContext _database;
        private readonly JWT _jwt;

        public AuthController(DAOContext database)
        {
            _database = database;
            _jwt = new JWT();
        }

        [HttpGet("token")]
        [Authorize]
        public void TokenValido()
        {
            return;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody]UserAuth auth)
        {
            if(ModelState.IsValid)
            {
                User user;
                try
                {
                    user = await _database.User.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(auth.Email.ToLower()));
                } catch (Exception e) 
                {
                    return BadRequest(new {msg = e.ToString()});
                }
                
                if(user == null)
                {
                    ///Usuário não encontrados
                    return NotFound(new {status = false, msg = "Usuário não encontrado"});
                } else {
                    //Verifica se a senha esta correta
                    Hash hash = new Hash();
                    if (hash.HashVerifyPassword(user, auth.Password))
                    {
                        var token = await new JWT().CreateTokenJWT(user);
                        return Ok(new {status = true, token = token, msg = "Login feito com sucesso!!!"});
                    } else 
                    {
                        return Ok(new {status = false, msg = "Senha incorreta"});
                    }
                }
            } else 
            {
                //Request errado
                return BadRequest();
            }
        }
    }
}