using Assessment__2.Enum;

namespace Assessment__2.Service;

// Declaring interface for system message service operations
public interface ISystemMessageService
{
    // Declaring method for getting error message by error type
    string GetMessage(SystemError error);
} 