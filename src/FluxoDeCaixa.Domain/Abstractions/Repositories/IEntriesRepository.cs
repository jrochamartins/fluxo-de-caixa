using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Abstractions.Repositories
{
    public interface IEntriesRepository
    {
        Task CreateAsync(Entry entity);
    }
}
