using Grus.Application.Authentication.Common;

namespace Grus.Application.Authentication.Command.Register;

public record RegisterCommand(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;