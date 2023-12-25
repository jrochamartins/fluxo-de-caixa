using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Services.Contracts
{
    public interface IDailyBalanceService
    {
        Task<DailyBalance> CreateUpdateAsync(Entry entry);
    }
}
