using Assessment__2.Model;

namespace Assessment__2.Repository.Impl;

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
        return _users.FirstOrDefault(user => 
            user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
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
        return _users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
} 