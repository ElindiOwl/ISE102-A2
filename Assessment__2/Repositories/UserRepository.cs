using Assessment__2.Models;

namespace Assessment__2.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User
        {
            Username = "Joe.Doe",
            Password = "Password123",
            Email = "joe.doe@example.com",
            Age = 30,
            Phone = "123-456-7890"
        }
    };

    public User? GetByUsername(string username)
    {
        return _users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public bool AddUser(User user)
    {
        if (IsUsernameExists(user.Username))
        {
            return false;
        }

        _users.Add(user);
        return true;
    }

    public bool IsUsernameExists(string username)
    {
        return _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
} 