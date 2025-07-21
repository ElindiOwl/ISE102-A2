using Assessment__2.Models;

namespace Assessment__2.Services;

public interface IAuthenticationService
{
    AuthenticationResult Login(string username, string password);
    bool IsLoginAttemptsExceeded();
    void ResetLoginAttempts();
} 