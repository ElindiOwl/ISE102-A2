// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Model;

namespace Assessment__2.Service;

// Declaring interface for authentication service operations
public interface IAuthenticationService
{
    // Declaring method for user login authentication
    AuthenticationResult Login(string username, string password);
}