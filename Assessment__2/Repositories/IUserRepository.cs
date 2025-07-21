using Assessment__2.Models;

namespace Assessment__2.Repositories;

public interface IUserRepository
{
    User? GetByUsername(string username);
    bool AddUser(User user);
    bool IsUsernameExists(string username);
} 