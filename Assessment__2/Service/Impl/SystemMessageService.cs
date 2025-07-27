// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Enum;

namespace Assessment__2.Service.Impl;

// Declaring class implementing system message service
public class SystemMessageService : ISystemMessageService
{
    // Implementing method for getting error message by error type with simplified switch
    public string GetMessage(SystemError error) => error switch
    {
        SystemError.InvalidIntegerInput => "Input is not a valid number",
        // Returning default message for unknown errors
        _ => "Unknown system error"
    };
} 