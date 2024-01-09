using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FluxoDeCaixa.Queue
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EntryConsumer(IBalanceService balanceService, ILogger<EntryConsumer> logger) : IConsumer<Entry>
    {
        public async Task Consume(ConsumeContext<Entry> context)
        {
            logger.LogInformation("EntryQueueHandler.Handle started");
            await balanceService.CalculateAsync(context.Message);
        }
    }
}
