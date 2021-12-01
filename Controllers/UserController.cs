using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DAOContext _database;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _imapper;
        private readonly JWT _jwt;
        
        public UserController(DAOContext database, IMapper imapper)
        {
            _database = database;
            _imapper = imapper;
            _jwtSettings = new JwtSettings();
            _jwt = new JWT();
        }

        public bool ValidateEmail(string email)
        {
            var address = new System.Net.Mail.MailAddress(email);
            return address.Address == email;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<User> ListUserById([FromRoute]int id)
        {
            return await _database.User
                                .AsNoTracking()
                                .FirstOrDefaultAsync(u => u.Id == id);
        }


        [HttpPost("new")]
        public async Task<ActionResult> NewUser(UserRegister user){
            if(ModelState.IsValid)
            {
                if (await _database.User.FirstOrDefaultAsync(u => u.Email.ToLower() == user.Email.ToLower()) != null) {
                    return BadRequest(new {status = false, msg =  $"O E-Mail {user.Email} já esta cadastrado, por favor, utilize outro"});
                }

                if (user.CpfPis.Contains(".")) 
                {
                    while (user.CpfPis.Contains(".")) {
                        user.CpfPis = user.CpfPis.Replace(".", "");
                    }
                }

                if (user.CpfPis.Contains("-")) 
                {
                    while (user.CpfPis.Contains("-")) {
                        user.CpfPis = user.CpfPis.Replace("-", "");
                    }
                }

                if (!ValidateEmail(user.Email))
                {
                    return Ok(new {
                        status = false,
                        msg = "O E-mail informado não é válido!"
                    });
                }
                
                Hash hash = new Hash();
                hash.HashPassword(user);
                User newUser =_imapper.Map<User>(user);
                newUser.Password = user.Password;
                newUser.Hash = user.Hash;
                newUser.IsPis = user.IsPis == false ? "N" : "S";
                await _database.User.AddAsync(newUser);
                try {
                    await _database.SaveChangesAsync();
                    return Ok(new {
                        status = true,
                        msg = "Usuário cadastrado com sucesso!"
                    });
                } catch (Exception e) {
                    return BadRequest(new {
                        status = false,
                        msg = "Falha ao cadastrar usuário",
                        erro = e.Message
                    });
                }
            } else {
                return BadRequest(new {
                    status = false,
                    msg = "Por favor, preencha todos os campos"
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser([FromRoute]int id,[FromBody] User user){
            if (ModelState.IsValid)
            {
                if(id != user.Id) {
                    return NotFound();
                }

                if (user.CpfPis.Contains(".")) 
                {
                    while (user.CpfPis.Contains(".")) {
                        user.CpfPis = user.CpfPis.Replace(".", "");
                    }
                }

                if (user.CpfPis.Contains("-")) 
                {
                    while (user.CpfPis.Contains("-")) {
                        user.CpfPis = user.CpfPis.Replace("-", "");
                    }
                }

                if (!ValidateEmail(user.Email))
                {
                    return Ok(new {
                        status = false,
                        msg = "O E-mail informado não é válido!"
                    });
                }

                _database.Entry(user).State = EntityState.Modified;
                _database.Entry(user).Property(u => u.Hash).IsModified = false;
                _database.Entry(user).Property(u => u.Password).IsModified = false;

                try {
                    await _database.SaveChangesAsync();
                    return Ok(new {
                        status = true,
                        msg = "Usuário alterado com sucesso!"
                    });
                } catch (Exception e) {
                    return BadRequest(new {
                        status = false,
                        msg = "Falha ao alterar dados do usuário",
                        erro = e.Message
                    });
                }
            } else {
                return BadRequest(new {
                    status = false,
                    msg = "Por favor, preencha todos os campos obrigatórios"
                });
            }
        }
    }
}