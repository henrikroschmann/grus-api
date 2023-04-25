using Grus.Domain.Entities.User;

namespace Grus.Application.Common.Interfaces.Persistence;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetById(Guid id);

    Task Add(UserProfile userProfile);
}