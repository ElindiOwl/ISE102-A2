using Assessment__2.Configuration;
using Assessment__2.Enum;

namespace Assessment__2.Services;

public class LoginMessageService(LoginConfig config) : ILoginMessageService
{
    private readonly LoginConfig _config = config;

    public string GetMessage(LoginError error) => error switch
    {
        LoginError.InvalidCredentials => "Invalid username or password",
        LoginError.MaxAttemptsExceeded => $"Maximum login attempts exceeded. Please try again after {_config.LockoutDurationMinutes} minutes.",
        LoginError.EmptyCredentials => "Username and password cannot be empty",
        _ => "Unknown login error"
    };
} 