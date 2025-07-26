using Assessment__2.Model;

namespace Assessment__2.Repository.Impl;

// Declaring class implementing user repository interface
public class UserRepository : IUserRepository
{
    // Declaring private list for storing users with initial data
    private readonly List<User> _users = new()
    {
        // Creating default user for testing
        new User
        {
            Username = "Joe.Doe",
            Password = "Password123",
            Email = "joe.doe@example.com",
            Age = 30,
            Phone = "123-456-7890"
        }
    };

    // Implementing method for retrieving user by username
    public User? GetByUsername(string username)
    {
        // Returning user matching username with case-insensitive comparison
        return _users.FirstOrDefault(user => 
            user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    // Implementing method for adding new user
    public bool AddUser(User user)
    {
        // Checking if username already exists
        if (IsUsernameExists(user.Username))
        {
            // Returning false if username exists
            return false;
        }

        // Adding user to collection
        _users.Add(user);
        // Returning true for successful addition
        return true;
    }

    // Implementing method for checking username existence
    public bool IsUsernameExists(string username)
    {
        // Returning true if any user matches username with case-insensitive comparison
        return _users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
} 