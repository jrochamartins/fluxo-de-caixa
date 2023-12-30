using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using MongoDB.Driver;

namespace FluxoDeCaixa.Data.Repositories
{
    public class BalanceRepository(DbContext context) : IBalanceRepository
    {
        public async Task<Balance?> GetByDateAsync(DateOnly date)
        {
            var filter = Builders<Balance>.Filter.Eq(e => e.Date, date);
            var result =  await context.DailyBalances.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task SaveAsync(Balance balance)
        {
            var filter = Builders<Balance>.Filter.Eq(e => e.Id, balance.Id);
            var options = new ReplaceOptions() { IsUpsert = true };
            var result = await context.DailyBalances.ReplaceOneAsync(filter, balance, options);
        }
    }
}
