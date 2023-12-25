using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Services.Contracts
{
    public interface IEntriesService
    {
        Task CreateAsync(Entry entity);
    }
}