using Assessment__2.Model;

namespace Assessment__2.Service;

public interface IAuthenticationService
{
    bool IsLoginAttemptsExceeded();
    void ResetLoginAttempts();
    AuthenticationResult Login(string username, string password);
    AuthenticationResult? GetLockoutStatus();
} 