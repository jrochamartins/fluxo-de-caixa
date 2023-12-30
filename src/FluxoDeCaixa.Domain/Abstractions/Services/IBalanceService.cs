using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Services
{
    public interface IBalanceService
    {
        Task<Balance> CreateUpdateAsync(Entry entry);
    }
}
