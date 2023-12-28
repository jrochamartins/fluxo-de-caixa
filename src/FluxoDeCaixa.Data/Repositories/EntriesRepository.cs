using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;

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
