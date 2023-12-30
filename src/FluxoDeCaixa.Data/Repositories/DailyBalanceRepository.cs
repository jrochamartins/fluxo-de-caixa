using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using MongoDB.Driver;

namespace FluxoDeCaixa.Data.Repositories
{
    public class DailyBalanceRepository(DbContext context) :
        IBalanceRepository
    {
        public async Task<Balance?> GetAsync(DateOnly date)
        {
            DateTime dateFilter = date.ToDateTime(TimeOnly.Parse("00:00 AM"));
            var filterDefinition = Builders<Balance>.Filter.Where(e => e.Date == date);
            var searchResult = await context.DailyBalance.FindAsync(filterDefinition);
            return searchResult.FirstOrDefault();
        }

        public async Task<Balance> CreateUpdateAsync(Balance balance)
        {
            if (!balance.IsNew)
            {
                var filterDefinition = Builders<Balance>.Filter.Where(e => e.Id == balance.Id);
                var result = await context.DailyBalance.FindOneAndReplaceAsync(filterDefinition, balance);
            }
            else
                await context.DailyBalance.InsertOneAsync(balance);
            return balance;
        }
    }
}
