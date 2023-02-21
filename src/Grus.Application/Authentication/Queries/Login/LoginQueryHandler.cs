using ErrorOr;
using Grus.Application.Authentication.Common;
using Grus.Application.Common.Interfaces.Authentication;
using Grus.Application.Common.Interfaces.Persistence;
using Grus.Domain.Common.Errors;
using MediatR;

namespace Grus.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // 1. Validate the user does exists
        if (await _userRepository.GetByEmail(request.Email) is not { } user)
        {
            return Errors.Authentication.InvalidPassword;
        }
        // 2. Validate that the password is correct
        if (user.Password != request.Password)
        {
            return Errors.Authentication.InvalidPassword;
        }
        // 3. Create the token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}