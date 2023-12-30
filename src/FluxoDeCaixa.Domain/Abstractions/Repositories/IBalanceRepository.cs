using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance?> GetAsync(DateOnly date);
        Task<Balance> CreateUpdateAsync(Balance balance);
    }
}
