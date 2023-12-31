using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FluxoDeCaixa.Data.Repositories
{
    public class EntriesRepository(DbContext context, ILogger<EntriesRepository> _logger) : IEntriesRepository
    {
        public async Task CreateAsync(Entry entity)
        {
            _logger.LogInformation("EntriesRepository.CreateAsync started");
            await context.Entries.InsertOneAsync(entity);
        }
    }
}
