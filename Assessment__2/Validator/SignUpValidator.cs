// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Configuration;
using Assessment__2.Service;
using Assessment__2.Enum;
using Assessment__2.Model;

// Declaring namespace for validator components
namespace Assessment__2.Validator;

// Declaring class for sign-up validator with constructor injection
public class SignUpValidator(SignUpConfig config, ISignUpMessageService signUpMessages, IUserService userService)
{
    // Declaring private fields for logic
    private readonly SignUpConfig _config = config;
    private readonly ISignUpMessageService _signUpMessages = signUpMessages;
    private readonly IUserService _userService = userService;

    // Implementing method for validating username
    public ValidationResult ValidateUsername(string username)
    {
        // Checking if username is empty or whitespace
        if (string.IsNullOrWhiteSpace(username))
        {
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.UsernameRequired));
        }
        // Checking if username meets minimum length requirement
        if (username.Length < _config.MinUsernameLength)
        {
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.UsernameTooShort));
        }
        // Checking if username already exists
        if (!_userService.IsUsernameUnique(username))
        {
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.UsernameExists));
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
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.EmailRequired));
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
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.AgeInvalid));
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
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.PhoneRequired));
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
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.PasswordRequired));
        }
        // Checking if password meets minimum length requirement
        if (password.Length < _config.MinPasswordLength)
        {
            return ValidationResult.Failure(_signUpMessages.GetMessage(SignUpError.PasswordTooShort));
        }
        // Returning success validation result
        return ValidationResult.Success();
    }
} 