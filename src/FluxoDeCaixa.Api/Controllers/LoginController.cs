using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("[controller]")]
    public class LoginController(INotifier notifier, ILogger<LoginController> logger, IConfiguration configuration) : MainController(notifier)
    {
        [HttpPost()]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] LoginPostRequest request)
        {
            var key = configuration["JWT_KEY"];
            if (string.IsNullOrEmpty(key))
            {
                AddNotification("JWT key is not configured.");
                return CustomResponse();
            }

            var jwtUser = configuration["JWT_USER"];
            if (string.IsNullOrEmpty(jwtUser))
            {
                AddNotification("JWT user is not configured.");
                return CustomResponse();
            }

            // TO DO: Aqui pode ser usado mecanismo de validação do usuário externo.
            if (jwtUser != request.User)
            {
                AddNotification("User invalid");
                return CustomResponse();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, request.User),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return CustomResponse(result: tokenHandler.WriteToken(token));
        }
    }
}
