using Assessment__2.Configuration;
using Assessment__2.Enum;

namespace Assessment__2.Service.Impl;

// Declaring class implementing login message service with constructor injection
public class LoginMessageService(LoginConfig config) : ILoginMessageService
{
    // Declaring private field for logic
    private readonly LoginConfig _config = config;

    // Implementing method for getting error message by error type with simplified switch
    public string GetMessage(LoginError error) => error switch
    {
        LoginError.InvalidCredentials => "Invalid username or password",
        // Returning message for exceeded attempts with lockout duration
        LoginError.MaxAttemptsExceeded => $"Maximum login attempts exceeded. Please try again after {_config.LockoutDurationMinutes} minutes.",
        LoginError.UsernameEmpty => "Username cannot be empty",
        LoginError.PasswordEmpty => "Password cannot be empty",
        // Returning default message for unknown errors
        _ => "Unknown login error"
    };
} 