using Assessment__2.Model;

namespace Assessment__2.Service;

// Declaring interface for user service operations
public interface IUserService
{
    // Declaring method for authenticating user credentials
    User? AuthenticateUser(string username, string password);
    // Declaring method for registering new user
    bool RegisterUser(User user);
    // Declaring method for checking username uniqueness
    bool IsUsernameUnique(string username);
} 