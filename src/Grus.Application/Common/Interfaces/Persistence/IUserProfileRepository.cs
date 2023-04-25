using Grus.Domain.Entities.User;

namespace Grus.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetByEmail(string Email);

    Task Add(User user);
}