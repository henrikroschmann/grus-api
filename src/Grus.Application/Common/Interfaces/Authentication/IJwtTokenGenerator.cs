using Grus.Domain.Entities.User;

namespace Grus.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}