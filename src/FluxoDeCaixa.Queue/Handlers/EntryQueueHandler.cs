using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Queue.Handlers
{
    public class EntryQueueHandler(IDailyBalanceService balanceService) : BaseQueueHandler<Entry>
    {
        public override void Handle(Entry payload) =>
            balanceService.CreateUpdateAsync(payload);
    }
}
