using Grus.Domain.Entities.Budget;
using Grus.Domain.Entities.User;

namespace Grus.Infrastructure.Services;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        var mongoDB = configuration.GetSection("MongoDb");
        var mongoConfigs = mongoDB.GetChildren().ToList();

        var client = new MongoClient(mongoConfigs.FirstOrDefault(x => x.Key == "ConnectionString")?.Value);
        _database = client.GetDatabase(configuration["MongoDb:Database"]);
    }

    public IMongoCollection<Budget> Budgets => _database.GetCollection<Budget>("Budgets");
    public IMongoCollection<UserProfile> UserProfiles => _database.GetCollection<UserProfile>("UserProfiles");
    public IMongoCollection<User> User => _database.GetCollection<User>("users");
}
