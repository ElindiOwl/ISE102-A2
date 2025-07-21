using Assessment__2.Configuration;
using Assessment__2.Models;
using Assessment__2.Enum;

namespace Assessment__2.Services;

public class AuthenticationService(
    IUserService userService, 
    LoginConfig loginConfig,
    ILoginMessageService loginMessages
) : IAuthenticationService
{
    private readonly IUserService _userService = userService;
    private readonly LoginConfig _loginConfig = loginConfig;
    private readonly ILoginMessageService _loginMessages = loginMessages;
    private int _loginAttempts;
    private DateTime? _lockoutUntil;

    public AuthenticationResult Login(string username, string password)
    {
        if (_lockoutUntil.HasValue && DateTime.Now < _lockoutUntil.Value)
        {
            return new AuthenticationResult(_loginMessages)
            {
                IsSuccess = false,
                Error = LoginError.MaxAttemptsExceeded,
                RemainingAttempts = 0
            };
        }

        if (IsLoginAttemptsExceeded())
        {
            _lockoutUntil = DateTime.Now.AddMinutes(_loginConfig.LockoutDurationMinutes);
            return new AuthenticationResult(_loginMessages)
            {
                IsSuccess = false,
                Error = LoginError.MaxAttemptsExceeded,
                RemainingAttempts = 0
            };
        }

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return new AuthenticationResult(_loginMessages)
            {
                IsSuccess = false,
                Error = LoginError.EmptyCredentials,
                RemainingAttempts = _loginConfig.MaxLoginAttempts - _loginAttempts
            };
        }

        var user = _userService.AuthenticateUser(username, password);
        
        if (user != null)
        {
            ResetLoginAttempts();
            return new AuthenticationResult(_loginMessages)
            {
                IsSuccess = true,
                User = user,
                RemainingAttempts = _loginConfig.MaxLoginAttempts
            };
        }
        
        _loginAttempts++;
        return new AuthenticationResult(_loginMessages)
        {
            IsSuccess = false,
            Error = _loginAttempts >= _loginConfig.MaxLoginAttempts 
                ? LoginError.MaxAttemptsExceeded 
                : LoginError.InvalidCredentials,
            RemainingAttempts = _loginConfig.MaxLoginAttempts - _loginAttempts
        };
    }

    public bool IsLoginAttemptsExceeded()
    {
        return _loginAttempts >= _loginConfig.MaxLoginAttempts;
    }

    public void ResetLoginAttempts()
    {
        _loginAttempts = 0;
        _lockoutUntil = null;
    }
} 