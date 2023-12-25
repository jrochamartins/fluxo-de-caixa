using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Repositories;
using MongoDB.Driver;

namespace FluxoDeCaixa.Data.Repositories
{
    public class DailyBalanceRepository(DbContext context) :
        IDailyBalanceRepository
    {
        private readonly DbContext _context = context;

        public async Task<DailyBalance?> GetAsync(DateOnly date)
        {
            DateTime dateFilter = date.ToDateTime(TimeOnly.Parse("00:00 AM"));
            var filterDefinition = Builders<DailyBalance>.Filter.Where(e => e.Date == date);
            var searchResult = await _context.DailyBalance.FindAsync(filterDefinition);
            return searchResult.FirstOrDefault();
        }

        public async Task<DailyBalance> CreateUpdateAsync(DailyBalance balance)
        {
            if (!balance.IsNew())
            {
                var filterDefinition = Builders<DailyBalance>.Filter.Where(e => e.Id == balance.Id);
                var result = await _context.DailyBalance.FindOneAndReplaceAsync(filterDefinition, balance);
            }
            else
                await _context.DailyBalance.InsertOneAsync(balance);
            return balance;
        }
    }
}
