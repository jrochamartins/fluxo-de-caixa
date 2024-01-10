using FluxoDeCaixa.Queue;
using MassTransit;

namespace FluxoDeCaixa.Api.Configurations
{
    public static class MassTransitConfiguration
    {
        public static WebApplicationBuilder AddMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumer<EntryConsumer>();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(builder.Configuration["RABBITMQ_HOST"], "/");
                    configurator.ConfigureEndpoints(context);
                });
            });

            return builder;
        }
    }
}
