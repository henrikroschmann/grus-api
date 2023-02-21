namespace Grus.Infrastructure.Services;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        _database = client.GetDatabase(configuration["MongoDb:Database"]);
    }

    //public IMongoCollection<StockEntity> Stocks => _database.GetCollection<StockEntity>("Stocks");
    //public IMongoCollection<PortfolioEntity> PortfolioStocks => _database.GetCollection<PortfolioEntity>("PortfolioStocks");
    public IMongoCollection<string> Symbols => _database.GetCollection<string>("Symbols");
}