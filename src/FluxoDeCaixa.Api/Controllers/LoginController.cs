using FluxoDeCaixa.Api.ViewModels;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FluxoDeCaixa.Api.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class LoginController(INotifier notifier, IConfiguration configuration) : MainController(notifier)
    {
        [HttpPost()]
        public ActionResult<string> Login([FromBody] PostLoginRequest request)
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

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            // TO DO: Aqui pode ser usado mecanismo de validação do usuário externo.
            if (jwtUser != request.User)
            {
                AddNotification("User invalid");
                return CustomResponse();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, request.User),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return CustomResponse(result: tokenHandler.WriteToken(token));
        }
    }
}
