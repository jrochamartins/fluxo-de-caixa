using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Services
{
    public interface IEntriesService
    {
        Task CreateAsync(Entry entity);
    }
}