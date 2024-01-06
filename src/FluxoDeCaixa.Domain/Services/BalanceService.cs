using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Validators;

namespace FluxoDeCaixa.Domain.Services
{
    public class BalanceService(INotifier notifier, IBalanceRepository balanceRepository) : BaseService(notifier), IBalanceService
    {
        public async Task CalculateAsync(Entry entry)
        {
            if (!Validate(new EntryValidator(), entry))
                return;

            var date = DateOnly.FromDateTime(entry.Date);

            var balance =
                await balanceRepository.GetByDateAsync(date) ??
                new Balance { Date = date };

            switch (entry.EntryType)
            {
                case EntryType.Credit:
                    balance.Credits += entry.Value;
                    break;
                case EntryType.Debt:
                    balance.Debts += entry.Value;
                    break;
                default:
                    return;
            }

            await balanceRepository.SaveAsync(balance);
        }
    }
}
