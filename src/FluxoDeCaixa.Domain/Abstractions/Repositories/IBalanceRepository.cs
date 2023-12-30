using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance?> GetByDateAsync(DateOnly date);
        Task SaveAsync(Balance balance);
    }
}
