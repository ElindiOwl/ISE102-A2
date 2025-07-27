// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Configuration;
using Assessment__2.Enum;

namespace Assessment__2.Service.Impl;

// Declaring class implementing sign-up message service with constructor injection
public class SignUpMessageService(SignUpConfig config) : ISignUpMessageService
{
    // Declaring private field for validation configuration
    private readonly SignUpConfig _config = config;

    // Implementing method for getting error message by error type
    public string GetMessage(SignUpError error) => error switch
    {
        SignUpError.UsernameRequired => "Username is required",
        SignUpError.UsernameExists => "Username already exists. Please choose a different username",
        // Returning message for username too short with minimum length
        SignUpError.UsernameTooShort => $"Username must be at least {_config.MinUsernameLength} characters long",
        SignUpError.EmailRequired => "Email is required",
        SignUpError.AgeInvalid => "Age must be between 1 and 120",
        SignUpError.PhoneRequired => "Phone number is required",
        SignUpError.PasswordRequired => "Password is required",
        // Returning message for password too short with minimum length
        SignUpError.PasswordTooShort => $"Password must be at least {_config.MinPasswordLength} characters long",
        SignUpError.RegistrationFailed => "Registration failed. Please try again",
        // Returning default message for unknown errors
        _ => "Unknown sign-up error"
    };
} 