using Microsoft.AspNetCore.Mvc;
using PrimeiraApi.Application.Services;

namespace PrimeiraApi.Controllers
{
    [ApiController]
    [Route("Api/Auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string userName, string passworld)
        {
            if (userName == "Miguel" && passworld =="12345") 
            {
                var token = TokenService.GenerateToken(new Domain.Model.User());

                return Ok(token);
            }
            return BadRequest("Errou no Nome de usuario ou Senha errada");
        }
    }
}
