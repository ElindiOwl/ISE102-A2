using Assessment__2.Configuration;
using Assessment__2.Model;
using Assessment__2.Enum;

namespace Assessment__2.Service.Impl;

// Declaring class implementing authentication service with constructor injection
public class AuthenticationService(
    IUserService userService, 
    LoginConfig loginConfig,
    ILoginMessageService loginMessages
) : IAuthenticationService
{
    // Declaring private fields for logic
    private readonly IUserService _userService = userService;
    private readonly LoginConfig _loginConfig = loginConfig;
    private readonly ILoginMessageService _loginMessages = loginMessages;
    // Declaring private field for tracking login attempts
    private int _loginAttempts;
    // Declaring private field for lockout expiration time
    private DateTime? _lockoutUntil;

    // Implementing method for user login authentication
    public AuthenticationResult Login(string username, string password)
    {
        // Checking if lockout period has expired
        if (_lockoutUntil.HasValue && DateTime.Now >= _lockoutUntil.Value)
        {
            // Resetting login attempts and lockout
            _loginAttempts = 0;
            _lockoutUntil = null;
        }

        // Checking if currently in lockout period
        if (_lockoutUntil.HasValue && DateTime.Now < _lockoutUntil.Value)
        {
            // Returning lockout result
            return new AuthenticationResult
            {
                IsSuccess = false,
                Error = LoginError.MaxAttemptsExceeded,
                RemainingAttempts = 0,
                Message = _loginMessages.GetMessage(LoginError.MaxAttemptsExceeded)
            };
        }

        // Checking if maximum login attempts exceeded
        if (IsLoginAttemptsExceeded())
        {
            // Setting lockout period
            _lockoutUntil = DateTime.Now.AddMinutes(_loginConfig.LockoutDurationMinutes);
            
            // Returning lockout result
            return new AuthenticationResult
            {
                IsSuccess = false,
                Error = LoginError.MaxAttemptsExceeded,
                RemainingAttempts = 0,
                Message = _loginMessages.GetMessage(LoginError.MaxAttemptsExceeded)
            };
        }

        // Attempting to authenticate user
        var user = _userService.AuthenticateUser(username, password);
        
        // Checking if authentication successful
        if (user != null)
        {
            // Resetting login attempts on success
            ResetLoginAttempts();
            // Returning successful authentication result
            return new AuthenticationResult
            {
                IsSuccess = true,
                User = user,
                RemainingAttempts = _loginConfig.MaxLoginAttempts,
                Message = $"Welcome, {user.Username}!"
            };
        }
        
        // Incrementing failed login attempts
        _loginAttempts++;

        // Determining error type based on attempt count
        var error = _loginAttempts >= _loginConfig.MaxLoginAttempts 
            ? LoginError.MaxAttemptsExceeded 
            : LoginError.InvalidCredentials;
            
        // Returning failed authentication result
        return new AuthenticationResult
        {
            IsSuccess = false,
            Error = error,
            RemainingAttempts = _loginConfig.MaxLoginAttempts - _loginAttempts,
            Message = _loginMessages.GetMessage(error)
        };
    }

    // Implementing method for checking if login attempts exceeded
    public bool IsLoginAttemptsExceeded()
    {
        // Returning true if attempts reach maximum
        return _loginAttempts >= _loginConfig.MaxLoginAttempts;
    }

    // Implementing method for resetting login attempts
    public void ResetLoginAttempts()
    {
        // Resetting attempt counter
        _loginAttempts = 0;
        // Clearing lockout period
        _lockoutUntil = null;
    }
} 