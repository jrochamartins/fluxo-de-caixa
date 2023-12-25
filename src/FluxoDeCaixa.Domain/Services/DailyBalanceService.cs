using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.Services.Contracts;

namespace FluxoDeCaixa.Domain.Services
{
    public class DailyBalanceService(
        IDailyBalanceRepository balanceRepository) :
        IDailyBalanceService
    {
        private readonly IDailyBalanceRepository _balanceRepository = balanceRepository;

        public async Task<DailyBalance> CreateUpdateAsync(Entry entry)
        {
            var date = DateOnly.FromDateTime(entry.Date);
            var balance = await _balanceRepository.GetAsync(date)
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

            return await _balanceRepository.CreateUpdateAsync(balance);
        }
    }
}
