using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace desafio_pontotel_backend.Controllers
{
    [Route("api/v1/cep")]
    [ApiController]
    public class CEPController : ControllerBase
    {
        private static string route = "https://www.cepaberto.com/api/v3/cep?cep=";

        [HttpGet("{cep}")]
        public async Task<ActionResult<Object>> ListaTarefaById([FromRoute]string cep)
        {
            TimeSpan timeOut = TimeSpan.FromSeconds(20);
            try {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage resposta;
                    client.DefaultRequestHeaders.Add("Authorization", "Token token=ff891c5d4b862780070df60ad3a11d40");
                    client.Timeout = timeOut;
                    resposta = await client.GetAsync(route + cep);
                    var resultado = await resposta.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject(resultado);
                    return json;
                }
            } catch (Exception e) {
                return BadRequest(new {
                    status = false,
                    msg = "Não foi possível obter dados do CEP",
                    erro = e.Message
                });
            }
        }
    }
}