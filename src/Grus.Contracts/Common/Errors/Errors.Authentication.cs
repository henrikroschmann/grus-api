using ErrorOr;

namespace Grus.Contracts.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidPassword = Error.Conflict(code: "Auth.InvalidPassword", description: "Invalid password");
    }
}