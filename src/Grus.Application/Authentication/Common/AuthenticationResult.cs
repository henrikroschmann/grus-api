using Grus.Domain.Entities.User;

namespace Grus.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);