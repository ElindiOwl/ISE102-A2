using Assessment__2.Configuration;
using Assessment__2.Enum;

namespace Assessment__2.Service;

public class ValidationMessageService(ValidationConfig config) : IValidationMessageService
{
    private readonly ValidationConfig _config = config;

    public string GetMessage(ValidationError error) => error switch
    {
        ValidationError.UsernameRequired => "Username is required",
        ValidationError.UsernameExists => "Username already exists. Please choose a different username",
        ValidationError.EmailRequired => "Email is required",
        ValidationError.AgeInvalid => "Age must be between 1 and 120",
        ValidationError.PhoneRequired => "Phone number is required",
        ValidationError.PasswordRequired => "Password is required",
        ValidationError.PasswordTooShort => $"Password must be at least {_config.MinPasswordLength} characters long",
        ValidationError.RegistrationFailed => "Registration failed. Please try again",
        ValidationError.UnexpectedError => "An unexpected error occurred. Please try again",
        _ => "Unknown validation error"
    };
} 