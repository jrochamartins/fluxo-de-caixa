using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Services
{
    public class BalanceService(IBalanceRepository balanceRepository) :
        IBalanceService
    {
        public async Task<Balance> CalculateAsync(Entry entry)
        {
            var date = DateOnly.FromDateTime(entry.Date);
            var balance = await balanceRepository.GetByDateAsync(date)
                ?? new Balance() { Date = date };

            switch (entry.EntryType)
            {
                case EntryType.Credit:
                    balance.Credits += entry.Value;
                    break;
                case EntryType.Debt:
                    balance.Debts += entry.Value;
                    break;
            }

            await balanceRepository.SaveAsync(balance);
            return balance;
        }
    }
}
