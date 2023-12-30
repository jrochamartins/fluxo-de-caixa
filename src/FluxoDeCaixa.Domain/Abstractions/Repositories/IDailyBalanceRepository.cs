using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Repositories
{
    public interface IDailyBalanceRepository
    {
        Task<DailyBalance?> GetAsync(DateOnly date);
        Task<DailyBalance> CreateUpdateAsync(DailyBalance balance);
    }
}
