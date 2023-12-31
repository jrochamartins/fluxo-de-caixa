using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;


namespace FluxoDeCaixa.Data.Repositories
{
    public class BalanceRepository(DbContext context, ILogger<BalanceRepository> _logger) : IBalanceRepository
    {
        public async Task<Balance?> GetByDateAsync(DateOnly date)
        {
            _logger.LogInformation("BalanceRepository.GetByDateAsync started");
            var filter = Builders<Balance>.Filter.Eq(e => e.Date, date);
            var result =  await context.DailyBalances.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task SaveAsync(Balance balance)
        {
            _logger.LogInformation("BalanceRepository.SaveAsync started");
            var filter = Builders<Balance>.Filter.Eq(e => e.Id, balance.Id);
            var options = new ReplaceOptions() { IsUpsert = true };
            var result = await context.DailyBalances.ReplaceOneAsync(filter, balance, options);
        }
    }
}
