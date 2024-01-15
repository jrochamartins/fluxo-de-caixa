using FluxoDeCaixa.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FluxoDeCaixa.Api.Controllers
{
    public static class LoginEndpoints
    {
        public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api")
                .WithOpenApi();

            group.MapPost("login", Login)
                .WithName(nameof(Login));
        }

        private static Ok<string> Login(
            [FromBody] PostLoginRequest request,
            IConfiguration configuration,
            ILogger<LoginController> logger)
        {
            logger.LogInformation("HTTP POST /Login started");

            var key = configuration["JWT_KEY"];
            //var jwtUser = configuration["JWT_USER"];
            
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
            return TypedResults.Ok(tokenHandler.WriteToken(token));
        }
    }
}
