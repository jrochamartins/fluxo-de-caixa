using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FluxoDeCaixa.Api.Configurations
{
    public static class JwtAuthenticationConfiguration
    {
        public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            var key = builder.Configuration["JWT_KEY"];
            ArgumentException.ThrowIfNullOrEmpty(key);

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                });

            return builder;
        }
    }
}
