using Assessment__2.Enum;
using Assessment__2.Services;

namespace Assessment__2.Models;

public class AuthenticationResult
{
    private readonly ILoginMessageService _loginMessages;

    public AuthenticationResult(ILoginMessageService loginMessages)
    {
        _loginMessages = loginMessages;
    }

    public bool IsSuccess { get; set; }
    public User? User { get; set; }
    public LoginError? Error { get; set; }
    public int RemainingAttempts { get; set; }

    public string Message => Error.HasValue 
        ? _loginMessages.GetMessage(Error.Value)
        : IsSuccess && User != null 
            ? $"Welcome, {User.Username}!" 
            : _loginMessages.GetMessage(LoginError.InvalidCredentials);
} 