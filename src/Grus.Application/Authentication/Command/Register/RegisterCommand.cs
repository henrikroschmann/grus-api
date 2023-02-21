using ErrorOr;
using Grus.Application.Authentication.Common;
using MediatR;

namespace Grus.Application.Authentication.Command.Register;

public record RegisterCommand(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;