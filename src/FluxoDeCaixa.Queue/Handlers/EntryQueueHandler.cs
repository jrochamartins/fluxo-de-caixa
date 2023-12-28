using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Services.Contracts;

namespace FluxoDeCaixa.Queue.Handlers
{
    public class EntryQueueHandler(IDailyBalanceService balanceService) : BaseQueueSubscriberHandler<Entry>
    {
        public override void Execute(Entry payload) =>
            balanceService.CreateUpdateAsync(payload);
    }
}
