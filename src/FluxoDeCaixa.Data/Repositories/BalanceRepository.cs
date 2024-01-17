using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;


namespace FluxoDeCaixa.Data.Repositories
{
    public class BalanceRepository(DbContext context, ILogger<BalanceRepository> logger) : IBalanceRepository
    {
        public async Task<Balance?> GetByDateAsync(DateOnly date)
        {
            logger.LogInformation("{Object}.{Method} started", nameof(BalanceRepository), nameof(GetByDateAsync));

            var filter = Builders<Balance>.Filter.Eq(e => e.Date, date);
            var result =  await context.DailyBalances.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task SaveAsync(Balance balance)
        {
            logger.LogInformation("{Object}.{Method} started", nameof(BalanceRepository), nameof(SaveAsync));

            var filter = Builders<Balance>.Filter.Eq(e => e.Id, balance.Id);
            var options = new ReplaceOptions() { IsUpsert = true };
            var result = await context.DailyBalances.ReplaceOneAsync(filter, balance, options);
        }
    }
}
