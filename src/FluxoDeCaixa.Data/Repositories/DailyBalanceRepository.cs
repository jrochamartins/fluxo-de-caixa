using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using MongoDB.Driver;

namespace FluxoDeCaixa.Data.Repositories
{
    public class DailyBalanceRepository(DbContext context) :
        IDailyBalanceRepository
    {
        public async Task<DailyBalance?> GetAsync(DateOnly date)
        {
            DateTime dateFilter = date.ToDateTime(TimeOnly.Parse("00:00 AM"));
            var filterDefinition = Builders<DailyBalance>.Filter.Where(e => e.Date == date);
            var searchResult = await context.DailyBalance.FindAsync(filterDefinition);
            return searchResult.FirstOrDefault();
        }

        public async Task<DailyBalance> CreateUpdateAsync(DailyBalance balance)
        {
            if (!balance.IsNew())
            {
                var filterDefinition = Builders<DailyBalance>.Filter.Where(e => e.Id == balance.Id);
                var result = await context.DailyBalance.FindOneAndReplaceAsync(filterDefinition, balance);
            }
            else
                await context.DailyBalance.InsertOneAsync(balance);
            return balance;
        }
    }
}
