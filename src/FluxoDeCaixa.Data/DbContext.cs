using FluxoDeCaixa.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FluxoDeCaixa.Data
{
    public class DbContext
    {
        private readonly DbContextOptions _options;
        private readonly IMongoDatabase _database;

        public DbContext(IOptionsMonitor<DbContextOptions> optionsAccessor)
        {
            _options = optionsAccessor.CurrentValue;
            _database = DatabaseFactory(_options.MONGO_CONNECTION_STRING, _options.MONGO_DATABASE);
        }

        private static IMongoDatabase DatabaseFactory(string? databaseConnection, string? databaseName) =>
            new MongoClient(databaseConnection).GetDatabase(databaseName);

        internal IMongoCollection<Entry> Entries
            => _database.GetCollection<Entry>(nameof(Entries).ToLower());

        internal IMongoCollection<Balance> DailyBalance
            => _database.GetCollection<Balance>(nameof(DailyBalance).ToLower());
    }
}
