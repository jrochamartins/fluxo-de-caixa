using FluxoDeCaixa.Api.Configurations;
using FluxoDeCaixa.Api.Middlewares;
using Serilog;

namespace FluxoDeCaixa.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            
            builder.Services
                .AddHealthChecks();

            builder.AddDomainDependencies();
            builder.AddJwtAuthentication();
            builder.AddAddAutoMapper();
            builder.AddMassTransit();
            builder.AddSwaggerGen();
            builder.AddSerilog();
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<RequestLogContextMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");
            
            app.Run();
        }
    }
}
