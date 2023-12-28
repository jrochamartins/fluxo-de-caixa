using FluxoDeCaixa.Api.Configurations;
using Serilog;

namespace FluxoDeCaixa.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder
                .ConfigureDependencies()
                .ConfigureJwtAuthentication();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.UseRabbitListener();

            app.Run();
        }
    }
}
