using Grus.Domain.Entities.User;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Grus.Infrastructure.GraphQL;

public class Query
{
    public async Task<UserProfile> GetUserProfileAsync(
        [Service] MongoDbContext dbContext,
        [Service] IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("Invalid user token")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        var filter = Builders<UserProfile>.Filter.Eq(x => x.Id, userId);
        var user = await dbContext.UserProfiles.Find(filter).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("User not found")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        return user;
    }
}