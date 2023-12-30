using FluxoDeCaixa.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FluxoDeCaixa.Data
{
    public class DbContext(IOptionsMonitor<DbContextOptions> options)
    {
        private readonly IMongoDatabase _database = new MongoClient(options.CurrentValue.MONGO_CONNECTION_STRING).GetDatabase(options.CurrentValue.MONGO_DATABASE);

        internal IMongoCollection<Entry> Entries => _database.GetCollection<Entry>(nameof(Entries).ToLower());

        internal IMongoCollection<Balance> DailyBalances => _database.GetCollection<Balance>(nameof(DailyBalances).ToLower());
    }
}
