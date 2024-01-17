using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FluxoDeCaixa.Data.Repositories
{
    public class EntriesRepository(DbContext context, ILogger<EntriesRepository> logger) : IEntriesRepository
    {
        public async Task CreateAsync(Entry entity)
        {
            logger.LogInformation("{Object}.{Method} started", nameof(EntriesRepository), nameof(CreateAsync));
            
            await context.Entries.InsertOneAsync(entity);
        }
    }
}
