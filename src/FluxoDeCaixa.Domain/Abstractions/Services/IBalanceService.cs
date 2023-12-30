using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Services
{
    public interface IBalanceService
    {
        Task<Balance> CalculateAsync(Entry entry);
    }
}
