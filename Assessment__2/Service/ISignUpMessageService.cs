// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Enum;

namespace Assessment__2.Service;

// Declaring interface for signup message service operations
public interface ISignUpMessageService
{
    // Declaring method for getting error message by error type
    string GetMessage(SignUpError error);
}