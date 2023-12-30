using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Data.Repositories
{
    public class EntriesRepository(DbContext context) : IEntriesRepository
    {
        public async Task CreateAsync(Entry entity)
        {
            await context.Entries.InsertOneAsync(entity);
        }
    }
}
