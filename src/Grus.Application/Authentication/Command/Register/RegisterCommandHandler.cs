using Grus.Application.Authentication.Common;
using Grus.Application.Common.Interfaces.Authentication;
using Grus.Domain.Common.Errors;
using Grus.Domain.Entities.User;

namespace Grus.Application.Authentication.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // check if user already exists
        if (await _userRepository.GetByEmail(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        // create user (generate unique ID)

        var user = new User
        {
            Email = request.Email,
            Password = request.Password
        };

        await _userRepository.Add(user);

        //create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}