using Assessment__2.Configuration;
using Assessment__2.Enum;

namespace Assessment__2.Service.Impl;

// Declaring class implementing validation message service with constructor injection
public class ValidationMessageService(ValidationConfig config) : IValidationMessageService
{
    // Declaring private field for validation configuration
    private readonly ValidationConfig _config = config;

    // Implementing method for getting error message by error type
    public string GetMessage(ValidationError error) => error switch
    {
        ValidationError.UsernameRequired => "Username is required",
        ValidationError.UsernameExists => "Username already exists. Please choose a different username",
        ValidationError.EmailRequired => "Email is required",
        ValidationError.AgeInvalid => "Age must be between 1 and 120",
        ValidationError.PhoneRequired => "Phone number is required",
        ValidationError.PasswordRequired => "Password is required",
        // Returning message for password too short with minimum length
        ValidationError.PasswordTooShort => $"Password must be at least {_config.MinPasswordLength} characters long",
        ValidationError.RegistrationFailed => "Registration failed. Please try again",
        ValidationError.UnexpectedError => "An unexpected error occurred. Please try again",
        // Returning default message for unknown errors
        _ => "Unknown validation error"
    };
} 