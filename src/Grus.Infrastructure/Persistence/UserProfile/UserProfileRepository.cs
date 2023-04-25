using userEntity = Grus.Domain.Entities.User;

namespace Grus.Infrastructure.Persistence.UserProfile;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly IMongoCollection<userEntity.UserProfile> _userProfiles;

    public UserProfileRepository(MongoDbContext dbContext)
    {
        _userProfiles = dbContext.UserProfiles;
    }

    public async Task<userEntity.UserProfile?> GetById(Guid id)
    {
        var userProfile = await _userProfiles.FindAsync(x => x.Id == id);
        return userProfile.FirstOrDefault();
    }

    public async Task Add(userEntity.UserProfile userProfile)
    {
        await _userProfiles.InsertOneAsync(userProfile);
    }

    public async Task DeleteOne(userEntity.UserProfile userProfile)
    {
        await _userProfiles.DeleteOneAsync(x => x.Email == userProfile.Email);
    }
}