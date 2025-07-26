using Assessment__2.Configuration;
using Assessment__2.Service;
using Assessment__2.Enum;
using Assessment__2.Model;

// Declaring namespace for validator components
namespace Assessment__2.Validator;

// Declaring class for user validator with constructor injection
public class UserValidator(ValidationConfig config, IValidationMessageService validationMessages, IUserService userService)
{
    // Declaring private fields for logic
    private readonly ValidationConfig _config = config;
    private readonly IValidationMessageService _validationMessages = validationMessages;
    private readonly IUserService _userService = userService;

    // Implementing method for validating username
    public ValidationResult ValidateUsername(string username)
    {
        // Checking if username is empty or whitespace
        if (string.IsNullOrWhiteSpace(username))
        {
            // Returning failure with required message
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.UsernameRequired));
        }
        // Checking if username already exists
        if (!_userService.IsUsernameUnique(username))
        {
            // Returning failure with exists message
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.UsernameExists));
        }
        // Returning success validation result
        return ValidationResult.Success();
    }

    // Implementing method for validating email
    public ValidationResult ValidateEmail(string email)
    {
        // Checking if email is empty or whitespace
        if (string.IsNullOrWhiteSpace(email))
        {
            // Returning failure with required message
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.EmailRequired));
        }
        
        // Returning success validation result
        return ValidationResult.Success();
    }

    // Implementing method for validating age
    public ValidationResult ValidateAge(int age)
    {
        // Checking if age is within valid range
        if (age < 1 || age > 120)
        {
            // Returning failure with invalid message
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.AgeInvalid));
        }
        
        // Returning success validation result
        return ValidationResult.Success();
    }

    // Implementing method for validating phone
    public ValidationResult ValidatePhone(string phone)
    {
        // Checking if phone is empty or whitespace
        if (string.IsNullOrWhiteSpace(phone))
        {
            // Returning failure with required message
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.PhoneRequired));
        }
        
        // Returning success validation result
        return ValidationResult.Success();
    }

    // Implementing method for validating password
    public ValidationResult ValidatePassword(string password)
    {
        // Checking if password is empty or whitespace
        if (string.IsNullOrWhiteSpace(password))
        {
            // Returning failure with required message
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.PasswordRequired));
        }
        
        // Checking if password meets minimum length requirement
        if (password.Length < _config.MinPasswordLength)
        {
            // Returning failure with too short message
            return ValidationResult.Failure(_validationMessages.GetMessage(ValidationError.PasswordTooShort));
        }
        
        // Returning success validation result
        return ValidationResult.Success();
    }
} 