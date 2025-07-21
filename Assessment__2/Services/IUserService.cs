using Assessment__2.Models;

namespace Assessment__2.Services;

public interface IUserService
{
    User? AuthenticateUser(string username, string password);
    bool RegisterUser(User user);
    bool IsUsernameUnique(string username);
} 