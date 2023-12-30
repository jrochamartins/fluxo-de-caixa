using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Services
{
    public interface IDailyBalanceService
    {
        Task<DailyBalance> CreateUpdateAsync(Entry entry);
    }
}
