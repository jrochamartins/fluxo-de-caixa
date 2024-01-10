using FluxoDeCaixa.Data;
using FluxoDeCaixa.Data.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Adapters;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Notifications;
using FluxoDeCaixa.Domain.Services;
using FluxoDeCaixa.Queue;

namespace FluxoDeCaixa.Api.Configurations
{
    public static class DomainDependenciesConfiguration
    {
        public static WebApplicationBuilder AddDomainDependencies(this WebApplicationBuilder builder)
        {
            // Database & repositories
            builder.Services.Configure<DbContextOptions>(builder.Configuration);
            builder.Services.AddSingleton<DbContext>();
            builder.Services.AddScoped<IEntriesRepository, EntriesRepository>();
            builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();

            // Queue publisher
            builder.Services.AddScoped<IPublisher, Publisher>();

            //Domain services
            builder.Services.AddScoped<INotifier, Notifier>();
            builder.Services.AddScoped<IEntriesService, EntriesService>();
            builder.Services.AddScoped<IBalanceService, BalanceService>();
            
            return builder;
        }
    }
}
