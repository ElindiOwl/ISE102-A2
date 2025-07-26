using Assessment__2.Enum;
using Assessment__2.Model;
using Assessment__2.Service;

namespace Assessment__2.Validator;

// Declaring class for login validator with primary constructor
public class LoginValidator( ILoginMessageService loginMessages)
{
    // Declaring private fields for logic
    private readonly ILoginMessageService _loginMessages = loginMessages;

    // Implementing method for validating login username
    public ValidationResult ValidateUsername(string username)
    {
        // Checking if username is empty or whitespace
        if (string.IsNullOrWhiteSpace(username))
        {
            return ValidationResult.Failure(_loginMessages.GetMessage(LoginError.UsernameEmpty));
        }
        // Returning success validation result
        return ValidationResult.Success();
    }

    // Implementing method for validating login password
    public ValidationResult ValidatePassword(string password)
    {
        // Checking if password is empty or whitespace
        if (string.IsNullOrWhiteSpace(password))
        {
            return ValidationResult.Failure(_loginMessages.GetMessage(LoginError.PasswordEmpty));
        }
        // Returning success validation result
        return ValidationResult.Success();
    }
} 