using FluxoDeCaixa.Data;
using FluxoDeCaixa.Data.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Adapters;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Notifications;
using FluxoDeCaixa.Domain.Services;
using FluxoDeCaixa.Queue;
using MassTransit;

namespace FluxoDeCaixa.Api.Configurations
{
    public static class ServicesConfigurationExtensions
    {
        public static IHostApplicationBuilder ConfigureDependencies(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(o =>
                {
                    o.SuppressModelStateInvalidFilter = true;
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Options
            builder.Services.Configure<DbContextOptions>(builder.Configuration);

            // Infra
            builder.Services.AddScoped<INotifier, Notifier>();
            builder.Services.AddSingleton<DbContext>();

            //UseCases Dependencies
            // Add Entry
            builder.Services.AddScoped<IEntriesService, EntriesService>();
            builder.Services.AddScoped<IEntriesRepository, EntriesRepository>();
            builder.Services.AddScoped<IPublisher, Publisher>();

            // Create Balance
            builder.Services.AddScoped<IBalanceService, BalanceService>();
            builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();

            //Queues
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
