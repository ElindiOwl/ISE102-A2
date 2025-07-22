using Assessment__2.Model;

namespace Assessment__2.Repository;

public interface IUserRepository
{
    User? GetByUsername(string username);
    bool AddUser(User user);
    bool IsUsernameExists(string username);
} 