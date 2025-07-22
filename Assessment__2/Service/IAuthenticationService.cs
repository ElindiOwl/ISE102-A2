using Assessment__2.Model;

namespace Assessment__2.Service;

public interface IAuthenticationService
{
    AuthenticationResult Login(string username, string password);
}