using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Repositories
{
    public interface IEntriesRepository
    {
        Task CreateAsync(Entry entity);
    }
}
