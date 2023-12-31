using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Services
{
    public interface IBalanceService
    {
        Task CalculateAsync(Entry entry);
    }
}
