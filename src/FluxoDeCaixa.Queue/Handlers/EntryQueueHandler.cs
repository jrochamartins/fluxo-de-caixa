using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FluxoDeCaixa.Queue.Handlers
{
    public class EntryQueueHandler(IBalanceService balanceService, ILogger<EntryQueueHandler> _logger) : BaseQueueHandler<Entry>
    {
        public override void Handle(Entry payload)
        {
            _logger.LogInformation("EntryQueueHandler.Handle started");
            balanceService.CalculateAsync(payload);
        }
    }
}
