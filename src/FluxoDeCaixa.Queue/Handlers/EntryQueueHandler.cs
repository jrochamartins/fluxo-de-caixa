using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Queue.Handlers
{
    public class EntryQueueHandler(IBalanceService balanceService) : BaseQueueHandler<Entry>
    {
        public override void Handle(Entry payload) =>
            balanceService.CalculateAsync(payload);
    }
}
