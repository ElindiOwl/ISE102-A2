using Assessment__2.Configuration;
using Assessment__2.Model;
using Assessment__2.Enum;

namespace Assessment__2.Service;

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
        if (_lockoutUntil.HasValue && DateTime.Now >= _lockoutUntil.Value)
        {
            _loginAttempts = 0;
            _lockoutUntil = null;
        }

        if (_lockoutUntil.HasValue && DateTime.Now < _lockoutUntil.Value)
        {
            return new AuthenticationResult
            {
                IsSuccess = false,
                Error = LoginError.MaxAttemptsExceeded,
                RemainingAttempts = 0,
                Message = _loginMessages.GetMessage(LoginError.MaxAttemptsExceeded)
            };
        }

        if (IsLoginAttemptsExceeded())
        {
            _lockoutUntil = DateTime.Now.AddMinutes(_loginConfig.LockoutDurationMinutes);
            return new AuthenticationResult
            {
                IsSuccess = false,
                Error = LoginError.MaxAttemptsExceeded,
                RemainingAttempts = 0,
                Message = _loginMessages.GetMessage(LoginError.MaxAttemptsExceeded)
            };
        }

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return new AuthenticationResult
            {
                IsSuccess = false,
                Error = LoginError.EmptyCredentials,
                RemainingAttempts = _loginConfig.MaxLoginAttempts - _loginAttempts,
                Message = _loginMessages.GetMessage(LoginError.EmptyCredentials)
            };
        }

        var user = _userService.AuthenticateUser(username, password);
        
        if (user != null)
        {
            ResetLoginAttempts();
            return new AuthenticationResult
            {
                IsSuccess = true,
                User = user,
                RemainingAttempts = _loginConfig.MaxLoginAttempts,
                Message = $"Welcome, {user.Username}!"
            };
        }
        
        _loginAttempts++;

        var error = _loginAttempts >= _loginConfig.MaxLoginAttempts 
            ? LoginError.MaxAttemptsExceeded 
            : LoginError.InvalidCredentials;
            
        return new AuthenticationResult
        {
            IsSuccess = false,
            Error = error,
            RemainingAttempts = _loginConfig.MaxLoginAttempts - _loginAttempts,
            Message = _loginMessages.GetMessage(error)
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

    public AuthenticationResult? GetLockoutStatus()
    {
        if (_lockoutUntil.HasValue && DateTime.Now < _lockoutUntil.Value)
        {
            return new AuthenticationResult
            {
                IsSuccess = false,
                Error = LoginError.MaxAttemptsExceeded,
                RemainingAttempts = 0,
                Message = _loginMessages.GetMessage(LoginError.MaxAttemptsExceeded)
            };
        }
        return null;
    }
} 