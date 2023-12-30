using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Abstractions.Services;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Services
{
    public class DailyBalanceService(IDailyBalanceRepository balanceRepository) :
        IDailyBalanceService
    {
        public async Task<DailyBalance> CreateUpdateAsync(Entry entry)
        {
            var date = DateOnly.FromDateTime(entry.Date);
            var balance = await balanceRepository.GetAsync(date)
                ?? new DailyBalance() { Date = date };

            switch (entry.EntryType)
            {
                case EntryType.Credit:
                    balance.Credits += entry.Value;
                    break;
                case EntryType.Debt:
                    balance.Debts += entry.Value;
                    break;
            }

            return await balanceRepository.CreateUpdateAsync(balance);
        }
    }
}
