using userEntity = Grus.Domain.Entities.User;

namespace Grus.Infrastructure.Persistence.User;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<userEntity.User> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<userEntity.User>("users");
    }

    public async Task<userEntity.User?> GetByEmail(string email)
    {
        var user = await _users.FindAsync(x => x.Email == email);
        return user.FirstOrDefault();
    }

    public async Task Add(userEntity.User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task DeleteOne(userEntity.User user)
    {
        await _users.DeleteOneAsync(x => x.Email == user.Email);
    }
}