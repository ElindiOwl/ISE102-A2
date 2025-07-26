using Assessment__2.Enum;

namespace Assessment__2.Service;

// Declaring interface for validation message service operations
public interface IValidationMessageService
{
    // Declaring method for getting error message by error type
    string GetMessage(ValidationError error);
}