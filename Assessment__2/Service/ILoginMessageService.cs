using Assessment__2.Enum;

namespace Assessment__2.Service;
 
// Declaring interface for login message service operations
public interface ILoginMessageService
{
    // Declaring method for getting error message by error type
    string GetMessage(LoginError error);
} 