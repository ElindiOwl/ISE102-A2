// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Model;

namespace Assessment__2.Repository;

// Declaring interface for user repository operations
public interface IUserRepository
{
    // Declaring method for retrieving user by username
    User? GetByUsername(string username);
    // Declaring method for adding new user
    bool AddUser(User user);
    // Declaring method for checking username existence
    bool IsUsernameExists(string username);
} 