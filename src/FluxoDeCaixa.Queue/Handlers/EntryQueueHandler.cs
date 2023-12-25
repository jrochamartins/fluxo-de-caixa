using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Services.Contracts;

namespace FluxoDeCaixa.Queue.Handlers
{
    public class EntryQueueHandler(IDailyBalanceService balanceService) : BaseQueueSubscriberHandler<Entry>
    {
        private readonly IDailyBalanceService _balanceService = balanceService;

        public override void Execute(Entry payload) =>
            _balanceService.CreateUpdateAsync(payload);
    }
}
