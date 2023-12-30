using FluxoDeCaixa.Data;
using FluxoDeCaixa.Data.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Adapters;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Notifications;
using FluxoDeCaixa.Domain.Services;
using FluxoDeCaixa.Queue;
using FluxoDeCaixa.Queue.Handlers;

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
            builder.Services.Configure<QueueContextOptions>(builder.Configuration);

            // Infra
            builder.Services.AddScoped<INotifier, Notifier>();
            builder.Services.AddSingleton<DbContext>();
            builder.Services.AddSingleton<QueueContext>();
            builder.Services.AddSingleton<IQueueSubscriber, QueueSubscriber>();

            //UseCases Dependencies
            // Add Entry
            builder.Services.AddScoped<IEntriesService, EntriesService>();
            builder.Services.AddScoped<IEntriesRepository, EntriesRepository>();
            builder.Services.AddScoped<IQueuePublisher, QueuePublisher>();

            // Create Balance
            builder.Services.AddScoped<EntryQueueHandler>();
            builder.Services.AddScoped<IBalanceService, DailyBalanceService>();
            builder.Services.AddScoped<IBalanceRepository, DailyBalanceRepository>();

            return builder;
        }
    }
}
