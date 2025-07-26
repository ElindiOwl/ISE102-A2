using Assessment__2.Model;
using Assessment__2.Repository;

namespace Assessment__2.Service.Impl;

// Declaring class implementing user service with constructor injection
public class UserService(IUserRepository userRepository) : IUserService
{
    // Declaring private field for user repository dependency
    private readonly IUserRepository _userRepository = userRepository;

    // Implementing method for authenticating user credentials
    public User? AuthenticateUser(string username, string password)
    {
        // Retrieving user by username
        var user = _userRepository.GetByUsername(username);
        
        // Checking if user exists and password matches
        if (user != null && user.Password == password)
        {
            // Returning authenticated user
            return user;
        }
        
        // Returning null for failed authentication
        return null;
    }

    // Implementing method for registering new user
    public bool RegisterUser(User user)
    {
        // Adding user to repository
        return _userRepository.AddUser(user);
    }

    // Implementing method for checking username uniqueness
    public bool IsUsernameUnique(string username)
    {
        // Returning true if username doesn't exist
        return !_userRepository.IsUsernameExists(username);
    }
} 