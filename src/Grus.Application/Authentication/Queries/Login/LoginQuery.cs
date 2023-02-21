using ErrorOr;
using Grus.Application.Authentication.Common;
using MediatR;

namespace Grus.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;