using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;

namespace FluxoDeCaixa.Data.Repositories
{
    public class EntriesRepository(DbContext context) : IEntriesRepository
    {
        private readonly DbContext _context = context;

        public async Task CreateAsync(Entry entity)
        {
            await _context.Entries.InsertOneAsync(entity);
        }
    }
}
