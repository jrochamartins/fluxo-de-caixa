using Serilog;

namespace FluxoDeCaixa.Api.Configurations
{
    public static class SerilogConfiguration
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            return builder;
        }
    }
}
