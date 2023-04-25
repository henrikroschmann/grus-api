using Grus.Application.Authentication.Queries.Login;

namespace Grus.Controllers;

[Route("api/auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.Email, request.Password);
        var authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = new LoginQuery(request.Email, request.Password);
        var authResult = await _mediator.Send(command);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidPassword)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult) =>
        new(
        authResult.User.Id,
        authResult.User.Email,
        authResult.Token
        );
}